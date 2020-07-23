create table [User](
    Id INT PRIMARY KEY IDENTITY,
	[Name] nvarchar(100),
	LastName nvarchar(100),
	Email varchar(200),
	[Password] varchar(50)
	)

create table [Role](
    Id INT PRIMARY KEY,
	[Name] varchar(100),
	)

create table [User_Role](
    Id INT PRIMARY KEY IDENTITY,
	UserId INT references [User] (Id),
	RoleId INT references [Role] (Id)
	)

create table [Album](
    Id INT PRIMARY KEY IDENTITY,
	[Name] nvarchar(100),
	CreationDate datetime2,
	Comment nvarchar(400)
	)

create table [Order](
    Id INT PRIMARY KEY IDENTITY,
	UserId int references [User] (Id),
	StatusId int,
	AlbumId int references [Album] (Id)
	)
	
create table PrintFormat(
	Id INT PRIMARY KEY,
	[Properties] varchar (100) 
	)

create table [Photo](
    Id INT PRIMARY KEY IDENTITY,
	AlbumId int references [Album] (Id),
	FilePath varchar(500), 
	[FileName] nvarchar(100), 
	)

create table Order_Photo(
	Id INT PRIMARY KEY IDENTITY,
	OpderId int references [Order] (Id),
	PhotoId int references [Photo] (Id),
	IsForPrint bit,
	PrintFormatId int references PrintFormat (Id),
	Comment nvarchar(400)
	)