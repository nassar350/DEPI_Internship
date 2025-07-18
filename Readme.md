## 📈 SQL Queries on StoreDB

```sql
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


-- 2. Product Price Threshold Report
-- Create a query using variables to count how many products cost more than $1500. 
-- Store the threshold price in a variable and display both the threshold and count in a formatted message.

declare @price int = 1500
declare @product_num int 

select @product_num = count(product_id) from production.products
where list_price > 1500
 
print 'There is ' + CAST(@product_num as nvarchar(20)) + ' product that costs more than ' + cast(@price as nvarchar(20))



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



-- Global Variables Information
-- Create a query that displays the current server name, SQL Server version, and the number of rows affected by the last statement.
-- Use appropriate global variables.

select @@SERVERNAME as server_name,
    @@VERSION as server_version,
    @@ROWCOUNT as last_row_affected


-- 5.Write a query that checks the inventory level for product ID 1 in store ID 1.
-- Use IF statements to display different messages based on stock levels:#
-- If quantity > 20: Well stocked
-- If quantity 10-20: Moderate stock
-- If quantity < 10: Low stock - reorder needed

declare @quantity INT

select @quantity = quantity from production.stocks
where product_id = 1 and store_id = 1

if @quantity > 20
BEGIN
    print 'Well Stocked'
END
ELSE IF @quantity <= 20 and @quantity >= 10
BEGIN
    print 'Moderate Stock'
END
ELSE IF @quantity < 10
BEGIN
    print 'Low Stock - reorder needed'
END


-- 6.Create a WHILE loop that updates low-stock items (quantity < 5) in batches of 3 products at a time. 
-- Add 10 units to each product and display progress messages after each batch.

declare @rows_affected int = 1

while @rows_affected > 0
BEGIN
    UPDATE top (3) production.stocks
    set quantity = quantity + 10
    where quantity < 5

    set @rows_affected = @@rowcount
    
    print cast(@rows_affected as varchar(20)) + ' Row Affected'
END



-- 7. Product Price Categorization
-- Write a query that categorizes all products using CASE WHEN based on their list price:
-- Under $300: Budget
-- $300-$800: Mid-Range
-- $801-$2000: Premium
-- Over $2000: Luxury

select product_id, product_name, list_price,
case
    when list_price < 300 then 'Budget'
    when list_price >= 300 and list_price <= 800 then 'Mid-Range'
    when list_price >= 801 and list_price <= 2000 then 'Premium'
    when list_price > 2000 then 'Luxury'
end as 'Category'
from production.products



-- 8. Customer Order Validation
-- Create a query that checks if customer ID 5 exists in the database. 
-- If they exist, show their order count. If not, display an appropriate message.

declare @customer_id int = 5
declare @order_count int

if exists (select 1 from sales.customers where customer_id = @customer_id)
BEGIN
    select @order_count = count(order_id) from sales.orders where customer_id = @customer_id
    print 'Customer order count is ' + cast(@order_count as varchar(20))
END
else
BEGIN
    print 'Customer Not Found'
END



-- 9. Shipping Cost Calculator Function
-- Create a scalar function named CalculateShipping that takes an order total as input and returns shipping cost:
-- Orders over $100: Free shipping ($0)
-- Orders $50-$99: Reduced shipping ($5.99)
-- Orders under $50: Standard shipping ($12.99)

create FUNCTION CalculateShipping (@order_total decimal(10,2))
RETURNS decimal(10,2)
as 
BEGIN
    declare @shipping decimal(10,2)

    if @order_total > 100
    BEGIN
        set @shipping = 0
    END
    else if @order_total >= 50 and @order_total <= 99
    BEGIN
        set @shipping = 5.99
    END
    else if @order_total < 50
    BEGIN
        set @shipping = 12.99
    END
    return @shipping
END;
GO

select dbo.CalculateShipping(15) as 'shipping';



-- Product Category Function
-- Create an inline table-valued function named GetProductsByPriceRange that accepts minimum and maximum price parameters 
-- returns all products within that price range with their brand and category information.

create function GetproductsbypriceRange(@minimum_price decimal(10,2), @maximum_price decimal(10,2))
returns TABLE
AS
return
(
    select pp.product_id, pp.product_name, pb.brand_name, pc.category_name
    from production.brands as pb
    join production.products as pp on pp.brand_id = pb.brand_id
    join production.categories as pc on pp.category_id = pc.category_id
    where pp.list_price >= @minimum_price and pp.list_price <= @maximum_price
);
GO

select * from GetproductsbypriceRange(1.50,5000);



-- Customer Sales Summary Function
-- Create a multi-statement function named GetCustomerYearlySummary that takes a customer ID and 
-- returns a table with yearly sales data including total orders, total spent, and average order value for each year.

create FUNCTION GetcustomerYearlySummary(@customer_id int)
returns @summary_table TABLE
(
    name nvarchar(255),
    "No._of_orders" int,
    year INT,
    "total_spent" decimal(10,2),
    "average_spent" decimal(10,2)
)
AS
BEGIN
    INSERT into @summary_table
        select (sc.first_name + ' ' + sc.last_name) as 'name', count(so.order_id) as 'No._of_orders',
        year(order_date) as 'year',
        SUM(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_spent',
        avg(soi.quantity * soi.list_price * (1 - soi.discount)) as 'average_spent'
        from sales.customers as sc
        join sales.orders as so on sc.customer_id = so.customer_id
        join sales.order_items as soi on soi.order_id = so.order_id
        where sc.customer_id = @customer_id
        group by sc.first_name, sc.last_name, year(order_date)
    RETURN
end
go

select * from GetcustomerYearlySummary(10);



-- Discount Calculation Function
-- Write a scalar function named CalculateBulkDiscount that determines discount percentage based on quantity:
-- 1-2 items: 0% discount
-- 3-5 items: 5% discount
-- 6-9 items: 10% discount
-- 10+ items: 15% discount

create function CalculateBulkDiscount(@item_numbers int)
returns nvarchar(255)
AS
BEGIN
    declare @discount nvarchar(255)

    if @item_numbers >= 1 and @item_numbers <= 2
    BEGIN
    set @discount = '0% discount'
    END
    else if @item_numbers >= 3 and @item_numbers <= 5
    BEGIN
    set @discount = '5% doscount'
    END
    else if @item_numbers >= 6 and @item_numbers <= 9
    BEGIN
    set @discount = '10% discount'
    END
    else if @item_numbers >= 10
    BEGIN
    set @discount = '15% discount'
    END

    return @discount

END
GO


SELECT dbo.CalculateBulkDiscount(10) as 'discount_ratio';



-- 13. Customer Order History Procedure#
-- Create a stored procedure named sp_GetCustomerOrderHistory that accepts a customer ID and optional start/end dates. 
-- Return the customer's order history with order totals calculated.

create PROCEDURE sp_GetCustomerOrderHistory
    @customer_id INT,
    @start_date date = NULL,
    @end_date date = NULL
AS
BEGIN
    select (sc.first_name + ' ' + sc.last_name) as 'name', so.order_id,
    SUM(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_spent'
    from sales.customers as sc
    join sales.orders as so on sc.customer_id = so.customer_id
    join sales.order_items as soi on soi.order_id = so.order_id
    where sc.customer_id = @customer_id
    and (@start_date IS NULL or order_date >= @start_date)
    and (@end_date IS NULL or order_date <= @end_date)
    group by sc.first_name, sc.last_name, so.order_id
END
GO


EXEC sp_GetCustomerOrderHistory @customer_id = 1;



-- 14. Inventory Restock Procedure#
-- Write a stored procedure named sp_RestockProduct with input parameters for store ID, product ID, and restock quantity. 
-- Include output parameters for old quantity, new quantity, and success status.

create PROCEDURE sp_RestockProduct
    @store_id int, 
    @product_id int,
    @restock_quantity int,
    @old_quantity int OUTPUT,
    @new_quantity int output,
    @success_status nvarchar(255) OUTPUT
as 
BEGIN
    select @old_quantity = quantity from production.stocks where store_id = @store_id and product_id = @product_id

    update production.stocks
    set quantity = quantity + @restock_quantity
    where store_id = @store_id and product_id = @product_id

    if @@ROWCOUNT > 0
    BEGIN
        set @success_status = 'Success'
    END
    else
    BEGIN
        set @success_status = 'Failed'
    END

    select @new_quantity = quantity from production.stocks where store_id = @store_id and product_id = @product_id

END;
GO

DECLARE @old_quantity INT
DECLARE @new_quantity INT
DECLARE @success_status NVARCHAR(255)

exec sp_RestockProduct @store_id = 1, @product_id = 1, @restock_quantity = 5,
    @old_quantity = @old_quantity OUTPUT, @new_quantity = @new_quantity OUTPUT, @success_status = @success_status OUTPUT;

select @old_quantity as Old_Quantity, @new_quantity as New_Quantity, @success_status as Success_Status;
GO



-- 15. Order Processing Procedure
-- Create a stored procedure named sp_ProcessNewOrder 
-- that handles complete order creation with proper transaction control and error handling. 
-- Include parameters for customer ID, product ID, quantity, and store ID.

CREATE PROCEDURE sp_ProcessNewOrder
    @customer_ID INT,
    @product_ID INT,
    @store_ID INT,
    @quantity INT
AS
BEGIN
    declare @available_quantity int 

    if exists (select 1 from production.stocks where store_id = @store_ID and product_id = @product_ID)
    BEGIN
        select @available_quantity = quantity from production.stocks where store_id = @store_ID and product_id = @product_ID

        if @available_quantity >= @quantity
        BEGIN
            -- update quantity
            update production.stocks
            set quantity = quantity - @quantity
            where store_id = @store_ID and product_id = @product_ID
            -- add order
            insert into sales.orders (customer_id, order_date, order_status, store_id)
            values (@customer_ID, GETDATE(), 1, @store_ID)
        END
        else
        BEGIN
            print 'there is no available quantity in the stock'
        END
    END
    ELSE
    BEGIN
        print 'Product is not available in the store'
    end
END;
go



-- 16. Dynamic Product Search Procedure
-- Write a stored procedure named sp_SearchProducts that builds dynamic SQL based on optional parameters: 
--     product name search term, category ID, minimum price, maximum price, and sort column.

create PROCEDURE sp_SearchProducts
    @product_name nvarchar(255) = NULL,
    @category_id int = NULL,
    @minimum_price decimal(10, 2) = NULL,
    @maximum_price decimal(10, 2) = NULL,
    @sort_column nvarchar(255) = null,
    @sort_type NVARCHAR(5) = 'asce'
AS
BEGIN
    declare @query nvarchar(600) = 'select * from production.products'
    declare @is_where int = 0

    if @product_name is not NULL
    BEGIN
        set @query = @query + ' where product_name = ' + @product_name
        set @is_where = 1
    END

    if @category_id is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and category_id = ' + cast(@category_id as varchar(50))
    END

    if @category_id is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where category_id = ' + cast(@category_id as varchar(50))
        set @is_where = 1
    END

    if @minimum_price is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and list_price = ' + cast(@minimum_price as varchar(50))
    END

    if @minimum_price is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where list_price = ' + cast(@minimum_price as varchar(50))
        set @is_where = 1
    END

    if @maximum_price is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and list_price = '+ cast(@maximum_price as varchar(50))
    END

    if @maximum_price is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where list_price = '+ cast(@maximum_price as varchar(50))
        set @is_where = 1
    END

    if @sort_column is NOT NULL
    BEGIN
        set @query = @query + ' order by ' + @sort_column + ' ' + @sort_type
    END

    select @query as query 
END;
go

exec sp_SearchProducts @category_id = 2




```
---