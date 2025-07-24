-- 3.1 Count the number of products by category, ordered by count descending.

select pc.Name as category, count(pp.ProductID) as 'No._of_products'
from Production.Product pp
join Production.ProductSubcategory ps on pp.ProductSubcategoryID = ps.ProductSubcategoryID
join Production.ProductCategory pc on pc.ProductCategoryID = ps.ProductCategoryID
group by pc.Name
order by count(pp.ProductID) desc


-- 3.2 Show the average list price by product subcategory, including only subcategories with more than five products.

select ps.Name, avg(pp.ListPrice) as 'Average_price'
from Production.Product pp
join Production.ProductSubcategory ps on pp.ProductSubcategoryID = ps.ProductSubcategoryID
join Production.ProductCategory pc on pc.ProductCategoryID = ps.ProductCategoryID
group by ps.Name having count(pp.ProductID) > 5


-- 3.3 List the top 10 customers by total order count, including customer name.

select top 10 (pp.FirstName + ' ' + pp.LastName) as 'full_name', count(ss.SalesOrderID) as 'order_count'
from Sales.Customer sc
join Person.Person pp on sc.CustomerID = pp.BusinessEntityID
join Sales.SalesOrderHeader ss on ss.TerritoryID = sc.TerritoryID
group by pp.FirstName, pp.LastName
order by count(ss.SalesOrderID) desc


-- 3.4 Show monthly sales totals for 2013, displaying the month name and total amount.

select MONTH(OrderDate) as 'Month', sum(TotalDue) as 'total_amount'
from Sales.SalesOrderHeader
where YEAR(OrderDate) = 2013
group by month(OrderDate)
order by month(OrderDate)