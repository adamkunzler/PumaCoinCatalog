SELECT 	
	scc.Title AS 'Collection',
	scg.Title AS 'Category',
	sct.Title AS 'Type',
	sct.Details AS 'Details',
	sc.Year,
	sc.Variety
FROM ScrapeCoin sc
	LEFT JOIN ScrapeCoinType sct ON sct.Id = sc.CoinType_Id
	LEFT JOIN ScrapeCoinCategory scg ON scg.Id = sct.CoinCategory_Id
	LEFT JOIN ScrapeCoinCollection scc ON scc.Id = scg.CoinCollection_Id
ORDER BY
	scc.SortOrder,
	scg.SortOrder,
	sct.SortOrder,
	sc.SortOrder