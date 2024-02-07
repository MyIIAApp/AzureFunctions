/****** Object:  StoredProcedure [dbo].[Admin_CheckIfAdminExists]    Script Date: 27-09-2021 21:26:22 ******/
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

/****** Object:  StoredProcedure [dbo].[Admin_CheckRoles]    Script Date: 27-09-2021 21:26:22 ******/
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

/****** Object:  StoredProcedure [dbo].[Chapters_GetChapters]    Script Date: 27-09-2021 21:26:23 ******/
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

/****** Object:  StoredProcedure [dbo].[IIA_GetLeaderDetails]    Script Date: 27-09-2021 21:26:23 ******/
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

/****** Object:  StoredProcedure [dbo].[News_CreateNews]    Script Date: 27-09-2021 21:26:23 ******/
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

/****** Object:  StoredProcedure [dbo].[News_GetNewsForAllCategory]    Script Date: 27-09-2021 21:26:23 ******/
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

/****** Object:  StoredProcedure [dbo].[News_GetNewsForSpecificCategory]    Script Date: 27-09-2021 21:26:23 ******/
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

/****** Object:  StoredProcedure [dbo].[OfferCategories_CreateCategories]    Script Date: 27-09-2021 21:26:23 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[OfferCategories_CreateCategories](
@CategoryName varchar(225)
)
As
Begin
Insert into OfferCategories(CategoryName) values (@CategoryName)
end
GO

/****** Object:  StoredProcedure [dbo].[Offers_CreateOffers]    Script Date: 27-09-2021 21:26:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[Offers_CreateOffers](
@CategoryId int, @OrganisationName nvarchar(225), @Title nvarchar(100), @PercentageDiscount int, @FixedDiscount int, @OrganisationAddress nvarchar(225),@City nvarchar(225),
@Email nvarchar(225), @Phone nvarchar(100), @NationalValidity BIT, @StartDate datetime,
@ExpiryDate datetime, @ImagePath nvarchar(400) , @OfferDescription varchar(MAX))

As
Begin
	
	 INSERT INTO Offers (CategoryId, OrganisationName, Title, PercentageDiscount, FixedDiscount, OrganisationAddress,City, Email, Phone, NationalValidity, StartDate, ExpiryDate, ImagePath , OfferDescription)
       VALUES (@CategoryId, @OrganisationName, @Title, @PercentageDiscount, @FixedDiscount, @OrganisationAddress,@City, @Email, @Phone, @NationalValidity, @StartDate, @ExpiryDate, @ImagePath, @OfferDescription )


End
GO

/****** Object:  StoredProcedure [dbo].[Offers_GetOfferCategories]    Script Date: 27-09-2021 21:26:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offers_GetOfferCategories]
AS
BEGIN
    Select O.CategoryName from OfferCategories as O
END
GO

/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForAllCategory]    Script Date: 27-09-2021 21:26:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offers_GetOffersForAllCategory]
AS
BEGIN
    Select O.Id, O.CategoryId, O.Title, O.OrganisationName, O.PercentageDiscount, O.FixedDiscount, O.OrganisationAddress, O.City, O.Email, O.Phone, O.NationalValidity, O.StartDate, O.ExpiryDate, O.ImagePath, A.CategoryName, O.OfferDescription from Offers as O
	join OfferCategories as A on A.Id=O.CategoryId
END
GO

/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForSpecificCategory]    Script Date: 27-09-2021 21:26:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Offers_GetOffersForSpecificCategory]
(
	@category varchar(50)
)
AS
BEGIN
    Select O.Id, O.CategoryId, O.Title, O.OrganisationName, O.PercentageDiscount, O.FixedDiscount, O.OrganisationAddress, O.City, O.Email, O.Phone, O.NationalValidity, O.StartDate, O.ExpiryDate, O.ImagePath, A.CategoryName, O.OfferDescription from Offers as O
	join OfferCategories as A on A.Id=O.CategoryId where A.CategoryName=@category
END
GO

/****** Object:  StoredProcedure [dbo].[Payment_GetInvoiceId]    Script Date: 27-09-2021 21:26:24 ******/
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

/****** Object:  StoredProcedure [dbo].[Payment_InsertDetails]    Script Date: 27-09-2021 21:26:25 ******/
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

/****** Object:  StoredProcedure [dbo].[Payment_InsertInvoicePath]    Script Date: 27-09-2021 21:26:25 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_AddAttachment]    Script Date: 27-09-2021 21:26:25 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_ChangeChapter]    Script Date: 27-09-2021 21:26:25 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_ChangeStatus]    Script Date: 27-09-2021 21:26:25 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_CloseTicket]    Script Date: 27-09-2021 21:26:26 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForAllChapters]    Script Date: 27-09-2021 21:26:26 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[Tickets_GetSummaryForAllChapters]
AS
BEGIN
       SELECT * FROM Tickets
	   WHERE ChapterId <> 0
END
GO

/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForChapters]    Script Date: 27-09-2021 21:26:26 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_GetSummaryForUsers]    Script Date: 27-09-2021 21:26:26 ******/
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

/****** Object:  StoredProcedure [dbo].[Tickets_GetTicketDetails]    Script Date: 27-09-2021 21:26:26 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Company_UpdateDetails]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_CreateUserIfNotExists]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_GetDetails]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_IIA_GetChapterId]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Insert_Update_Details]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Membership_UpdateDetails]    Script Date: 27-09-2021 21:26:27 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_MembershipProfile_GetDetails]    Script Date: 27-09-2021 21:26:28 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_MembershipProfile_GetDetails_ByPhoneNumber]    Script Date: 27-09-2021 21:26:28 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Payments_GetPaymentUserId]    Script Date: 27-09-2021 21:26:28 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_AddComment]    Script Date: 27-09-2021 21:26:29 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_CreateTickets]    Script Date: 27-09-2021 21:26:29 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_GetAttachment]    Script Date: 27-09-2021 21:26:29 ******/
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

/****** Object:  StoredProcedure [dbo].[UserProfile_Tickets_GetComments]    Script Date: 27-09-2021 21:26:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[UserProfile_Tickets_GetComments]
(
    @TicketNumber int
)
AS
BEGIN 
	   SELECT C.TicketNumber, CONCAT(U.FirstName, U.LastName) AS UserName, C.Comment, C.CommentCreationTime, CH.Name AS AdminName From Comments AS C
	   LEFT Join Admin AS A on A.Id = C.AdminId
	   LEFT JOIN UserProfile AS U on U.Id = C.UserId
	   LEFT JOIN Chapters AS CH on CH.Id = A.Chapter
	   WHERE TicketNumber = @TicketNumber
END
GO


