
/*********************************** 
[dbo].[CreditPointTransaction]  
***********************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditPointTransaction](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[NetCreditPoints] [int] NULL,
	[AmountPaid] [int] NULL,
	[CreditPackageId] [int] NULL,
	[PurchasedDate] [datetime] NULL,
	[IsDeleted] [bit] NULL,
	[ModifiedDate] [datetime] NULL,
	[CouponId] [int] NULL,
	[CreditCardId] [int] NULL,
	[CreatedDate] [datetime] NULL,
	[ConfirmationNo] [int] NULL
) 

GO
/**************************  
[dbo].[CreditPackageMaster]    
****************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CreditPackageMaster](
	[credit_package_id] [int] IDENTITY(1,1) NOT NULL,
	[credit_package_name] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[credit_points] [int] NOT NULL,
	[created_date] [datetime] NOT NULL,
	[tribute_type] [int] NULL,
	[deleted_date] [datetime] NULL
) 

GO
/****************************** 
[dbo].[CreditPackageCostMaster]   
*********************************/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CreditPackageCostMaster](
	[CostId] [int] IDENTITY(1,1) NOT NULL,
	[CreditPoints] [int] NOT NULL,
	[CostPerCredit] [numeric](18, 2) NOT NULL,
	[TributeType] [int] NULL,
	[IsActive] [bit] NULL
) 
GO
/********************************
dbo.TRIBUTES
*********************************/
Alter table dbo.TRIBUTES add IsOrderDVDChecked bit ,IsMemTributeBoxChecked bit;
GO
Alter table tributes add LinkMemTributeId int;
GO
/********************************
dbo.USERBUSINESS
*********************************/

Alter table [dbo].[USERBUSINESS] Add HeaderBGColor Varchar(10);
GO
Alter table [dbo].[USERBUSINESS] Add IsAddressOn bit;
GO
Alter table [dbo].[USERBUSINESS] Add IsWebAddressOn bit;
GO
Alter table [dbo].[USERBUSINESS] Add IsObUrlLinkOn bit;
GO
Alter table [dbo].[USERBUSINESS] Add IsPhoneNoOn bit;
GO
Alter table [dbo].[USERBUSINESS] Add DisplayCustomHeader bit;
GO
Alter table [dbo].[USERBUSINESS] Add HeaderLogo varchar(255);
GO
ALTER table dbo.USERBUSINESS ADD ObituaryLinkPage varchar(100);
Go
--only to be rum for the first time

Update [dbo].[USERBUSINESS] Set IsAddressOn = 'false';
GO
Update [dbo].[USERBUSINESS] Set IsWebAddressOn = 'false';
GO
Update [dbo].[USERBUSINESS] Set IsObUrlLinkOn = 'false';
GO
Update [dbo].[USERBUSINESS] Set IsPhoneNoOn = 'false'
GO
Update [dbo].[USERBUSINESS] Set DisplayCustomHeader = 'false';

GO
/********************************
dbo.LinkVdeoMemorialTribute
*********************************/
/****** Object:  Table [dbo].[LinkVideoMemorialTribute]    Script Date: 11/30/2010 12:09:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkVideoMemorialTribute](
	[UserId] [int] NOT NULL,
	[VideoTributeId] [int] NULL,
	[MemTributeId] [int] NULL
) ON [PRIMARY]

GO

/********************************
dbo.themes
*********************************/
Alter table themes add SubCategory varchar(100) ,FolderName varchar(255);
alter table dbo.TRIBUTEPACKAGE alter column AmountPaid numeric(18,2)