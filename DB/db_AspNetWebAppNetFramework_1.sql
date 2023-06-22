create database db_AspNetWebAppNetFramework_1

use db_AspNetWebAppNetFramework_1

create table Users(
	u_Id int primary key identity,
	uName varchar(50),
	uEmail varchar(50),
	uPassword varchar(200),
	uReset bit,
	uConfirmed bit,
	uToken varchar(200)
)