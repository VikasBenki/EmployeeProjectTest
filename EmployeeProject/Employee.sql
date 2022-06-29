use project;

Create table EmployeeDetails
( 
EmpId Int identity(1,1) primary key,
Employee_Name varchar(225) not null,
Contact Bigint not null,
Email varchar(225) not null unique,
Department varchar(225),
UserId int not null foreign key (UserId) references Users(UserId)
)

select * from EmployeeDetails;
------- Procedure to add Employee ------
Create proc SP_Add_Employee
(
@EmployeeName varchar(225),
@Contact bigint,
@Email varchar(225),
@Department varchar(225),
@UserId int 
)
As
Begin try
		insert into EmployeeDetails values(@EmployeeName,@Contact ,@Email ,@Department,@UserId);
end try
BEGIN CATCH
SELECT
ERROR_NUMBER() AS ErrorNumber,
ERROR_SEVERITY() AS ErrorSeverity,
ERROR_STATE() AS ErrorState,
ERROR_PROCEDURE() AS ErrorProcedure,
ERROR_LINE() AS ErrorLine,
ERROR_MESSAGE() AS ErrorMessage;
END CATCH

exec SP_Add_Employee 'Virat',8232564652, 'virat@gmail.com','HR',4;

--------------------------procedure to delete Employee -------------------

Create Proc SP_Delete_Employee
(
@UserId int,
@EmpId int
)
As
Begin try
delete from EmployeeDetails where EmpId = @EmpId and UserId =@UserId;
end try
begin catch
select
ERROR_NUMBER() as ErrorNumber,
ERROR_LINE() as ErrorLine,
ERROR_PROCEDURE() as ErrorProcedure,
ERROR_SEVERITY() as ErrorSeverity,
ERROR_MESSAGE() as ErrorMessage,
ERROR_STATE() as ErrorState
end catch

-------------------------Stored Procedure for the Get all Employees -----------

Create Proc SP_GetAll_Employee
As
Begin
select * From EmployeeDetails;
end

