-- 20. Product Lifecycle Management
-- Write a stored procedure that handles product discontinuation including checking for pending orders, 
-- optional product replacement in existing orders, clearing inventory, and providing detailed status messages.


create PROCEDURE sp_productCheck
    @product_id INT,
    @product_replacement_id int = null,
    @is_pending int = 0
AS
BEGIN
    select @is_pending = 1 from sales.orders as so
    join sales.order_items as soi on so.order_id = soi.order_id
    where soi.product_id = @product_id and so.order_status = 'Pending';

    IF @is_pending = 1
    BEGIN
        PRINT 'the product has a pending order';
    END

    if @is_pending = 0
    BEGIN
        delete from production.products where product_id = @product_id
        print 'the product has been deleted'
    END

END;
go

exec sp_productCheck @product_id = 7
