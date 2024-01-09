/* Migration Script */
CREATE Table [LMS.UserDetails](
UserDetailsId uniqueidentifier NOT NULL,
Name varchar(50) NOT NULL,
RollNum varchar(20) NOT NULL,
Department varchar(15) NOT Null,
Password varchar(200) NOT NULL,
UserTypeId int NOT NULL,
Email varchar(100) NOT NULL,
Mobile bigint NOT NULL,
IsDeleted bit  NULL,
IsFirstLogin bit NOT NULL,
CreatedBy varchar(50) NOT NULL,
CreatedDate datetime2 NOT NULL,
EditedBy varchar(50) NULL,
EditedDate datetime2 NULL)

GO

ALTER TABLE [LMS.UserDetails]
ADD CONSTRAINT PK_UserDetailsId PRIMARY KEY(UserDetailsId)

ALTER TABLE [LMS.UserDetails]
ADD CONSTRAINT U_RollNum UNIQUE (RollNum)






