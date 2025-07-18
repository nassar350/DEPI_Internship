-- 2. Product Price Threshold Report
-- Create a query using variables to count how many products cost more than $1500. 
-- Store the threshold price in a variable and display both the threshold and count in a formatted message.

declare @price int = 1500
declare @product_num int 

select @product_num = count(product_id) from production.products
where list_price > 1500
 
print 'There is ' + CAST(@product_num as nvarchar(20)) + ' product that costs more than ' + cast(@price as nvarchar(20))