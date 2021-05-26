SELECT [Data].[uniqueName] AS PlatformName
	,[T_WellLatest].[id] AS Id
	,[T_WellLatest].[platformId] AS PlatformId
	,[T_WellLatest].[uniqueName] AS UniqueName
	,[T_WellLatest].[latitude] AS Latitude
	,[T_WellLatest].[longitude] AS Longitude
	,CONVERT(datetime2(3), [T_WellLatest].[createdAt] ) as CreatedAt -- From datetime2(7) to From datetime2(3) 
	,CONVERT(datetime2(3), [T_WellLatest].[updatedAt] ) as UpdatedAt -- From datetime2(7) to From datetime2(3) 
FROM [AssessmentTest].[dbo].[PlatformWellActual]
INNER JOIN [Data] ON [PlatformWellActual].dataf_id = [Data].f_id
INNER JOIN (
	SELECT *
	FROM (
		SELECT [Well].[f_id]
			,[platformId]
			,[dataf_id]
			,[PlatformwelldataSqf_id]
			,[id]
			,[uniqueName]
			,[latitude]
			,[longitude]
			,[createdAt]
			,[updatedAt]
			,row_number() OVER (
				PARTITION BY [PlatformwelldataSqf_id] ORDER BY [updatedAt] DESC 
				) AS seqnum
		FROM [AssessmentTest].[dbo].[Well]
		INNER JOIN [Data] ON [Well].dataf_id = [Data].f_id
		) T_WellOrdered 
	WHERE seqnum = 1
	) T_WellLatest ON [PlatformWellActual].[f_id] = T_WellLatest.[PlatformwelldataSqf_id]