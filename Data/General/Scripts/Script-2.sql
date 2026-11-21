CREATE TABLE [dbo].[tblUsers](
	[pkiUserID] [int] IDENTITY(1,1) NOT NULL,
	[userName] [nvarchar](50) NOT NULL,
	[firstName] [nvarchar](255) NOT NULL,
	[lastName] [nvarchar](255) NOT NULL,
	[userEmail] [nvarchar](255) NOT NULL,
	[fkiRoleID] INT NOT NULL,
	[genderID] INT NULL,
	[hashPassword] [nvarchar](255) NOT NULL,
	[isActive] [bit] NOT NULL,
	[isBlocked] [bit] NOT NULL,
	[dateCreated] [datetime] NOT NULL,
	[lastLogin] [datetime] NULL
)

CREATE TABLE [dbo].[tblUserRoles](
	[pkiRoleID] [int] IDENTITY(1,1) NOT NULL,
	[txtRoleName] [nvarchar](50) NOT NULL,
	[txtDescription] [nvarchar](255) NOT NULL,
	[isActive] [bit] NOT NULL,
)

CREATE TABLE [dbo].[tblNationality](
	[pkiNationalityID] [int] IDENTITY(1,1) NOT NULL,
	[txtNationality] [nvarchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
)

CREATE TABLE [dbo].[tblGender](
	[pkiGenderID] [int] IDENTITY(1,1) NOT NULL,
	[txtGender] [nvarchar](50) NOT NULL,
	[isActive] [bit] NOT NULL,
)

INSERT INTO [dbo].[tblUserRoles]
    ([txtRoleName], [txtDescription], [isActive])
VALUES
    ('User', 'Standard user with basic system access.', 1),
    ('Admin', 'Administrator with access to manage users and system settings.', 1),
    ('Manager', 'Manager with access to oversee operations and manage assigned resources.', 1),
    ('Superuser', 'Full system access with all administrative privileges.', 1);

CREATE VIEW [dbo].[vw_ActiveUsers]
AS
SELECT
    u.pkiUserID,
    u.userName,
    u.firstName,
    u.lastName,
    u.userEmail,
    u.fkiRoleID,
    r.txtRoleName,
    r.txtDescription AS roleDescription,
    u.genderID,
    g.txtGender,
    u.isActive,
    u.isBlocked,
    u.dateCreated,
    u.lastLogin
FROM dbo.tblUsers u
LEFT JOIN dbo.tblUserRoles r ON u.fkiRoleID = r.pkiRoleID
LEFT JOIN dbo.tblGender g ON u.genderID = g.pkiGenderID
LEFT JOIN dbo.tblNationality n ON u.nationalityID = n.pkiNationalityID
WHERE
    u.isActive = 1
    AND u.isBlocked = 0;






