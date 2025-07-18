-- 9. Shipping Cost Calculator Function
-- Create a scalar function named CalculateShipping that takes an order total as input and returns shipping cost:
-- Orders over $100: Free shipping ($0)
-- Orders $50-$99: Reduced shipping ($5.99)
-- Orders under $50: Standard shipping ($12.99)

create FUNCTION CalculateShipping (@order_total decimal(10,2))
RETURNS decimal(10,2)
as 
BEGIN
    declare @shipping decimal(10,2)

    if @order_total > 100
    BEGIN
        set @shipping = 0
    END
    else if @order_total >= 50 and @order_total <= 99
    BEGIN
        set @shipping = 5.99
    END
    else if @order_total < 50
    BEGIN
        set @shipping = 12.99
    END
    return @shipping
END;
GO

select dbo.CalculateShipping(15) as 'shipping';