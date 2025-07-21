-- 1.Create a non-clustered index on the email column in the sales.customers table to 
-- improve search performance when looking up customers by email.

create NONCLUSTERED INDEX IX_Customer_email on sales.customers(email);


-- 2.Create a composite index on the production.products table that 
-- includes category_id and brand_id columns to optimize searches that filter by both category and brand.

create CLUSTERED INDEX IX_Products_Category_Brand on production.products(category_id, brand_id);


-- 3.Create an index on sales.orders table for the order_date column and 
-- include customer_id, store_id, and order_status as included columns to improve reporting queries.

create nonCLUSTERED INDEX IX_orders_orderdate on sales.orders(order_date)
include(customer_id, store_id, order_status);


-- 4.Create a trigger that automatically inserts a welcome record into a customer_log table 
-- whenever a new customer is added to sales.customers. (First create the log table, then the trigger)

create TRIGGER tr_Customer_welcome
on sales.customers
for INSERT
AS
BEGIN
    declare @customer_id int
    select @customer_id = customer_id from inserted
    
    insert into sales.customer_log (customer_id, action, log_date)
    values (@customer_id, 'Welcome', GETDATE())
END
GO;

insert into sales.customers values (12000, 'h', 'e', '12000', 'rr', 'hh', 'tt', 'dd');
select * from sales.customer_log where customer_id = 12000;


-- 5.Create a trigger on production.products that logs any changes to the list_price column into a price_history table, 
-- storing the old price, new price, and change date.

create TRIGGER tr_price_change
on production.products
for UPDATE
AS
BEGIN
    insert into production.price_history (product_id, old_price, new_price, changed_by)
    select i.product_id, d.list_price, i.list_price, 'user'
    from inserted as i
    join deleted as d on i.product_id = d.product_id
END
GO

update production.products set list_price = 5441.56
where product_id = 12

select * from production.price_history where product_id = 12;


-- 6.Create an INSTEAD OF DELETE trigger on production.categories that prevents deletion of categories that have associated products. 
-- Display an appropriate error message.

create TRIGGER tr_handle_category_deletion
on production.categories
INSTEAD OF DELETE
AS
BEGIN
    declare @count_product INT = NULL

    select @count_product = count(pp.product_id)
    from production.products as pp 
    join deleted as d on pp.category_id = d.category_id
    where pp.category_id = d.category_id

    if @count_product = 0 or @count_product is NULL
    BEGIN
        DELETE from production.categories where category_id = (select category_id from deleted)
        print 'category deleted successfully'
    END
    ELSE
    BEGIN
        print 'category has related products'
    END

END
GO

delete from production.categories where category_id = 2
select * from production.categories where category_id = 2


-- 7.Create a trigger on sales.order_items that automatically reduces the quantity in production.stocks
-- when a new order item is inserted.

create TRIGGER tr_update_quantity
on sales.order_items
for INSERT
AS
BEGIN
    update ps
    set quantity = ps.quantity - (i.quantity)
    from production.stocks as ps
    join inserted as i on i.product_id = ps.product_id
END
GO

insert into sales.order_items values (550, 4, 6, 2, 51.5, 0.15);
select * from production.stocks where product_id = 6;


-- 8.Create a trigger that logs all new orders into an order_audit table, 
-- capturing order details and the date/time when the record was created.

create TRIGGER tr_save_order_details
on sales.orders
for INSERT
AS
begin
    insert into sales.order_audit
    select i.order_id, i.customer_id, i.store_id, i.staff_id, i.order_date, GETDATE()
    from sales.orders as so
    join inserted as i on so.order_id = i.order_id
END
GO

insert into sales.orders values (440, 5, 2, GETDATE(), GETDATE(), GETDATE(), 5, 1);
select * from sales.order_audit where order_id = 440;