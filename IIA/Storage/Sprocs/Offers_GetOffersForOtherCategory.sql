/****** Object:  StoredProcedure [dbo].[Offers_GetOffersForOtherCategory]    Script Date: 10/2/2021 5:35:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[Offers_GetOffersForOtherCategory]
AS
BEGIN
    Select * FROM Offers WHERE Category != 'Healthcare' AND Category != 'IT Solution & Electronics' AND Category != 'Health Insurance' AND Category != 'Hotels and Restaurants' AND Category != 'Automobiles' AND Category != 'Steel Pallet & Material Handling' AND Category != 'Utensils & Kitchen Items' AND Category != 'COVID-19 Related Items' 
END
