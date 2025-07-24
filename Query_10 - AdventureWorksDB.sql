-- 10.1 Calculate the total sales amount by year from 2011 to 2014, showing year, total sales, average order value, and order count.

select year(OrderDate) as 'year', sum(SubTotal) as 'total_sale', avg(SubTotal) as 'average_order_value', 
count(SalesOrderID) as 'no. of orders'
from Sales.SalesOrderHeader
group by year(OrderDate)
having YEAR(OrderDate) >= 2011 and year(OrderDate) <= 2014
order by [year]


-- 10.2 For each customer, show customer ID, total orders, total amount, average order value, first order date, and last order date.

select sc.CustomerID, sum(ss.SubTotal) as 'total_amount', avg(ss.SubTotal) as 'average_amount',
min(ss.OrderDate) as 'first_order_date', max(ss.OrderDate) as 'last_order_date'
from Sales.SalesOrderHeader as ss
join Sales.Customer sc on ss.CustomerID = sc.CustomerID
join Person.Person pp on sc.PersonID = pp.BusinessEntityID
GROUP by sc.CustomerID


-- 10.3 List the top 20 products by total sales amount, including product name, category, total quantity sold, and total revenue.

select top 20 pp.Name, ppc.Name, sum(o.OrderQty) as 'total_quantity', sum(o.UnitPrice) as 'total_revenue'
from Production.Product pp
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
join Sales.SalesOrderDetail as o on o.ProductID = pp.ProductID 
group by pp.ProductID ,pp.Name, ppc.Name
order by sum(o.UnitPrice) DESC


-- 10.4 Show sales amount by month for 2013, displaying the month name, sales amount, and percentage of the yearly total.

select month(OrderDate) as 'month', sum(SubTotal) as 'sales_amount',
(sum(SubTotal) / (select sum(SubTotal) from Sales.SalesOrderHeader where YEAR(OrderDate) = 2013) * 100) as 'percentage of year'
from Sales.SalesOrderHeader
where YEAR(OrderDate) = 2013
group by MONTH(OrderDate)
order by month