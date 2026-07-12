INSERT INTO Roles (RoleName)
VALUES
('SuperAdmin'),
('Admin'),
('User');

select * from Roles


INSERT INTO Branches (BranchName, Email, PasswordHash, Location, Phone, Pincode, Gender, ContactPerson, ProfileImage, ContactNumber, Status, CreatedAt, ModifiedAt) VALUES 
('Mumbai Branch','mumbaiadmin@masstech.com','Admin@123','Mumbai','9876543210','400001',1,'Rahul Sharma',NULL,'9876543210',1,GETDATE(),GETDATE()), 
('Pune Branch','puneadmin@masstech.com','Admin@123','Pune','9876543211','411001',2,'Priya Patil',NULL,'9876543211',1,GETDATE(),GETDATE()),
('Delhi Branch','delhiadmin@masstech.com','Admin@123','New Delhi','9876543212','110001',1,'Amit Verma',NULL,'9876543212',1,GETDATE(),GETDATE()), 
('Bangalore Branch','bangaloreadmin@masstech.com','Admin@123','Bangalore','9876543213','560001',1,'Sneha Rao',NULL,'9876543213',1,GETDATE(),GETDATE()),
('Hyderabad Branch','hyderabadadmin@masstech.com','Admin@123','Hyderabad','9876543214','500001',1,'Vikram Reddy',NULL,'9876543214',1,GETDATE(),GETDATE());

select * from Branches


INSERT INTO Users (RoleId, BranchId, FullName, Email, PasswordHash, Location, Phone, Pincode, Gender, ContactPerson, ProfileImage, ContactNumber, Status, CreatedAt, ModifiedAt) VALUES 
(1,3,'Super Admin','superadmin@masstech.com','Admin@123','Mumbai','9999999999','400001',1,'Head Office',NULL,'9999999999',1,GETDATE(),GETDATE()), 
(2,1,'Rahul Sharma','mumbaiadmin@masstech.com','Admin@123','Mumbai','9876543210','400001',1,'Mumbai Branch',NULL,'9876543210',1,GETDATE(),GETDATE()), 
(2,2,'Priya Patil','puneadmin@masstech.com','Admin@123','Pune','9876543211','411001',2,'Pune Branch',NULL,'9876543211',1,GETDATE(),GETDATE()), 
(3,1,'Shubham Patil','shubham@gmail.com','User@123','Mumbai','9876500001','400001',1,'Self',NULL,'9876500001',1,GETDATE(),GETDATE()), 
(3,2,'Rohit Joshi','rohit@gmail.com','User@123','Pune','9876500002','411001',1,'Self',NULL,'9876500002',1,GETDATE(),GETDATE());


select * from Users
