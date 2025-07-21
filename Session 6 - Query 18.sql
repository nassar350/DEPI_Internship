-- 18. Smart Inventory Management
-- Write a complex query with nested IF statements that manages inventory restocking. 
-- Check current stock levels and apply different reorder quantities based on product categories and current stock levels.


select pc.category_id, pc.category_name, pp.product_name,
case
    when pc.category_name like '%Jeans' and ps.quantity < 5 then 15
    when pc.category_name like '%Shorts' and ps.quantity < 2 then 9
    else ps.quantity+2
end as 'restocked_quantity'
from production.products as pp
join production.categories as pc on pp.category_id = pc.category_id
join sales.order_items as soi on soi.product_id = pp.product_id
join sales.orders as so on so.order_id = soi.order_id
join production.stocks as ps on ps.store_id = so.store_id and ps.product_id = pp.product_id


select top 10 * from production.categories;