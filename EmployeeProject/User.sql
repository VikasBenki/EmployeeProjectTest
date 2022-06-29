Create Database Project

Use Project;

Create table Users
(
UserId int identity(1,1) primary key,
UserName Varchar(225),
Email varchar(225),
Password varchar(225)
)

select * from Users
-------Stored Procedure for the Users---------

alter Procedure SPAddUser
(
@UserName varchar(225),
@Email varchar(225),
@Password varchar(225)
)
As
Begin
		insert Users values(@UserName,@Email,@Password)
End

insert Users values('vikas','vikasms@gmail.com','Vikas@123')

-------------stored procedure for login -------

Alter proc SP_User_Login
(
@UserName varchar(225),
@Password Varchar(225)
)
As
Begin 
 
	 select * from Users where UserName=@UserName or Email=@UserName  and Password= @Password;
End

Exec SP_User_Login 'vikasms@gmail.com','Vikas@123';

 select * from Users where UserName='vikas' Or Email ='vikasms@gmail.com' and Password= 'Vikas@123';