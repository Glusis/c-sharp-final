CREATE TABLE [dbo].[WeatherData]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY,
    [temp] FLOAT NULL, 
    [city] VARCHAR(50) NULL, 
    [time] DATETIME NULL
)
