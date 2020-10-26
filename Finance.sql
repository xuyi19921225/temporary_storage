USE [FinanceInvoiceCompare]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2020/10/26 16:58:55 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NOT NULL,
	[CompanyName] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [nvarchar](50) NOT NULL,
	[InvoiceDate] [datetime] NOT NULL,
	[Amount] [float] NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[DataSource] [nvarchar](50) NOT NULL,
	[MatchDate] [datetime] NULL,
	[IsMatch] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Icon] [nvarchar](50) NOT NULL,
	[Path] [nvarchar](200) NOT NULL,
	[Redirect] [nvarchar](50) NOT NULL,
	[Component] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[AlwaysShow] [bit] NOT NULL,
	[Hidden] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleCode] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuMapping]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleMenuMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[MenuID] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_RolePermissionMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SAPInvoiceData]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAPInvoiceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Vendor] [nvarchar](50) NOT NULL,
	[Reference] [nvarchar](50) NOT NULL,
	[Cocd] [nvarchar](50) NOT NULL,
	[DocumentNo] [nvarchar](50) NOT NULL,
	[NetDueDT] [datetime] NOT NULL,
	[PstngDate] [datetime] NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[Curr] [nvarchar](50) NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[AmountInDC] [float] NOT NULL,
	[PBk] [nvarchar](50) NULL,
	[Text] [nvarchar](50) NOT NULL,
	[BlineDate] [datetime] NOT NULL,
	[AmtLC2] [float] NOT NULL,
	[Assign] [nvarchar](50) NOT NULL,
	[GL] [int] NULL,
	[ClrngDoc] [nvarchar](50) NULL,
	[IsDelete] [bit] NOT NULL,
	[IsMatch] [char](1) NOT NULL,
	[Check] [int] NOT NULL,
	[Remark] [nvarchar](50) NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_SAPData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NTID] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCompanyMapping]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCompanyMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompanyID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_UserCompanyMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleMapping]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRoleMapping](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
 CONSTRAINT [PK_UserRoleMapping] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 2020/10/26 16:58:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vendor](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[VendorCode] [nvarchar](50) NOT NULL,
	[VendorChName] [nvarchar](50) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreateBy] [nvarchar](50) NOT NULL,
	[CreateAt] [datetime] NOT NULL,
	[UpdatedBy] [nvarchar](50) NULL,
	[UpdatedAt] [datetime] NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([ID], [Code], [CompanyName], [IsDelete], [CreateAt], [CreateBy], [UpdatedBy], [UpdatedAt]) VALUES (1, N'A001', N'SLF', 0, CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'1272417', NULL, NULL)
