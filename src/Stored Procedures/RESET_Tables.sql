-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[RESET_Tables]
AS
BEGIN

-- EXEC RESET_Tables

DROP TABLE [dbo].[Request_Header]
DROP TABLE [dbo].[Request_QueryString]
DROP TABLE [dbo].[Request]



CREATE TABLE [dbo].[Request](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[os] [varchar](50) NULL,
	[webserver] [varchar](50) NULL,
	[runningLdap] [varchar](50) NULL,
	[runningSqlDb] [varchar](50) NULL,
	[runningXpath] [varchar](50) NULL,
	[class_type] [varchar](50) NULL,
	[class_incontext] [varchar](50) NULL,
	[method] [varchar](50) NULL,
	[protocol] [varchar](50) NULL,
	[uri] [varchar](max) NULL,
	[query] [varchar](max) NULL,
	[header] [varchar](max) NULL,
 CONSTRAINT [PK_Request] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Request_Header](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Request_id] [int] NOT NULL,
	[Header_Host] [varchar](max) NULL,
	[Header_Connection] [varchar](max) NULL,
	[Header_Accept] [varchar](max) NULL,
	[Header_Accept-Charset] [varchar](max) NULL,
	[Header_Accept-Encoding] [varchar](max) NULL,
	[Header_Accept-Language] [varchar](max) NULL,
	[Header_Date] [varchar](max) NULL,
	[Header_If-Unmodified-Since] [varchar](max) NULL,
	[Header_Referer] [varchar](max) NULL,
	[Header_User-Agent] [varchar](max) NULL,
	[Header_UA-CPU] [varchar](max) NULL,
	[Header_Via] [varchar](max) NULL,
	[Header_Transfer-Encoding] [varchar](max) NULL,
	[Header_Warning] [varchar](max) NULL,
	[Header_Cache-Control] [varchar](max) NULL,
	[Header_Cookie2] [varchar](max) NULL,
	[Header_ETag] [varchar](max) NULL,
	[Header_If-Modified-Since] [varchar](max) NULL,
	[Header_If-Match] [varchar](max) NULL,
	[Header_If-None-Match] [varchar](max) NULL,
	[Header_Pragma] [varchar](max) NULL,
	[Header_Authorization] [varchar](max) NULL,
	[Header_TE] [varchar](max) NULL,
	[Header_Cookie] [varchar](max) NULL,
	[Header_If-Range] [varchar](max) NULL,
	[Header_Trailer] [varchar](max) NULL,
	[Header_X-Serial-Number] [varchar](max) NULL,
	[Header_Max-Forwards] [varchar](max) NULL,
	[Header_Expect] [varchar](max) NULL,
	[Header_Range] [varchar](max) NULL,
	[Header_Client-ip] [varchar](max) NULL,
	[Header_From] [varchar](max) NULL,
	[Header_X-Forwarded-For] [varchar](max) NULL,
	[Header_UA-OS] [varchar](max) NULL,
	[Header_UA-Color] [varchar](max) NULL,
	[Header_MIME-Version] [varchar](max) NULL,
	[Header_~~~~~] [varchar](max) NULL,
	[Header_Proxy-Authorization] [varchar](max) NULL,
	[Header_UA-Pixels] [varchar](max) NULL,
	[Header_Upgrade] [varchar](max) NULL,
	[Header_----] [varchar](max) NULL,
	[Header_UA-Disp] [varchar](max) NULL,
	[Header_Content-Length] [varchar](max) NULL,
	[Header_Content-Language] [varchar](max) NULL,
	[Header_Content-Encoding] [varchar](max) NULL,
	[Header_Content-Location] [varchar](max) NULL,
	[Header_Content-MD5] [varchar](max) NULL,
	[Header_Content-Type] [varchar](max) NULL,
	[Header_Expires] [varchar](max) NULL,
	[Header_Last-Modified] [varchar](max) NULL,
 CONSTRAINT [PK_Request_Header] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

 

 

CREATE TABLE [dbo].[Request_QueryString](
	[id] [int]IDENTITY(1,1) NOT NULL,
	[request_id] [int] NULL,
	[key] [varchar](max) NULL,
	[value] [varchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
 



DROP TABLE [dbo].[AggregateFeatures]
CREATE TABLE [dbo].[AggregateFeatures](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[request_id] [int] NOT NULL,
	[uri_extention] [varchar](10) NOT NULL,
	[uri_contains_os_cmd] [bit] NOT NULL,
	[uri_contains_invalidchars] [bit] NOT NULL,
	[uri_contains_javascript] [bit] NOT NULL,
	[hdr_contains_invalidchars] [bit] NOT NULL,
	[hdr_contains_html] [bit] NOT NULL,
	[qs_contains_invalidchars] [bit] NOT NULL,
	[qs_contains_javascript] [bit] NOT NULL,
	[qs_contains_html] [bit] NOT NULL,
	[qs_contains_os] [bit] NOT NULL,
	[qs_contains_pathing] [bit] NOT NULL,
	[uri_sql_score] [int] NULL,
	[hdr_sql_score] [int] NULL,
	[qs_sql_score] [int] NULL,
	[uri_ldap_score] [int] NULL,
	[hdr_ldap_score] [int] NULL,
	[qs_ldap_score] [int] NULL,
	[uri_os_score] [int] NULL,
	[hdr_os_score] [int] NULL,
	[qs_os_score] [int] NULL,
	[uri_ssi_score] [int] NULL,
	[hdr_ssi_score] [int] NULL,
	[qs_ssi_score] [int] NULL,
	[uri_xss_score] [int] NULL,
	[hdr_xss_score] [int] NULL,
	[qs_xss_score] [int] NULL,
	[uri_xpath_score] [int] NULL,
	[hdr_xpath_score] [int] NULL,
	[qs_xpath_score] [int] NULL,

 CONSTRAINT [PK_AggregateFeatures] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

END
