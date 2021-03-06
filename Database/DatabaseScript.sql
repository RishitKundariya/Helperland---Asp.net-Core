USE [master]
GO
/****** Object:  Database [Helperland]    Script Date: 19-01-2022 3.16.52 PM ******/
CREATE DATABASE [Helperland]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Helperland', FILENAME = N'G:\Tatvasoft\Tatvasoft_work\Database\Helperland.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Helperland_log', FILENAME = N'G:\Tatvasoft\Tatvasoft_work\Database\Helperland_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Helperland] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Helperland].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Helperland] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Helperland] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Helperland] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Helperland] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Helperland] SET ARITHABORT OFF 
GO
ALTER DATABASE [Helperland] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Helperland] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Helperland] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Helperland] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Helperland] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Helperland] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Helperland] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Helperland] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Helperland] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Helperland] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Helperland] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Helperland] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Helperland] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Helperland] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Helperland] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Helperland] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Helperland] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Helperland] SET RECOVERY FULL 
GO
ALTER DATABASE [Helperland] SET  MULTI_USER 
GO
ALTER DATABASE [Helperland] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Helperland] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Helperland] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Helperland] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Helperland] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Helperland', N'ON'
GO
ALTER DATABASE [Helperland] SET QUERY_STORE = OFF
GO
USE [Helperland]
GO
/****** Object:  Table [dbo].[Address]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Address](
	[AddressID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[StreetName] [varchar](100) NOT NULL,
	[HouseName] [varchar](100) NOT NULL,
	[PostalCode] [varchar](10) NOT NULL,
	[CityID] [int] NOT NULL,
	[PhoneNumber] [varchar](10) NULL,
 CONSTRAINT [PK_Address] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Avatar]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Avatar](
	[AvatarID] [int] IDENTITY(1,1) NOT NULL,
	[AvatarImage] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Avatar] PRIMARY KEY CLUSTERED 
(
	[AvatarID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Block]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Block](
	[BlockID] [int] IDENTITY(1,1) NOT NULL,
	[BlockingUserID] [int] NOT NULL,
	[BlockedUserID] [int] NOT NULL,
 CONSTRAINT [PK_Block] PRIMARY KEY CLUSTERED 
(
	[BlockID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CancelService]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CancelService](
	[CancelServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NOT NULL,
	[Message] [varchar](250) NOT NULL,
 CONSTRAINT [PK_CancelService] PRIMARY KEY CLUSTERED 
(
	[CancelServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[CityID] [int] IDENTITY(1,1) NOT NULL,
	[CityName] [varchar](100) NOT NULL,
	[StateID] [int] NOT NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[CityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtraService]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraService](
	[ExtraServiceID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NOT NULL,
	[ExtraServiceTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ExtraService] PRIMARY KEY CLUSTERED 
(
	[ExtraServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtraServiceType]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraServiceType](
	[ExtraServiceTypeID] [int] IDENTITY(1,1) NOT NULL,
	[ExtraServiceName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ExtraServiceType] PRIMARY KEY CLUSTERED 
(
	[ExtraServiceTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favourite]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favourite](
	[FavouriteID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[ServiceProviderID] [int] NOT NULL,
 CONSTRAINT [PK_Favourite] PRIMARY KEY CLUSTERED 
(
	[FavouriteID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Gender]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gender](
	[GenderID] [int] IDENTITY(1,1) NOT NULL,
	[Gender] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Gender] PRIMARY KEY CLUSTERED 
(
	[GenderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ratting]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ratting](
	[RattingID] [int] IDENTITY(1,1) NOT NULL,
	[ServiceID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ServiceProviderID] [int] NOT NULL,
	[OverAllRating] [decimal](5, 2) NOT NULL,
	[OnTime] [decimal](5, 2) NULL,
	[Friendly] [decimal](5, 2) NULL,
	[QualityOfService] [decimal](5, 2) NULL,
	[Feedback] [varchar](250) NULL,
 CONSTRAINT [PK_Ratting] PRIMARY KEY CLUSTERED 
(
	[RattingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[ServiceID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[StartTime] [varchar](50) NOT NULL,
	[TotalTime] [decimal](5, 2) NOT NULL,
	[AddressID] [int] NOT NULL,
	[HaveAPet] [int] NOT NULL,
	[Amount] [decimal](8, 3) NOT NULL,
	[IsAccept] [int] NULL,
	[IsCompleted] [int] NULL,
	[ServiceProviderID] [int] NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceProvider]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceProvider](
	[ServiceProviderID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[GenderID] [int] NOT NULL,
	[AvatarID] [int] NOT NULL,
	[BillingAddressID] [int] NOT NULL,
	[TaxNo] [varchar](50) NOT NULL,
	[IsWorkingWithPet] [int] NOT NULL,
 CONSTRAINT [PK_ServiceProvider] PRIMARY KEY CLUSTERED 
(
	[ServiceProviderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[State]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[State](
	[StateID] [int] IDENTITY(1,1) NOT NULL,
	[StateName] [varchar](100) NOT NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[StateID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](100) NOT NULL,
	[LastName] [varchar](100) NOT NULL,
	[UserTypeID] [int] NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[MobileNumber] [varchar](20) NULL,
	[DOB] [date] NULL,
	[IsActive] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 19-01-2022 3.16.52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserType](
	[UserTypeID] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[UserTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_City]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_User]
GO
ALTER TABLE [dbo].[Address]  WITH CHECK ADD  CONSTRAINT [FK_Address_UserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[Address] CHECK CONSTRAINT [FK_Address_UserType]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_User] FOREIGN KEY([BlockedUserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_User]
GO
ALTER TABLE [dbo].[Block]  WITH CHECK ADD  CONSTRAINT [FK_Block_User1] FOREIGN KEY([BlockingUserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Block] CHECK CONSTRAINT [FK_Block_User1]
GO
ALTER TABLE [dbo].[CancelService]  WITH CHECK ADD  CONSTRAINT [FK_CancelService_Service] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Service] ([ServiceID])
GO
ALTER TABLE [dbo].[CancelService] CHECK CONSTRAINT [FK_CancelService_Service]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_City] FOREIGN KEY([CityID])
REFERENCES [dbo].[City] ([CityID])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_City]
GO
ALTER TABLE [dbo].[City]  WITH CHECK ADD  CONSTRAINT [FK_City_State] FOREIGN KEY([StateID])
REFERENCES [dbo].[State] ([StateID])
GO
ALTER TABLE [dbo].[City] CHECK CONSTRAINT [FK_City_State]
GO
ALTER TABLE [dbo].[ExtraService]  WITH CHECK ADD  CONSTRAINT [FK_ExtraService_ExtraService] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Service] ([ServiceID])
GO
ALTER TABLE [dbo].[ExtraService] CHECK CONSTRAINT [FK_ExtraService_ExtraService]
GO
ALTER TABLE [dbo].[ExtraService]  WITH CHECK ADD  CONSTRAINT [FK_ExtraService_ExtraServiceType] FOREIGN KEY([ExtraServiceTypeID])
REFERENCES [dbo].[ExtraServiceType] ([ExtraServiceTypeID])
GO
ALTER TABLE [dbo].[ExtraService] CHECK CONSTRAINT [FK_ExtraService_ExtraServiceType]
GO
ALTER TABLE [dbo].[Favourite]  WITH CHECK ADD  CONSTRAINT [FK_Favourite_ServiceProvider] FOREIGN KEY([ServiceProviderID])
REFERENCES [dbo].[ServiceProvider] ([ServiceProviderID])
GO
ALTER TABLE [dbo].[Favourite] CHECK CONSTRAINT [FK_Favourite_ServiceProvider]
GO
ALTER TABLE [dbo].[Favourite]  WITH CHECK ADD  CONSTRAINT [FK_Favourite_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Favourite] CHECK CONSTRAINT [FK_Favourite_User]
GO
ALTER TABLE [dbo].[Ratting]  WITH CHECK ADD  CONSTRAINT [FK_Ratting_Service] FOREIGN KEY([ServiceID])
REFERENCES [dbo].[Service] ([ServiceID])
GO
ALTER TABLE [dbo].[Ratting] CHECK CONSTRAINT [FK_Ratting_Service]
GO
ALTER TABLE [dbo].[Ratting]  WITH CHECK ADD  CONSTRAINT [FK_Ratting_ServiceProvider] FOREIGN KEY([ServiceProviderID])
REFERENCES [dbo].[ServiceProvider] ([ServiceProviderID])
GO
ALTER TABLE [dbo].[Ratting] CHECK CONSTRAINT [FK_Ratting_ServiceProvider]
GO
ALTER TABLE [dbo].[Ratting]  WITH CHECK ADD  CONSTRAINT [FK_Ratting_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Ratting] CHECK CONSTRAINT [FK_Ratting_User]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Address] FOREIGN KEY([AddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Address]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ServiceProvider] FOREIGN KEY([ServiceProviderID])
REFERENCES [dbo].[ServiceProvider] ([ServiceProviderID])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ServiceProvider]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_User]
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProvider_Address] FOREIGN KEY([BillingAddressID])
REFERENCES [dbo].[Address] ([AddressID])
GO
ALTER TABLE [dbo].[ServiceProvider] CHECK CONSTRAINT [FK_ServiceProvider_Address]
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProvider_Avatar] FOREIGN KEY([AvatarID])
REFERENCES [dbo].[Avatar] ([AvatarID])
GO
ALTER TABLE [dbo].[ServiceProvider] CHECK CONSTRAINT [FK_ServiceProvider_Avatar]
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProvider_Gender] FOREIGN KEY([GenderID])
REFERENCES [dbo].[Gender] ([GenderID])
GO
ALTER TABLE [dbo].[ServiceProvider] CHECK CONSTRAINT [FK_ServiceProvider_Gender]
GO
ALTER TABLE [dbo].[ServiceProvider]  WITH CHECK ADD  CONSTRAINT [FK_ServiceProvider_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO
ALTER TABLE [dbo].[ServiceProvider] CHECK CONSTRAINT [FK_ServiceProvider_User]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_UserType] FOREIGN KEY([UserTypeID])
REFERENCES [dbo].[UserType] ([UserTypeID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_UserType]
GO
USE [master]
GO
ALTER DATABASE [Helperland] SET  READ_WRITE 
GO
