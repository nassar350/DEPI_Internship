
-- Count the total number of products in the database
select count(product_id) as 'No. of products' from production.products;

-- Find the average, minimum, and maximum price of all products.
select round(avg(list_price),2) as 'Average price', MIN(list_price) as 'Min price', MAX(list_price) as 'Max price'
from production.products;

-- Count how many products are in each category.
select pc.category_name, count(pp.product_id) as 'No. of Products' from production.categories as pc
join production.products as pp on pc.category_id = pp.category_id
group by pc.category_name;

-- Find the total number of orders for each store.
select ss.store_id, ss.store_name, count(so.order_id) as 'No. of orders' from sales.stores as ss
join sales.orders as so on ss.store_id = so.store_id
group by ss.store_id, ss.store_name;

-- Show customer first names in UPPERCASE and last names in lowercase for the first 10 customers.
select top 10 UPPER(first_name) as 'First_Name', LOWER(last_name) as 'Last_Name' from sales.customers;

-- Get the length of each product name. Show product name and its length for the first 10 products.
select top 10 product_name, LEN(product_name) as 'product_name length' from production.products;

-- Format customer phone numbers to show only the area code (first 3 digits) for customers 1-15.
select customer_id, first_name, last_name, LEFT(phone,3) as 'area_code', phone, email, street, city, state, zip_code 
from sales.customers
where customer_id between 1 and 15;

-- Show the current date and extract the year and month from order dates for orders 1-10.
select order_id, order_date, MONTH(order_date) as 'Month', YEAR(order_date) as 'Year' from sales.orders
where order_id between 1 and 10;

-- Join products with their categories. Show product name and category name for first 10 products.
select top 10 pp.product_name, pc.category_name from production.products as pp
join production.categories as pc on pp.category_id = pc.category_id;

-- Join customers with their orders. Show customer name and order date for first 10 orders.
select top 10 sc.first_name, sc.last_name, so.order_date from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id;

-- Show all products with their brand names, even if some products don't have brands.
-- Include product name, brand name (show 'No Brand' if null).
select pp.product_name, COALESCE(pb.brand_name, 'No Brand') as 'Brand_name' from production.products as pp
join production.brands as pb on pp.brand_id = pb.brand_id;

-- Find products that cost more than the average product price. Show product name and price.
select product_name, list_price from production.products
where list_price > (select avg(list_price) from production.products);

-- Find customers who have placed at least one order. Use a subquery with IN. Show customer_id and customer_name.
select customer_id, first_name, last_name from sales.customers
where customer_id in (select customer_id from sales.orders);

-- For each customer, show their name and total number of orders using a subquery in the SELECT clause.
select first_name, last_name, 
(select count(order_id) from sales.orders where customer_id = sc.customer_id) as 'No. of orders'
from sales.customers as sc;

--  Create a simple view called easy_product_list that shows product name, category name, and price. 
-- Then write a query to select all products from this view where price > 100.
create view easy_product_list AS
select pp.product_name, pc.category_name, pp.list_price from production.products as pp
join production.categories as pc on pp.category_id = pc.category_id;

select * from easy_product_list
where list_price > 100;

-- Create a view called customer_info that shows customer ID, full name (first + last), email, and city and state combined. 
-- Then use this view to find all customers from California (CA)
create view customer_info AS
select customer_id, (first_name + ' ' + last_name) as 'Full_name', email, (city + ', ' + state) as 'Location' 
from sales.customers;

select * from customer_info
where Location like '%CA';

-- Find all products that cost between $50 and $200. Show product name and price, ordered by price from lowest to highest.
select product_name, list_price from production.products
where list_price between 50 and 200
order by list_price;

-- Count how many customers live in each state. Show state and customer count, ordered by count from highest to lowest.
select state, count(customer_id) as 'NO. of Customers' from sales.customers
group by state
order by count(customer_id) desc;

-- Find the most expensive product in each category. Show category name, product name, and price.
select pc.category_name, pp.product_name, pp.list_price from production.products as pp
join production.categories as pc on pp.category_id = pc.category_id
where list_price = (select max(list_price) from production.products where category_id = pc.category_id);

-- Show all stores and their cities, including the total number of orders from each store. Show store name, city, and order count.
select ss.store_name, ss.city, count(so.order_id) as 'No. of Orders' from sales.stores as ss
join sales.orders as so on ss.store_id = so.store_id
group by ss.store_name, ss.city;