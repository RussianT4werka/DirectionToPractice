USE [master]
GO
/****** Object:  Database [practice]    Script Date: 05.05.2023 0:09:32 ******/
CREATE DATABASE [practice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'practice', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\practice.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'practice_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\practice_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [practice] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [practice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [practice] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [practice] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [practice] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [practice] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [practice] SET ARITHABORT OFF 
GO
ALTER DATABASE [practice] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [practice] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [practice] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [practice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [practice] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [practice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [practice] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [practice] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [practice] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [practice] SET  DISABLE_BROKER 
GO
ALTER DATABASE [practice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [practice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [practice] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [practice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [practice] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [practice] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [practice] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [practice] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [practice] SET  MULTI_USER 
GO
ALTER DATABASE [practice] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [practice] SET DB_CHAINING OFF 
GO
ALTER DATABASE [practice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [practice] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [practice] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [practice] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [practice] SET QUERY_STORE = OFF
GO
USE [practice]
GO
/****** Object:  Table [dbo].[Group]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Group](
	[Number] [int] NOT NULL,
	[SpecialityID] [int] NOT NULL,
	[Course] [int] NOT NULL,
 CONSTRAINT [PK_Group_1] PRIMARY KEY CLUSTERED 
(
	[Number] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModulePractice]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModulePractice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Number] [nvarchar](10) NOT NULL,
	[Text] [nvarchar](100) NOT NULL,
	[SpecialityID] [int] NOT NULL,
	[PracticeTypeID] [int] NOT NULL,
 CONSTRAINT [PK_ModulePractice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PracticeType]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PracticeType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_PracticeType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Speciality]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Speciality](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Speciality] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Student]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GroupNumber] [int] NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Organisation] [nvarchar](100) NULL,
	[City] [nvarchar](50) NULL,
	[StreetHouse] [nvarchar](60) NULL,
	[CanCreateDirection] [tinyint] NULL,
	[PracticeTypeID] [int] NULL,
	[ModulePracticeID] [int] NULL,
	[TeacherID] [int] NULL,
	[DateStart] [date] NULL,
	[DateEnd] [date] NULL,
	[CountHours] [int] NULL,
 CONSTRAINT [PK_Student] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Teacher]    Script Date: 05.05.2023 0:09:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teacher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Teacher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Group] ([Number], [SpecialityID], [Course]) VALUES (1135, 1, 3)
INSERT [dbo].[Group] ([Number], [SpecialityID], [Course]) VALUES (1231, 2, 3)
GO
SET IDENTITY_INSERT [dbo].[ModulePractice] ON 

INSERT [dbo].[ModulePractice] ([Id], [Number], [Text], [SpecialityID], [PracticeTypeID]) VALUES (2, N'01', N'Выполнение работ по профессии «Наладчик технологического оборудования»', 2, 2)
INSERT [dbo].[ModulePractice] ([Id], [Number], [Text], [SpecialityID], [PracticeTypeID]) VALUES (3, N'01', N'Разработка ПО для компьютеров', 1, 2)
SET IDENTITY_INSERT [dbo].[ModulePractice] OFF
GO
SET IDENTITY_INSERT [dbo].[PracticeType] ON 

INSERT [dbo].[PracticeType] ([Id], [Name]) VALUES (1, N'учебной')
INSERT [dbo].[PracticeType] ([Id], [Name]) VALUES (2, N'производственной')
INSERT [dbo].[PracticeType] ([Id], [Name]) VALUES (3, N'преддипломной')
SET IDENTITY_INSERT [dbo].[PracticeType] OFF
GO
SET IDENTITY_INSERT [dbo].[Speciality] ON 

INSERT [dbo].[Speciality] ([Id], [Name], [Code]) VALUES (1, N'Информационные системы и программирование', N'09.02.07')
INSERT [dbo].[Speciality] ([Id], [Name], [Code]) VALUES (2, N'Компьютерные сети и комплексы', N'02.02.02')
SET IDENTITY_INSERT [dbo].[Speciality] OFF
GO
SET IDENTITY_INSERT [dbo].[Student] ON 

INSERT [dbo].[Student] ([Id], [GroupNumber], [Surname], [Name], [Patronymic], [Organisation], [City], [StreetHouse], [CanCreateDirection], [PracticeTypeID], [ModulePracticeID], [TeacherID], [DateStart], [DateEnd], [CountHours]) VALUES (1, 1135, N'Кокорин', N'Александр', N'Валерьевич', NULL, NULL, NULL, 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([Id], [GroupNumber], [Surname], [Name], [Patronymic], [Organisation], [City], [StreetHouse], [CanCreateDirection], [PracticeTypeID], [ModulePracticeID], [TeacherID], [DateStart], [DateEnd], [CountHours]) VALUES (2, 1135, N'Шкорко', N'Дмитрий', N'Олегович', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([Id], [GroupNumber], [Surname], [Name], [Patronymic], [Organisation], [City], [StreetHouse], [CanCreateDirection], [PracticeTypeID], [ModulePracticeID], [TeacherID], [DateStart], [DateEnd], [CountHours]) VALUES (3, 1135, N'Валитова', N'Анна', N'Максимовна', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([Id], [GroupNumber], [Surname], [Name], [Patronymic], [Organisation], [City], [StreetHouse], [CanCreateDirection], [PracticeTypeID], [ModulePracticeID], [TeacherID], [DateStart], [DateEnd], [CountHours]) VALUES (4, 1231, N'Кузин', N'Максим', N'Олегович', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Student] ([Id], [GroupNumber], [Surname], [Name], [Patronymic], [Organisation], [City], [StreetHouse], [CanCreateDirection], [PracticeTypeID], [ModulePracticeID], [TeacherID], [DateStart], [DateEnd], [CountHours]) VALUES (5, 1231, N'Выборов', N'Максим', N'Эдуардович', NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Student] OFF
GO
SET IDENTITY_INSERT [dbo].[Teacher] ON 

INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (1, N'Пушкин', N'Алексей', N'Анатольевич')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (2, N'Побежимов', N'Михаил', N'Валентинович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (3, N'Михайлюк', N'Павел', N'Константинович')
INSERT [dbo].[Teacher] ([Id], [Surname], [Name], [Patronymic]) VALUES (4, N'Вишневский', N'Данил', N'Олегович')
SET IDENTITY_INSERT [dbo].[Teacher] OFF
GO
/****** Object:  Index [IX_Group_SpecialityID]    Script Date: 05.05.2023 0:09:32 ******/
CREATE NONCLUSTERED INDEX [IX_Group_SpecialityID] ON [dbo].[Group]
(
	[SpecialityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ModulePractice_PracticeTypeID]    Script Date: 05.05.2023 0:09:32 ******/
CREATE NONCLUSTERED INDEX [IX_ModulePractice_PracticeTypeID] ON [dbo].[ModulePractice]
(
	[PracticeTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_ModulePractice_SpecialityID]    Script Date: 05.05.2023 0:09:32 ******/
CREATE NONCLUSTERED INDEX [IX_ModulePractice_SpecialityID] ON [dbo].[ModulePractice]
(
	[SpecialityID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Student_GroupID]    Script Date: 05.05.2023 0:09:32 ******/
CREATE NONCLUSTERED INDEX [IX_Student_GroupID] ON [dbo].[Student]
(
	[GroupNumber] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Group]  WITH CHECK ADD  CONSTRAINT [FK_Group_Speciality] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[Speciality] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Group] CHECK CONSTRAINT [FK_Group_Speciality]
GO
ALTER TABLE [dbo].[ModulePractice]  WITH CHECK ADD  CONSTRAINT [FK_ModulePractice_PracticeType] FOREIGN KEY([PracticeTypeID])
REFERENCES [dbo].[PracticeType] ([Id])
GO
ALTER TABLE [dbo].[ModulePractice] CHECK CONSTRAINT [FK_ModulePractice_PracticeType]
GO
ALTER TABLE [dbo].[ModulePractice]  WITH CHECK ADD  CONSTRAINT [FK_ModulePractice_Speciality] FOREIGN KEY([SpecialityID])
REFERENCES [dbo].[Speciality] ([Id])
GO
ALTER TABLE [dbo].[ModulePractice] CHECK CONSTRAINT [FK_ModulePractice_Speciality]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Group] FOREIGN KEY([GroupNumber])
REFERENCES [dbo].[Group] ([Number])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Group]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_ModulePractice] FOREIGN KEY([ModulePracticeID])
REFERENCES [dbo].[ModulePractice] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_ModulePractice]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_PracticeType] FOREIGN KEY([PracticeTypeID])
REFERENCES [dbo].[PracticeType] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_PracticeType]
GO
ALTER TABLE [dbo].[Student]  WITH CHECK ADD  CONSTRAINT [FK_Student_Teacher] FOREIGN KEY([TeacherID])
REFERENCES [dbo].[Teacher] ([Id])
GO
ALTER TABLE [dbo].[Student] CHECK CONSTRAINT [FK_Student_Teacher]
GO
USE [master]
GO
ALTER DATABASE [practice] SET  READ_WRITE 
GO
