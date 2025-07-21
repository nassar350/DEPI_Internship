-- 21. Advanced Analytics Query
-- Create a query that combines multiple advanced concepts to generate a comprehensive sales report showing monthly trends, 
-- staff performance, and product category analysis.


with monthly_trends as (
    select pp.product_id, pp.product_name, pc.category_name, pb.brand_name,
    month(so.order_date) as 'Month', sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_sale'
    FROM
    production.products as pp 
    join production.categories as pc on pp.category_id = pc.category_id
    join production.brands as pb on pp.brand_id = pb.brand_id
    join sales.order_items as soi on pp.product_id = soi.product_id
    join sales.orders as so on so.order_id = soi.order_id
    where year(so.order_date) = 2022
    group by pp.product_id, pp.product_name, pc.category_name, pb.brand_name, month(so.order_date)
),

 staff_performance as (
    select ss.staff_id, (ss.first_name + ' ' + ss.last_name) as 'full_name', ss.email, ss.phone,
    sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_sale',
    RANK() over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) as 'staff_rank'
    from sales.staffs as ss 
    join sales.orders as so on ss.staff_id = so.staff_id
    join sales.order_items as soi on soi.order_id = so.order_id
    where YEAR(so.order_date) = 2022
    group by ss.staff_id, ss.first_name, ss.last_name, ss.email, ss.phone
),

 product_category_analysis as (
    select so.staff_id, pp.product_id, pp.product_name, pc.category_name, count(so.order_id) as 'total_orders'
    FROM
    production.products as pp 
    join production.categories as pc on pp.category_id = pc.category_id
    join sales.order_items as soi on pp.product_id = soi.product_id
    join sales.orders as so on so.order_id = soi.order_id
    where year(so.order_date) = 2022
    group by so.staff_id, pp.product_id, pp.product_name, pc.category_name
)


select mt.product_id, mt.product_name, mt.category_name, mt.brand_name, mt.Month, mt.total_sale,
pca.total_orders, sp.staff_id, sp.full_name, sp.email, sp.phone, sp.total_sale, sp.staff_rank
FROM monthly_trends as mt
join product_category_analysis as pca on pca.product_id = mt.product_id
join staff_performance as sp on pca.staff_id = sp.staff_id 
