using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Hska_Grades_Tool
{
    [XmlRoot("exam")]
    public class ExamGrades
    {
        [XmlElement("examId")]
        public string ExamId { get; set; }

        [XmlElement("name")]
        public string ExamTitle { get; set; }

        // -1 if no grade
        [XmlElement("grade")]
        public float Grade
        {
            get
            {
                return grade;
            }
            set
            {
                // Grade can be set with and without comma
                if (value >= 100) 
                    grade = value / 100;
                else
                    grade = value;
            }
        }
        private float grade;

        [XmlElement("status")]
        public string Status { get; set; }

        [XmlElement("participants")]
        public int Participants { get; set; }

        [XmlElement("examDate")]
        public string ExamDate { get; set; }

        [XmlElement("semester")]
        public string Semester
        {
            get
            {
                // Format: 20151 -> year + number 1 or 2 for ss and ws
                string result = semester.Substring(0, 4);
                result += semester[4] == '1' ? " SS " : " WS ";
                
                return result;
            }
            set
            {
                semester = value;
            }
        }
        private String semester;

        [XmlElement("attempts")]
        public int Attempts { get; set; }

        public int G100 {
            get
            {
                return g100.GetValueOrDefault(0);
            }
            set {
                g100 = value;
            }
        }

        public int G130
        {
            get
            {
                return g130.GetValueOrDefault(0);
            }
            set
            {
                g130 = value;
            }
        }

        public int G170
        {
            get
            {
                return g170.GetValueOrDefault(0);
            }
            set
            {
                g170 = value;
            }
        }

        public int G200
        {
            get
            {
                return g200.GetValueOrDefault(0);
            }
            set
            {
                g200 = value;
            }
        }

        public int G230
        {
            get
            {
                return g230.GetValueOrDefault(0);
            }
            set
            {
                g230 = value;
            }
        }

        public int G270
        {
            get
            {
                return g270.GetValueOrDefault(0);
            }
            set
            {
                g270 = value;
            }
        }

        public int G300
        {
            get
            {
                return g300.GetValueOrDefault(0);
            }
            set
            {
                g300 = value;
            }
        }

        public int G330
        {
            get
            {
                return g330.GetValueOrDefault(0);
            }
            set
            {
                g330 = value;
            }
        }

        public int G370
        {
            get
            {
                return g370.GetValueOrDefault(0);
            }
            set
            {
                g370 = value;
            }
        }

        public int G400
        {
            get
            {
                return g400.GetValueOrDefault(0);
            }
            set
            {
                g400 = value;
            }
        }

        public int G430
        {
            get
            {
                return g430.GetValueOrDefault(0);
            }
            set
            {
                g430 = value;
            }
        }

        public int G470
        {
            get
            {
                return g470.GetValueOrDefault(0);
            }
            set
            {
                g470 = value;
            }
        }

        public int G500
        {
            get
            {
                return g500.GetValueOrDefault(0);
            }
            set
            {
                g500 = value;
            }
        }

        // All grade elements can be missing
        [XmlElement("gotGrade100", IsNullable=true)]
        public int? g100;

        [XmlElement("gotGrade130", IsNullable = true)]
        public int? g130;

        [XmlElement("gotGrade170", IsNullable = true)]
        public int? g170;

        [XmlElement("gotGrade200", IsNullable = true)]
        public int? g200;

        [XmlElement("gotGrade230", IsNullable = true)]
        public int? g230;

        [XmlElement("gotGrade270", IsNullable = true)]
        public int? g270;

        [XmlElement("gotGrade300", IsNullable = true)]
        public int? g300;

        [XmlElement("gotGrade330", IsNullable = true)]
        public int? g330;

        [XmlElement("gotGrade370", IsNullable = true)]
        public int? g370;

        [XmlElement("gotGrade400", IsNullable = true)]
        public int? g400;

        [XmlElement("gotGrade430", IsNullable = true)]
        public int? g430;

        [XmlElement("gotGrade470", IsNullable = true)]
        public int? g470;

        [XmlElement("gotGrade500", IsNullable = true)]
        public int? g500;

        public override string ToString()
        {
            return ExamTitle;
        }
    }


}
