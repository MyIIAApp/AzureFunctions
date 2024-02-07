/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForSpecificCategory]    Script Date: 10/2/2021 5:35:48 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Offers_GetOffersForSpecificCategory]
(
	@Category varchar(50)
)
AS
BEGIN
    Select * FROM Offers WHERE Category = @Category
END
