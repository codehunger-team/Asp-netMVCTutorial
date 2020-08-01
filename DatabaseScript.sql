USE [SchoolApp]
GO
/****** Object:  Table [dbo].[ClassMaster]    Script Date: 01-08-2020 21:48:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ClassMaster](
	[ClassId] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [varchar](20) NOT NULL,
	[CourseId] [int] NOT NULL,
	[SchoolCode] [varchar](20) NULL,
 CONSTRAINT [PK_ClassMaster] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[CourseMaster]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[CourseMaster](
	[CourseId] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](30) NOT NULL,
	[SchoolCode] [varchar](20) NOT NULL,
 CONSTRAINT [PK_CourseMaster] PRIMARY KEY CLUSTERED 
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrationAmountMaster]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrationAmountMaster](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CourseId] [int] NOT NULL,
	[SchoolCode] [varchar](20) NOT NULL,
	[RegistrationAmount] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_RegistrationAmountMaster] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RegistrationMaster]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RegistrationMaster](
	[RegistrationId] [int] IDENTITY(1,1) NOT NULL,
	[RegistrationNo] [varchar](15) NOT NULL,
	[CourseId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
	[StudentName] [varchar](50) NOT NULL,
	[FatherName] [varchar](50) NOT NULL,
	[MobileNo] [varchar](10) NULL,
	[Address] [varchar](100) NULL,
	[SchoolCode] [varchar](10) NULL,
	[RegistrationAmount] [decimal](18, 0) NULL,
	[RegistrationDate] [date] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_RegistrationMaster] PRIMARY KEY CLUSTERED 
(
	[RegistrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SectionMaster]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SectionMaster](
	[SectionId] [int] IDENTITY(1,1) NOT NULL,
	[ClassId] [int] NOT NULL,
	[SectionName] [varchar](20) NOT NULL,
	[SchoolCode] [varchar](20) NULL,
 CONSTRAINT [PK_SectionMaster] PRIMARY KEY CLUSTERED 
(
	[SectionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Login]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Login](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[UserPassword] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_tb_Login] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_SchoolRegister]    Script Date: 01-08-2020 21:48:43 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_SchoolRegister](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SchoolName] [varchar](50) NOT NULL,
	[SchoolCode] [varchar](20) NOT NULL,
	[SchoolEmail] [varchar](30) NULL,
	[SchoolContactNo] [varchar](15) NULL,
 CONSTRAINT [PK_tb_SchoolRegister] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[RegistrationMaster] ADD  CONSTRAINT [DF_RegistrationMaster_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[tb_Login] ADD  CONSTRAINT [DF_tb_Login_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ClassMaster]  WITH CHECK ADD  CONSTRAINT [FK_ClassMaster_CourseMaster] FOREIGN KEY([CourseId])
REFERENCES [dbo].[CourseMaster] ([CourseId])
GO
ALTER TABLE [dbo].[ClassMaster] CHECK CONSTRAINT [FK_ClassMaster_CourseMaster]
GO
ALTER TABLE [dbo].[RegistrationAmountMaster]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationAmountMaster_CourseMaster] FOREIGN KEY([CourseId])
REFERENCES [dbo].[CourseMaster] ([CourseId])
GO
ALTER TABLE [dbo].[RegistrationAmountMaster] CHECK CONSTRAINT [FK_RegistrationAmountMaster_CourseMaster]
GO
ALTER TABLE [dbo].[RegistrationMaster]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationMaster_ClassMaster] FOREIGN KEY([ClassId])
REFERENCES [dbo].[ClassMaster] ([ClassId])
GO
ALTER TABLE [dbo].[RegistrationMaster] CHECK CONSTRAINT [FK_RegistrationMaster_ClassMaster]
GO
ALTER TABLE [dbo].[RegistrationMaster]  WITH CHECK ADD  CONSTRAINT [FK_RegistrationMaster_CourseMaster] FOREIGN KEY([CourseId])
REFERENCES [dbo].[CourseMaster] ([CourseId])
GO
ALTER TABLE [dbo].[RegistrationMaster] CHECK CONSTRAINT [FK_RegistrationMaster_CourseMaster]
GO
ALTER TABLE [dbo].[SectionMaster]  WITH CHECK ADD  CONSTRAINT [FK_SectionMaster_ClassMaster] FOREIGN KEY([ClassId])
REFERENCES [dbo].[ClassMaster] ([ClassId])
GO
ALTER TABLE [dbo].[SectionMaster] CHECK CONSTRAINT [FK_SectionMaster_ClassMaster]
GO
