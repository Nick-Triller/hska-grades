<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="xml" indent="yes"/>

    <xsl:template match="node()|@*">
      <xsl:copy>
        <xsl:apply-templates select="node()|@*"/>
      </xsl:copy>
    </xsl:template>

  <xsl:template match="e">
    <exam>
      <xsl:apply-templates/>
    </exam>
  </xsl:template>
  
  <xsl:template match="t">
    <semester>
      <xsl:value-of select="."/>
    </semester>
  </xsl:template>

  <xsl:template match="m">

    <xsl:choose>
      <xsl:when test="not(node())">
        <grade>-1</grade>
      </xsl:when>
      <xsl:otherwise>
        <grade>
          <xsl:value-of select="."/>
        </grade>
      </xsl:otherwise>
    </xsl:choose>
  </xsl:template>

  <xsl:template match="n">
    <examId>
      <xsl:value-of select="."/>
    </examId>
  </xsl:template>

  <xsl:template match="d">
    <examDate>
      <xsl:value-of select="."/>
    </examDate>
  </xsl:template>
  
  <xsl:template match="a">
    <attempts>
      <xsl:value-of select="."/>
    </attempts>
  </xsl:template>

  <xsl:template match="s">
    <status>
      <xsl:value-of select="."/>
    </status>
  </xsl:template>

  <xsl:template match="x">
    <name>
      <xsl:value-of select="."/>
    </name>
  </xsl:template>
  
  <xsl:template match="v">
    <xsl:element name="{concat('gotGrade', p)}">
        <xsl:value-of select="w/x"/>
    </xsl:element>
  </xsl:template>

  <xsl:template match="i">
    <participants>
      <xsl:value-of select="c"/>
    </participants>
  </xsl:template>

</xsl:stylesheet>
