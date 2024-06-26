USE [master]
GO

CREATE DATABASE INVERSION
GO

USE [INVERSION]
GO

--Tabla Activos
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Activos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ticker] [varchar](50) NOT NULL,
	[Nombre] [varchar](max) NOT NULL,
	[TipoActivo] [int] NOT NULL,
	[PrecioUnitario] [decimal](18, 4) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

--Tabla Ordenes
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ordenes](
	[IdOrden] [int] IDENTITY(1,1) NOT NULL,
	[IdCuenta] [int] NOT NULL,
	[IdActivo] [int] NOT NULL,
	[NombreActivo] [varchar](32) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[Precio] [decimal](18, 4) NULL,
	[Operacion] [char](1) NOT NULL,
	[Estado] [int] NOT NULL,
	[MontoTotal] [decimal](18, 4) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOrden] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

--Data Activos
SET IDENTITY_INSERT [dbo].[Activos] ON 

INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (1, N'AAPL', N'Apple', 1, CAST(177.9700 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (2, N'GOOGL', N'Alphabet Inc', 1, CAST(138.2100 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (3, N'MSFT', N'Microsoft', 1, CAST(329.0400 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (4, N'KO', N'Coca Cola', 1, CAST(58.3000 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (5, N'WMT', N'Walmart', 1, CAST(163.4200 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (6, N'AL30', N'BONOS ARGENTINA USD 2030 L.A', 2, CAST(307.4000 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (7, N'GD30', N'Bonos Globales Argentina USD Step Up 2030', 2, CAST(336.1000 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (8, N'Delta.Pesos', N'Delta Pesos Clase A', 3, CAST(0.0181 AS Decimal(18, 4)))
INSERT [dbo].[Activos] ([Id], [Ticker], [Nombre], [TipoActivo], [PrecioUnitario]) VALUES (9, N'Fima.Premium', N'Fima Premium Clase A', 3, CAST(0.0317 AS Decimal(18, 4)))
SET IDENTITY_INSERT [dbo].[Activos] OFF
GO