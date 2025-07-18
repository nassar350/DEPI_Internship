-- 3. Staff Performance Calculator
-- Write a query that calculates the total sales for staff member ID 2 in the year 2017. 
-- Use variables to store the staff ID, year, and calculated total. Display the results with appropriate labels.

declare @staff_id int = 2
declare @year int = 2017
declare @total_sales INT

select @total_sales = sum(soi.quantity * soi.list_price * (1 - soi.discount))
from sales.orders as so
join sales.order_items as soi on so.order_id = soi.order_id
where so.staff_id = @staff_id

print 'Staff member with id ' + cast(@staff_id as varchar(20)) + ' has total sales of ' + cast(@total_sales as varchar(20)) 
    + ' in ' + cast(@year as varchar(20))