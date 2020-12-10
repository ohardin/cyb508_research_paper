<Query Kind="Program">
  <Connection>
    <ID>c61703b2-6c95-4c38-8d65-fc5c06f337cc</ID>
    <Persist>true</Persist>
    <Server>db01</Server>
    <Database>Cyb_508</Database>
  </Connection>
</Query>

void Main()
{
	//Regex rgx = new Regex(@"^((?!\+\+\+).)");
	//.Select(r => new { r.Class_type, r.Query, valid = rgx.IsMatch(r.Query) ? false: true })

	string[] XPath_ReserveWords = new string[] { "+++" };

	Requests.Select(r => r.Class_type).Distinct().Dump();
	Requests.Count().Dump();
	Requests.Where(r => r.Class_type == "XPathInjection" || r.Class_type == "XPathInjection").Count().Dump();
	Requests.Where(r => r.Class_type == "XPathInjection" || r.Class_type == "XPathInjection")
		.Select(r => new { 
			r.Class_type, 
			uri_sql_matches = stringMatches(r.Uri.ToLower(), XPath_ReserveWords.ToArray()),
			header_sql_matches = stringMatches(r.Header.ToLower(), XPath_ReserveWords.ToArray()),
			query_sql_matches = stringMatches(r.Query.ToLower(), XPath_ReserveWords.ToArray()),
			r.Uri,
			r.Header,
			r.Query
			}).ToList()
		.Where(r => (r.header_sql_matches + r.uri_sql_matches + r.query_sql_matches) < 1)
	.Dump();
}

// Define other methods and classes here

  int stringMatches(string textToQuery, string[] stringsToFind)
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