INSERT [dbo].[Company] ([ID], [Code], [CompanyName], [IsDelete], [CreateAt], [CreateBy], [UpdatedBy], [UpdatedAt]) VALUES (2, N'A002', N'SND', 0, CAST(N'2020-10-26T00:00:00.000' AS DateTime), N'1272417', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[Invoice] ON 

INSERT [dbo].[Invoice] ([ID], [InvoiceNumber], [InvoiceDate], [Amount], [EntryDate], [DataSource], [MatchDate], [IsMatch], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, N'010101', CAST(N'2020-10-11T16:00:00.000' AS DateTime), 0, CAST(N'1753-01-01T00:00:00.000' AS DateTime), N'Manual', CAST(N'1753-01-01T00:00:00.000' AS DateTime), N'N', 0, N'3', CAST(N'2020-10-26T16:55:54.413' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Invoice] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, -1, N'系统设置', N'el-icon-s-tools', N'/system', N'/system/user', N'layout', N'系统设置', 1, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-23T16:35:26.367' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, 1, N'用户管理', N'icon', N'/user', N'', N'user', N'用户管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-23T16:38:22.593' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (7, 1, N'角色管理', N'icon', N'/role', N'', N'role', N'角色管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-23T16:38:29.333' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (8, 1, N'菜单管理', N'icon', N'/menu', N'', N'menu', N'菜单管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-23T16:38:33.400' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (9, 1, N'客户信息', N'icon', N'/vendor', N'', N'vendor', N'客户信息', 0, 0, 0, N'3', CAST(N'2020-10-21T16:40:45.470' AS DateTime), N'3', CAST(N'2020-10-23T16:38:37.913' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (10, -1, N'asd', N'asd', N'asd', N'asd', N'asd', N'asd', 1, 0, 1, N'3', CAST(N'2020-10-21T20:14:02.803' AS DateTime), NULL, CAST(N'2020-10-21T20:14:12.967' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (11, -1, N'发票信息', N'ss', N'/invoice', N'/invoice/sap', N'layout', N'发票信息', 1, 0, 0, N'3', CAST(N'2020-10-23T16:20:13.030' AS DateTime), N'3', CAST(N'2020-10-26T15:22:05.100' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (12, 11, N'SAP发票', N'ss', N'/sap', N'', N'sapInvoice', N'SAP发票', 0, 0, 0, N'3', CAST(N'2020-10-23T16:21:01.700' AS DateTime), N'3', CAST(N'2020-10-26T15:22:11.013' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (13, 11, N'Site发票', N'dasd', N'/site', N'', N'siteInvoice', N'Site发票', 0, 0, 0, N'3', CAST(N'2020-10-26T15:20:11.140' AS DateTime), N'3', CAST(N'2020-10-26T15:23:05.077' AS DateTime))
SET IDENTITY_INSERT [dbo].[Menu] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, N'Admin', N'A001', 0, N'1272417', CAST(N'2020-10-19T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (2, N'superadmin1', N'A002', 0, N'3', CAST(N'2020-10-21T14:15:47.247' AS DateTime), N'3', CAST(N'2020-10-21T14:19:33.717' AS DateTime))
INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'12312111', N'123', 1, N'3', CAST(N'2020-10-21T14:19:48.357' AS DateTime), N'3', CAST(N'2020-10-21T14:57:00.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] ON 

INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (42, 1, 1, N'1272417', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (43, 1, 3, N'1272417', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (44, 1, 7, N'1272417', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (45, 1, 8, N'1272417', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (46, 1, 9, N'3', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (47, 1, 11, N'3', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (48, 1, 12, N'3', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (49, 1, 13, N'3', CAST(N'2020-10-26T15:20:20.817' AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] OFF
SET IDENTITY_INSERT [dbo].[SAPInvoiceData] ON 

INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, N'538', N'9441914354', N'0930', N'5100395852', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3497.83, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3497.83, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.207' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (2, N'538', N'9442153912', N'0930', N'5100395853', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3242.59, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3242.59, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'538', N'9441914360', N'0930', N'5100395854', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3496.06, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3496.06, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (4, N'538', N'9441914358', N'0930', N'5100395855', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3108, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3108, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (5, N'538', N'9442153917', N'0930', N'5100395856', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3176.93, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3176.93, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (6, N'538', N'9442153914', N'0930', N'5100395857', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -3259.87, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -3259.87, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (7, N'538', N'9442153915', N'0930', N'5100395858', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -5795.33, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -5795.33, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (8, N'538', N'9442153921', N'0930', N'5100395859', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -16531.2, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -16531.2, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (9, N'538', N'9442153922', N'0930', N'5100395860', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -22050, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -22050, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (10, N'538', N'9442153919', N'0930', N'5100395861', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-22T00:00:00.000' AS DateTime), N'USD', N'RE', -5678.59, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -5678.59, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (11, N'538', N'9442028575', N'0930', N'5100395862', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), N'USD', N'RE', -244530, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -244530, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (12, N'538', N'9442104226', N'0930', N'5100395875', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), N'USD', N'RE', -15050, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -15050, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (13, N'538', N'9442104228', N'0930', N'5100395876', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), N'USD', N'RE', -16929.1, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -16929.1, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (14, N'538', N'9442104227', N'0930', N'5100395877', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), N'USD', N'RE', -11283.2, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -11283.2, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (15, N'538', N'9442104229', N'0930', N'5100395878', CAST(N'2020-10-15T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), N'USD', N'RE', -16937.7, N'', N'', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -16937.7, N'20200823', 21000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (16, N'190102', N'36327406', N'0930', N'5100395863', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -28815, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -4164.34, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (17, N'190102', N'36327407', N'0930', N'5100395864', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -89270, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -12901.3, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (18, N'190102', N'36327408', N'0930', N'5100395865', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -239673, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -34637.54, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (19, N'190102', N'36327409', N'0930', N'5100395866', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -32770, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -4735.92, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (20, N'190102', N'36327410', N'0930', N'5100395867', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -42375, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -6124.04, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (21, N'190102', N'36327411', N'0930', N'5100395868', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -4257.66, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -615.32, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (22, N'190102', N'36327412', N'0930', N'5100395869', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -94784.4, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -13698.22, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (23, N'190102', N'36327413', N'0930', N'5100395870', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -47392.24, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -6849.16, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (24, N'190102', N'36327414', N'0930', N'5100395871', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-11T00:00:00.000' AS DateTime), N'CNY', N'RE', -27685, N'', N'PO:FINAL INV', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -4001.04, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (25, N'254052', N'71967706', N'0930', N'5100395872', CAST(N'2020-10-30T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-08T00:00:00.000' AS DateTime), N'CNY', N'RE', -49084.37, N'', N'MS:REBATE', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -7093.68, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (26, N'10015484', N'25946481', N'0930', N'5100395874', CAST(N'2020-11-29T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-08-10T00:00:00.000' AS DateTime), N'CNY', N'RE', -73990.32, N'', N'PL', CAST(N'2020-08-31T00:00:00.000' AS DateTime), -10693.06, N'20200823', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
INSERT [dbo].[SAPInvoiceData] ([ID], [Vendor], [Reference], [Cocd], [DocumentNo], [NetDueDT], [PstngDate], [DocDate], [Curr], [Type], [AmountInDC], [PBk], [Text], [BlineDate], [AmtLC2], [Assign], [GL], [ClrngDoc], [IsDelete], [IsMatch], [Check], [Remark], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (27, N'10017576', N'01127265', N'0930', N'2300065799', CAST(N'2020-10-16T00:00:00.000' AS DateTime), CAST(N'2020-08-23T00:00:00.000' AS DateTime), CAST(N'2020-07-18T00:00:00.000' AS DateTime), N'CNY', N'ZK', -107350, N'', N'', CAST(N'2020-07-18T00:00:00.000' AS DateTime), -15514.22, N'C-10000702025', 20000, N'', 0, N'N', 0, N'', N'3', CAST(N'2020-10-26T14:47:05.287' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[SAPInvoiceData] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'1272417', N'1272417', N'994080906@qq.com', 1, 0, N'1272417', CAST(N'2020-10-21T11:42:57.127' AS DateTime), N'3', CAST(N'2020-10-22T20:21:46.850' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (8, N'123', N'123', N'yi_xu5@jabil.com', 1, 1, N'3', CAST(N'2020-10-21T10:53:58.570' AS DateTime), NULL, CAST(N'2020-10-21T10:53:58.570' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (9, N'123', N'123', N'yi_xu5@jabil.com', 1, 1, N'3', CAST(N'2020-10-21T10:54:09.020' AS DateTime), NULL, CAST(N'2020-10-21T10:54:09.020' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (10, N'123', N'123', N'yi_xu5@jabil.com', 0, 0, N'3', CAST(N'2020-10-21T10:56:30.697' AS DateTime), N'3', CAST(N'2020-10-21T21:42:04.400' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (11, N'1234', N'1234', N'yi_xu5@jabil.com', 0, 0, N'3', CAST(N'2020-10-21T11:45:23.183' AS DateTime), N'3', CAST(N'2020-10-21T21:07:15.707' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (12, N'qwe', N'weqwe', N'99@qq.com', 1, 1, N'3', CAST(N'2020-10-21T12:06:13.110' AS DateTime), NULL, CAST(N'2020-10-21T13:58:05.007' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (13, N'123213123', N'123', N'994080906@qq.com', 1, 0, N'3', CAST(N'2020-10-21T21:13:00.890' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] ON 

INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (1, 1, 1, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (4, 1, 3, N'1272417', CAST(N'2020-10-26T15:57:47.633' AS DateTime))
INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (5, 2, 3, N'1272417', CAST(N'2020-10-26T15:57:47.633' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] OFF
SET IDENTITY_INSERT [dbo].[UserRoleMapping] ON 

INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (7, 11, 1, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (9, 10, 2, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (10, 3, 1, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserRoleMapping] OFF
SET IDENTITY_INSERT [dbo].[Vendor] ON 

INSERT [dbo].[Vendor] ([ID], [VendorCode], [VendorChName], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (20, N'7451', N'tets1', 0, N'3', CAST(N'2020-10-23T14:06:38.590' AS DateTime), NULL, NULL)
INSERT [dbo].[Vendor] ([ID], [VendorCode], [VendorChName], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (21, N'9862', N'tets2', 0, N'3', CAST(N'2020-10-23T14:06:38.603' AS DateTime), NULL, NULL)
INSERT [dbo].[Vendor] ([ID], [VendorCode], [VendorChName], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (22, N'1231', N'1231123123', 0, N'3', CAST(N'2020-10-23T14:06:42.830' AS DateTime), N'3', CAST(N'2020-10-23T14:06:49.113' AS DateTime))
SET IDENTITY_INSERT [dbo].[Vendor] OFF
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Hidden]  DEFAULT ((0)) FOR [Hidden]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_IsActive]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SAPInvoiceData] ADD  CONSTRAINT [DF_SAPData_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_Vendor_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
