-- 14.1 List products with above-average list price, showing product ID, name, list price, and price difference from the average.

select ProductID, Name, ListPrice, ABS(ListPrice - (select avg(ListPrice) from Production.Product)) as 'diff from average'
from Production.Product
group by ProductID, Name, ListPrice
having ListPrice > (select avg(ListPrice) from Production.Product)


-- 14.2 List customers who bought products from the 'Mountain' category, showing customer name, total orders, and total amount spent.

select (p.FirstName + ' ' + p.LastName) as 'name', count(h.SalesOrderID) as 'total_order', sum(h.SubTotal) as 'amount spent'
from Production.Product pp
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
join Sales.SalesOrderDetail as o on o.ProductID = pp.ProductID 
join Sales.SalesOrderHeader as h on h.SalesOrderID = o.SalesOrderID
join Sales.Customer sc on sc.CustomerID = h.CustomerID
join Person.Person p on p.BusinessEntityID = sc.PersonID
where ppc.Name = 'Mountain'
group by p.BusinessEntityID, p.FirstName, p.LastName


-- 14.3 List products that have been ordered by more than 100 different customers, 
-- showing product name, category, and unique customer count.

select pp.Name 'product', ppc.Name 'category', count(sc.CustomerID) as 'customer_count'
from Production.Product pp
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
join Sales.SalesOrderDetail as o on o.ProductID = pp.ProductID 
join Sales.SalesOrderHeader as h on h.SalesOrderID = o.SalesOrderID
join Sales.Customer sc on sc.CustomerID = h.CustomerID
join Person.Person p on p.BusinessEntityID = sc.PersonID
group by pp.Name, ppc.Name
having count(sc.CustomerID) > 100


-- 14.4 For each customer, show their order count and their rank among all customers.

select (p.FirstName + ' ' + p.LastName) as 'name', count(h.SalesOrderID) as 'order_count',
RANK() over (order by count(h.SalesOrderID) desc) as 'rank'
from Sales.SalesOrderHeader h
join Sales.Customer sc on sc.CustomerID = h.CustomerID
join Person.Person p on p.BusinessEntityID = sc.PersonID
group by p.BusinessEntityID, p.FirstName, p.LastName