ALTER TABLE [dbo].[PACKAGE] alter column Price numeric(18,2)

INSERT dbo.PACKAGE VALUES (11, 'Photo LifeTime', 49.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (21, 'Photo Yearly', 29.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (12, 'Tribute LifeTime', 149.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (22, 'Tribute Yearly', 59.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)

INSERT dbo.PACKAGE VALUES (4, 'Tribute LifeTime', 149.95, 1, 1, 0, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (5, 'Tribute Yearly', 59.95, 1, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (6, 'Photo LifeTime', 129.95, 1, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (7, 'Photo Yearly', 49.95, 0, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)
INSERT dbo.PACKAGE VALUES (8, 'Announcement', 0, 0, 1, 1, NULL, '20110118 00:00:00:000', NULL, NULL, 1, 0)