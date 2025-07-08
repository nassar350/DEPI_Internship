## 📈 SQL Queries on StoreDB

```sql
-- List all products with list price greater than 1000
SELECT * FROM production.products WHERE list_price > 1000;

-- Get customers from "CA" or "NY" states
SELECT * FROM sales.customers WHERE state = 'CA' OR state = 'NY';

-- Retrieve all orders placed in 2023
SELECT * FROM sales.orders WHERE YEAR(order_date) = 2023;

-- Show customers whose emails end with @gmail.com
SELECT * FROM sales.customers WHERE email LIKE '%@gmail.com';

-- Show all inactive staff
SELECT * FROM sales.staffs WHERE active = 0;

-- List top 5 most expensive products
SELECT TOP 5 * FROM production.products ORDER BY list_price DESC;

-- Show latest 10 orders sorted by date
SELECT TOP 10 * FROM sales.orders ORDER BY order_date DESC;

-- Retrieve the first 3 customers alphabetically by last name
SELECT TOP 3 * FROM sales.customers ORDER BY last_name;

-- Find customers who did not provide a phone number
SELECT * FROM sales.customers WHERE phone IS NULL;

-- Show all staff who have a manager assigned
SELECT * FROM sales.staffs WHERE manager_id IS NOT NULL;

-- Count number of products in each category
SELECT pc.category_name, COUNT(pp.product_id) AS 'No. of Products'
FROM production.categories AS pc
JOIN production.products AS pp ON pc.category_id = pp.category_id
GROUP BY pc.category_name;

-- Count number of customers in each state
SELECT state, COUNT(customer_id) AS 'No. of Customers'
FROM sales.customers
GROUP BY state;

-- Get average list price of products per brand
SELECT pb.brand_name, ROUND(AVG(pp.list_price), 2) AS 'Average price'
FROM production.brands AS pb
JOIN production.products AS pp ON pb.brand_id = pp.brand_id
GROUP BY pb.brand_name;

-- Show number of orders per staff
SELECT ss.first_name, ss.last_name, COUNT(so.order_id) AS 'No. of orders'
FROM sales.staffs AS ss
JOIN sales.orders AS so ON ss.staff_id = so.staff_id
GROUP BY ss.first_name, ss.last_name;

-- Find customers who made more than 2 orders
SELECT first_name, last_name FROM sales.customers
WHERE customer_id IN (
    SELECT customer_id FROM sales.orders
    GROUP BY customer_id
    HAVING COUNT(order_id) > 2
);

SELECT sc.first_name, sc.last_name
FROM sales.customers AS sc
JOIN sales.orders AS so ON sc.customer_id = so.customer_id
GROUP BY sc.customer_id, sc.first_name, sc.last_name
HAVING COUNT(so.order_id) > 2;

-- Products priced between 500 and 1500
SELECT * FROM production.products WHERE list_price BETWEEN 500 AND 1500;

-- Customers in cities starting with "S"
SELECT * FROM sales.customers WHERE city LIKE 'S%';

-- Orders with order_status either 2 or 4
SELECT * FROM sales.orders WHERE order_status IN (2, 4);

-- Products from category_id IN (1, 2, 3)
SELECT * FROM production.products WHERE category_id IN (1, 2, 3);

-- Staff working in store_id = 1 OR without phone number
SELECT * FROM sales.staffs WHERE store_id = 1 OR phone IS NULL;
```

---
