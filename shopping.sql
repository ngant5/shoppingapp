USE [master]
GO
/****** Object:  Database [shopping]    Script Date: 19/04/2024 1:18:15 CH ******/
CREATE DATABASE [shopping]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'shopping', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.PC\MSSQL\DATA\shopping.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'shopping_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.PC\MSSQL\DATA\shopping_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [shopping] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [shopping].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [shopping] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [shopping] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [shopping] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [shopping] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [shopping] SET ARITHABORT OFF 
GO
ALTER DATABASE [shopping] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [shopping] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [shopping] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [shopping] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [shopping] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [shopping] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [shopping] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [shopping] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [shopping] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [shopping] SET  ENABLE_BROKER 
GO
ALTER DATABASE [shopping] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [shopping] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [shopping] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [shopping] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [shopping] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [shopping] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [shopping] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [shopping] SET RECOVERY FULL 
GO
ALTER DATABASE [shopping] SET  MULTI_USER 
GO
ALTER DATABASE [shopping] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [shopping] SET DB_CHAINING OFF 
GO
ALTER DATABASE [shopping] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [shopping] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [shopping] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [shopping] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'shopping', N'ON'
GO
ALTER DATABASE [shopping] SET QUERY_STORE = ON
GO
ALTER DATABASE [shopping] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [shopping]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryID] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[CustomerID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](100) NULL,
	[Address] [nvarchar](max) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DeliveryType]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DeliveryType](
	[DeliveryTypeID] [int] NOT NULL,
	[Title] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[DeliveryTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[FullName] [nvarchar](100) NULL,
	[Role] [nvarchar](50) NULL,
	[PhoneNumber] [nvarchar](20) NULL,
	[Email] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[EmployeeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderID] [int] IDENTITY(1,1) NOT NULL,
	[OrderNumber] [int] NULL,
	[CustomerID] [int] NULL,
	[EmployeeID] [int] NULL,
	[OrderDate] [datetime] NULL,
	[TotalAmount] [decimal](18, 2) NULL,
	[Status] [nvarchar](50) NULL,
	[ShippingAddress] [nvarchar](max) NULL,
	[Notes] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductCode]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductCode](
	[CodeID] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CodeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 19/04/2024 1:18:16 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](100) NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [decimal](18, 2) NULL,
	[Code] [int] NULL,
	[CategoryID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (1, N'category 1 edit ')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (2, N'Category 2')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (3, N'Category test')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (4, N'Category 4')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (33, N'Category 5')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (34, N'Category 5')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (51, N'test')
INSERT [dbo].[Categories] ([CategoryID], [CategoryName]) VALUES (53, N'add')
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Customers] ON 

INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (1, N'john_doe', N'12345678', N'John Doe', N'123 Main Street, Cityville, State', N'john.doe@example.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (2, N'jenny_smith', N'P@ssw0rd', N'Jenny Smith', N'456 Elm Avenue, Townsville, State', N'jenny.smith@example.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (3, N'bob_jones', N'password123', N'Bob Jones', N'789 Oak Road, Villagetown, State', N'bob.jones@example.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (4, N'john_doe', N'12345678', N'John Doe', N'123 Main Street, Cityville, State', N'john.doe@example.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (5, N'bob_jones', N'password123', N'Bob Jones', N'789 Oak Road, Villagetown, State', N'bob.jones@example.com')
INSERT [dbo].[Customers] ([CustomerID], [Username], [Password], [FullName], [Address], [Email]) VALUES (6, N'bob_jones', N'password123', N'Bob Jones', N'789 Oak Road, Villagetown, State', N'bob.jones@example.com')
SET IDENTITY_INSERT [dbo].[Customers] OFF
GO
INSERT [dbo].[DeliveryType] ([DeliveryTypeID], [Title]) VALUES (0, N'0 Day Delivery')
INSERT [dbo].[DeliveryType] ([DeliveryTypeID], [Title]) VALUES (1, N'Standard Delivery')
INSERT [dbo].[DeliveryType] ([DeliveryTypeID], [Title]) VALUES (2, N'Express Delivery')
INSERT [dbo].[DeliveryType] ([DeliveryTypeID], [Title]) VALUES (3, N'Next Day Delivery')
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (1, N'employee1', N'password123', N'John Smith', N'Sales Representative', N'123-456-7890', N'john.smith@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (2, N'employee2', N'securepass', N'Alice Johnson', N'Customer Service Representative', N'987-654-3210', N'alice.johnson@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (3, N'employee3', N'pass123', N'Michael Brown', N'Warehouse Manager', N'555-555-5555', N'michael.brown@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (4, N'employee1', N'password123', N'test ', N'test ', N'123-456-7890', N'john.smith@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (5, N'Username', N'Password', N'FullName ', N'Role ', N'000000000', N'55@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (8, N'employee3', N'pass123', N'Michael Brown', N'Warehouse Manager', N'555-555-5555', N'michael.brown@example.com')
INSERT [dbo].[Employees] ([EmployeeID], [Username], [Password], [FullName], [Role], [PhoneNumber], [Email]) VALUES (9, N'employee3', N'pass123', N'Michael Brown', N'Warehouse Manager', N'555-555-5555', N'michael.brown@example.com')
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderID], [OrderNumber], [CustomerID], [EmployeeID], [OrderDate], [TotalAmount], [Status], [ShippingAddress], [Notes]) VALUES (4, 1002, 2, 2, CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(200.00 AS Decimal(18, 2)), N'Processing', N'456 Elm Avenue, Townsville, State', N'Fragile items, handle with care.')
INSERT [dbo].[Orders] ([OrderID], [OrderNumber], [CustomerID], [EmployeeID], [OrderDate], [TotalAmount], [Status], [ShippingAddress], [Notes]) VALUES (5, 55555, 3, 3, CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'Shipped', N'789 Oak Road, Villagetown, State', N'Customer requested gift wrapping.')
INSERT [dbo].[Orders] ([OrderID], [OrderNumber], [CustomerID], [EmployeeID], [OrderDate], [TotalAmount], [Status], [ShippingAddress], [Notes]) VALUES (15, 666666, 5, 8, CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'Shipped', N'789 Oak Road, Villagetown, State', N'Customer requested gift wrapping.')
INSERT [dbo].[Orders] ([OrderID], [OrderNumber], [CustomerID], [EmployeeID], [OrderDate], [TotalAmount], [Status], [ShippingAddress], [Notes]) VALUES (16, 8888888, 6, 9, CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'Shipped', N'789 Oak Road, Villagetown, State', N'Customer requested gift wrapping.')
INSERT [dbo].[Orders] ([OrderID], [OrderNumber], [CustomerID], [EmployeeID], [OrderDate], [TotalAmount], [Status], [ShippingAddress], [Notes]) VALUES (17, 55555, 3, 3, CAST(N'2024-04-15T00:00:00.000' AS DateTime), CAST(100.00 AS Decimal(18, 2)), N'Shipped', N'789 Oak Road, Villagetown, State', N'Customer requested gift wrapping.')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductCode] ON 

INSERT [dbo].[ProductCode] ([CodeID], [Title]) VALUES (1, N'PC001')
INSERT [dbo].[ProductCode] ([CodeID], [Title]) VALUES (2, N'LPT002')
INSERT [dbo].[ProductCode] ([CodeID], [Title]) VALUES (3, N'EDIT')
INSERT [dbo].[ProductCode] ([CodeID], [Title]) VALUES (5, N'55555')
SET IDENTITY_INSERT [dbo].[ProductCode] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductID], [ProductName], [Description], [Price], [Code], [CategoryID]) VALUES (2, N'Product 1', N'Description of Product 1', CAST(50.00 AS Decimal(18, 2)), 1, 1)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Description], [Price], [Code], [CategoryID]) VALUES (3, N'Product 2', N'Description of Product 2', CAST(75.00 AS Decimal(18, 2)), 2, 2)
INSERT [dbo].[Products] ([ProductID], [ProductName], [Description], [Price], [Code], [CategoryID]) VALUES (4, N'Product 3', N'Description of Product 3', CAST(100.00 AS Decimal(18, 2)), 3, 3)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([CustomerID])
REFERENCES [dbo].[Customers] ([CustomerID])
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD FOREIGN KEY([EmployeeID])
REFERENCES [dbo].[Employees] ([EmployeeID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([CategoryID])
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
USE [master]
GO
ALTER DATABASE [shopping] SET  READ_WRITE 
GO
