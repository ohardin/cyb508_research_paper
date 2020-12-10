<Query Kind="Program">
  <Reference>&lt;RuntimeDirectory&gt;\System.IO.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Xml.Serialization.dll</Reference>
  <Namespace>System.Xml.Serialization</Namespace>
</Query>

void Main()
{
	string file = @"D:\SBU\508\web-application-attacks-datasets-master\ecml_pkdd\learning_dataset.xml";

	string cnstr = "Server=db01;Initial Catalog=Cyb_508; Integrated Security=True; MultipleActiveResultSets=True";
	//var mySerializer = new XmlSerializer(typeof(Dataset));
	//var myFileStream = new FileStream(file, FileMode.Open);
	//var myObject = (DataSet)mySerializer.Deserialize(myFileStream);



	//	XmlDocument doc = new XmlDocument();
	//	doc.Load(file);
	//	XmlNodeList nodes = doc.GetElementsByTagName("sample");
	//	foreach (XmlNode node in nodes)
	//	{
	//
	//
	//
	//	}

	Dataset result;
	XmlSerializer serializer = new XmlSerializer(typeof(Dataset));
	using (FileStream fileStream = new FileStream(file, FileMode.Open))
	{
		result = (Dataset)serializer.Deserialize(fileStream);
	}
	result.Samples.Count();

	result.Samples.First(s => s.Class.InContext == "FALSE");

	using (SqlConnection cn = new SqlConnection(cnstr))
	{
		cn.Open();

		using (SqlCommand reset = new SqlCommand("RESET_Tables", cn))
		{
			reset.CommandType = CommandType.StoredProcedure;
			reset.ExecuteNonQuery();
		}

		foreach (var record in result.Samples)
		{
			string sql = "INSERT INTO [dbo].[Request]([os],[webserver],[runningLdap],[runningSqlDb],[runningXpath],[class_type],[class_incontext],[method],[protocol],[uri],[query],[header])  ";
			sql += "VALUES (@os,@webserver,@runningLdap,@runningSqlDb,@runningXpath,@class_type,@class_incontext,@method,@protocol,@uri,@query,@header) SELECT SCOPE_IDENTITY()";


			using (SqlCommand cmd = new SqlCommand(sql, cn))
			{
				cmd.Parameters.Add(new SqlParameter("@os", record.ReqContext.Os));
				cmd.Parameters.Add(new SqlParameter("@webserver", record.ReqContext.Webserver));
				cmd.Parameters.Add(new SqlParameter("@runningLdap", record.ReqContext.runningLdap));
				cmd.Parameters.Add(new SqlParameter("@runningSqlDb", record.ReqContext.RunningSqlDb));
				cmd.Parameters.Add(new SqlParameter("@runningXpath", record.ReqContext.RunningXpath));
				cmd.Parameters.Add(new SqlParameter("@class_type", record.Class.Type));
				cmd.Parameters.Add(new SqlParameter("@class_incontext", record.Class.InContext));
				cmd.Parameters.Add(new SqlParameter("@method", record.Request.Method));
				cmd.Parameters.Add(new SqlParameter("@protocol", record.Request.Protocol));
				cmd.Parameters.Add(new SqlParameter("@uri", record.Request.Uri));
				cmd.Parameters.Add(new SqlParameter("@query", record.Request.Query_string));
				cmd.Parameters.Add(new SqlParameter("@header", record.Request.Headers_string));
				var ret = cmd.ExecuteScalar();
				int id = int.Parse(ret.ToString());

				StringBuilder ins = new StringBuilder();
				StringBuilder val = new StringBuilder();
				foreach (var h in record.Request.Headers)
				{
					ins.Append($",[Header_{h.Key}]");
					val.Append($",'{h.Value.Replace("'", "''")}'");
				}

				cmd.CommandText = $"INSERT INTO [dbo].[Request_header](Request_id {ins.ToString()}) VALUES ({id}{val})";
				try
				{
					cmd.ExecuteNonQuery();
				}
				catch (Exception ex)
				{
					ex.Dump();
					cmd.Dump();

				}

				if (record.Request.Queries.Count() > 1)
				{
					cmd.CommandText = $"INSERT INTO [dbo].[Request_querystring](Request_id, [key], value) VALUES ({id}, @key, @value)";
					foreach (var q in record.Request.Queries)
					{
						cmd.Parameters.Clear();
						cmd.Parameters.Add(new SqlParameter("@key", q.Key));
						cmd.Parameters.Add(new SqlParameter("@value", q.Value));
					}

					try
					{
						cmd.ExecuteNonQuery();
					}
					catch (Exception ex)
					{
						ex.Dump();
						cmd.Dump();
					}
				}


				cmd.CommandText = $"INSERT INTO [dbo].[AggregateFeatures]([request_id],[uri_extention],[uri_contains_os_cmd],[uri_contains_invalidchars],[uri_contains_javascript],[hdr_contains_invalidchars],[hdr_contains_html],[qs_contains_invalidchars],[qs_contains_javascript],[qs_contains_html],[qs_contains_os],[qs_contains_pathing],[uri_sql_score],[hdr_sql_score]      ,[qs_sql_score]      ,[uri_ldap_score]      ,[hdr_ldap_score]      ,[qs_ldap_score]      ,[uri_os_score]      ,[hdr_os_score]      ,[qs_os_score]      ,[uri_ssi_score]      ,[hdr_ssi_score]      ,[qs_ssi_score]      ,[uri_xss_score]      ,[hdr_xss_score]      ,[qs_xss_score]      ,[uri_xpath_score]      ,[hdr_xpath_score]      ,[qs_xpath_score]) VALUES(@request_id,@uri_extention,@uri_contains_os_cmd,@uri_contains_invalidchars,@uri_contains_javascript,@hdr_contains_invalidchars,@hdr_contains_html,@qs_contains_invalidchars,@qs_contains_javascript,@qs_contains_html,@qs_contains_os,@qs_contains_pathing,@uri_sql_score      ,@hdr_sql_score      ,@qs_sql_score      ,@uri_ldap_score      ,@hdr_ldap_score      ,@qs_ldap_score      ,@uri_os_score      ,@hdr_os_score      ,@qs_os_score      ,@uri_ssi_score      ,@hdr_ssi_score      ,@qs_ssi_score      ,@uri_xss_score      ,@hdr_xss_score      ,@qs_xss_score      ,@uri_xpath_score,@hdr_xpath_score,@qs_xpath_score)";
			
				cmd.Parameters.Add(new SqlParameter("@request_id", id)); cmd.Parameters.Add(new SqlParameter("@uri_extention", record.Request.uri_extention));
				cmd.Parameters.Add(new SqlParameter("@uri_contains_os_cmd", record.Request.uri_contains_os_cmd));
				cmd.Parameters.Add(new SqlParameter("@uri_contains_invalidchars", record.Request.uri_contains_invalidchars));
				cmd.Parameters.Add(new SqlParameter("@uri_contains_javascript", record.Request.uri_contains_javascript));
				cmd.Parameters.Add(new SqlParameter("@hdr_contains_invalidchars", record.Request.hdr_contains_invalidchars));
				cmd.Parameters.Add(new SqlParameter("@hdr_contains_html", record.Request.hdr_contains_html));
				cmd.Parameters.Add(new SqlParameter("@qs_contains_html", record.Request.qs_contains_html));
				cmd.Parameters.Add(new SqlParameter("@qs_contains_javascript", record.Request.qs_contains_javascript));
				cmd.Parameters.Add(new SqlParameter("@qs_contains_os", record.Request.qs_contains_os));
				cmd.Parameters.Add(new SqlParameter("@qs_contains_pathing", record.Request.qs_contains_pathing));
				cmd.Parameters.Add(new SqlParameter("@qs_contains_invalidchars", record.Request.qs_contains_invalidchars));

				cmd.Parameters.Add(new SqlParameter("@uri_sql_score", record.Request.uri_sql_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_sql_score", record.Request.hdr_sql_score));
				cmd.Parameters.Add(new SqlParameter("@qs_sql_score", record.Request.qs_sql_score));

				cmd.Parameters.Add(new SqlParameter("@uri_ldap_score", record.Request.uri_ldap_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_ldap_score", record.Request.hdr_ldap_score));
				cmd.Parameters.Add(new SqlParameter("@qs_ldap_score", record.Request.qs_ldap_score));

				cmd.Parameters.Add(new SqlParameter("@uri_xss_score", record.Request.uri_xss_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_xss_score", record.Request.hdr_xss_score));
				cmd.Parameters.Add(new SqlParameter("@qs_xss_score", record.Request.qs_xss_score));

				cmd.Parameters.Add(new SqlParameter("@uri_ssi_score", record.Request.uri_ssi_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_ssi_score", record.Request.hdr_ssi_score));
				cmd.Parameters.Add(new SqlParameter("@qs_ssi_score", record.Request.qs_ssi_score));

				cmd.Parameters.Add(new SqlParameter("@uri_xpath_score", record.Request.uri_xpath_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_xpath_score", record.Request.hdr_xpath_score));
				cmd.Parameters.Add(new SqlParameter("@qs_xpath_score", record.Request.qs_xpath_score));

				cmd.Parameters.Add(new SqlParameter("@uri_os_score", record.Request.uri_os_score));
				cmd.Parameters.Add(new SqlParameter("@hdr_os_score", record.Request.hdr_os_score));
				cmd.Parameters.Add(new SqlParameter("@qs_os_score", record.Request.qs_os_score));



				cmd.ExecuteNonQuery();

			}
		}
	}
}



[XmlRoot("dataset")]
public class Dataset
{
	[XmlElement("sample")]
	public List<Sample> Samples { get; set; }
}

public class Sample
{
	[XmlElement("reqContext")]
	public ReqContext ReqContext { get; set; }

	[XmlElement("class")]
	public Class Class { get; set; }

	[XmlElement("request")]
	public Request Request { get; set; }

	[XmlAttribute("id")]
	public string Id { get; set; }
}






public class ReqContext
{

	[XmlElement("os")]
	public string Os { get; set; }

	[XmlElement("webserver")]
	public string Webserver { get; set; }

	[XmlElement("runningLdap")]
	public string runningLdap { get; set; }

	[XmlElement("runningSqlDb")]
	public string RunningSqlDb { get; set; }

	[XmlElement("runningXpath")]
	public string RunningXpath { get; set; }
}

public class Class
{
	[XmlElement("type")]
	public string Type { get; set; }

	[XmlElement("inContext")]
	public string InContext { get; set; }
}

public class Request
{
	string _headers = string.Empty;
	string _queries = string.Empty;
	Dictionary<string, string> _headerDict = new Dictionary<string, string>();
	Dictionary<string, string> _queryDict = new Dictionary<string, string>();



	public bool HasDuplicateQueryString { get; set; }
	public string uri_extention
	{
		get
		{
			try
			{
				return System.IO.Path.GetExtension(this.Uri).ToLower();
			}
			catch
			{
				return "";
			}
		}
	}

	public bool uri_contains_os_cmd => constants.Illegal_os.Any(c => this.Uri.Contains(c));
	public bool uri_contains_invalidchars => constants.Illegal_strings.Any(c => this.Uri.Contains(c));
	public bool uri_contains_javascript => constants.Illegal_Script.Any(c => this.Uri.Contains(c));
	public bool hdr_contains_invalidchars => constants.Illegal_strings.Any(c => this.Headers_string.Contains(c));
	public bool hdr_contains_html => constants.Illegal_HTML.Any(c => this.Headers_string.Contains(c));
	public bool qs_contains_html => constants.Illegal_HTML.Any(c => this.Query_string.Contains(c));
	public bool qs_contains_javascript => constants.Illegal_Script.Any(c => this.Query_string.Contains(c));
	public bool qs_contains_os => constants.Illegal_os.Any(c => this.Query_string.Contains(c));
	public bool qs_contains_pathing => constants.Illegal_Pathing.Any(c => this.Query_string.Contains(c));
	public bool qs_contains_invalidchars => constants.Illegal_strings.Any(c => this.Query_string.Contains(c));

	public int uri_sql_score => utils.stringMatches(this.Uri, constants.sqlReserveWords);
	public int hdr_sql_score => utils.stringMatches(this.Headers_string, constants.sqlReserveWords);
	public int qs_sql_score => utils.stringMatches(this.Query_string, constants.sqlReserveWords);

	public int uri_ldap_score => utils.stringMatches(this.Uri, constants.LdapReserveWords);
	public int hdr_ldap_score => utils.stringMatches(this.Headers_string, constants.LdapReserveWords);
	public int qs_ldap_score => utils.stringMatches(this.Query_string, constants.LdapReserveWords);

	public int uri_os_score => utils.stringMatches(this.Uri, constants.Os_Path_ReserveWords);
	public int hdr_os_score => utils.stringMatches(this.Headers_string, constants.Os_Path_ReserveWords);
	public int qs_os_score => utils.stringMatches(this.Query_string, constants.Os_Path_ReserveWords);

	public int uri_xss_score => utils.stringMatches(this.Uri, constants.XSS_ReserveWords);
	public int hdr_xss_score => utils.stringMatches(this.Headers_string, constants.XSS_ReserveWords);
	public int qs_xss_score => utils.stringMatches(this.Query_string, constants.XSS_ReserveWords);


	public int uri_ssi_score => utils.stringMatches(this.Uri, constants.SSI_ReserveWords);
	public int hdr_ssi_score => utils.stringMatches(this.Headers_string, constants.SSI_ReserveWords);
	public int qs_ssi_score => utils.stringMatches(this.Query_string, constants.SSI_ReserveWords);


	public int uri_xpath_score => utils.stringMatches(this.Uri, constants.XPath_ReserveWords);
	public int hdr_xpath_score => utils.stringMatches(this.Headers_string, constants.XPath_ReserveWords);
	public int qs_xpath_score => utils.stringMatches(this.Query_string, constants.XPath_ReserveWords);





	[XmlElement("protocol")]
	public string Protocol { get; set; }

	[XmlElement("uri")]
	public string Uri { get; set; }


	[XmlElement("method")]
	public string Method { get; set; }


	[XmlElement("query")]
	public string Query_string
	{
		get => _queries;
		set
		{
			_queries = value;
			try
			{

				foreach (var q in _queries.Split('&'))
				{
					if (string.IsNullOrEmpty(q))
						continue;
					int pos = q.IndexOf("=");

					if (pos > 0)
					{
						string key = q.Substring(0, pos).Trim();
						string val = q.Substring(pos + 1).Trim();

						if (_queryDict.ContainsKey(key))
							this.HasDuplicateQueryString = true;
						else
							_queryDict.Add(key, val);
					}
					else
					{
						_queryDict.Add("no_key", value);

					}
				}
			}
			catch
			{
				this.Query_string.Dump(" ");

			}
		}
	}

	[XmlElement("headers")]
	public string Headers_string
	{
		get => _headers;
		set
		{
			_headers = value;

			foreach (var h in _headers.Split(Environment.NewLine.ToCharArray()))
			{
				if (string.IsNullOrEmpty(h))
					continue;
				int pos = h.IndexOf(":");
				string key = h.Substring(0, pos).Trim();
				string val = h.Substring(pos + 1).Trim();
				_headerDict.Add(key, val);
			}

		}
	}
	[XmlIgnore]
	public Dictionary<string, string> Headers => _headerDict;
	[XmlIgnore]
	public Dictionary<string, string> Queries => _queryDict;




}


public class constants
{
	public static readonly List<string> Illegal_Script = new List<string> { "<", ">", "'", "&#x27;", "&#x2F;" };
	public static readonly List<string> Illegal_HTML = new List<string> { "<", "&lt;", ">", "&gt;", "&quot;", "&#x27;", "&#x2F;" };
	public static readonly List<string> Illegal_Pathing = new List<string> { ".." };
	public static readonly List<string> Illegal_os = new List<string> { "dir", "cat", "exec", "rsync", "/bin", ".bat" };
	public static readonly List<string> Illegal_strings = new List<string> { "<", "&lt;", ">", "&gt;", "&quot;", "&#x27;", "&#x2F;" };
	public static readonly List<string> Illegal_cmd = new List<string> { "exec", "cmd.exe", "include", "virtual", "%5CWINNT%5", "..", "pwdump.exe", "ping.exe", "dir+", "bat", "cat", "bin", "win.ini", "//", "/./", "%2f%2e%2e%2f" };

	public static string[] sqlReserveWords = new string[] { "select", "delete", "from", "where", "drop", "and", "insert", "dbo", "exec", "group", "having", "Like", "++%27" };
	public static string[] LdapReserveWords = new string[] { "objectclass", "targetfilter", "ou", "cn", "%3D", "displayname" };
	public static string[] Os_Path_ReserveWords = new string[] { "exec", "cmd.exe", "include", "virtual", "%5CWINNT%5", "..", "pwdump.exe", "ping.exe", "dir+", "bat", "cat",  "bin", "win.ini", "//", "/./", "%2f%2e%2e%2f" };
	public static string[] XSS_ReserveWords = new string[] { "<script>", "alert", "window.open", "document." };
	public static string[] SSI_ReserveWords = new string[] { "<script>", "alert", "window.open", "document."m "fromhost", "fromaddress", "replyto", "htaccess", "echo" };
	public static string[] XPath_ReserveWords = new string[] { "xmldoc", "xmlDocument" };


}

public static class utils
{
	public static int stringMatches(string textToQuery, string[] stringsToFind)
	{
		int count = 0;
		foreach (var stringToFind in stringsToFind)
		{
			int currentIndex = 0;

			while ((currentIndex = textToQuery.IndexOf(stringToFind, currentIndex, StringComparison.Ordinal)) != -1)
			{
				currentIndex++;
				count++;
			}
		}
		return count;
	}
}