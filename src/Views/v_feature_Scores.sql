
CREATE VIEW [dbo].[v_feature_Scores]
AS
SELECT        r.method, a.uri_sql_score, a.uri_ldap_score, a.uri_os_score, a.uri_xss_score, a.uri_ssi_score, a.uri_xpath_score, a.hdr_sql_score, a.hdr_ldap_score, a.hdr_xpath_score, a.hdr_xss_score, a.hdr_ssi_score, a.qs_ldap_score, 
                         a.qs_xpath_score, a.qs_sql_score, a.qs_xss_score, a.qs_ssi_score, a.qs_os_score, CAST(CASE WHEN [class_type] = 'Valid' THEN 0 ELSE 1 END AS BIT) AS IsAttack
FROM            dbo.Request AS r INNER JOIN
                         dbo.AggregateFeatures AS a ON a.request_id = r.id
