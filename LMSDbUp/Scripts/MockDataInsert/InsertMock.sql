USE [LMS]
GO

INSERT INTO [dbo].[LMS.BookDetails]
           ([BookDetailsId]
           ,[BookCode]
           ,[BookName]
           ,[RackId]
           ,[RowId]
           ,[StatusId]
           ,[IsDeleted]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[EditedBy]
           ,[EditedDate])
    
      VALUES
            (NEWID(),'BookCode1','BookName1',1,1,1, 'false',N'', GETDATE(), NULL, NULL),
            (NEWID(),'BookCode2','BookName2',1,1,1, 'false',N'', GETDATE(), NULL, NULL)

USE [LMS]
GO

INSERT INTO [dbo].[LMS.UserDetails]
           ([UserDetailsId]
           ,[Name]
           ,[RollNum]
           ,[Department]
           ,[Password]
           ,[UserTypeId]
           ,[Email]
           ,[Mobile]
           ,[IsDeleted]
           ,[IsFirstLogin]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[EditedBy]
           ,[EditedDate])
     VALUES
           (NEWID(),'Admin1','Admin','mockdata','$2a$11$ShJRj1sA6drzJeD7Qim6fOxQiNmpnVObHv6b/CxtGJhqaJEV7W6Vi','1','mockdata',9876543210,'false','false', 'mockdata', GETDATE(),NULL,NULL),
           (NEWID(),'User1','User','mockData','$2a$11$QMKYNPRwW2fkBgG0zXSzRut6K4gsZK0f2jJP7gIFVjYRbZYTBNplG','2','mockdata',9876543210,'false','false', 'mockdata', GETDATE(),NULL,NULL)
GO