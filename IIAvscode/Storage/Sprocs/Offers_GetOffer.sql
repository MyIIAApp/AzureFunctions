/****** Object:  StoredProcedure [dbo].[Offers_GetOffer]    Script Date: 10/2/2021 5:35:43 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Offers_GetOffer]
(
	@SNo int
)
AS
BEGIN
    Select * FROM Offers WHERE SNo = @SNo
END
