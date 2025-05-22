/****** Object:  Table [dbo].[StockMovements]    Script Date: 22/05/2025 14:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StockMovements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ItemId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
 CONSTRAINT [PK_StockMovements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WarehouseStockItems]    Script Date: 22/05/2025 14:11:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WarehouseStockItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[QuantityInStock] [int] NOT NULL,
	[DateCreation] [datetime] NOT NULL,
	[DateLastUpdate] [datetime] NOT NULL,
 CONSTRAINT [PK_WarehouseStockItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[StockMovements] ADD  CONSTRAINT [DF_StockMovements_CreationDate]  DEFAULT (getdate()) FOR [DateCreation]
GO
ALTER TABLE [dbo].[WarehouseStockItems] ADD  CONSTRAINT [DF_WarehouseStockItems_QuantityInStock]  DEFAULT ((0)) FOR [QuantityInStock]
GO
ALTER TABLE [dbo].[WarehouseStockItems] ADD  CONSTRAINT [DF_WarehouseStockItems_CreationDate]  DEFAULT (getdate()) FOR [DateCreation]
GO
ALTER TABLE [dbo].[WarehouseStockItems] ADD  CONSTRAINT [DF_WarehouseStockItems_DateLastUpdate]  DEFAULT (getdate()) FOR [DateLastUpdate]
GO
ALTER TABLE [dbo].[StockMovements]  WITH CHECK ADD  CONSTRAINT [FK_StockMovements_WarehouseStockItems] FOREIGN KEY([ItemId])
REFERENCES [dbo].[WarehouseStockItems] ([Id])
GO
ALTER TABLE [dbo].[StockMovements] CHECK CONSTRAINT [FK_StockMovements_WarehouseStockItems]
GO
