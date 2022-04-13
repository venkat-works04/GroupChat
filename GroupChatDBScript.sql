USE [GroupChat]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [FK_UserLogins_Members]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
ALTER TABLE [dbo].[Groups] DROP CONSTRAINT IF EXISTS [FK_Groups_Members]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMessages]') AND type in (N'U'))
ALTER TABLE [dbo].[GroupMessages] DROP CONSTRAINT IF EXISTS [FK_GroupMessages_Members]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMessages]') AND type in (N'U'))
ALTER TABLE [dbo].[GroupMessages] DROP CONSTRAINT IF EXISTS [FK_GroupMessages_Groups]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembers]') AND type in (N'U'))
ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT IF EXISTS [FK_GroupMembers_Members]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembers]') AND type in (N'U'))
ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT IF EXISTS [FK_GroupMembers_Groups]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [DF_UserLogins_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Members]') AND type in (N'U'))
ALTER TABLE [dbo].[Members] DROP CONSTRAINT IF EXISTS [DF_Members_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
ALTER TABLE [dbo].[Groups] DROP CONSTRAINT IF EXISTS [DF_Groups_IsActive]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GroupMembers]') AND type in (N'U'))
ALTER TABLE [dbo].[GroupMembers] DROP CONSTRAINT IF EXISTS [DF_GroupMembers_IsActive]
GO
/****** Object:  Index [IX_UserLogins]    Script Date: 4/13/2022 5:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserLogins]') AND type in (N'U'))
ALTER TABLE [dbo].[UserLogins] DROP CONSTRAINT IF EXISTS [IX_UserLogins]
GO
/****** Object:  Index [IX_Members]    Script Date: 4/13/2022 5:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Members]') AND type in (N'U'))
ALTER TABLE [dbo].[Members] DROP CONSTRAINT IF EXISTS [IX_Members]
GO
/****** Object:  Index [IX_Groups]    Script Date: 4/13/2022 5:26:42 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Groups]') AND type in (N'U'))
ALTER TABLE [dbo].[Groups] DROP CONSTRAINT IF EXISTS [IX_Groups]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 4/13/2022 5:26:42 PM ******/
DROP TABLE IF EXISTS [dbo].[UserLogins]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 4/13/2022 5:26:42 PM ******/
DROP TABLE IF EXISTS [dbo].[Members]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 4/13/2022 5:26:42 PM ******/
DROP TABLE IF EXISTS [dbo].[Groups]
GO
/****** Object:  Table [dbo].[GroupMessages]    Script Date: 4/13/2022 5:26:42 PM ******/
DROP TABLE IF EXISTS [dbo].[GroupMessages]
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 4/13/2022 5:26:42 PM ******/
DROP TABLE IF EXISTS [dbo].[GroupMembers]
GO
/****** Object:  Table [dbo].[GroupMembers]    Script Date: 4/13/2022 5:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMembers](
	[GroupMemberId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[MemberId] [bigint] NOT NULL,
	[JoinedDateTime] [datetime] NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_GroupMembers] PRIMARY KEY CLUSTERED 
(
	[GroupMemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GroupMessages]    Script Date: 4/13/2022 5:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupMessages](
	[GroupMessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupId] [bigint] NOT NULL,
	[MessageSentBy] [bigint] NOT NULL,
	[Message] [varchar](max) NOT NULL,
	[MessageSentDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_GroupMessages] PRIMARY KEY CLUSTERED 
(
	[GroupMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Groups]    Script Date: 4/13/2022 5:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Groups](
	[GroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](50) NOT NULL,
	[GroupImageFileName] [varchar](100) NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDateTime] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Members]    Script Date: 4/13/2022 5:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Members](
	[MemberId] [bigint] IDENTITY(1,1) NOT NULL,
	[FullName] [varchar](150) NOT NULL,
	[DateOfBirth] [date] NOT NULL,
	[Gender] [smallint] NOT NULL,
	[PhoneNumber] [varchar](20) NOT NULL,
	[EmailId] [varchar](250) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[CreatedDateTime] [datetime] NOT NULL,
	[ModifiedBy] [bigint] NULL,
	[ModifiedDateTime] [datetime] NULL,
 CONSTRAINT [PK_Members] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 4/13/2022 5:26:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserLogins](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[Password] [varchar](50) NOT NULL,
	[ForgotPassword] [varchar](50) NULL,
	[LastLogin] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[MemberId] [bigint] NOT NULL,
 CONSTRAINT [PK_UserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[GroupMembers] ON 
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (1, 1, 1, CAST(N'2022-04-13T10:16:50.430' AS DateTime), 1)
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (2, 2, 2, CAST(N'2022-04-13T10:21:23.297' AS DateTime), 1)
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (3, 3, 3, CAST(N'2022-04-13T10:21:52.900' AS DateTime), 1)
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (4, 1, 2, CAST(N'2022-04-13T10:25:05.273' AS DateTime), 1)
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (5, 2, 3, CAST(N'2022-04-13T10:26:08.630' AS DateTime), 1)
GO
INSERT [dbo].[GroupMembers] ([GroupMemberId], [GroupId], [MemberId], [JoinedDateTime], [IsActive]) VALUES (6, 2, 1, CAST(N'2022-04-13T10:35:37.723' AS DateTime), 1)
GO
SET IDENTITY_INSERT [dbo].[GroupMembers] OFF
GO
SET IDENTITY_INSERT [dbo].[GroupMessages] ON 
GO
INSERT [dbo].[GroupMessages] ([GroupMessageId], [GroupId], [MessageSentBy], [Message], [MessageSentDateTime]) VALUES (1, 2, 2, N'Group Message Demo - 1', CAST(N'2022-04-13T10:27:19.973' AS DateTime))
GO
INSERT [dbo].[GroupMessages] ([GroupMessageId], [GroupId], [MessageSentBy], [Message], [MessageSentDateTime]) VALUES (2, 1, 2, N'Group Message Demo - 2', CAST(N'2022-04-13T10:27:34.163' AS DateTime))
GO
INSERT [dbo].[GroupMessages] ([GroupMessageId], [GroupId], [MessageSentBy], [Message], [MessageSentDateTime]) VALUES (3, 2, 3, N'Group Message Demo - 3', CAST(N'2022-04-13T10:35:12.043' AS DateTime))
GO
INSERT [dbo].[GroupMessages] ([GroupMessageId], [GroupId], [MessageSentBy], [Message], [MessageSentDateTime]) VALUES (4, 2, 1, N'Group Message Demo - 4', CAST(N'2022-04-13T10:36:03.890' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[GroupMessages] OFF
GO
SET IDENTITY_INSERT [dbo].[Groups] ON 
GO
INSERT [dbo].[Groups] ([GroupId], [GroupName], [GroupImageFileName], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime], [IsActive]) VALUES (1, N'Group - 1', NULL, 1, CAST(N'2022-04-13T10:16:50.360' AS DateTime), 1, CAST(N'2022-04-13T10:18:03.087' AS DateTime), 1)
GO
INSERT [dbo].[Groups] ([GroupId], [GroupName], [GroupImageFileName], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime], [IsActive]) VALUES (2, N'Group - 2', NULL, 2, CAST(N'2022-04-13T10:21:23.290' AS DateTime), 2, CAST(N'2022-04-13T10:24:35.903' AS DateTime), 1)
GO
INSERT [dbo].[Groups] ([GroupId], [GroupName], [GroupImageFileName], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime], [IsActive]) VALUES (3, N'Group - 3', NULL, 3, CAST(N'2022-04-13T10:21:52.883' AS DateTime), NULL, NULL, 1)
GO
SET IDENTITY_INSERT [dbo].[Groups] OFF
GO
SET IDENTITY_INSERT [dbo].[Members] ON 
GO
INSERT [dbo].[Members] ([MemberId], [FullName], [DateOfBirth], [Gender], [PhoneNumber], [EmailId], [IsActive], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime]) VALUES (1, N'Demo Member 1', CAST(N'1990-01-01' AS Date), 1, N'1111111111', N'demo1@sample.com', 1, 0, CAST(N'2022-04-13T09:53:37.420' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Members] ([MemberId], [FullName], [DateOfBirth], [Gender], [PhoneNumber], [EmailId], [IsActive], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime]) VALUES (2, N'Demo Member 2', CAST(N'1980-01-01' AS Date), 2, N'2222222222', N'demo2@sample.com', 1, 0, CAST(N'2022-04-13T09:54:17.973' AS DateTime), 1, CAST(N'2022-04-13T10:15:22.413' AS DateTime))
GO
INSERT [dbo].[Members] ([MemberId], [FullName], [DateOfBirth], [Gender], [PhoneNumber], [EmailId], [IsActive], [CreatedBy], [CreatedDateTime], [ModifiedBy], [ModifiedDateTime]) VALUES (3, N'Demo Member 3', CAST(N'1985-01-01' AS Date), 3, N'3333333333', N'demo3@sample.com', 1, 0, CAST(N'2022-04-13T09:54:53.997' AS DateTime), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Members] OFF
GO
SET IDENTITY_INSERT [dbo].[UserLogins] ON 
GO
INSERT [dbo].[UserLogins] ([UserId], [UserName], [Password], [ForgotPassword], [LastLogin], [IsActive], [MemberId]) VALUES (1, N'demo1', N'reset', NULL, CAST(N'2022-04-13T09:57:30.950' AS DateTime), 1, 1)
GO
INSERT [dbo].[UserLogins] ([UserId], [UserName], [Password], [ForgotPassword], [LastLogin], [IsActive], [MemberId]) VALUES (2, N'demo2', N'reset', NULL, CAST(N'2022-04-13T09:58:13.670' AS DateTime), 1, 2)
GO
INSERT [dbo].[UserLogins] ([UserId], [UserName], [Password], [ForgotPassword], [LastLogin], [IsActive], [MemberId]) VALUES (3, N'demo3', N'reset', NULL, CAST(N'2022-04-13T09:58:52.343' AS DateTime), 1, 3)
GO
SET IDENTITY_INSERT [dbo].[UserLogins] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Groups]    Script Date: 4/13/2022 5:26:42 PM ******/
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [IX_Groups] UNIQUE NONCLUSTERED 
(
	[GroupName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Members]    Script Date: 4/13/2022 5:26:42 PM ******/
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [IX_Members] UNIQUE NONCLUSTERED 
(
	[PhoneNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_UserLogins]    Script Date: 4/13/2022 5:26:42 PM ******/
ALTER TABLE [dbo].[UserLogins] ADD  CONSTRAINT [IX_UserLogins] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[GroupMembers] ADD  CONSTRAINT [DF_GroupMembers_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Groups] ADD  CONSTRAINT [DF_Groups_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Members] ADD  CONSTRAINT [DF_Members_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserLogins] ADD  CONSTRAINT [DF_UserLogins_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Groups]
GO
ALTER TABLE [dbo].[GroupMembers]  WITH CHECK ADD  CONSTRAINT [FK_GroupMembers_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[GroupMembers] CHECK CONSTRAINT [FK_GroupMembers_Members]
GO
ALTER TABLE [dbo].[GroupMessages]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessages_Groups] FOREIGN KEY([GroupId])
REFERENCES [dbo].[Groups] ([GroupId])
GO
ALTER TABLE [dbo].[GroupMessages] CHECK CONSTRAINT [FK_GroupMessages_Groups]
GO
ALTER TABLE [dbo].[GroupMessages]  WITH CHECK ADD  CONSTRAINT [FK_GroupMessages_Members] FOREIGN KEY([MessageSentBy])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[GroupMessages] CHECK CONSTRAINT [FK_GroupMessages_Members]
GO
ALTER TABLE [dbo].[Groups]  WITH CHECK ADD  CONSTRAINT [FK_Groups_Members] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[Groups] CHECK CONSTRAINT [FK_Groups_Members]
GO
ALTER TABLE [dbo].[UserLogins]  WITH CHECK ADD  CONSTRAINT [FK_UserLogins_Members] FOREIGN KEY([MemberId])
REFERENCES [dbo].[Members] ([MemberId])
GO
ALTER TABLE [dbo].[UserLogins] CHECK CONSTRAINT [FK_UserLogins_Members]
GO
