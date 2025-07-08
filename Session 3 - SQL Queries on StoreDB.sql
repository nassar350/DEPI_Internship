
-- List all products with list price greater than 1000
select * from production.products
where list_price > 1000;

-- Get customers from "CA" or "NY" states
select * from sales.customers
where state = 'CA' or state = 'NY';

-- Retrieve all orders placed in 2023
select * from sales.orders
where year(order_date) = 2023;

-- Show customers whose emails end with @gmail.com
select * from sales.customers
where email like '%@gmail.com';

-- Show all inactive staff
select * from sales.staffs
where active = 0;

-- List top 5 most expensive products
select top 5 * from production.products
order by list_price desc;

-- Show latest 10 orders sorted by date
select top 10 * from sales.orders
order by order_date desc;

-- Retrieve the first 3 customers alphabetically by last name
select top 3 * from sales.customers
order by last_name;

-- Find customers who did not provide a phone number
select * from sales.customers
where phone IS NULL;

-- Show all staff who have a manager assigned
select * from sales.staffs
where manager_id IS NOT NULL;

-- Count number of products in each category
select pc.category_name, count(pp.product_id) as 'No. of Products' from production.categories as pc
join production.products as pp on pc.category_id = pp.category_id
group by pc.category_name;

-- Count number of customers in each state
select state, count(customer_id) as 'No. of Customers' from sales.customers
group by state;

-- Get average list price of products per brand
select pb.brand_name, round(avg(pp.list_price),2) as 'Average price' from production.brands as pb
join production.products as pp on pb.brand_id = pp.brand_id
GROUP by pb.brand_name;

-- Show number of orders per staff
select ss.first_name, ss.last_name, count(so.order_id) as 'No. of orders' from sales.staffs as ss
join sales.orders as so on ss.staff_id = so.staff_id
group by  ss.first_name, ss.last_name;

-- Find customers who made more than 2 orders
select first_name, last_name from sales.customers
where customer_id in (
    select customer_id from sales.orders
    group by customer_id
    having count(order_id) > 2
);

select sc.first_name, sc.last_name from sales.customers as sc
join sales.orders as so on sc.customer_id = so.customer_id
GROUP by sc.customer_id, sc.first_name, sc.last_name
having count(so.order_id) > 2;

-- Products priced between 500 and 1500
select * from production.products
where list_price BETWEEN 500 and 1500;

-- Customers in cities starting with "S"
select * from sales.customers
where city like 'S%';

-- Orders with order_status either 2 or 4
select * from sales.orders
where order_status in (2, 4);

-- Products from category_id IN (1, 2, 3)
select * from production.products
where category_id in (1, 2, 3);

-- Staff working in store_id = 1 OR without phone number
select * from sales.staffs
where store_id = 1 or phone IS NULL;