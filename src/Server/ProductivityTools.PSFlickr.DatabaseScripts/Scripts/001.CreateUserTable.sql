CREATE TABLE [Flickr].[User]
(
	UserId INT IDENTITY(1,1),
	Name VARCHAR(30),
	OAuthToken VARCHAR(100),

	CONTRAINT pk_user PRIMARY KEY(UserId)
)