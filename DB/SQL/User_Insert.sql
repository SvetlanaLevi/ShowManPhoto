USE [ShowManPhotoDB]
GO
/****** Object:  StoredProcedure [dbo].[User_Insert]    Script Date: 11.05.2020 22:20:21 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[User_Insert]
	@Name nvarchar(100),
	@LastName nvarchar(100),
	@Email varchar(200),
	@Password varchar(50)
AS
BEGIN	
	INSERT INTO [dbo].[User]
	(
      [Name]
      ,LastName
      ,Email
      ,[Password]
      ,RegistrationDate
	)
	VALUES(
      @Name
      ,@LastName
      ,@Email
      ,@Password
      ,GETDATE()
	)
	select SCOPE_IDENTITY()
END

--EXEC [dbo].[User_Insert] 'User', 'User', 'user@mail.com', '1'