-- 19.1 Rank products by sales within each category, showing product name, category, sales amount, rank, dense rank, 
-- and row number.

select p.ProductID, p.Name,ppc.Name , sum(OrderQty * UnitPrice) as 'sales',
RANK() over (order by sum(OrderQty * UnitPrice) desc),
dense_RANK() over (order by sum(OrderQty * UnitPrice) desc),
row_number() over (order by sum(OrderQty * UnitPrice) desc)
from Production.Product p 
join Sales.SalesOrderDetail so on p.ProductID = so.ProductID
join Production.ProductSubcategory pc on p.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
join Sales.SalesOrderHeader sh on sh.SalesOrderID = so.SalesOrderID
group by p.ProductID, p.Name, ppc.Name


-- 19.2 Show the running total of sales by month for 2013, displaying month, monthly sales, running total, 
-- and percentage of year-to-date.


-- 19.3 Show the three-month moving average of sales for each territory, displaying territory, month, sales, and moving average.

-- 19.4 Show month-over-month sales growth, displaying month, sales, previous month sales, growth amount, and growth percentage.

-- 19.5 Divide customers into four quartiles based on total purchase amount, showing customer name, 
-- total purchases, quartile, and quartile average.

