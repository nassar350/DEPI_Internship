-- Customer Sales Summary Function
-- Create a multi-statement function named GetCustomerYearlySummary that takes a customer ID and 
-- returns a table with yearly sales data including total orders, total spent, and average order value for each year.

create FUNCTION GetcustomerYearlySummary(@customer_id int)
returns @summary_table TABLE
(
    name nvarchar(255),
    "No._of_orders" int,
    year INT,
    "total_spent" decimal(10,2),
    "average_spent" decimal(10,2)
)
AS
BEGIN
    INSERT into @summary_table
        select (sc.first_name + ' ' + sc.last_name) as 'name', count(so.order_id) as 'No._of_orders',
        year(order_date) as 'year',
        SUM(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_spent',
        avg(soi.quantity * soi.list_price * (1 - soi.discount)) as 'average_spent'
        from sales.customers as sc
        join sales.orders as so on sc.customer_id = so.customer_id
        join sales.order_items as soi on soi.order_id = so.order_id
        where sc.customer_id = @customer_id
        group by sc.first_name, sc.last_name, year(order_date)
    RETURN
end
go

select * from GetcustomerYearlySummary(10);