using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Configuration;
using System.Reflection;
using System.Web;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Hska_Grades_Tool
{
    static class GradesService
    {
        private static string examXslt;
        private static XmlDocument envelopTemplate = getEnvelop();
        private static XmlDocument payloadTemplate = getPayload();

        static GradesService()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(MainWindow));
            // load exam xslt resource
            Stream resourceStream = assembly.GetManifestResourceStream("Hska_Grades_Tool.xslt.xslt");
            using (TextReader reader = new StreamReader(resourceStream))
            {
                string resourceXslAsString = reader.ReadToEnd();
                examXslt = resourceXslAsString;
            }
        }

        private static String getEscapedPayloadStringWithCredentials(string username, string password) {
            XmlDocument resultDoc = cloneXmlDocument(payloadTemplate);
            resultDoc.SelectSingleNode("//id").InnerText = username;
            resultDoc.SelectSingleNode("//username").InnerText = username;
            resultDoc.SelectSingleNode("//password").InnerText = password;
            return resultDoc.OuterXml;
        }

        private static String getSoapRequest(string username, string password)
        {
            XmlDocument resultDoc = cloneXmlDocument(envelopTemplate);
            resultDoc.SelectSingleNode("//xmlParams").InnerText = getEscapedPayloadStringWithCredentials(username, password);
            return resultDoc.OuterXml;
        }

        private static XmlDocument cloneXmlDocument(XmlDocument doc)
        {
            XmlDocument clone = new XmlDocument();
            clone.LoadXml(doc.OuterXml);
            return clone;
        }

        private static XmlDocument getPayload()
        {
            string payload = @"
<HsKAApp13.1>
<general>
    <object>Exam13.1</object>
    <id>USERNAME</id>
</general>
    <user-auth>
        <username>USERNAME</username>
        <password>PASSWORD</password>
    </user-auth>
</HsKAApp13.1>";
            XmlDocument result = new XmlDocument();
            result.LoadXml(payload);
            return result;
        }

        static private XmlDocument getEnvelop()
        {
            string envelopString = @"<?xml version=""1.0"" encoding=""UTF-8""?>
<soapenv:Envelope xmlns:soapenv=""http://schemas.xmlsoap.org/soap/envelope/"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"">
  <soapenv:Body>
    <ns1:getDataXML xmlns:ns1=""http://dbinterface.xml.his.de"" soapenv:encodingStyle=""http://schemas.xmlsoap.org/soap/encoding/"">
      <xmlParams xmlns:soapenc=""http://schemas.xmlsoap.org/soap/encoding/"" xsi:type=""soapenc:string"">

      </xmlParams>
    </ns1:getDataXML>
  </soapenv:Body>
</soapenv:Envelope>";
            XmlDocument result = new XmlDocument();
            result.LoadXml(envelopString);
            return result;
        }

        static private WebRequest getRequest(string username, string password)
        {
            Uri gradesServerUri = new Uri(ConfigurationManager.AppSettings["GradesServerUri"]);
            WebRequest request = WebRequest.Create(gradesServerUri);
            request.Method = "POST";
            // Write body
            ASCIIEncoding encoding = new ASCIIEncoding();
            Debug.WriteLine(getSoapRequest(username, password));
            byte[] data = encoding.GetBytes(getSoapRequest(username, password));
            Stream newStream = request.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Close();
            // Add header
            request.Headers.Add("SOAPAction:FromWebApp");
            return request;
        }

        static public List<ExamGrades> getGrades(string username, string password)
        {
            WebRequest request = getRequest(username, password);
            return responseToExamGrades(request.GetResponse());
        }

        static public List<ExamGrades> responseToExamGrades(WebResponse response)
        {
            List<ExamGrades> examGrades = new List<ExamGrades>();
            var encoding = ASCIIEncoding.ASCII;
            string responseText;
            using (var reader = new System.IO.StreamReader(response.GetResponseStream(), encoding))
            {
                responseText = reader.ReadToEnd();
            }
            using (XmlReader reader = XmlReader.Create(new StringReader(responseText)))
            {
                while (reader.Read()) {
                    // getDataXMLReturn contains cdata
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "getDataXMLReturn")
                    {
                        string gradesResponse = reader.ReadInnerXml();
                        // Decode encoded cdata
                        gradesResponse = HttpUtility.HtmlDecode(gradesResponse);
                        // Create xml
                        XmlDocument gradesResponseXml = new XmlDocument();
                        gradesResponseXml.LoadXml(gradesResponse);
                        // Select relevant nodes
                        XmlNode node = gradesResponseXml.SelectSingleNode("//es");
                        
                        // Transform
                        string e = applyXlst(node).ToString();

                        // Deserialize
                        ExamGrades examGradesObj = null;
                        // Construct an instance of the XmlSerializer with the type
                        // of object that is being deserialized.
                        XmlSerializer serializer = new XmlSerializer(typeof(ExamGrades));
                        // Call the Deserialize method and cast to the object type.

                        gradesResponseXml.LoadXml(e);
                        XmlNodeList examNodes = gradesResponseXml.SelectNodes("//exam");
                        foreach (XmlNode asd in examNodes)
                        {
                            examGradesObj = (ExamGrades)serializer.Deserialize(new StringReader(asd.OuterXml));
                            examGrades.Add(examGradesObj);
                        }
                        
                    }
                }
            }
            return examGrades;
        }

        static StringWriter applyXlst(XmlNode node)
        {
            XslCompiledTransform myXslTrans = new XslCompiledTransform();
            XmlTextReader stylesheetReader = new XmlTextReader(new StringReader(examXslt));
            myXslTrans.Load(stylesheetReader);
            StringWriter result = new StringWriter();
            myXslTrans.Transform(node, null, result);
            return result;
        }

    }

}
