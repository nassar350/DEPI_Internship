-- 19. Customer Loyalty Tier Assignment
-- Create a comprehensive solution that assigns loyalty tiers to customers based on their total spending. 
-- Handle customers with no orders appropriately and use proper NULL checking.


select sc.customer_id, sc.first_name, sc.last_name, sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_spending',
case
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) <= 500 then 'Basic'
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) > 500 and sum(soi.quantity * soi.list_price * (1 - soi.discount)) <= 10000
        then 'Intermediate'
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) > 10000 then 'High'
end as 'loyalty_tier'
from sales.customers as sc 
left join sales.orders as so on sc.customer_id = so.customer_id
join sales.order_items soi on soi.order_id = so.order_id
where so.order_id is not NULL
group by sc.customer_id, sc.first_name, sc.last_name