USE [Universidade]
GO

CREATE TABLE [dbo].[turma](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[curso_id] [int] NOT NULL,
	[turma] [varchar](45) NULL,
	[ano] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[aluno](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](255) NULL,
	[usuario] [varchar](45) NULL,
	[senha] [char](60) NULL,
 CONSTRAINT [Primary] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[aluno_turma](
	[aluno_id] [int] NOT NULL,
	[turma_id] [int] NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[aluno_turma]  WITH CHECK ADD FOREIGN KEY([aluno_id])
REFERENCES [dbo].[aluno] ([id])
GO

ALTER TABLE [dbo].[aluno_turma]  WITH CHECK ADD FOREIGN KEY([turma_id])
REFERENCES [dbo].[turma] ([id])
GO