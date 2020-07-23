USE [ShowManPhotoDB]
GO
/****** Object:  StoredProcedure [dbo].[User_Update]    Script Date: 11.05.2020 22:20:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[User_Update]
	@Id int,
	@Name nvarchar(100),
	@LastName nvarchar(100),
	@Email varchar(200),
	@Password varchar(50)
AS
BEGIN	
	update [dbo].[User]
    set 
	[Name]=@Name,
    [LastName]=@LastName,
    [Email]=@Email,
    [Password]=@Password
	where [Id]=@Id
END

--EXEC [dbo].[User_Update] 'User', 'User', 'user@mail.com', '1'