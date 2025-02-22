USE [Banka]
GO
/****** Object:  StoredProcedure [dbo].[CalısanSube]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[CalısanSube](@calısanınNumarası INT)
AS
BEGIN
    DECLARE @calısanNo INT,
            @adı VARCHAR(255),
            @soyadı VARCHAR(255),
            @subeNumarası INT,
            @subeAdı VARCHAR(255),
            @subeTel VARCHAR(20);

    SELECT @calısanNo = calısanno,
           @adı = calısanadı,
           @soyadı = calısansoyadı,
           @subeNumarası = c.subeno,
           @subeAdı = subeadi,
           @subeTel = subetel
    FROM Calısanlar c
    INNER JOIN Subeler s ON s.subeno = c.subeno
    WHERE c.calısanno = @calısanınNumarası;


	SELECT @calısanNo AS calısanNo,
           @adı AS adı,
           @soyadı AS soyadı,
           @subeNumarası AS subeNumarası,
           @subeAdı AS subeAdı,
           @subeTel AS subeTel;
END

GO
/****** Object:  StoredProcedure [dbo].[Havale]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Havale]
(
    @GonderenHesapNo int,
    @AlanHesapNo int,
    @tutar decimal(18,2)
)
AS
BEGIN
    BEGIN TRANSACTION Kontrol;

    DECLARE @GonderenBakiye decimal(18,2), @AlanBakiye decimal(18,2);

    -- Gönderen hesabın varlığını kontrol et
    IF NOT EXISTS (SELECT 1 FROM Hesaplar WHERE hesapno = @GonderenHesapNo)
    BEGIN
        THROW 50000, 'Gönderen hesap bulunamadı.', 1;
    END;

    -- Alıcı hesabın varlığını kontrol et
    IF NOT EXISTS (SELECT 1 FROM Hesaplar WHERE hesapno = @AlanHesapNo)
    BEGIN
        THROW 50000, 'Alıcı hesap bulunamadı.', 1;
    END;

    -- Gönderen hesaptaki bakiyeyi al
    SELECT @GonderenBakiye = bakiye
    FROM Hesaplar
    WHERE hesapno = @GonderenHesapNo;

    -- Gönderen hesapta yeterli bakiye yoksa işlemi iptal et
    IF @tutar > @GonderenBakiye
    BEGIN
        THROW 50001, 'Gönderen hesapta yeterli bakiye yok.', 1;
    END;

    -- Alıcı hesaptaki bakiyeyi al
    SELECT @AlanBakiye = bakiye
    FROM Hesaplar
    WHERE hesapno = @AlanHesapNo;

    -- Gönderen ve alıcı hesapları güncelle
    UPDATE Hesaplar SET bakiye = bakiye - @tutar WHERE hesapno = @GonderenHesapNo;
    UPDATE Hesaplar SET bakiye = bakiye + @tutar WHERE hesapno = @AlanHesapNo;

    -- İşlemi tamamla
    COMMIT TRANSACTION Kontrol;

    -- İşlemi Islemler tablosuna ekle
    INSERT INTO Islemler (musterino, islemtarihi, islemtur, tutar)
    VALUES (@GonderenHesapNo, GETDATE(), 'Havale', @tutar);
END;
GO
/****** Object:  StoredProcedure [dbo].[KrediKartiBorcuOde]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[KrediKartiBorcuOde]
(
    @musterino int,
    @borcMiktari decimal(18,2)
)
AS
BEGIN
    BEGIN TRANSACTION;

    DECLARE @krediKartLimit decimal(18,2);
    DECLARE @kumuleverecekMiktar decimal(18,2);

   
    UPDATE Hesaplar 
    SET kumuleverecek = kumuleverecek - @borcMiktari 
    WHERE musterino = @musterino;

  
    SELECT @kumuleverecekMiktar = kumuleverecek
    FROM Hesaplar
    WHERE musterino = @musterino;

   
    SELECT @krediKartLimit = limit
    FROM KrediKartı
    WHERE musterino = @musterino;

   
    UPDATE KrediKartı
    SET limit = limit + @borcMiktari
    WHERE musterino = @musterino;

     UPDATE Hesaplar
	SET bakiye=bakiye - @borcMiktari
	where musterino = @musterino;

   
    COMMIT TRANSACTION;
END;


GO
/****** Object:  UserDefinedFunction [dbo].[ToplamBakiyeGetir]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ToplamBakiyeGetir]
(
    @musterino1 INT,
    @musterino2 INT
)
RETURNS DECIMAL(18,2)
AS
BEGIN
    DECLARE @toplamBakiye DECIMAL(18,2)

    SELECT @toplamBakiye = SUM(bakiye)
    FROM Hesaplar
    WHERE musterino IN (@musterino1, @musterino2)

    RETURN @toplamBakiye
END;
GO
/****** Object:  Table [dbo].[BankaKartBasvurulari]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankaKartBasvurulari](
	[BasvuruId] [int] IDENTITY(1,1) NOT NULL,
	[MusteriNo] [int] NOT NULL,
	[AdSoyad] [nvarchar](100) NOT NULL,
	[DogumTarihi] [date] NOT NULL,
	[Cinsiyet] [nvarchar](1) NOT NULL,
	[TelefonNo] [nvarchar](15) NOT NULL,
	[Adres] [nvarchar](max) NOT NULL,
	[BasvuruTarihi] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[BasvuruId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankaKartı]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BankaKartı](
	[kartno] [int] IDENTITY(1,1) NOT NULL,
	[musterino] [int] NULL,
	[kartsahibiadi] [varchar](50) NULL,
	[skt] [date] NULL,
	[cvv] [nvarchar](3) NULL,
	[karttur] [varchar](16) NULL,
	[vadetarihi] [date] NULL,
	[Kartnumarası] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[kartno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Calisanlar]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Calisanlar](
	[calisanno] [int] IDENTITY(1,1) NOT NULL,
	[calisanadi] [varchar](50) NULL,
	[calisansoyadi] [varchar](50) NULL,
	[subeno] [int] NULL,
	[sifre] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[calisanno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Hesaplar]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Hesaplar](
	[hesapno] [int] IDENTITY(1,1) NOT NULL,
	[musterino] [int] NULL,
	[bakiye] [decimal](18, 2) NULL,
	[hesaptur] [varchar](50) NULL,
	[kumulealacak] [decimal](18, 2) NULL DEFAULT ((0)),
	[kumuleverecek] [decimal](18, 2) NULL DEFAULT ((0)),
PRIMARY KEY CLUSTERED 
(
	[hesapno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Islemler]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Islemler](
	[islemno] [int] IDENTITY(1,1) NOT NULL,
	[musterino] [int] NULL,
	[islemtarihi] [datetime] NULL,
	[islemtur] [varchar](50) NULL,
	[tutar] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[islemno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[KrediKartı]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[KrediKartı](
	[kartno] [int] IDENTITY(1,1) NOT NULL,
	[musterino] [int] NULL,
	[kartsahibiadi] [varchar](50) NULL,
	[skt] [date] NULL,
	[cvv] [varchar](4) NULL,
	[limit] [decimal](18, 2) NULL,
	[karttur] [varchar](16) NULL,
	[vadetarihi] [date] NULL,
	[Kartnumarası] [varchar](50) NULL,
	[MaxLimit] [decimal](18, 2) NULL,
PRIMARY KEY CLUSTERED 
(
	[kartno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Musteriler]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Musteriler](
	[musterino] [int] IDENTITY(1,1) NOT NULL,
	[isim] [varchar](50) NULL,
	[soyisim] [varchar](50) NULL,
	[dogumtarihi] [date] NULL,
	[cinsiyet] [char](1) NULL,
	[telno] [varchar](50) NULL,
	[sifre] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[musterino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Personeller]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Personeller](
	[personelno] [int] IDENTITY(1,1) NOT NULL,
	[personelad] [varchar](50) NULL,
	[personelsoyad] [varchar](50) NULL,
	[unvanno] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[personelno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SilinenMusteriler]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SilinenMusteriler](
	[musterino] [int] NULL,
	[isim] [varchar](50) NULL,
	[soyisim] [varchar](50) NULL,
	[dogumtarihi] [date] NULL,
	[cinsiyet] [char](1) NULL,
	[telno] [varchar](50) NULL,
	[silinme_tarihi] [datetime] NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Subeler]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Subeler](
	[subeno] [int] IDENTITY(1,1) NOT NULL,
	[subeadi] [varchar](50) NULL,
	[subetel] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[subeno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Transfer]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transfer](
	[transferno] [int] IDENTITY(1,1) NOT NULL,
	[musterino] [int] NULL,
	[alicihesapno] [int] NULL,
	[tutar] [decimal](18, 2) NULL,
	[transfertarihi] [datetime] NULL,
	[durum] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[transferno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Unvan]    Script Date: 29.01.2025 17:08:33 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Unvan](
	[unvanno] [int] NOT NULL,
	[unvanadi] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[unvanno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[BankaKartı] ON 

INSERT [dbo].[BankaKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [karttur], [vadetarihi], [Kartnumarası]) VALUES (1, 1, N'Kerem Erden', CAST(N'2028-05-22' AS Date), N'698', N'Mastercard', CAST(N'2025-08-22' AS Date), N'4344283324242424')
INSERT [dbo].[BankaKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [karttur], [vadetarihi], [Kartnumarası]) VALUES (2, 2, N'Bahadır Elibol', CAST(N'2029-06-01' AS Date), N'485', N'Visa', CAST(N'2025-04-12' AS Date), N'4642270254685362')
INSERT [dbo].[BankaKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [karttur], [vadetarihi], [Kartnumarası]) VALUES (3, 3, N'Alperen Çerçi', CAST(N'2028-02-21' AS Date), N'712', N'Mastercard', CAST(N'2025-07-11' AS Date), N'4257894589435785')
INSERT [dbo].[BankaKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [karttur], [vadetarihi], [Kartnumarası]) VALUES (4, 4, N'Akif Emre Bayrak', CAST(N'2029-03-28' AS Date), N'651', N'Mastercard', CAST(N'2025-01-13' AS Date), N'4064156726272362')
SET IDENTITY_INSERT [dbo].[BankaKartı] OFF
SET IDENTITY_INSERT [dbo].[Calisanlar] ON 

INSERT [dbo].[Calisanlar] ([calisanno], [calisanadi], [calisansoyadi], [subeno], [sifre]) VALUES (1, N'Kerem', N'Erden', 3, N'123')
INSERT [dbo].[Calisanlar] ([calisanno], [calisanadi], [calisansoyadi], [subeno], [sifre]) VALUES (2, N'admin', N'admin', 2, N'123')
SET IDENTITY_INSERT [dbo].[Calisanlar] OFF
SET IDENTITY_INSERT [dbo].[Hesaplar] ON 

INSERT [dbo].[Hesaplar] ([hesapno], [musterino], [bakiye], [hesaptur], [kumulealacak], [kumuleverecek]) VALUES (1, 1, CAST(22382.00 AS Decimal(18, 2)), N'Cari', CAST(0.00 AS Decimal(18, 2)), CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Hesaplar] ([hesapno], [musterino], [bakiye], [hesaptur], [kumulealacak], [kumuleverecek]) VALUES (2, 2, CAST(22118.00 AS Decimal(18, 2)), N'Cari', CAST(0.00 AS Decimal(18, 2)), CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Hesaplar] ([hesapno], [musterino], [bakiye], [hesaptur], [kumulealacak], [kumuleverecek]) VALUES (3, 3, CAST(15000.00 AS Decimal(18, 2)), N'Cari', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
INSERT [dbo].[Hesaplar] ([hesapno], [musterino], [bakiye], [hesaptur], [kumulealacak], [kumuleverecek]) VALUES (4, 4, CAST(10000.00 AS Decimal(18, 2)), N'Cari', CAST(0.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Hesaplar] OFF
SET IDENTITY_INSERT [dbo].[Islemler] ON 

INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (3, 2, CAST(N'2024-04-12 21:10:58.770' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (4, 1, CAST(N'2024-04-12 21:11:29.363' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (5, 1, CAST(N'2024-04-12 21:50:39.577' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (6, 1, CAST(N'2024-04-12 21:50:39.583' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (7, 1, CAST(N'2024-04-12 21:51:06.837' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (8, 1, CAST(N'2024-04-12 21:51:06.843' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (9, 1, CAST(N'2024-04-12 21:51:35.183' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (10, 1, CAST(N'2024-04-12 21:51:35.190' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (11, 1, CAST(N'2024-04-12 21:52:46.713' AS DateTime), N'Havale', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (12, 1, CAST(N'2024-04-12 21:52:46.717' AS DateTime), N'Havale', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (13, 1, CAST(N'2024-04-12 21:52:52.637' AS DateTime), N'Havale', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (14, 1, CAST(N'2024-04-12 21:52:52.640' AS DateTime), N'Havale', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (15, 1, CAST(N'2024-04-12 21:53:15.747' AS DateTime), N'Havale', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (16, 1, CAST(N'2024-04-12 21:53:15.750' AS DateTime), N'Havale', CAST(2000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (17, 2, CAST(N'2024-04-12 21:54:29.367' AS DateTime), N'Havale', CAST(6000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (18, 2, CAST(N'2024-04-12 21:54:39.987' AS DateTime), N'Havale', CAST(1.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (19, 2, CAST(N'2024-04-12 21:54:44.743' AS DateTime), N'Havale', CAST(999.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (20, 2, CAST(N'2024-04-12 21:55:22.113' AS DateTime), N'Havale', CAST(1000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (21, 1, CAST(N'2024-04-12 21:55:37.587' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (22, 2, CAST(N'2024-04-12 21:55:42.653' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (23, 1, CAST(N'2024-04-20 18:55:18.750' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (24, 2, CAST(N'2024-04-20 18:55:28.673' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (25, 1, CAST(N'2024-04-26 15:36:10.447' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (26, 2, CAST(N'2024-04-26 15:36:19.853' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (27, 1, CAST(N'2024-05-01 21:13:08.447' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (28, 2, CAST(N'2024-05-01 21:13:20.383' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (29, 2, CAST(N'2024-05-02 14:36:52.547' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (30, 1, CAST(N'2025-01-16 23:39:18.577' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (31, 2, CAST(N'2025-01-16 23:39:42.830' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (32, 1, CAST(N'2025-01-16 23:57:47.267' AS DateTime), N'Havale', CAST(500.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (33, 1, CAST(N'2025-01-16 23:58:32.503' AS DateTime), N'Havale', CAST(22.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (34, 1, CAST(N'2025-01-16 23:58:44.453' AS DateTime), N'Havale', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (35, 1, CAST(N'2025-01-17 00:05:07.657' AS DateTime), N'Havale', CAST(1.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (36, 2, CAST(N'2025-01-17 00:06:25.827' AS DateTime), N'Havale', CAST(15530.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (37, 1, CAST(N'2025-01-17 00:18:16.010' AS DateTime), N'Havale', CAST(1.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (38, 1, CAST(N'2025-01-17 00:28:16.927' AS DateTime), N'Havale', CAST(21.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (39, 1, CAST(N'2025-01-17 00:42:08.420' AS DateTime), N'Havale', CAST(11.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (40, 1, CAST(N'2025-01-17 01:02:08.080' AS DateTime), N'Havale', CAST(50.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (41, 1, CAST(N'2025-01-18 21:52:02.660' AS DateTime), N'Havale', CAST(1.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (42, 1, CAST(N'2025-01-18 21:52:07.800' AS DateTime), N'Havale', CAST(12.00 AS Decimal(18, 2)))
INSERT [dbo].[Islemler] ([islemno], [musterino], [islemtarihi], [islemtur], [tutar]) VALUES (43, 1, CAST(N'2025-01-18 22:48:33.303' AS DateTime), N'Havale', CAST(22.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[Islemler] OFF
SET IDENTITY_INSERT [dbo].[KrediKartı] ON 

INSERT [dbo].[KrediKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [limit], [karttur], [vadetarihi], [Kartnumarası], [MaxLimit]) VALUES (1, 1, N'Kerem Erden', CAST(N'2028-05-22' AS Date), N'698', CAST(9500.00 AS Decimal(18, 2)), N'Mastercard', CAST(N'2025-08-22' AS Date), N'4344283324242424', CAST(10000.00 AS Decimal(18, 2)))
INSERT [dbo].[KrediKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [limit], [karttur], [vadetarihi], [Kartnumarası], [MaxLimit]) VALUES (2, 2, N'Bahadır Elibol', CAST(N'2029-06-01' AS Date), N'485', CAST(7000.00 AS Decimal(18, 2)), N'Visa', CAST(N'2025-04-12' AS Date), N'4642270254685362', CAST(8000.00 AS Decimal(18, 2)))
INSERT [dbo].[KrediKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [limit], [karttur], [vadetarihi], [Kartnumarası], [MaxLimit]) VALUES (3, 3, N'Alperen Çerçi', CAST(N'2028-02-21' AS Date), N'712', CAST(8500.00 AS Decimal(18, 2)), N'Mastercard', CAST(N'2025-07-11' AS Date), N'4257894589435785', CAST(8500.00 AS Decimal(18, 2)))
INSERT [dbo].[KrediKartı] ([kartno], [musterino], [kartsahibiadi], [skt], [cvv], [limit], [karttur], [vadetarihi], [Kartnumarası], [MaxLimit]) VALUES (4, 4, N'Akif Emre Bayrak', CAST(N'2029-03-28' AS Date), N'651', CAST(10000.00 AS Decimal(18, 2)), N'Mastercard', CAST(N'2025-01-13' AS Date), N'4064156726272362', CAST(10000.00 AS Decimal(18, 2)))
SET IDENTITY_INSERT [dbo].[KrediKartı] OFF
SET IDENTITY_INSERT [dbo].[Musteriler] ON 

INSERT [dbo].[Musteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [sifre]) VALUES (1, N'Kerem', N'Erden', CAST(N'2003-05-23' AS Date), N'E', N'5539242424', N'123')
INSERT [dbo].[Musteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [sifre]) VALUES (2, N'Bahadır', N'Elibol', CAST(N'2001-06-15' AS Date), N'E', N'5447892424', N'123')
INSERT [dbo].[Musteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [sifre]) VALUES (3, N'Alperen', N'Çerçi', CAST(N'2003-01-12' AS Date), N'E', N'5536872424', N'123')
INSERT [dbo].[Musteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [sifre]) VALUES (4, N'Akif Emre', N'Bayrak', CAST(N'2003-11-29' AS Date), N'E', N'5536892424', N'123')
INSERT [dbo].[Musteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [sifre]) VALUES (21, N'admin', N'admin', CAST(N'2025-01-15' AS Date), N'E', N'1234567891', N'123')
SET IDENTITY_INSERT [dbo].[Musteriler] OFF
SET IDENTITY_INSERT [dbo].[Personeller] ON 

INSERT [dbo].[Personeller] ([personelno], [personelad], [personelsoyad], [unvanno]) VALUES (1, N'Ahmet', N'Ekinci', 2)
INSERT [dbo].[Personeller] ([personelno], [personelad], [personelsoyad], [unvanno]) VALUES (2, N'Figen', N'Esmer', 5)
INSERT [dbo].[Personeller] ([personelno], [personelad], [personelsoyad], [unvanno]) VALUES (3, N'Emir', N'Yanık', 4)
SET IDENTITY_INSERT [dbo].[Personeller] OFF
INSERT [dbo].[SilinenMusteriler] ([musterino], [isim], [soyisim], [dogumtarihi], [cinsiyet], [telno], [silinme_tarihi]) VALUES (22, N'Deneme Üyesi', N'Deneme Üyesi', CAST(N'2025-01-29' AS Date), N'E', N'1234567899', CAST(N'2025-01-29 16:53:18.343' AS DateTime))
SET IDENTITY_INSERT [dbo].[Subeler] ON 

INSERT [dbo].[Subeler] ([subeno], [subeadi], [subetel]) VALUES (1, N'Tokat', N'4441444')
INSERT [dbo].[Subeler] ([subeno], [subeadi], [subetel]) VALUES (2, N'Edirne', N'4442444')
INSERT [dbo].[Subeler] ([subeno], [subeadi], [subetel]) VALUES (3, N'İstanbul', N'4443444')
INSERT [dbo].[Subeler] ([subeno], [subeadi], [subetel]) VALUES (4, N'Ankara', N'4444444')
INSERT [dbo].[Subeler] ([subeno], [subeadi], [subetel]) VALUES (5, N'Balıkesir', N'4445444')
SET IDENTITY_INSERT [dbo].[Subeler] OFF
INSERT [dbo].[Unvan] ([unvanno], [unvanadi]) VALUES (1, N'Müdür')
INSERT [dbo].[Unvan] ([unvanno], [unvanadi]) VALUES (2, N'Gişe Görevlisi')
INSERT [dbo].[Unvan] ([unvanno], [unvanadi]) VALUES (3, N'Satış Danışmanı')
INSERT [dbo].[Unvan] ([unvanno], [unvanadi]) VALUES (4, N'Müşteri Temsilcisi')
INSERT [dbo].[Unvan] ([unvanno], [unvanadi]) VALUES (5, N'Banka Müfettişi')
ALTER TABLE [dbo].[BankaKartBasvurulari] ADD  DEFAULT (getdate()) FOR [BasvuruTarihi]
GO
ALTER TABLE [dbo].[BankaKartı]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[BankaKartı]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Calisanlar]  WITH CHECK ADD FOREIGN KEY([subeno])
REFERENCES [dbo].[Subeler] ([subeno])
GO
ALTER TABLE [dbo].[Calisanlar]  WITH CHECK ADD FOREIGN KEY([subeno])
REFERENCES [dbo].[Subeler] ([subeno])
GO
ALTER TABLE [dbo].[Hesaplar]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Hesaplar]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Islemler]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Islemler]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[KrediKartı]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[KrediKartı]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Personeller]  WITH CHECK ADD FOREIGN KEY([unvanno])
REFERENCES [dbo].[Unvan] ([unvanno])
GO
ALTER TABLE [dbo].[Personeller]  WITH CHECK ADD FOREIGN KEY([unvanno])
REFERENCES [dbo].[Unvan] ([unvanno])
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
ALTER TABLE [dbo].[Transfer]  WITH CHECK ADD FOREIGN KEY([musterino])
REFERENCES [dbo].[Musteriler] ([musterino])
GO
