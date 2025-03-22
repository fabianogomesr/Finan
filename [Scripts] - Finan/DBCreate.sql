USE [master]
GO
/****** Object:  Database [Finan]    Script Date: 19/01/2025 22:33:05 ******/
CREATE DATABASE [Finan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Finan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Finan.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Finan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Finan_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Finan] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Finan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Finan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Finan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Finan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Finan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Finan] SET ARITHABORT OFF 
GO
ALTER DATABASE [Finan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Finan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Finan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Finan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Finan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Finan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Finan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Finan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Finan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Finan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Finan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Finan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Finan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Finan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Finan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Finan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Finan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Finan] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Finan] SET  MULTI_USER 
GO
ALTER DATABASE [Finan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Finan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Finan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Finan] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Finan] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Finan] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Finan] SET QUERY_STORE = OFF
GO
USE [Finan]
GO
/****** Object:  Table [dbo].[Account]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Account](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BankId] [int] NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Agency] [varchar](10) NOT NULL,
	[Number] [varchar](15) NOT NULL,
	[CreditLimit] [decimal](18, 0) NOT NULL,
	[Balance] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountDeposit]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountDeposit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[FinancialGroupId] [int] NOT NULL,
	[FinancialClassificationId] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Date] [datetime] NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[AccrualPeriodDate] [datetime] NOT NULL,
	[Observation] [varchar](100) NULL
 CONSTRAINT [PK_AccountLaunch] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Code] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CostCenter]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CostCenter](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_CostCenter] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currency]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [varchar](3) NOT NULL,
	[Symbol] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialClassification]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialClassification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[FinancialGroupId] [int] NOT NULL,
	[Type] [tinyint] NOT NULL,
 CONSTRAINT [PK_FinancialClassification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FinancialGroup]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FinancialGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
 CONSTRAINT [PK_FinancialGroup] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payer]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payer](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Payer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[FinancialGroupId] [int] NOT NULL,
	[FinancialClassificationId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[LatePayments] [decimal](18, 0) NOT NULL,
	[TotalPaid] [decimal](18, 0) NULL,
	[IssueDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[CashFlowDate] [datetime] NOT NULL,
	[AccrualPeriodDate] [datetime] NOT NULL,
	[PayerId] [smallint] NOT NULL,
	[Observation] [varchar](100) NULL,
	[Status] [tinyint] NOT NULL
 CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Receivable]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Receivable](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CostCenterId] [int] NOT NULL,
	[FinancialGroupId] [int] NOT NULL,
	[FinancialClassificationId] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[Type] [tinyint] NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[Discount] [decimal](18, 0) NOT NULL,
	[TotalReceivable] [decimal](18, 0) NOT NULL,
	[IssueDate] [datetime] NOT NULL,
	[DueDate] [datetime] NOT NULL,
	[CashFlowDate] [datetime] NOT NULL,
	[AccrualPeriodDate] [datetime] NOT NULL,
	[Observation] [varchar](100) NULL,
	[Status] [tinyint] NOT NULL
 CONSTRAINT [PK_Receivable] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statement]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statement](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FlowDate] [datetime] NOT NULL,
	[ReconciledDate] [datetime] NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
	[Balance] [decimal](18, 0) NOT NULL,
	[PaymentId] [int] NULL,
	[ReceivableId] [int] NULL,
	[AccountDepositId] [int] NULL,
	[AccountId] [int] NOT NULL,
	[Reversed] [bit] NOT NULL
 CONSTRAINT [PK_Statement] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 19/01/2025 22:33:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](100) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Role] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Account]  WITH CHECK ADD  CONSTRAINT [FK_Account_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([Id])
GO
ALTER TABLE [dbo].[Account] CHECK CONSTRAINT [FK_Account_Bank]
GO
ALTER TABLE [dbo].[AccountDeposit]  WITH CHECK ADD  CONSTRAINT [FK_AccountDeposit_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
ALTER TABLE [dbo].[AccountDeposit] CHECK CONSTRAINT [FK_AccountDeposit_CostCenter]
GO
ALTER TABLE [dbo].[AccountDeposit]  WITH CHECK ADD  CONSTRAINT [FK_AccountDeposit_FinancialClassification] FOREIGN KEY([FinancialClassificationId])
REFERENCES [dbo].[FinancialClassification] ([Id])
GO
ALTER TABLE [dbo].[AccountDeposit] CHECK CONSTRAINT [FK_AccountDeposit_FinancialClassification]
GO
ALTER TABLE [dbo].[AccountDeposit]  WITH CHECK ADD  CONSTRAINT [FK_AccountDeposit_FinancialGroup] FOREIGN KEY([FinancialGroupId])
REFERENCES [dbo].[FinancialGroup] ([Id])
GO
ALTER TABLE [dbo].[AccountDeposit] CHECK CONSTRAINT [FK_AccountDeposit_FinancialGroup]
GO
ALTER TABLE [dbo].[FinancialClassification]  WITH CHECK ADD  CONSTRAINT [FK_FinancialClassification_FinancialGroup] FOREIGN KEY([FinancialGroupId])
REFERENCES [dbo].[FinancialGroup] ([Id])
GO
ALTER TABLE [dbo].[FinancialClassification] CHECK CONSTRAINT [FK_FinancialClassification_FinancialGroup]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_CostCenter]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Currency]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_FinancialClassification] FOREIGN KEY([FinancialClassificationId])
REFERENCES [dbo].[FinancialClassification] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_FinancialClassification]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_FinancialGroup] FOREIGN KEY([FinancialGroupId])
REFERENCES [dbo].[FinancialGroup] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_FinancialGroup]
GO
ALTER TABLE [dbo].[Payment]  WITH CHECK ADD  CONSTRAINT [FK_Payment_Payer] FOREIGN KEY([PayerId])
REFERENCES [dbo].[Payer] ([Id])
GO
ALTER TABLE [dbo].[Payment] CHECK CONSTRAINT [FK_Payment_Payer]
GO
ALTER TABLE [dbo].[Receivable]  WITH CHECK ADD  CONSTRAINT [FK_Receivable_CostCenter] FOREIGN KEY([CostCenterId])
REFERENCES [dbo].[CostCenter] ([Id])
GO
ALTER TABLE [dbo].[Receivable] CHECK CONSTRAINT [FK_Receivable_CostCenter]
GO
ALTER TABLE [dbo].[Receivable]  WITH CHECK ADD  CONSTRAINT [FK_Receivable_Currency] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currency] ([Id])
GO
ALTER TABLE [dbo].[Receivable] CHECK CONSTRAINT [FK_Receivable_Currency]
GO
ALTER TABLE [dbo].[Receivable]  WITH CHECK ADD  CONSTRAINT [FK_Receivable_FinancialClassification] FOREIGN KEY([FinancialClassificationId])
REFERENCES [dbo].[FinancialClassification] ([Id])
GO
ALTER TABLE [dbo].[Receivable] CHECK CONSTRAINT [FK_Receivable_FinancialClassification]
GO
ALTER TABLE [dbo].[Receivable]  WITH CHECK ADD  CONSTRAINT [FK_Receivable_FinancialGroup] FOREIGN KEY([FinancialGroupId])
REFERENCES [dbo].[FinancialGroup] ([Id])
GO
ALTER TABLE [dbo].[Receivable] CHECK CONSTRAINT [FK_Receivable_FinancialGroup]
GO
ALTER TABLE [dbo].[Statement]  WITH CHECK ADD  CONSTRAINT [FK_Statement_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[Account] ([Id])
GO
ALTER TABLE [dbo].[Statement] CHECK CONSTRAINT [FK_Statement_Account]
GO
ALTER TABLE [dbo].[Statement]  WITH NOCHECK ADD  CONSTRAINT [FK_Statement_AccountDeposit] FOREIGN KEY([AccountDepositId])
REFERENCES [dbo].[AccountDeposit] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Statement] NOCHECK CONSTRAINT [FK_Statement_AccountDeposit]
GO
ALTER TABLE [dbo].[Statement]  WITH NOCHECK ADD  CONSTRAINT [FK_Statement_Payment] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payment] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Statement] NOCHECK CONSTRAINT [FK_Statement_Payment]
GO
ALTER TABLE [dbo].[Statement]  WITH NOCHECK ADD  CONSTRAINT [FK_Statement_Receivable] FOREIGN KEY([ReceivableId])
REFERENCES [dbo].[Receivable] ([Id])
NOT FOR REPLICATION 
GO
ALTER TABLE [dbo].[Statement] NOCHECK CONSTRAINT [FK_Statement_Receivable]
GO
USE [master]
GO
ALTER DATABASE [Finan] SET  READ_WRITE 
GO
