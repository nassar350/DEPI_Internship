-- 13. Customer Order History Procedure#
-- Create a stored procedure named sp_GetCustomerOrderHistory that accepts a customer ID and optional start/end dates. 
-- Return the customer's order history with order totals calculated.

create PROCEDURE sp_GetCustomerOrderHistory
    @customer_id INT,
    @start_date date = NULL,
    @end_date date = NULL
AS
BEGIN
    select (sc.first_name + ' ' + sc.last_name) as 'name', so.order_id,
    SUM(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_spent'
    from sales.customers as sc
    join sales.orders as so on sc.customer_id = so.customer_id
    join sales.order_items as soi on soi.order_id = so.order_id
    where sc.customer_id = @customer_id
    and (@start_date IS NULL or order_date >= @start_date)
    and (@end_date IS NULL or order_date <= @end_date)
    group by sc.first_name, sc.last_name, so.order_id
END
GO


EXEC sp_GetCustomerOrderHistory @customer_id = 1;

