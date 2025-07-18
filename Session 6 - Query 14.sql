-- 14. Inventory Restock Procedure#
-- Write a stored procedure named sp_RestockProduct with input parameters for store ID, product ID, and restock quantity. 
-- Include output parameters for old quantity, new quantity, and success status.

create PROCEDURE sp_RestockProduct
    @store_id int, 
    @product_id int,
    @restock_quantity int,
    @old_quantity int OUTPUT,
    @new_quantity int output,
    @success_status nvarchar(255) OUTPUT
as 
BEGIN
    select @old_quantity = quantity from production.stocks where store_id = @store_id and product_id = @product_id

    update production.stocks
    set quantity = quantity + @restock_quantity
    where store_id = @store_id and product_id = @product_id

    if @@ROWCOUNT > 0
    BEGIN
        set @success_status = 'Success'
    END
    else
    BEGIN
        set @success_status = 'Failed'
    END

    select @new_quantity = quantity from production.stocks where store_id = @store_id and product_id = @product_id

END;
GO

DECLARE @old_quantity INT
DECLARE @new_quantity INT
DECLARE @success_status NVARCHAR(255)

exec sp_RestockProduct @store_id = 1, @product_id = 1, @restock_quantity = 5,
    @old_quantity = @old_quantity OUTPUT, @new_quantity = @new_quantity OUTPUT, @success_status = @success_status OUTPUT;

select @old_quantity as Old_Quantity, @new_quantity as New_Quantity, @success_status as Success_Status;
GO
