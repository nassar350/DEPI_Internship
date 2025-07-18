## 📈 SQL Queries on StoreDB

```sql

-- Write a query that classifies all products into price categories:
-- Products under $300: "Economy"
-- Products $300-$999: "Standard"
-- Products $1000-$2499: "Premium"
-- Products $2500 and above: "Luxury"

select product_id, product_name, list_price,
case
    when list_price < 300 then 'Economy'
    when list_price between 300 and 999 then 'Standard'
    when list_price between 1000 and 2499 then 'Premium'
    when list_price > 2499 then 'Luxury'
end as 'price_category'
from production.products;


-- Create a query that shows order processing information with user-friendly status descriptions:
-- Status 1: "Order Received"
-- Status 2: "In Preparation"
-- Status 3: "Order Cancelled"
-- Status 4: "Order Delivered"
-- Also add a priority level:
-- Orders with status 1 older than 5 days: "URGENT"
-- Orders with status 2 older than 3 days: "HIGH"
-- All other orders: "NORMAL"

select order_id, order_date, required_date, shipped_date, 
case
    when order_status = 1 then 'Order Received'
    when order_status = 2 then 'In Preparation'
    when order_status = 3 then 'Order Cancelled'
    when order_status = 4 then 'Order Delivered'
end as 'order_status',
case
    when order_status = 1 and DATEDIFF(DAY , order_date, GETDATE()) > 5 then 'URGENT'
    when order_status = 2 and DATEDIFF(DAY , order_date, GETDATE()) > 3 then 'HIGH'
    else 'NORMAL'
end as 'priority_level'
from sales.orders;


-- Write a query that categorizes staff based on the number of orders they've handled:
-- 0 orders: "New Staff"
-- 1-10 orders: "Junior Staff"
-- 11-25 orders: "Senior Staff"
-- 26+ orders: "Expert Staff"

select ss.staff_id, ss.first_name, ss.last_name, ss.email, ss.phone, count(order_id) as 'No. of orders Handled',
case 
    when count(order_id) = 0 then 'New Staff'
    when count(order_id) between 1 and 10 then 'Junior Staff'
    when count(order_id) between 11 and 25 then 'Senior Staff'
    when count(order_id) > 26 then 'Expert Staff'
end as 'Staff Category'
from sales.orders as so
right join sales.staffs as ss on so.staff_id = ss.staff_id
group by ss.staff_id, ss.first_name, ss.last_name, ss.email, ss.phone;


-- Create a query that handles missing customer contact information:
-- Use ISNULL to replace missing phone numbers with "Phone Not Available"
-- COALESCE to create a preferred_contact field (phone first, then email, then "No Contact Method")
-- Show complete customer information

select customer_id, first_name, last_name, ISNULL(phone, 'Phone Not Available') as 'Phone', email,
coalesce(phone, email, 'No Contact Method') as 'Contact Information', street, city, state, zip_code
from sales.customers;


-- Write a query that safely calculates price per unit in stock:
-- Use NULLIF to prevent division by zero when quantity is 0
-- Use ISNULL to show 0 when no stock exists
-- Include stock status using CASE WHEN
-- Only show products from store_id = 1

select pp.product_id, pp.product_name, (pp.list_price/NULLIF(ps.quantity, 0)) as 'price per unit',
ISNULL(ps.quantity, 0) as 'Stock Available',
case
    when ps.quantity < 40 then 'Limited Stock Available'
    else 'Sufficient Stock Available'
end as 'Stock_status'
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
where ps.store_id = 1;


-- Create a query that formats complete addresses safely:
-- Use COALESCE for each address component
-- Create a formatted_address field that combines all components
-- Handle missing ZIP codes gracefully

select customer_id, first_name, last_name, phone, email,
coalesce(street, '') + ', ' + coalesce(city, '') + ', ' + coalesce(state, '') as 'Address',
ISNULL(zip_code, 'No_zipcode') as 'zip_code'
from sales.customers;


-- Use a CTE to find customers who have spent more than $1,500 total:
-- Create a CTE that calculates total spending per customer
-- Join with customer information
-- Show customer details and spending
-- Order by total_spent descending

with customer_total_spent as (
    select sc.customer_id, sc.first_name, sc.last_name, sc.phone, sc.email, sc.street,sc.city, sc.state, sc.zip_code,
    sum(soi.list_price * soi.quantity * (1 - soi.discount)) as 'Total_Spent' 
    from sales.customers as sc 
    join sales.orders as so on sc.customer_id = so.customer_id
    join sales.order_items as soi on so.order_id = soi.order_id
    group by sc.customer_id, sc.first_name, sc.last_name, sc.phone, sc.email, sc.street,sc.city, sc.state, sc.zip_code
)

select * from customer_total_spent
order by 'Total_Spent' desc;


-- Create a multi-CTE query for category analysis:
-- CTE 1: Calculate total revenue per category
-- CTE 2: Calculate average order value per category
-- Main query: Combine both CTEs
-- Use CASE to rate performance: >$50000 = "Excellent", >$20000 = "Good", else = "Needs Improvement"

with total_revenue_category as (
    select pc.category_id, pc.category_name, 
    sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'Total_revenue'
    from production.categories as pc
    join production.products as pp on pc.category_id = pp.category_id
    join sales.order_items as soi on pp.product_id = soi.product_id
    group by pc.category_id, pc.category_name
),

 average_value_category as (
    select pc.category_id, pc.category_name, soi.order_id,
    sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'Total_order_value'
    from production.categories as pc
    join production.products as pp on pc.category_id = pp.category_id
    join sales.order_items as soi on pp.product_id = soi.product_id
    group by pc.category_id, pc.category_name, soi.order_id
)

select trc.category_id, trc.category_name, trc.Total_revenue, avg(avc.Total_order_value) as 'Average_order_value',
case 
    when trc.Total_revenue > 50000 then 'Excellent'
    when trc.Total_revenue > 20000 then 'Good'
    else 'Needs Improvement'
end as 'Performance_rate'
from total_revenue_category as trc
join average_value_category as avc on trc.category_id = avc.category_id
group by trc.category_id, trc.category_name, trc.Total_revenue;


-- Use CTEs to analyze monthly sales trends:
-- CTE 1: Calculate monthly sales totals
-- CTE 2: Add previous month comparison
-- Show growth percentage

with monthly_sales as (
    select month(so.shipped_date) as 'month', sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_sale'
    from sales.orders as so
    join sales.order_items as soi on so.order_id = soi.order_id
    where year(so.shipped_date) = 2022
    group by month(so.shipped_date)
)

select * from monthly_sales
order by month


-- Create a query that ranks products within each category:
-- Use ROW_NUMBER() to rank by price (highest first)
-- Use RANK() to handle ties
-- Use DENSE_RANK() for continuous ranking
-- Only show top 3 products per category

select pc.category_id, pc.category_name, pp.product_name, pp.list_price,
ROW_NUMBER() over (order by pp.list_price desc) as 'Row_Number',
RANK() over (order by pp.list_price desc) as 'Rank',
DENSE_RANK() over (order by pp.list_price desc) as 'Dense_Rank'
from production.categories as pc
join production.products as pp on pc.category_id = pp.category_id
where pp.product_id in (
    select top 3 product_id
    from production.products
    where category_id = pc.category_id
)


-- Rank customers by their total spending:
-- Calculate total spending per customer
-- Use RANK() for customer ranking
-- Use NTILE(5) to divide into 5 spending groups
-- Use CASE for tiers: 1="VIP", 2="Gold", 3="Silver", 4="Bronze", 5="Standard"

select sc.customer_id, sc.first_name, sc.last_name, sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'Total_Money_Spent',
rank() over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) as 'Rank',
Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) as 'ntile',
case
    when Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) = 1 then 'VIP'
    when Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) = 2 then 'Gold'
    when Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) = 3 then 'Silver'
    when Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) = 4 then 'Bronze'
    when Ntile(5) over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) = 5 then 'Standard'
end as 'Tiers'
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
join sales.order_items as soi on soi.order_id = so.order_id
GROUP by sc.customer_id, sc.first_name, sc.last_name


-- Create a comprehensive store performance ranking:
-- Rank stores by total revenue
-- Rank stores by number of orders
-- Use PERCENT_RANK() to show percentile performance

SELECT ss.store_id, ss.store_name, sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_revenue',
count(so.order_id) as 'No. of orders',
rank() over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) as 'revenue_rank',
rank() over (order by count(so.order_id) desc) as 'order_rank',
percent_rank() over (order by sum(soi.quantity * soi.list_price * (1 - soi.discount)) desc) as 'percent_rank'
from sales.stores as ss
join sales.orders as so on ss.store_id = so.store_id
join sales.order_items as soi on soi.order_id = so.order_id
GROUP by ss.store_id, ss.store_name


-- Create a PIVOT table showing product counts by category and brand:
-- Rows: Categories
-- Columns: Top 4 brands (Electra, Haro, Trek, Surly)
-- Values: Count of products

select *
from (
    select pp.product_id, pb.brand_name, pc.category_name
    from production.products as pp
    join production.brands as pb on pp.brand_id = pb.brand_id
    join production.categories as pc on pp.category_id = pc.category_id
) as ca
PIVOT(
    count(product_id)
    FOR brand_name in (
        [Electro],
        [Haro],
        [Trek],
        [Surly]
    )
) as pivot_t


-- Create a PIVOT showing monthly sales revenue by store:
-- Rows: Store names
-- Columns: Months (Jan through Dec)
-- Values: Total revenue
-- Add a total column

select store_name, "1" as "Jan", "2" as "Feb", "3" as "Mar", "4" as "Apr", "5" as "May", "6" as "Jun", "7" as "Jul",
    "8" as "Aug", "9" as "Sep", "10" as "Oct", "11" as "Nov", "12" as "Dec",
    ([1] + [2] + [3] + [4] + [5] + [6] + [7] + [8] + [9] + [10] + [11] + [12]) as 'Total_Revenue'
from (
    select ss.store_name, month(so.shipped_date) as 'month', sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_sale'
    from sales.orders as so
    join sales.order_items as soi on so.order_id = soi.order_id
    join sales.stores as ss on ss.store_id = so.store_id
    where year(so.shipped_date) = 2022
    group by ss.store_name, month(so.shipped_date)
) as ca
pivot(
    sum(total_sale)
    for month in ([1],[2],[3],[4],[5],[6],[7],[8],[9],[10],[11],[12])
) as pivot_t


-- PIVOT order statuses across stores:
-- Rows: Store names
-- Columns: Order statuses (Pending, Processing, Completed, Rejected)
-- Values: Count of orders

select store_name, "1" as 'Pending', "2" as 'Processing', "3" as 'Completed', "4" as 'Rejected'
from (
    select ss.store_name, so.order_id, so.order_status
    from sales.orders as so
    join sales.stores as ss on ss.store_id = so.store_id
) as ca
PIVOT(
    count(order_id)
    for order_status in (
        [1], [2], [3], [4]
    )
) as pivot_t


-- Create a PIVOT comparing sales across years:
-- Rows: Brand names
-- Columns: Years (2016, 2017, 2018)
-- Values: Total revenue
-- Include percentage growth calculations

SELECT *
from (
    select pb.brand_name, year(so.shipped_date) as 'year', sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'Total_revenue'
    from production.brands as pb
    join production.products as pp on pb.brand_id = pp.brand_id
    join sales.order_items as soi on pp.product_id = soi.product_id
    join sales.orders as so on so.order_id = soi.order_id
    group by pb.brand_name, year(so.shipped_date)
) as ca
pivot(
    sum(Total_revenue)
    for year in ([2016], [2017], [2018], [2022])
) as pivot_t


-- Use UNION to combine different product availability statuses:
-- Query 1: In-stock products (quantity > 0)
-- Query 2: Out-of-stock products (quantity = 0 or NULL)
-- Query 3: Discontinued products (not in stocks table)

select pp.product_id, pp.product_name, 'In_stock' as 'Status'
from production.products as pp
JOIN production.stocks as ps on pp.product_id = ps.product_id
where ps.quantity > 0
UNION
select pp.product_id, pp.product_name, 'Out_of_stock' as 'Status'
from production.products as pp
JOIN production.stocks as ps on pp.product_id = ps.product_id
where ps.quantity is NULL or ps.quantity = 0
UNION
select pp.product_id, pp.product_name, 'Discontinued_stock' as 'Status'
from production.products as pp
left JOIN production.stocks as ps on pp.product_id = ps.product_id
where ps.store_id is NULL;


-- Use INTERSECT to find loyal customers:
-- Find customers who bought in both 2017 AND 2018
-- Show their purchase patterns

SELECT sc.customer_id, sc.first_name, sc.last_name, so.order_id, soi.product_id, soi.quantity, soi.list_price
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
join sales.order_items as soi on soi.order_id = so.order_id
where year(order_date) = 2017
INTERSECT
SELECT sc.customer_id, sc.first_name, sc.last_name, so.order_id, soi.product_id, soi.quantity, soi.list_price
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
join sales.order_items as soi on soi.order_id = so.order_id
where year(order_date) = 2018


-- Use multiple set operators to analyze product distribution:
-- INTERSECT: Products available in all 3 stores
-- EXCEPT: Products available in store 1 but not in store 2
-- UNION: Combine above results with different labels

-- intersect
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 1
INTERSECT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 2
INTERSECT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 3

-- except
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 1
EXCEPT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 2

-- union combined
select *
from (
    select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 1
INTERSECT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 2
INTERSECT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 3
) as q1

UNION

select * 
from (
    select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 1
EXCEPT
select pp.product_id, pp.product_name
from production.products as pp
join production.stocks as ps on pp.product_id = ps.product_id
join sales.stores as ss on ps.store_id = ss.store_id
where ss.store_id = 2
) as q2


-- Complex set operations for customer retention:
-- Find customers who bought in 2016 but not in 2017 (lost customers)
-- Find customers who bought in 2017 but not in 2016 (new customers)
-- Find customers who bought in both years (retained customers)
-- Use UNION ALL to combine all three groups

-- lost customers
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016
EXCEPT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017

-- new customers
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017
EXCEPT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016

-- retained customers
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016
INTERSECT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017

-- union three groups
select *
from (
    select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016
EXCEPT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017
) as q1

union ALL

select *
from (
    select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017
EXCEPT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016
) as q2

UNION ALL

select *
from (
    select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2016
INTERSECT
select sc.customer_id, sc.first_name, sc.last_name
from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
where year(so.order_date) = 2017
) as q3
```
---