-- 1. Customer Spending Analysis
-- Write a query that uses variables to find the total amount spent by customer ID 1. 
-- Display a message showing whether they are a VIP customer (spent > $5000) or regular customer.

declare @total_spent INT
select @total_spent = sum(soi.quantity * soi.list_price * (1 - soi.discount))
from sales.orders as so
join sales.order_items as soi on so.order_id = soi.order_id
where so.customer_id = 1 

if @total_spent > 5000

BEGIN
    print 'VIP Customer'
END

ELSE

BEGIN
    print 'Regulear Customer'
END