-- 15. Order Processing Procedure
-- Create a stored procedure named sp_ProcessNewOrder 
-- that handles complete order creation with proper transaction control and error handling. 
-- Include parameters for customer ID, product ID, quantity, and store ID.

CREATE PROCEDURE sp_ProcessNewOrder
    @customer_ID INT,
    @product_ID INT,
    @store_ID INT,
    @quantity INT
AS
BEGIN
    declare @available_quantity int 

    if exists (select 1 from production.stocks where store_id = @store_ID and product_id = @product_ID)
    BEGIN
        select @available_quantity = quantity from production.stocks where store_id = @store_ID and product_id = @product_ID

        if @available_quantity >= @quantity
        BEGIN
            -- update quantity
            update production.stocks
            set quantity = quantity - @quantity
            where store_id = @store_ID and product_id = @product_ID
            -- add order
            insert into sales.orders (customer_id, order_date, order_status, store_id)
            values (@customer_ID, GETDATE(), 1, @store_ID)
        END
        else
        BEGIN
            print 'there is no available quantity in the stock'
        END
    END
    ELSE
    BEGIN
        print 'Product is not available in the store'
    end
END;
go
