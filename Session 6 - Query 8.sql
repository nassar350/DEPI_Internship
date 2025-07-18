-- 8. Customer Order Validation
-- Create a query that checks if customer ID 5 exists in the database. 
-- If they exist, show their order count. If not, display an appropriate message.

declare @customer_id int = 5
declare @order_count int

if exists (select 1 from sales.customers where customer_id = @customer_id)
BEGIN
    select @order_count = count(order_id) from sales.orders where customer_id = @customer_id
    print 'Customer order count is ' + cast(@order_count as varchar(20))
END
else
BEGIN
    print 'Customer Not Found'
END