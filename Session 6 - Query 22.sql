-- 22. Data Validation System
-- Build a complete data validation system using functions and procedures that ensures data integrity when inserting new orders, 
-- including customer validation, inventory checking, and business rule enforcement.


create FUNCTION Customer_validation(@customer_id int)
returns INT
AS
BEGIN
    declare @customer_valid int = 0

    select @customer_valid = 1 from sales.customers where customer_id = @customer_id

    return @customer_valid
end
go


create FUNCTION inventory_checking(@product_id int, @product_quantity int)
returns INT
AS
BEGIN
    declare @inventory_valid int = 0
    declare @inventory_quantity int = 0

    select @inventory_quantity = quantity
    from production.stocks where product_id = @product_id

    if @inventory_quantity >= @product_quantity
    BEGIN
        set @inventory_valid = 1
    END

    return @inventory_valid
END
go



create PROCEDURE sp_new_order_checking
    @order_id INT,
    @customer_id INT,
    @order_status INT = 1,
    @order_date date,
    @store_id INT,
    @staff_id INT,
    @product_id int,
    @item_id int,
    @quantity int,
    @list_price decimal(10,2),
    @discount decimal(4, 2),
    @customer_valid int,
    @inventory_valid INT
AS
BEGIN
    -- customer_validate
    set @customer_valid = dbo.Customer_validation(@customer_id)
    if @customer_valid = 0
    BEGIN
        print 'Customer not found'
    END
    ELSE
    BEGIN
        -- inventory_validate
        set @inventory_valid = dbo.inventory_checking(@product_id, @quantity)
        if @inventory_valid = 0
        BEGIN
            print 'Unsufficient Inventory'
        END
        else
        BEGIN
            insert into sales.orders (order_id,customer_id,order_status,order_date,store_id,staff_id)
            values(@order_id, @customer_id, @order_status, @order_date, @store_id, @staff_id)

            insert into sales.order_items(order_id, item_id, product_id, quantity, list_price, discount)
            values(@order_id, @item_id, @product_id, @quantity, @list_price, @discount)

            print 'order added successfully'
        END
    end
end
go

