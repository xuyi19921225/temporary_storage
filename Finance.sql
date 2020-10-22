USE [FinanceInvoiceCompare]
GO
/****** Object:  Table [dbo].[Company]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleMenuMapping]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SAPInvoiceData]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserCompanyMapping]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRoleMapping]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vendor]    Script Date: 2020/10/22 23:00:50 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Company] ON 

INSERT [dbo].[Company] ([ID], [Code], [CompanyName], [IsDelete], [CreateAt], [CreateBy], [UpdatedBy], [UpdatedAt]) VALUES (1, N'A001', N'SLF', 0, CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'1272417', NULL, NULL)
SET IDENTITY_INSERT [dbo].[Company] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 

INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, -1, N'系统设置', N'Icon', N'', N'/system/user', N'layout', N'系统设置', 1, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, 1, N'用户管理', N'icon', N'/system/user', N'', N'user', N'用户管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-21T16:47:29.420' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (7, 1, N'角色管理', N'icon', N'/system/role', N'', N'role', N'角色管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), N'3', CAST(N'2020-10-21T16:43:15.770' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (8, 1, N'菜单管理', N'icon', N'/system/menu', N'', N'menu', N'菜单管理', 0, 0, 0, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (9, 1, N'客户信息', N'icon', N'/system/vendor', N'', N'vendor', N'客户信息', 0, 0, 0, N'3', CAST(N'2020-10-21T16:40:45.470' AS DateTime), N'3', CAST(N'2020-10-22T21:19:28.550' AS DateTime))
INSERT [dbo].[Menu] ([ID], [ParentID], [Name], [Icon], [Path], [Redirect], [Component], [Title], [AlwaysShow], [Hidden], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (10, -1, N'asd', N'asd', N'asd', N'asd', N'asd', N'asd', 1, 0, 1, N'3', CAST(N'2020-10-21T20:14:02.803' AS DateTime), NULL, CAST(N'2020-10-21T20:14:12.967' AS DateTime))
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (1, N'Admin', N'A001', 0, N'1272417', CAST(N'2020-10-19T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (2, N'superadmin1', N'A002', 0, N'3', CAST(N'2020-10-21T14:15:47.247' AS DateTime), N'3', CAST(N'2020-10-21T14:19:33.717' AS DateTime))
INSERT [dbo].[Role] ([ID], [RoleName], [RoleCode], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'12312111', N'123', 1, N'3', CAST(N'2020-10-21T14:19:48.357' AS DateTime), N'3', CAST(N'2020-10-21T14:57:00.127' AS DateTime))
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] ON 

INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (30, 1, 1, N'1272417', CAST(N'2020-10-22T21:15:17.260' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (31, 1, 3, N'1272417', CAST(N'2020-10-22T21:15:17.260' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (32, 1, 7, N'1272417', CAST(N'2020-10-22T21:15:17.260' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (33, 1, 8, N'1272417', CAST(N'2020-10-22T21:15:17.260' AS DateTime))
INSERT [dbo].[RoleMenuMapping] ([ID], [RoleID], [MenuID], [CreateBy], [CreateAt]) VALUES (34, 1, 9, N'3', CAST(N'2020-10-22T21:15:17.260' AS DateTime))
SET IDENTITY_INSERT [dbo].[RoleMenuMapping] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (3, N'1272417', N'1272417', N'994080906@qq.com', 1, 0, N'1272417', CAST(N'2020-10-21T11:42:57.127' AS DateTime), N'3', CAST(N'2020-10-22T20:21:46.850' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (8, N'123', N'123', N'yi_xu5@jabil.com', 1, 1, N'3', CAST(N'2020-10-21T10:53:58.570' AS DateTime), NULL, CAST(N'2020-10-21T10:53:58.570' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (9, N'123', N'123', N'yi_xu5@jabil.com', 1, 1, N'3', CAST(N'2020-10-21T10:54:09.020' AS DateTime), NULL, CAST(N'2020-10-21T10:54:09.020' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (10, N'123', N'123', N'yi_xu5@jabil.com', 0, 0, N'3', CAST(N'2020-10-21T10:56:30.697' AS DateTime), N'3', CAST(N'2020-10-21T21:42:04.400' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (11, N'1234', N'1234', N'yi_xu5@jabil.com', 0, 0, N'3', CAST(N'2020-10-21T11:45:23.183' AS DateTime), N'3', CAST(N'2020-10-21T21:07:15.707' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (12, N'qwe', N'weqwe', N'99@qq.com', 1, 1, N'3', CAST(N'2020-10-21T12:06:13.110' AS DateTime), NULL, CAST(N'2020-10-21T13:58:05.007' AS DateTime))
INSERT [dbo].[User] ([ID], [NTID], [UserName], [Email], [IsActive], [IsDelete], [CreateBy], [CreateAt], [UpdatedBy], [UpdatedAt]) VALUES (13, N'123213123', N'123', N'994080906@qq.com', 1, 0, N'3', CAST(N'2020-10-21T21:13:00.890' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] ON 

INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (1, 1, 1, N'1272417', CAST(N'2020-10-20T00:00:00.000' AS DateTime))
INSERT [dbo].[UserCompanyMapping] ([ID], [CompanyID], [UserID], [CreateBy], [CreateAt]) VALUES (3, 1, 3, N'1272417', CAST(N'2020-10-22T20:22:00.917' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserCompanyMapping] OFF
GO
SET IDENTITY_INSERT [dbo].[UserRoleMapping] ON 

INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (7, 11, 1, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (9, 10, 2, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
INSERT [dbo].[UserRoleMapping] ([ID], [UserID], [RoleID], [CreateBy], [CreateAt]) VALUES (10, 3, 1, N'3', CAST(N'1753-01-01T00:00:00.000' AS DateTime))
SET IDENTITY_INSERT [dbo].[UserRoleMapping] OFF
GO
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
