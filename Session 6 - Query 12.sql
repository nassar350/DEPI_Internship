-- Discount Calculation Function
-- Write a scalar function named CalculateBulkDiscount that determines discount percentage based on quantity:
-- 1-2 items: 0% discount
-- 3-5 items: 5% discount
-- 6-9 items: 10% discount
-- 10+ items: 15% discount

create function CalculateBulkDiscount(@item_numbers int)
returns nvarchar(255)
AS
BEGIN
    declare @discount nvarchar(255)

    if @item_numbers >= 1 and @item_numbers <= 2
    BEGIN
    set @discount = '0% discount'
    END
    else if @item_numbers >= 3 and @item_numbers <= 5
    BEGIN
    set @discount = '5% doscount'
    END
    else if @item_numbers >= 6 and @item_numbers <= 9
    BEGIN
    set @discount = '10% discount'
    END
    else if @item_numbers >= 10
    BEGIN
    set @discount = '15% discount'
    END

    return @discount

END
GO


SELECT dbo.CalculateBulkDiscount(10) as 'discount_ratio';