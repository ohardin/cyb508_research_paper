CREATE VIEW dbo.v_headers
AS
SELECT        a.Header_Connection, a.Header_Accept, a.[Header_Accept-Charset], a.[Header_Accept-Encoding], a.[Header_Accept-Language], a.Header_Referer, a.[Header_User-Agent], a.Header_Via, a.[Header_Transfer-Encoding], 
                         a.Header_Warning, a.[Header_Cache-Control], a.Header_ETag, a.[Header_If-Match], a.[Header_If-None-Match], a.Header_Pragma, a.Header_Authorization, a.Header_TE, a.Header_Cookie, a.[Header_If-Range], a.Header_Trailer, 
                         a.[Header_Max-Forwards], a.Header_Expect, a.Header_Range, a.[Header_Client-ip], a.Header_From, a.[Header_X-Forwarded-For], a.[Header_UA-OS], a.[Header_UA-Color], a.[Header_MIME-Version], 
                         a.[Header_Proxy-Authorization], a.[Header_UA-Pixels], a.Header_Upgrade, a.[Header_UA-Disp], a.[Header_Content-Length], a.[Header_Content-Language], a.[Header_Content-Encoding], a.[Header_Content-Location], 
                         a.[Header_Content-MD5], a.[Header_Content-Type], a.Header_Expires, a.[Header_Last-Modified], CAST(CASE WHEN [class_type] = 'Valid' THEN 0 ELSE 1 END AS BIT) AS IsAttack
FROM            dbo.Request_Header AS a INNER JOIN
                         dbo.Request ON a.Request_id = dbo.Request.id

GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "a"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 530
               Right = 278
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "Request"
            Begin Extent = 
               Top = 6
               Left = 301
               Bottom = 326
               Right = 471
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 2595
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'v_headers';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'v_headers';

