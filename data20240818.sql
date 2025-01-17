CREATE DATABASE [G03Trinh_QLBHDabiLocal]
GO
USE [G03Trinh_QLBHDabiLocal]
GO
/****** Object:  UserDefinedFunction [dbo].[getQuantityCategory]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[getQuantityCategory]
(
	@id int,
	@startDate smalldatetime,
	@endDate smalldatetime
)
returns int
as begin
	declare @quantity int
	select @quantity = case when count(*) > 0 then sum(bill_info.quantity) else 0 end
	from bill_info join product on product.id = bill_info.product join category on category.id = product.category
	join bill on bill.id = bill_info.bill
	where category.id = @id
	and bill.booking_date >= @startDate
	and bill.booking_date <= @endDate
	return @quantity
end
GO
/****** Object:  UserDefinedFunction [dbo].[getQuantityProduct]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE function [dbo].[getQuantityProduct]
(
	@id int,
	@startDate smalldatetime,
	@endDate smalldatetime
)
returns int
as begin
	declare @quantity int
	select @quantity = case when count(*) > 0 then sum(bill_info.quantity) else 0 end
	from bill_info join product on product.id = bill_info.product join bill on bill.id = bill_info.bill
	where product.id = @id
	and bill.booking_date >= @startDate
	and bill.booking_date <= @endDate
	return @quantity
end
GO
/****** Object:  Table [dbo].[account]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[username] [varchar](100) NOT NULL,
	[password] [varchar](100) NULL,
	[permission] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bank]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bank](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[idBank] [varchar](100) NULL,
	[number] [varchar](100) NULL,
	[name] [nvarchar](1000) NULL,
	[content] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer] [int] NOT NULL,
	[booking_date] [date] NULL,
	[status] [int] NULL,
	[name] [nvarchar](500) NULL,
	[phone] [varchar](10) NULL,
	[address] [ntext] NULL,
	[payment] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bill_info]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bill_info](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[bill] [int] NOT NULL,
	[product] [int] NOT NULL,
	[quantity] [int] NULL,
	[color] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer] [int] NOT NULL,
	[product] [int] NOT NULL,
	[quantity] [int] NULL,
	[color] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[category]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[category](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[metatitle] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[contact]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[contact](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[customer] [int] NOT NULL,
	[title] [nvarchar](1000) NULL,
	[content] [ntext] NULL,
	[status] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[customer]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[customer](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[address] [ntext] NULL,
	[phone] [varchar](10) NULL,
	[email] [varchar](100) NULL,
	[username] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](100) NULL,
	[description] [ntext] NULL,
	[price] [decimal](18, 0) NULL,
	[image] [ntext] NULL,
	[category] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[slide]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[slide](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[url] [text] NULL,
	[position] [int] NULL,
	[username] [varchar](100) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[account] ([username], [password], [permission]) VALUES (N'admin', N'$2a$12$S0gza0QJX2InjU1IHlxDfO89VJodMQRzopV67WJhXrxlc0JdhP8DK', 1)
INSERT [dbo].[account] ([username], [password], [permission]) VALUES (N'test123', N'$2a$10$XL6KxS9jAuVqbgi8bvS49ucA6zP5xPnOXfNjbngDpybMi2XGDcQ1O', 0)
GO
SET IDENTITY_INSERT [dbo].[bank] ON 

INSERT [dbo].[bank] ([id], [idBank], [number], [name], [content]) VALUES (1, N'970432', N'03927334401', N'DO MINH HOAN', N'Thanh toan hoa don tai SNEAKERSHOP')
SET IDENTITY_INSERT [dbo].[bank] OFF
GO
SET IDENTITY_INSERT [dbo].[bill] ON 

INSERT [dbo].[bill] ([id], [customer], [booking_date], [status], [name], [phone], [address], [payment]) VALUES (11, 3, CAST(N'2024-08-18' AS Date), 1, N'test123', N'0965025017', N'test123', 0)
INSERT [dbo].[bill] ([id], [customer], [booking_date], [status], [name], [phone], [address], [payment]) VALUES (12, 3, CAST(N'2024-08-18' AS Date), 1, N'test123', N'0965025017', N'test123', 1)
SET IDENTITY_INSERT [dbo].[bill] OFF
GO
SET IDENTITY_INSERT [dbo].[bill_info] ON 

INSERT [dbo].[bill_info] ([id], [bill], [product], [quantity], [color]) VALUES (10, 11, 27, 2, 35)
INSERT [dbo].[bill_info] ([id], [bill], [product], [quantity], [color]) VALUES (11, 12, 28, 1, 35)
INSERT [dbo].[bill_info] ([id], [bill], [product], [quantity], [color]) VALUES (12, 12, 27, 3, 35)
INSERT [dbo].[bill_info] ([id], [bill], [product], [quantity], [color]) VALUES (13, 12, 26, 1, 36)
SET IDENTITY_INSERT [dbo].[bill_info] OFF
GO
SET IDENTITY_INSERT [dbo].[category] ON 

INSERT [dbo].[category] ([id], [name], [metatitle]) VALUES (8, N'Gucci', N'gucci')
INSERT [dbo].[category] ([id], [name], [metatitle]) VALUES (9, N'Hermes', N'hermes')
SET IDENTITY_INSERT [dbo].[category] OFF
GO
SET IDENTITY_INSERT [dbo].[customer] ON 

INSERT [dbo].[customer] ([id], [name], [address], [phone], [email], [username]) VALUES (3, N'test123', N'test123', N'0965025017', N'test123@gmail.com', N'test123')
SET IDENTITY_INSERT [dbo].[customer] OFF
GO
SET IDENTITY_INSERT [dbo].[product] ON 

INSERT [dbo].[product] ([id], [name], [description], [price], [image], [category]) VALUES (26, N'Túi Đeo Vai Nữ Gucci Blondie Mini Shoulder Bag', N'<p>Giới t&iacute;nh: Nữ</p>

<p>M&agrave;u sắc: N&acirc;u/Be</p>

<p>Chất liệu: Canvas, Da</p>

<p>K&iacute;ch thước: 7cm x 21.5cm x 13.5cm</p>
', CAST(51500000 AS Decimal(18, 0)), N'/Resources/admin/img/upload/product/product-2024-08-18-12-31-50-593.png', 8)
INSERT [dbo].[product] ([id], [name], [description], [price], [image], [category]) VALUES (27, N'Túi Đeo Chéo Gucci Neo Mini Bag', N'<p>Giới t&iacute;nh: Unisex</p>

<p>M&agrave;u sắc: N&acirc;u/Be</p>

<p>Chất liệu: Canvas, Da</p>

<p>K&iacute;ch thước: 7cm x 12cm x 16cm</p>
', CAST(15900000 AS Decimal(18, 0)), N'/Resources/admin/img/upload/product/product-2024-08-18-12-33-02-739.png', 8)
INSERT [dbo].[product] ([id], [name], [description], [price], [image], [category]) VALUES (28, N'Túi Đeo Chéo Nam Gucci Bag Multicolor Phối Màu', N'<p>Giới t&iacute;nh: Nam</p>

<p>M&agrave;u sắc: Phối m&agrave;u</p>

<p>Chất liệu: Canvas, Da</p>

<p>K&iacute;ch thước: 4.5cm x 24cm x 16cm</p>
', CAST(35800000 AS Decimal(18, 0)), N'/Resources/admin/img/upload/product/product-2024-08-18-12-34-43-984.png', 8)
INSERT [dbo].[product] ([id], [name], [description], [price], [image], [category]) VALUES (29, N'Túi Tote Nữ Hermès Rouge Casaque', N'<p>Giới t&iacute;nh: Nữ</p>

<p>M&agrave;u sắc: Đỏ</p>

<p>Chất liệu: Da cao cấp</p>

<p>K&iacute;ch thước: 18cm x 18cm x 13.5cm</p>
', CAST(55400000 AS Decimal(18, 0)), N'/Resources/admin/img/upload/product/product-2024-08-18-12-40-01-404.png', 9)
INSERT [dbo].[product] ([id], [name], [description], [price], [image], [category]) VALUES (30, N'Túi Đeo Chéo Nữ Hermès Leather Crossbody Bag', N'<p>Giới t&iacute;nh: Nữ</p>

<p>M&agrave;u sắc: Đỏ đ&ocirc;</p>

<p>Chất liệu: Da cao cấp</p>

<p>K&iacute;ch thước: 23cm x 19cm x 9cm</p>
', CAST(155000000 AS Decimal(18, 0)), N'/Resources/admin/img/upload/product/product-2024-08-18-12-41-34-479.png', 9)
SET IDENTITY_INSERT [dbo].[product] OFF
GO
SET IDENTITY_INSERT [dbo].[slide] ON 

INSERT [dbo].[slide] ([id], [url], [position], [username]) VALUES (1, N'/Resources/admin/img/upload/slide/slide-2024-08-18-12-43-34-860.png', 1, N'admin')
INSERT [dbo].[slide] ([id], [url], [position], [username]) VALUES (2, N'/Resources/admin/img/upload/slide/slide-2024-08-18-12-45-36-355.png', 2, N'admin')
INSERT [dbo].[slide] ([id], [url], [position], [username]) VALUES (3, N'/Resources/admin/img/upload/slide/slide-2024-08-18-12-46-22-574.png', 3, N'admin')
INSERT [dbo].[slide] ([id], [url], [position], [username]) VALUES (4, N'/Resources/admin/img/upload/slide/slide-2024-08-18-12-47-00-745.png', 4, N'admin')
INSERT [dbo].[slide] ([id], [url], [position], [username]) VALUES (5, N'/Resources/admin/img/upload/slide/slide-2024-08-18-12-48-11-682.png', 5, N'admin')
SET IDENTITY_INSERT [dbo].[slide] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__category__855D26552FE46E3E]    Script Date: 18/08/2024 9:23:11 CH ******/
ALTER TABLE [dbo].[category] ADD UNIQUE NONCLUSTERED 
(
	[metatitle] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[bill]  WITH CHECK ADD  CONSTRAINT [fk_bill_customer] FOREIGN KEY([customer])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[bill] CHECK CONSTRAINT [fk_bill_customer]
GO
ALTER TABLE [dbo].[bill_info]  WITH CHECK ADD  CONSTRAINT [fk_bill_info_bill] FOREIGN KEY([bill])
REFERENCES [dbo].[bill] ([id])
GO
ALTER TABLE [dbo].[bill_info] CHECK CONSTRAINT [fk_bill_info_bill]
GO
ALTER TABLE [dbo].[bill_info]  WITH CHECK ADD  CONSTRAINT [fk_bill_info_product] FOREIGN KEY([product])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[bill_info] CHECK CONSTRAINT [fk_bill_info_product]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [fk_cart_customer] FOREIGN KEY([customer])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [fk_cart_customer]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD  CONSTRAINT [fk_cart_product] FOREIGN KEY([product])
REFERENCES [dbo].[product] ([id])
GO
ALTER TABLE [dbo].[cart] CHECK CONSTRAINT [fk_cart_product]
GO
ALTER TABLE [dbo].[contact]  WITH CHECK ADD  CONSTRAINT [fk_contact_customer] FOREIGN KEY([customer])
REFERENCES [dbo].[customer] ([id])
GO
ALTER TABLE [dbo].[contact] CHECK CONSTRAINT [fk_contact_customer]
GO
ALTER TABLE [dbo].[customer]  WITH CHECK ADD  CONSTRAINT [fk_customer_account] FOREIGN KEY([username])
REFERENCES [dbo].[account] ([username])
GO
ALTER TABLE [dbo].[customer] CHECK CONSTRAINT [fk_customer_account]
GO
ALTER TABLE [dbo].[product]  WITH CHECK ADD  CONSTRAINT [fk_product_category] FOREIGN KEY([category])
REFERENCES [dbo].[category] ([id])
GO
ALTER TABLE [dbo].[product] CHECK CONSTRAINT [fk_product_category]
GO
ALTER TABLE [dbo].[slide]  WITH CHECK ADD  CONSTRAINT [fk_slide_account] FOREIGN KEY([username])
REFERENCES [dbo].[account] ([username])
GO
ALTER TABLE [dbo].[slide] CHECK CONSTRAINT [fk_slide_account]
GO
/****** Object:  StoredProcedure [dbo].[reportCategory]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[reportCategory]
(
	@startDate smalldatetime,
	@endDate smalldatetime
)
as begin
	select name [label], dbo.getQuantityCategory(id, @startDate, @endDate) [data]
	from category
	order by dbo.getQuantityCategory(id, @startDate, @endDate) desc
end
GO
/****** Object:  StoredProcedure [dbo].[reportProduct]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[reportProduct]
(
	@startDate smalldatetime,
	@endDate smalldatetime
)
as begin
	select name [label], dbo.getQuantityProduct(id, @startDate, @endDate) [data]
	from product
	order by dbo.getQuantityProduct(id, @startDate, @endDate) desc
end
GO
/****** Object:  Trigger [dbo].[tg_del_acc]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[tg_del_acc] on [dbo].[account] instead of delete
as begin
	declare @username varchar(100)
	select @username = username from deleted
	declare @count int
	select @count = count(id) from customer where username = @username
	while @count > 0
	begin
		delete customer where username = @username
		select @count = count(id) from customer where username = @username
	end
	delete account where username = @username
end
GO
ALTER TABLE [dbo].[account] ENABLE TRIGGER [tg_del_acc]
GO
/****** Object:  Trigger [dbo].[tg_del_bill]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[tg_del_bill] on [dbo].[bill] instead of delete
as begin
	declare @id int
	declare @count int
	select @id = id from deleted
	select @count = count(id) from bill_info where bill = @id
	while @count > 0
	begin
		delete bill_info where bill = @id
		select @count = count(id) from bill_info where bill = @id
	end
	delete bill where id = @id
end
GO
ALTER TABLE [dbo].[bill] ENABLE TRIGGER [tg_del_bill]
GO
/****** Object:  Trigger [dbo].[tg_del_bill_info]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[tg_del_bill_info] on [dbo].[bill_info] instead of delete
as begin
	declare @id int
	declare @bill int
	select @id = id, @bill = bill from deleted
	declare @count int
	delete bill_info where id = @id
	select @count = count(id) from bill_info where bill = @bill
	if @count = 0
		delete bill where id = @bill
end
GO
ALTER TABLE [dbo].[bill_info] ENABLE TRIGGER [tg_del_bill_info]
GO
/****** Object:  Trigger [dbo].[tg_del_category]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[tg_del_category] on [dbo].[category] instead of delete
as begin
	declare @id int
	declare @count int
	select @id = id from deleted
	select @count = count(id) from product where category = @id
	while @count > 0
	begin
		delete product where category = @id
		select @count = count(id) from product where category = @id
	end
	delete category where id = @id
end
GO
ALTER TABLE [dbo].[category] ENABLE TRIGGER [tg_del_category]
GO
/****** Object:  Trigger [dbo].[tg_del_cus]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--trigger
create trigger [dbo].[tg_del_cus] on [dbo].[customer] instead of delete
as begin
	declare @username varchar(100)
	select @username = username from deleted
	declare @id int
	declare @count int
	select @id = id from deleted
	select @count = count(id) from contact where customer = @id
	while @count > 0
	begin
		delete contact where customer = @id
		select @count = count(id) from contact where customer = @id
	end
	select @count = count(id) from cart where customer = @id
	while @count > 0
	begin
		delete cart where customer = @id
		select @count = count(id) from cart where customer = @id
	end
	select @count = count(id) from bill where customer = @id
	while @count > 0
	begin
		delete bill where customer = @id
		select @count = count(id) from bill where customer = @id
	end
	delete customer where id = @id
	delete account where username = @username
end
GO
ALTER TABLE [dbo].[customer] ENABLE TRIGGER [tg_del_cus]
GO
/****** Object:  Trigger [dbo].[tg_del_product]    Script Date: 18/08/2024 9:23:11 CH ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create trigger [dbo].[tg_del_product] on [dbo].[product] instead of delete
as begin
	declare @id int
	declare @count int
	select @id = id from deleted
	select @count = count(id) from bill_info where product = @id
	while @count > 0
	begin
		delete bill_info where product = @id
		select @count = count(id) from bill_info where product = @id
	end
	select @count = count(id) from cart where product = @id
	while @count > 0
	begin
		delete cart where product = @id
		select @count = count(id) from cart where product = @id
	end
	delete product where id = @id
end
GO
ALTER TABLE [dbo].[product] ENABLE TRIGGER [tg_del_product]
GO
