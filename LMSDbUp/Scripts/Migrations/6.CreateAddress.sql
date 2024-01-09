	
CREATE Table [LMS.Address](
AddressId UniqueIdentifier NOT NULL,
UserDetailsId UniqueIdentifier NOT NULL,
HouseNo varchar(10) unique NOT NULL,
Street varchar(100) NOT NULL,
Area varchar(100) NOT NULL,
LandMark varchar(50) Null,
City varchar(20) NOT NULL,
State varchar(20) NOT NULL,
Country	varchar(20)	NOT Null,
Pincode int NOT NULL,
CreatedBy varchar(50) NOT NULL,
CreatedDate DateTime2 NOT NULL,
EditedBy varchar(50) NULL,
EditedDate DateTime2 NULL)
GO
ALTER TABLE [LMS.Address]
ADD CONSTRAINT FK_Address FOREIGN KEY (UserDetailsId) REFERENCES [LMS.UserDetails](UserDetailsId) 
GO