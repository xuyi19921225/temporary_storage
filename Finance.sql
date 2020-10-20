USE [FinanceInvoiceCompare]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2020/10/20 17:00:01 ******/
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
/****** Object:  Table [dbo].[Invoice]    Script Date: 2020/10/20 17:00:02 ******/
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
	[MatchDate] [datetime] NOT NULL,
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
/****** Object:  Table [dbo].[Menu]    Script Date: 2020/10/20 17:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentID] [int] NOT NULL,
	[MenuName] [nvarchar](50) NOT NULL,
	[Icon] [nvarchar](50) NOT NULL,
	[Path] [nvarchar](200) NOT NULL,
	[Redirect] [nvarchar](50) NOT NULL,
	[Component] [nvarchar](50) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[AlwaysShow] [bit] NOT NULL,
	[Hidden] [bit] NOT NULL,
	[HasChildren] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[Role]    Script Date: 2020/10/20 17:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NOT NULL,
	[RoleCode] [nvarchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
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
/****** Object:  Table [dbo].[RoleMenuMapping]    Script Date: 2020/10/20 17:00:02 ******/
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
/****** Object:  Table [dbo].[SAPInvoiceData]    Script Date: 2020/10/20 17:00:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SAPInvoiceData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Vendor] [nvarchar](50) NOT NULL,
	[Reference] [nvarchar](50) NOT NULL,
	[Cocd] [nvarchar](50) NOT NULL,
	[Document] [nvarchar](50) NOT NULL,
	[NetDueDT] [datetime] NOT NULL,
	[PstngDate] [datetime] NOT NULL,
	[DocDate] [datetime] NOT NULL,
	[Curr.] [nvarchar](50) NOT NULL,
	[AmountInDC] [float] NOT NULL,
	[PBk] [nvarchar](50) NULL,
	[Text] [nvarchar](50) NOT NULL,
	[BlineDate] [datetime] NOT NULL,
	[Amt. LC2] [float] NOT NULL,
	[Assign.] [nvarchar](50) NOT NULL,
	[G/L] [int] NOT NULL,
	[ClrngDoc.] [nvarchar](50) NOT NULL,
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
/****** Object:  Table [dbo].[User]    Script Date: 2020/10/20 17:00:02 ******/
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
/****** Object:  Table [dbo].[UserCompanyMapping]    Script Date: 2020/10/20 17:00:02 ******/
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
/****** Object:  Table [dbo].[UserRoleMapping]    Script Date: 2020/10/20 17:00:02 ******/
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
/****** Object:  Table [dbo].[Vendor]    Script Date: 2020/10/20 17:00:03 ******/
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
SET IDENTITY_INSERT [dbo].[Company] OFF
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([ID], [ParentID], [MenuName], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [HasChildren], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, -1, N'系统设置', N'Icon', N'', N'/system/user', N'layout', N'系统设置', 1, 0, 1, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Menu] ([ID], [ParentID], [MenuName], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [HasChildren], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, 1, N'用户管理', N'icon', N'/system/user', N'', N'user', N'用户管理', 0, 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Menu] ([ID], [ParentID], [MenuName], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [HasChildren], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (7, 1, N'角色管理', N'icon', N'/system/role', N'', N'role', N'角色管理', 0, 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Menu] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, N'Admin', N'A001', 1, 0, N'1272417', CAST(N'2020-10-19T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] ON 

INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (2, 1, 3, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (3, 1, 1, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (5, 1, 7, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'1272417', N'Xu Yi', N'994080906@qq.com', 1, 0, N'1272417', CAST(N'2020-10-19T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] ON 

INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (1, 1, 1, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] OFF
SET IDENTITY_INSERT [dbo].[UserRoleMapping] ON 

INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (1, 3, 1, N'1272417', CAST(N'2020-10-19T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserRoleMapping] OFF
ALTER TABLE [dbo].[Company] ADD  CONSTRAINT [DF_Company_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Invoice] ADD  CONSTRAINT [DF_Invoice_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_Hidden]  DEFAULT ((0)) FOR [Hidden]
GO
ALTER TABLE [dbo].[Menu] ADD  CONSTRAINT [DF_Menu_IsActive]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsActive]  DEFAULT ((0)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[SAPInvoiceData] ADD  CONSTRAINT [DF_SAPData_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
ALTER TABLE [dbo].[Vendor] ADD  CONSTRAINT [DF_Vendor_IsDelete]  DEFAULT ((0)) FOR [IsDelete]
GO
