/****** Object:  Table [dbo].[Admin]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[Attachment]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[Chapters]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[Comments]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[News]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[OfferCategories]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[Offers]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Offers](
	[SNo] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[OffersSummary] [varchar](max) NULL,
	[PhoneNumber] [varchar](max) NULL,
	[EmailId] [varchar](100) NULL,
	[Address] [varchar](max) NULL,
	[Website] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[Category] [varchar](100) NOT NULL,
	[BulletPoint1] [varchar](max) NULL,
	[BulletPoint2] [varchar](max) NULL,
	[BulletPoint3] [varchar](max) NULL,
	[BulletPoint4] [varchar](max) NULL,
	[BulletPoint5] [varchar](max) NULL,
	[DocumentName] [varchar](max) NULL,
	[ProspectusPath] [varchar](max) NULL,
 CONSTRAINT [PK_Offers1] PRIMARY KEY CLUSTERED 
(
	[SNo] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payment]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[roleId] [int] IDENTITY(1,1) NOT NULL,
	[roleName] [varchar](50) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tickets]    Script Date: 20-10-2021 15:32:10 ******/
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
/****** Object:  Table [dbo].[UserProfile]    Script Date: 20-10-2021 15:32:10 ******/
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
ALTER TABLE [dbo].[Attachment] ADD  CONSTRAINT [DF_Attachment_CreationTime]  DEFAULT (dateadd(minute,(330),getdate())) FOR [AttachmentCreationTime]
GO
ALTER TABLE [dbo].[Comments] ADD  CONSTRAINT [DF_Comments_CreationTime]  DEFAULT (dateadd(minute,(330),getdate())) FOR [CommentCreationTime]
GO
ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_Status]  DEFAULT ('Pending on IIA') FOR [Status]
GO
ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_CreationTime]  DEFAULT (dateadd(minute,(330),getdate())) FOR [TicketCreationTime]
GO
ALTER TABLE [dbo].[Tickets] ADD  CONSTRAINT [DF_Tickets_ClosedTicketTime]  DEFAULT (dateadd(minute,(330),getdate())) FOR [ClosedTicketTime]
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
/****** Object:  StoredProcedure [dbo].[Admin_CheckIfAdminExists]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Admin_CheckIfAdminExists]
( 
    @PhoneNumber nvarchar(50) 
) 
AS
BEGIN
	Select Id FROM Admin Where PhoneNumber = @PhoneNumber AND ProfileState = 'Enabled'
END 
GO
/****** Object:  StoredProcedure [dbo].[Admin_CheckRoles]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Admin_CheckRoles]
( 
    @Id int 
) 
AS
BEGIN 
	Select CreateNews, ChaptersHelpdesk, AllHelpdesk, RecordAllPayment, RecordChapterPayment, ApproveAllMembership, ApproveChapterMembership, EditUserProfile, CreateUserProfile FROM Admin Where Id = @Id
END 
GO
/****** Object:  StoredProcedure [dbo].[Chapters_GetChapters]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Chapters_GetChapters]
AS
BEGIN
       SELECT * FROM Chapters
END
GO
/****** Object:  StoredProcedure [dbo].[IIA_GetLeaderDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[IIA_GetLeaderDetails]
(
    @ChapterId int
)
AS
BEGIN
		BEGIN
			SELECT A.Name, A.Designation, C.Name, A.PhoneNumber, A.Email, A.ProfilePhoto, C.Id FROM Admin as A 
			Left Join Chapters C on C.Id = A.Chapter
			WHERE Chapter in (@ChapterId,82) AND A.Position = 'Leader' AND A.ProfileState = 'Enabled'
		END
END  
GO
/****** Object:  StoredProcedure [dbo].[News_CreateNews]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[News_CreateNews]
(
    @Title varchar(MAX),
	@Description varchar(MAX),
	@Link varchar(MAX),
	@ImagePath varchar(MAX),
	@Category varchar(50),
	@CreationTime DateTime,
	@CreatorAdminId int
)
AS
DECLARE @ChapterId int
BEGIN
	SET @ChapterId = (SELECT Admin.Chapter from Admin where Admin.Id = @CreatorAdminId)
    INSERT INTO News (Title, Description, Link, ImagePath, CreationTime, Category, CreatorAdminId, ChapterId)
    VALUES (@Title, @Description, @Link, @ImagePath, @CreationTime, @Category, @CreatorAdminId, @ChapterId)
END
GO
/****** Object:  StoredProcedure [dbo].[News_GetNewsForAllCategory]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[News_GetNewsForAllCategory]
AS
BEGIN
    Select N.Id,N.Title,N.Description,N.Link,N.ImagePath,N.Category,N.CreatorAdminId,N.CreationTime,C.Name from News as N
	join Chapters as C on C.Id=N.ChapterId  order by N.Id desc
END
GO
/****** Object:  StoredProcedure [dbo].[News_GetNewsForSpecificCategory]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[News_GetNewsForSpecificCategory]
(
	@Category varchar(50)
)
AS
BEGIN
       Select N.Id,N.Title,N.Description,N.Link,N.ImagePath,N.Category,N.CreatorAdminId,N.CreationTime,C.Name from News as N 
	join Chapters as C on C.Id=N.ChapterId
END
GO
/****** Object:  StoredProcedure [dbo].[Offers_GetOffer]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Offers_GetOffer]
(
	@SNo int
)
AS
BEGIN
    Select * FROM Offers WHERE SNo = @SNo
END
GO
/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForOtherCategory]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Offers_GetOffersForOtherCategory]
AS
BEGIN
    Select * FROM Offers WHERE Category != 'Healthcare' AND Category != 'IT Solution & Electronics' AND Category != 'Health Insurance' AND Category != 'Hotels and Restaurants' AND Category != 'Automobiles' AND Category != 'Steel Pallet & Material Handling' AND Category != 'Utensils & Kitchen Items' AND Category != 'COVID-19 Related Items' 
END
GO
/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForSpecificCategory]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[Offers_GetOffersForSpecificCategory]
(
	@Category varchar(50)
)
AS
BEGIN
    Select * FROM Offers WHERE Category = @Category
END
GO
/****** Object:  StoredProcedure [dbo].[Payment_GetInvoiceId]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Payment_GetInvoiceId]
(
	@orderId varchar(50)
)
AS
BEGIN
    Select InvoiceId from Payment where OrderId=@orderId
END
GO
/****** Object:  StoredProcedure [dbo].[Payment_InsertDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Payment_InsertDetails] 

( 

    @UserId int,
	@AdminId int,
	@SubTotal int,
	@IGSTPercent int,
	@CGSTPercent int,
	@SGSTPercent int,
	@IGSTValue int,
	@CGSTValue int,
	@SGSTValue int,
	@PaymentReason varchar(MAX),
	@PaymentMode varchar(50),
	@ChequeNumber varchar(50),
	@OnlineTransactionId varchar(50),
	@OrderId varchar(50),
	@DiscountPercent int,
	@DiscountValue int,
	@Total int,
	@OnlineFees int,
	@HO_Share int,
	@ChapterShare int,
	@InvoicePath varchar(MAX),
	@CreateDateTimeStamp datetime
) 

AS 

BEGIN 

 INSERT INTO Payment(UserId, AdminId, SubTotal, IGSTPercent, CGSTPercent, SGSTPercent, IGSTValue, CGSTValue, SGSTValue, PaymentReason, PaymentMode, ChequeNumber, OnlineTransactionId, OrderId, DiscountPercent, DiscountValue, Total, OnlineFees, HO_Share, ChapterShare, InvoicePath, CreateDateTimeStamp)
       VALUES (@UserId, @AdminId, @SubTotal, @IGSTPercent, @CGSTPercent, @SGSTPercent, @IGSTValue, @CGSTValue, @SGSTValue, @PaymentReason, @PaymentMode, @ChequeNumber, @OnlineTransactionId, @OrderId, @DiscountPercent, @DiscountValue, @Total, @OnlineFees, @HO_Share, @ChapterShare, @InvoicePath, @CreateDateTimeStamp)
	   
END 

GO
/****** Object:  StoredProcedure [dbo].[Payment_InsertInvoicePath]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Payment_InsertInvoicePath]
(
	@invoicePath varchar(MAX),
	@orderId varchar(50)
)
AS
BEGIN
	Update Payment SET InvoicePath = @invoicePath where OrderId=@orderId
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_AddAttachment]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_AddAttachment]
(
    @TicketNumber int,
	@UserId int = -1,
	@AdminId int = -1,
	@AttachmentURL varchar(MAX)
)
AS
BEGIN 
	   INSERT INTO Attachment(TicketNumber,UserId,AdminId,AttachmentURL) 
	   VALUES (@TicketNumber, @UserId, @AdminId, @AttachmentURL)
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_ChangeChapter]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_ChangeChapter]
(
    @TicketNumber int
)
AS
BEGIN 
	   UPDATE Tickets
	   SET ChapterId = 82
	   WHERE TicketNumber = @TicketNumber
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_ChangeStatus]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_ChangeStatus]
(
    @TicketNumber int,
	@Status varchar(50)
)
AS
BEGIN 
	   UPDATE Tickets
	   SET Status = @Status
	   WHERE  TicketNumber = @TicketNumber
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_CloseTicket]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_CloseTicket]
(
    @TicketNumber int
)
AS
BEGIN 
	   UPDATE Tickets
	   SET Status = 'Closed', ClosedTicketTime = getDate()
	   WHERE  TicketNumber = @TicketNumber
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForAllChapters]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_GetSummaryForAllChapters]
AS
BEGIN
       SELECT * FROM Tickets
	   WHERE ChapterId <> 82
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForChapters]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_GetSummaryForChapters]
( 
	@id int
) 
AS
BEGIN
       SELECT * FROM Tickets
	   WHERE ChapterId = (SELECT Chapter from Admin WHERE Id = @id)
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForUsers]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_GetSummaryForUsers]
( 
    @UserId int 
) 
AS
BEGIN
	   SELECT * FROM Tickets 
	   WHERE UserId = @UserId
END
GO
/****** Object:  StoredProcedure [dbo].[Tickets_GetTicketDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_GetTicketDetails]
( 
    @TicketNumber int 
) 
AS
BEGIN
       SELECT *
	   FROM Tickets
	   WHERE TicketNumber = @TicketNumber
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Company_UpdateDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_Company_UpdateDetails]

( 

    @Id int,
	@ProfileStatus int,
	@Chapter int,
	@UnitName varchar(50),	
	@GSTIN varchar(50),
	@GSTcertpath varchar(200),
	@IndustryStatus varchar(50),
	@Address text,
	@District varchar(50),
	@City varchar(50),
	@State varchar(50),
	@Country varchar(50),
	@Pincode varchar(50),
	@WebsiteUrl varchar(50),
	@ProductCategory varchar(50),
	@ProductSubCategory varchar(50),
	@MajorProducts text,
	@AnnualTurnOver varchar(50),
	@EnterpriseType varchar(50),
	@Exporter varchar(50),
	@Classification varchar(50),
	@ProfileImagePath varchar(200),
	@FinancialProofPath varchar(200),
	@SignaturePath varchar(200),
	@UpdatedBy int,
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(50),
	@UpdatedDate datetime

) 

AS 

 
IF EXISTS (SELECT * FROM UserProfile  WHERE  Id = @Id ) 
	BEGIN
       UPDATE UserProfile  SET 
	   ProfileStatus = @ProfileStatus,
	   Chapter = @Chapter,
	   UnitName = @UnitName,
	   GSTIN = @GSTIN,
	   GSTcertpath = @GSTcertpath,
	   IndustryStatus = @IndustryStatus,
	   Address = @Address,
	   District = @District,
	   City = @City,
	   State = @State,
	   Country = @Country,
	   Pincode = @Pincode,
	   WebsiteUrl = @WebsiteUrl, 
	   ProductCategory = @ProductCategory,
	   ProductSubCategory = @ProductSubCategory,
	   MajorProducts = @MajorProducts,
	   AnnualTurnOver = @AnnualTurnOver,
	   EnterpriseType = @EnterpriseType,
	   Exporter = @Exporter,
	   Classification = @Classification,
	   FirstName = @FirstName,
	   LastName = @LastName,
	   Email = @Email,
	   ProfileImagePath = @ProfileImagePath,
	   FinancialProofPath = @FinancialProofPath,
	   SignaturePath = @SignaturePath,
	   UpdatedBy = @UpdatedBy,
	   UpdatedDate = @UpdatedDate
	   where Id = @Id
   END 
ELSE
	BEGIN
		RAISERROR (15600,-1,-1,'Profile Details Not Found')
	END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_CreateUserIfNotExists]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[UserProfile_CreateUserIfNotExists]
(
    @PhoneNumber varchar(10)
)
AS
BEGIN
   IF NOT EXISTS (SELECT Id FROM UserProfile
                   WHERE PhoneNumber = @PhoneNumber)
   BEGIN
       INSERT INTO UserProfile (PhoneNumber, ProfileStatus) Output Inserted.Id
       VALUES (@PhoneNumber, 0)
   END
   SELECT Id FROM UserProfile WHERE PhoneNumber = @PhoneNumber
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_GetDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_GetDetails]

( 
	@Id int,
    @Phonenumber varchar(10),
	@MembershipId varchar(50)

) 

AS 

IF @Phonenumber != ''

BEGIN 
  SELECT m.id, m.PhoneNumber, m.MembershipId, m.MembershipAdmissionfee, m.MembershipFees, m.MembershipCurrentExpiryYear, m.MembershipJoinDate, m.MembershipRenewDate, m.MembershipExpiryYears,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath, m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.UpdatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.PhoneNumber = @Phonenumber
END 
ELSE IF @MembershipId != ''
BEGIN
	SELECT m.id, m.PhoneNumber, m.MembershipId, m.MembershipAdmissionfee, m.MembershipFees, m.MembershipCurrentExpiryYear, m.MembershipJoinDate, m.MembershipRenewDate, m.MembershipExpiryYears,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath, m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.UpdatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.MembershipId = @MembershipId
END 
ELSE
BEGIN
	SELECT m.id, m.PhoneNumber, m.MembershipId, m.MembershipAdmissionfee, m.MembershipFees, m.MembershipCurrentExpiryYear, m.MembershipJoinDate, m.MembershipRenewDate, m.MembershipExpiryYears,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath, m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.UpdatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.Id = @Id
END 


GO
/****** Object:  StoredProcedure [dbo].[UserProfile_IIA_GetChapterId]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserProfile_IIA_GetChapterId]
(
	@IsAdmin int,
    @Id int
) 
AS
DECLARE @Chapter int
BEGIN
	IF(@IsAdmin = 1)
	BEGIN
		Select Chapter FROM Admin Where Id = @Id
	END
	ELSE
	BEGIN
		Select Chapter FROM UserProfile Where Id = @Id
	END
END 
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Insert_Update_Details]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_Insert_Update_Details] 

( 

    @Id int,
	@PhoneNumber varchar(10),
	@MembershipId varchar(50),
	@MembershipAdmissionfee int,
	@MembershipFees int,
	@MembershipCurrentExpiryYear int,
	@MembershipJoinDate datetime,
	@MembershipRenewDate datetime,
	@MembershipExpiryYears text,
	@ProfileStatus int,
	@Chapter int,
	@UnitName varchar(50),	
	@GSTIN varchar(50),
	@GSTcertpath varchar(200),
	@IndustryStatus varchar(50),
	@Address text,
	@District varchar(50),
	@City varchar(50),
	@State varchar(50),
	@Country varchar(50),
	@Pincode varchar(50),
	@WebsiteUrl varchar(50),
	@ProductCategory varchar(50),
	@ProductSubCategory varchar(50),
	@MajorProducts text,
	@AnnualTurnOver varchar(50),
	@EnterpriseType varchar(50),
	@Exporter varchar(50),
	@Classification varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@Email varchar(50),
	@ProfileImagePath varchar(200),
	@FinancialProofPath varchar(200),
	@SignaturePath varchar(200),
	@CreatedBy int,
	@UpdatedBy int,
	@UpdatedDate datetime

) 

AS 

BEGIN 

   IF NOT EXISTS (SELECT * FROM UserProfile  WHERE  MembershipId = @MembershipId or Id = @Id ) 

   BEGIN 

       INSERT INTO UserProfile (PhoneNumber, MembershipId, MembershipAdmissionfee, MembershipFees, MembershipCurrentExpiryYear, MembershipJoinDate, MembershipRenewDate, MembershipExpiryYears, ProfileStatus, Chapter, UnitName, GSTIN, GSTcertpath, IndustryStatus, Address, District, City, State, Country, Pincode, WebsiteUrl, ProductCategory, ProductSubCategory, MajorProducts, AnnualTurnOver, EnterpriseType, Exporter, Classification, FirstName, LastName, Email,ProfileImagePath,FinancialProofPath,SignaturePath,CreatedBy, UpdatedDate)
       VALUES (@PhoneNumber, @MembershipId, @MembershipAdmissionfee, @MembershipFees, @MembershipCurrentExpiryYear, @MembershipJoinDate, @MembershipRenewDate, @MembershipExpiryYears, @ProfileStatus, @Chapter, @UnitName, @GSTIN, @GSTcertpath, @IndustryStatus, @Address, @District, @City, @State, @Country, @Pincode, @WebsiteUrl, @ProductCategory, @ProductSubCategory, @MajorProducts, @AnnualTurnOver, @EnterpriseType, @Exporter, @Classification, @FirstName, @LastName, @Email,@ProfileImagePath,@FinancialProofPath,@SignaturePath,@CreatedBy, @UpdatedDate)
 
   END 
   ELSE
   BEGIN
       UPDATE UserProfile  SET 
	   PhoneNumber = @PhoneNumber,
	   MembershipId = @MembershipId,
	   MembershipAdmissionfee = @MembershipAdmissionfee,
	   MembershipFees = @MembershipFees,
	   MembershipCurrentExpiryYear = @MembershipCurrentExpiryYear,
	   MembershipJoinDate = @MembershipJoinDate,
	   MembershipRenewDate = @MembershipRenewDate,
	   MembershipExpiryYears = @MembershipExpiryYears,
	   ProfileStatus = @ProfileStatus,
	   Chapter = @Chapter,
	   UnitName = @UnitName,
	   GSTIN = @GSTIN,
	   GSTcertpath = @GSTcertpath,
	   IndustryStatus = @IndustryStatus,
	   Address = @Address,
	   District = @District,
	   City = @City,
	   State = @State,
	   Country = @Country,
	   Pincode = @Pincode,
	   WebsiteUrl = @WebsiteUrl, 
	   ProductCategory = @ProductCategory,
	   ProductSubCategory = @ProductSubCategory,
	   MajorProducts = @MajorProducts,
	   AnnualTurnOver = @AnnualTurnOver,
	   EnterpriseType = @EnterpriseType,
	   Exporter = @Exporter,
	   Classification = @Classification,
	   FirstName = @FirstName,
	   LastName = @LastName,
	   Email = @Email,
	   ProfileImagePath = @ProfileImagePath,
	   FinancialProofPath = @FinancialProofPath,
	   SignaturePath = @SignaturePath,
	   UpdatedBy = @UpdatedBy,
	   UpdatedDate = @UpdatedDate
	   where MembershipId = @MembershipId or Id = @Id
   END 

END 

GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Membership_UpdateDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_Membership_UpdateDetails] 

( 

    @UserId int,
	@ProfileStatus int,
	@MembershipId varchar(50),
	@MembershipAdmissionfee int,
	@MembershipFees int,
	@MembershipCurrentExpiryYear int,
	@MembershipJoinDate datetime,
	@MembershipRenewDate datetime,
	@MembershipExpiryYears text,
	@UpdatedBy int,
	@UpdatedDate datetime
) 

AS 

BEGIN
	IF EXISTS (SELECT Id FROM UserProfile 
                   WHERE Id = @UserId)
		BEGIN
			  UPDATE UserProfile  SET 
			    MembershipId = @MembershipId,
				ProfileStatus = @ProfileStatus,
				MembershipAdmissionfee = @MembershipAdmissionfee,
				MembershipFees = @MembershipFees,
				MembershipCurrentExpiryYear = @MembershipCurrentExpiryYear,
				MembershipJoinDate = @MembershipJoinDate,
				MembershipRenewDate = @MembershipRenewDate,
				MembershipExpiryYears = @MembershipExpiryYears,
				UpdatedBy = @UpdatedBy,
				UpdatedDate = @UpdatedDate
				where Id = @UserId
		END
END 

GO
/****** Object:  StoredProcedure [dbo].[UserProfile_MembershipProfile_GetDetails]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_MembershipProfile_GetDetails]

( 
	@Id int,
    @ProfileStatus int,
	@Chapter int

) 

AS 

IF @ProfileStatus != 0 and @Chapter !=0

BEGIN 
  SELECT m.id,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath, m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.ProfileStatus = @ProfileStatus and Chapter = @Chapter
END 
ELSE IF @ProfileStatus !=0
BEGIN
	SELECT m.id,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath,m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.ProfileStatus = @ProfileStatus
END
ELSE IF @Chapter !=0
BEGIN
	SELECT m.id,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath,m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.Chapter = @Chapter
END
ELSE
BEGIN
	SELECT m.id,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath,m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id WHERE m.Id = @Id
END


GO
/****** Object:  StoredProcedure [dbo].[UserProfile_MembershipProfile_GetDetails_ByPhoneNumber]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_MembershipProfile_GetDetails_ByPhoneNumber]

( 
	@PhoneNumber varchar(10)

) 

AS 


BEGIN 
  SELECT m.id,m.ProfileStatus,m.Chapter,c.Name,m.UnitName,m.GSTIN,m.GSTcertpath,m.IndustryStatus,m.Address,m.District,m.City,m.State,m.Country,m.Pincode,m.WebsiteUrl,m.ProductCategory,m.ProductSubCategory,m.MajorProducts,m.AnnualTurnOver,m.EnterpriseType,m.Exporter,m.Classification,m.FirstName,m.LastName,m.email,m.ProfileImagePath, m.FinancialProofPath, m.SignaturePath, m.CreatedBy, m.CreatedDate,m.UpdatedDate FROM UserProfile m LEFT JOIN Chapters c on m.Chapter=c.Id  WHERE m.PhoneNumber=@PhoneNumber
END 

GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Payments_GetPaymentUserId]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserProfile_Payments_GetPaymentUserId]
(
	@phoneNumber varchar(50)
)
AS
BEGIN
    Select U.PhoneNumber, U.Id, U.State, U.AnnualTurnOver, U.GSTIN from UserProfile as U where U.PhoneNumber=@phoneNumber
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_AddComment]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserProfile_Tickets_AddComment]
(
    @TicketNumber int,
	@Comment varchar(MAX),
	@UserId int= -1,
	@AdminId int = -1,
	@UserName varchar(50) = '',
	@AdminName varchar(50) = ''
)
AS
BEGIN 
	   INSERT INTO Comments(TicketNumber,Comment,UserId,AdminId) 
	   VALUES (@TicketNumber, @Comment, @UserId, @AdminId)

	   If(@UserId <> -1)
	   BEGIN
			UPDATE Tickets
			SET Status = 'Pending on IIA'
			WHERE TicketNumber = @TicketNumber
	   END
	   If(@AdminId <> -1)
	   BEGIN
			UPDATE Tickets
			SET Status = 'Pending on Member'
			WHERE TicketNumber = @TicketNumber
	   END
	   If(@UserId <> -1)
	   BEGIN
			UPDATE Comments
			SET UserName = CONCAT(U.FirstName, U.LastName) From Comments AS C
			LEFT JOIN UserProfile AS U on U.Id = C.UserId
			WHERE TicketNumber = @TicketNumber
	   END
	   If(@AdminId <> -1)
	   BEGIN
			UPDATE Comments
			SET AdminName = (SELECT Name FROM Chapters WHERE Id = (SELECT Chapter FROM Admin WHERE Id = @AdminId)) From Comments AS C
			LEFT Join Admin AS A on A.Id = C.AdminId
			WHERE TicketNumber = @TicketNumber
	   END
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_CreateTickets]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_Tickets_CreateTickets]
(
    @Title varchar(MAX),
	@Description varchar(MAX),
	@Category varchar(50),
	@UserId int,
	@Number int OUTPUT
)
AS
DECLARE @ChapterId int;
DECLARE @ClosedTicketTime datetime = '9999-12-1';
BEGIN
	   SET @ChapterId = (SELECT UserProfile.Chapter from UserProfile where UserProfile.Id = @UserId)
	   INSERT INTO Tickets (Title, Description, Category, ChapterId, UserId, ClosedTicketTime)
       VALUES (@Title, @Description, @Category, @ChapterId, @UserId, @ClosedTicketTime)
	   SET @Number = SCOPE_IDENTITY()
	   SELECT SCOPE_IDENTITY() Number
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_GetAttachment]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserProfile_Tickets_GetAttachment]
(
    @TicketNumber int
)
AS
BEGIN 
	   SELECT C.TicketNumber, CONCAT(U.FirstName, U.LastName) AS UserName, CH.Name AS ChapterName, C.AttachmentURL, C.AttachmentCreationTime From Attachment AS C
	   LEFT Join Admin AS A on A.Id = C.AdminId
	   LEFT JOIN UserProfile AS U on U.Id = C.UserId
	   LEFT JOIN Chapters AS CH on CH.Id = A.Chapter
	   WHERE TicketNumber = @TicketNumber
END
GO
/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_GetComments]    Script Date: 20-10-2021 15:32:10 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UserProfile_Tickets_GetComments]
(
    @TicketNumber int
)
AS
BEGIN 
	   SELECT C.TicketNumber, CONCAT(U.FirstName, U.LastName) AS UserName, C.Comment, C.CommentCreationTime, CH.Name AS AdminName From Comments AS C
	   LEFT Join Admin AS A on A.Id = C.AdminId
	   LEFT JOIN UserProfile AS U on U.Id = C.UserId
	   LEFT JOIN Chapters AS CH on CH.Id = A.Chapter
	   WHERE TicketNumber = @TicketNumber order by C.CommentCreationTime
END
GO
