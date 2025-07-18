-- Product Category Function
-- Create an inline table-valued function named GetProductsByPriceRange that accepts minimum and maximum price parameters 
-- returns all products within that price range with their brand and category information.

create function GetproductsbypriceRange(@minimum_price decimal(10,2), @maximum_price decimal(10,2))
returns TABLE
AS
return
(
    select pp.product_id, pp.product_name, pb.brand_name, pc.category_name
    from production.brands as pb
    join production.products as pp on pp.brand_id = pb.brand_id
    join production.categories as pc on pp.category_id = pc.category_id
    where pp.list_price >= @minimum_price and pp.list_price <= @maximum_price
);
GO

select * from GetproductsbypriceRange(1.50,5000);