USE [master]
GO
/****** Object:  Database [EscolaApp]    Script Date: 15/06/2021 19:33:54 ******/
CREATE DATABASE [EscolaApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'EscolaApp', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EscolaApp.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'EscolaApp_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\EscolaApp_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [EscolaApp] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [EscolaApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [EscolaApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [EscolaApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [EscolaApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [EscolaApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [EscolaApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [EscolaApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [EscolaApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [EscolaApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [EscolaApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [EscolaApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [EscolaApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [EscolaApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [EscolaApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [EscolaApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [EscolaApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [EscolaApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [EscolaApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [EscolaApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [EscolaApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [EscolaApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [EscolaApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [EscolaApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [EscolaApp] SET RECOVERY FULL 
GO
ALTER DATABASE [EscolaApp] SET  MULTI_USER 
GO
ALTER DATABASE [EscolaApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [EscolaApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [EscolaApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [EscolaApp] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [EscolaApp] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [EscolaApp] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'EscolaApp', N'ON'
GO
ALTER DATABASE [EscolaApp] SET QUERY_STORE = OFF
GO
USE [EscolaApp]
GO
/****** Object:  Table [dbo].[Alunos]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alunos](
	[IdAluno] [int] IDENTITY(1000,1) NOT NULL,
	[Nome] [nvarchar](20) NOT NULL,
	[Apelido] [nvarchar](50) NOT NULL,
	[DataNascimento] [datetime2](7) NOT NULL,
	[NIF] [nvarchar](9) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
	[LastUpdate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Alunos] PRIMARY KEY CLUSTERED 
(
	[IdAluno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alunos_Emails]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alunos_Emails](
	[IdContactoMail] [int] IDENTITY(1,1) NOT NULL,
	[IdAluno] [int] NOT NULL,
	[IdEmail] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Alunos_Emails] PRIMARY KEY CLUSTERED 
(
	[IdContactoMail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Alunos_Telefones]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Alunos_Telefones](
	[IdContactoTelefone] [int] IDENTITY(1,1) NOT NULL,
	[IdAluno] [int] NOT NULL,
	[IdTelefone] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Alunos_Telefones] PRIMARY KEY CLUSTERED 
(
	[IdContactoTelefone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Areas]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Areas](
	[IdArea] [int] IDENTITY(1,1) NOT NULL,
	[Designação] [nvarchar](50) NOT NULL,
	[Detalhes] [nvarchar](250) NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Areas] PRIMARY KEY CLUSTERED 
(
	[IdArea] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cursos]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cursos](
	[IdCurso] [int] IDENTITY(500,1) NOT NULL,
	[IdArea] [int] NOT NULL,
	[Designação] [nvarchar](50) NOT NULL,
	[Detalhes] [nvarchar](250) NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Cursos] PRIMARY KEY CLUSTERED 
(
	[IdCurso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[IdEmail] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[IdEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inscritos]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inscritos](
	[IdInscricao] [int] IDENTITY(1,1) NOT NULL,
	[IdTurma] [int] NOT NULL,
	[IdAluno] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Inscritos] PRIMARY KEY CLUSTERED 
(
	[IdInscricao] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Notas]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notas](
	[IdNota] [int] IDENTITY(1,1) NOT NULL,
	[IdAluno] [int] NOT NULL,
	[IdUFCD] [int] NOT NULL,
	[Nota] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Notas] PRIMARY KEY CLUSTERED 
(
	[IdNota] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Referenciais]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Referenciais](
	[IdReferencial] [int] IDENTITY(1,1) NOT NULL,
	[IdCurso] [int] NOT NULL,
	[IdUFCD] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Referenciais_1] PRIMARY KEY CLUSTERED 
(
	[IdReferencial] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Telefones]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Telefones](
	[IdTelefone] [int] IDENTITY(1,1) NOT NULL,
	[Telefone] [nvarchar](9) NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Telefones] PRIMARY KEY CLUSTERED 
(
	[IdTelefone] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Turmas]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Turmas](
	[IdTurma] [int] IDENTITY(50,1) NOT NULL,
	[IdCurso] [int] NOT NULL,
	[Designação] [nvarchar](250) NOT NULL,
	[DataInicio] [datetime2](7) NULL,
	[DataFim] [datetime2](7) NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_Turmas] PRIMARY KEY CLUSTERED 
(
	[IdTurma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UFCDs]    Script Date: 15/06/2021 19:33:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UFCDs](
	[IdUFCD] [int] IDENTITY(1,1) NOT NULL,
	[Designação] [nvarchar](50) NOT NULL,
	[Detalhes] [nvarchar](250) NULL,
	[Duração] [int] NOT NULL,
	[CreateDate] [datetime2](7) NULL,
	[LastUpdate] [datetime2](7) NULL,
 CONSTRAINT [PK_UFCDs] PRIMARY KEY CLUSTERED 
(
	[IdUFCD] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Alunos] ADD  CONSTRAINT [DF_Alunos_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Alunos] ADD  CONSTRAINT [DF_Alunos_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Alunos_Emails] ADD  CONSTRAINT [DF_Alunos_Emails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Alunos_Emails] ADD  CONSTRAINT [DF_Alunos_Emails_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Alunos_Telefones] ADD  CONSTRAINT [DF_Alunos_Telefones_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Alunos_Telefones] ADD  CONSTRAINT [DF_Alunos_Telefones_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Areas] ADD  CONSTRAINT [DF_Areas_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Areas] ADD  CONSTRAINT [DF_Areas_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Cursos] ADD  CONSTRAINT [DF_Cursos_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Cursos] ADD  CONSTRAINT [DF_Cursos_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Emails] ADD  CONSTRAINT [DF_Emails_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Emails] ADD  CONSTRAINT [DF_Emails_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Inscritos] ADD  CONSTRAINT [DF_Inscritos_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Inscritos] ADD  CONSTRAINT [DF_Inscritos_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Notas] ADD  CONSTRAINT [DF_Notas_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Notas] ADD  CONSTRAINT [DF_Notas_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Referenciais] ADD  CONSTRAINT [DF_Referenciais_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Referenciais] ADD  CONSTRAINT [DF_Referenciais_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Telefones] ADD  CONSTRAINT [DF_Telefones_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Telefones] ADD  CONSTRAINT [DF_Telefones_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Turmas] ADD  CONSTRAINT [DF_Turmas_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[Turmas] ADD  CONSTRAINT [DF_Turmas_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[UFCDs] ADD  CONSTRAINT [DF_UFCDs_CreateDate]  DEFAULT (getdate()) FOR [CreateDate]
GO
ALTER TABLE [dbo].[UFCDs] ADD  CONSTRAINT [DF_UFCDs_LastUpdate]  DEFAULT (getdate()) FOR [LastUpdate]
GO
ALTER TABLE [dbo].[Alunos_Emails]  WITH CHECK ADD  CONSTRAINT [FK_Alunos_Emails_Alunos] FOREIGN KEY([IdAluno])
REFERENCES [dbo].[Alunos] ([IdAluno])
GO
ALTER TABLE [dbo].[Alunos_Emails] CHECK CONSTRAINT [FK_Alunos_Emails_Alunos]
GO
ALTER TABLE [dbo].[Alunos_Emails]  WITH CHECK ADD  CONSTRAINT [FK_Alunos_Emails_Emails] FOREIGN KEY([IdEmail])
REFERENCES [dbo].[Emails] ([IdEmail])
GO
ALTER TABLE [dbo].[Alunos_Emails] CHECK CONSTRAINT [FK_Alunos_Emails_Emails]
GO
ALTER TABLE [dbo].[Alunos_Telefones]  WITH CHECK ADD  CONSTRAINT [FK_Alunos_Telefones_Alunos] FOREIGN KEY([IdAluno])
REFERENCES [dbo].[Alunos] ([IdAluno])
GO
ALTER TABLE [dbo].[Alunos_Telefones] CHECK CONSTRAINT [FK_Alunos_Telefones_Alunos]
GO
ALTER TABLE [dbo].[Alunos_Telefones]  WITH CHECK ADD  CONSTRAINT [FK_Alunos_Telefones_Telefones] FOREIGN KEY([IdTelefone])
REFERENCES [dbo].[Telefones] ([IdTelefone])
GO
ALTER TABLE [dbo].[Alunos_Telefones] CHECK CONSTRAINT [FK_Alunos_Telefones_Telefones]
GO
ALTER TABLE [dbo].[Cursos]  WITH CHECK ADD  CONSTRAINT [FK_Cursos_Areas] FOREIGN KEY([IdArea])
REFERENCES [dbo].[Areas] ([IdArea])
GO
ALTER TABLE [dbo].[Cursos] CHECK CONSTRAINT [FK_Cursos_Areas]
GO
ALTER TABLE [dbo].[Inscritos]  WITH CHECK ADD  CONSTRAINT [FK_Inscritos_Alunos] FOREIGN KEY([IdAluno])
REFERENCES [dbo].[Alunos] ([IdAluno])
GO
ALTER TABLE [dbo].[Inscritos] CHECK CONSTRAINT [FK_Inscritos_Alunos]
GO
ALTER TABLE [dbo].[Inscritos]  WITH CHECK ADD  CONSTRAINT [FK_Inscritos_Turmas] FOREIGN KEY([IdTurma])
REFERENCES [dbo].[Turmas] ([IdTurma])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Inscritos] CHECK CONSTRAINT [FK_Inscritos_Turmas]
GO
ALTER TABLE [dbo].[Notas]  WITH CHECK ADD  CONSTRAINT [FK_Notas_Alunos] FOREIGN KEY([IdAluno])
REFERENCES [dbo].[Alunos] ([IdAluno])
GO
ALTER TABLE [dbo].[Notas] CHECK CONSTRAINT [FK_Notas_Alunos]
GO
ALTER TABLE [dbo].[Notas]  WITH CHECK ADD  CONSTRAINT [FK_Notas_UFCDs] FOREIGN KEY([IdUFCD])
REFERENCES [dbo].[UFCDs] ([IdUFCD])
GO
ALTER TABLE [dbo].[Notas] CHECK CONSTRAINT [FK_Notas_UFCDs]
GO
ALTER TABLE [dbo].[Referenciais]  WITH CHECK ADD  CONSTRAINT [FK_Referenciais_Cursos] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Cursos] ([IdCurso])
GO
ALTER TABLE [dbo].[Referenciais] CHECK CONSTRAINT [FK_Referenciais_Cursos]
GO
ALTER TABLE [dbo].[Referenciais]  WITH CHECK ADD  CONSTRAINT [FK_Referenciais_UFCDs] FOREIGN KEY([IdUFCD])
REFERENCES [dbo].[UFCDs] ([IdUFCD])
GO
ALTER TABLE [dbo].[Referenciais] CHECK CONSTRAINT [FK_Referenciais_UFCDs]
GO
ALTER TABLE [dbo].[Turmas]  WITH CHECK ADD  CONSTRAINT [FK_Turmas_Cursos] FOREIGN KEY([IdCurso])
REFERENCES [dbo].[Cursos] ([IdCurso])
GO
ALTER TABLE [dbo].[Turmas] CHECK CONSTRAINT [FK_Turmas_Cursos]
GO
ALTER TABLE [dbo].[Notas]  WITH CHECK ADD  CONSTRAINT [CK_NotaBetween0AND20] CHECK  (([Nota]>=(0) AND [Nota]<=(20)))
GO
ALTER TABLE [dbo].[Notas] CHECK CONSTRAINT [CK_NotaBetween0AND20]
GO
ALTER TABLE [dbo].[UFCDs]  WITH CHECK ADD  CONSTRAINT [CK_Duracao_eq_25_or_eq_50] CHECK  (([Duração]=(25) OR [Duração]=(50)))
GO
ALTER TABLE [dbo].[UFCDs] CHECK CONSTRAINT [CK_Duracao_eq_25_or_eq_50]
GO
USE [master]
GO
ALTER DATABASE [EscolaApp] SET  READ_WRITE 
GO
