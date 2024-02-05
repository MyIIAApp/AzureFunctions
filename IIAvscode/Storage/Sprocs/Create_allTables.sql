/****** Object:  Table [dbo].[Admin]    Script Date: 27-09-2021 21:19:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Admin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Email] [varchar](50) NULL,
	[Chapter] [int] NULL,
	[Designation] [varchar](50) NULL,
	[Roles] [varchar](50) NULL,
	[CreatedDate] [date] NOT NULL,
	[UpdatedDate] [date] NOT NULL,
	[ProfilePhoto] [varchar](50) NULL,
	[CreateNews] [int] NOT NULL,
	[ChaptersHelpdesk] [int] NOT NULL,
	[AllHelpdesk] [int] NOT NULL,
	[RecordChapterPayment] [int] NOT NULL,
	[RecordAllPayment] [int] NOT NULL,
	[ApproveChapterMembership] [int] NOT NULL,
	[ApproveAllMembership] [int] NOT NULL,
	[EditUserProfile] [int] NOT NULL,
	[CreateUserProfile] [int] NOT NULL,
	[ProfileState] [varchar](50) NOT NULL,
	[Position] [char](10) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Attachment]    Script Date: 27-09-2021 21:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Attachment](
	[TicketNumber] [int] NOT NULL,
	[UserId] [int] NULL,
	[AdminId] [int] NULL,
	[AttachmentURL] [varchar](max) NOT NULL,
	[AttachmentCreationTime] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Chapters]    Script Date: 27-09-2021 21:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Chapters](
	[Id] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_Chapters] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Comments]    Script Date: 27-09-2021 21:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Comments](
	[TicketNumber] [int] NOT NULL,
	[UserId] [int] NULL,
	[Comment] [varchar](max) NOT NULL,
	[CommentCreationTime] [datetime] NOT NULL,
	[AdminId] [int] NULL,
	[UserName] [nvarchar](50) NULL,
	[AdminName] [nvarchar](50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[News]    Script Date: 27-09-2021 21:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NULL,
	[Description] [varchar](max) NULL,
	[Link] [varchar](max) NULL,
	[ImagePath] [varchar](max) NULL,
	[Category] [varchar](50) NULL,
	[CreatorAdminId] [int] NULL,
	[CreationTime] [datetime] NULL,
	[ChapterId] [int] NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[OfferCategories]    Script Date: 27-09-2021 21:19:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[OfferCategories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [varchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Offers]    Script Date: 27-09-2021 21:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Offers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Category] [varchar](100) NOT NULL,
	[OffersSummary] [varchar](100) NULL,
	[PhoneNumber] [varchar](100) NULL,
	[EmailId] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Website] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[BulletPoint1] [varchar](100) NULL,
	[BulletPoint2] [varchar](100) NULL,
	[BulletPoint3] [varchar](100) NULL,
	[BulletPoint4] [varchar](100) NULL,
	[BulletPoint5] [varchar](100) NULL,
	[DocumentName] [varchar](100) NULL,
	[ExpiryDate] [datetime] NULL,
 CONSTRAINT [PK_Offers1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Payment]    Script Date: 27-09-2021 21:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Payment](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[AdminId] [int] NOT NULL,
	[SubTotal] [int] NULL,
	[IGSTPercent] [int] NULL,
	[CGSTPercent] [int] NULL,
	[SGSTPercent] [int] NULL,
	[IGSTValue] [int] NULL,
	[CGSTValue] [int] NULL,
	[SGSTValue] [int] NULL,
	[PaymentReason] [nvarchar](max) NULL,
	[PaymentMode] [nvarchar](50) NOT NULL,
	[ChequeNumber] [nvarchar](50) NULL,
	[OnlineTransactionId] [nvarchar](50) NULL,
	[OrderId] [nvarchar](50) NOT NULL,
	[DiscountPercent] [int] NULL,
	[DiscountValue] [int] NULL,
	[Total] [int] NOT NULL,
	[OnlineFees] [int] NULL,
	[HO_Share] [int] NULL,
	[ChapterShare] [int] NULL,
	[InvoicePath] [nvarchar](max) NULL,
	[CreateDateTimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 27-09-2021 21:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Tickets]    Script Date: 27-09-2021 21:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tickets](
	[TicketNumber] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](max) NOT NULL,
	[Description] [varchar](max) NOT NULL,
	[Category] [varchar](50) NOT NULL,
	[ChapterId] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[TicketCreationTime] [datetime] NOT NULL,
	[UserId] [int] NOT NULL,
	[ClosedTicketTime] [datetime] NULL,
 CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED 
(
	[TicketNumber] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[UserProfile]    Script Date: 27-09-2021 21:19:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserProfile](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](10) NOT NULL,
	[MembershipId] [varchar](50) NULL,
	[MembershipAdmissionfee] [int] NULL,
	[MembershipFees] [int] NULL,
	[MembershipCurrentExpiryYear] [int] NULL,
	[MembershipJoinDate] [datetime] NULL,
	[MembershipRenewDate] [datetime] NULL,
	[MembershipExpiryYears] [text] NULL,
	[ProfileStatus] [int] NOT NULL,
	[Chapter] [int] NULL,
	[UnitName] [varchar](50) NULL,
	[GSTIN] [varchar](50) NULL,
	[GSTcertpath] [varchar](200) NULL,
	[IndustryStatus] [varchar](50) NULL,
	[Address] [text] NULL,
	[District] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](50) NULL,
	[Country] [varchar](50) NULL,
	[Pincode] [varchar](50) NULL,
	[WebsiteUrl] [varchar](50) NULL,
	[ProductCategory] [varchar](50) NULL,
	[ProductSubCategory] [varchar](50) NULL,
	[MajorProducts] [text] NULL,
	[AnnualTurnOver] [varchar](50) NULL,
	[EnterpriseType] [varchar](50) NULL,
	[Exporter] [varchar](50) NULL,
	[Classification] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[email] [varchar](200) NULL,
	[ProfileImagePath] [varchar](200) NULL,
	[SignaturePath] [varchar](200) NULL,
	[FinancialProofPath] [varchar](200) NULL,
	[CreatedBy] [int] NULL,
	[UpdatedBy] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserProfile] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Admin] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[Admin] ADD  DEFAULT (getdate()) FOR [UpdatedDate]
GO

ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_CreationTime]  DEFAULT (getdate()) FOR [AttachmentCreationTime]
GO

ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_CreationTime]  DEFAULT (getdate()) FOR [CommentCreationTime]
GO

ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_Status]  DEFAULT ('Pending on IIA') FOR [Status]
GO

ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_CreationTime]  DEFAULT (getdate()) FOR [TicketCreationTime]
GO

ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_ClosedTicketTime]  DEFAULT (getdate()) FOR [ClosedTicketTime]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_MembershipAdmissionfee]  DEFAULT ((0)) FOR [MembershipAdmissionfee]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_MembershipFees]  DEFAULT ((0)) FOR [MembershipFees]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_MembershipCurrentExpiryYear]  DEFAULT ((0)) FOR [MembershipCurrentExpiryYear]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_MembershipJoinDate]  DEFAULT ('1970-01-01 00:00:00') FOR [MembershipJoinDate]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_MembershipRenewDate]  DEFAULT ('1970-01-01 00:00:00') FOR [MembershipRenewDate]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_ProfileStatus]  DEFAULT ((0)) FOR [ProfileStatus]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_Chapter]  DEFAULT ((-1)) FOR [Chapter]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_CreatedBy]  DEFAULT ((-2)) FOR [CreatedBy]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_UpdatedBy]  DEFAULT ((-2)) FOR [UpdatedBy]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[UserProfile] ADD  CONSTRAINT [DF_UserProfile_UpdatedDate]  DEFAULT (getdate()) FOR [UpdatedDate]
GO

ALTER TABLE [dbo].[Attachment]  WITH CHECK ADD  CONSTRAINT [FK_Attachment_Tickets] FOREIGN KEY([TicketNumber])
REFERENCES [dbo].[Tickets] ([TicketNumber])
GO

ALTER TABLE [dbo].[Attachment] CHECK CONSTRAINT [FK_Attachment_Tickets]
GO

ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_Comments_Tickets] FOREIGN KEY([TicketNumber])
REFERENCES [dbo].[Tickets] ([TicketNumber])
GO

ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_Comments_Tickets]
GO

ALTER TABLE [dbo].[News]  WITH CHECK ADD  CONSTRAINT [FK_News_Admin] FOREIGN KEY([Id])
REFERENCES [dbo].[Admin] ([Id])
GO

ALTER TABLE [dbo].[News] CHECK CONSTRAINT [FK_News_Admin]
GO


