-- 18.1 Create a recursive query to show the complete employee hierarchy, 
-- including employee name, manager name, hierarchy level, and path.
-- can not be solved

select (pp.FirstName + ' ' + pp.LastName) as 'name', sc.PersonID
from Sales.Customer SC
join Person.Person pp on sc.PersonID = pp.BusinessEntityID


-- 18.2 Create a query to compare year-over-year sales for each product, 
-- showing product, sales for 2013, sales for 2014, growth percentage, and growth category.

select p.ProductID, p.Name, YEAR(OrderDate), sum(OrderQty * UnitPrice) as 'sales'
from Production.Product p 
join Sales.SalesOrderDetail so on p.ProductID = so.ProductID
join Sales.SalesOrderHeader sh on sh.SalesOrderID = so.SalesOrderID
where YEAR(OrderDate) in (2013, 2014)
group by p.ProductID, p.Name , YEAR(OrderDate)