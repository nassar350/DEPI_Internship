-- 2.1 List products whose name contains 'Mountain' and color is 'Black', 
-- showing product ID, name, color, and list price.

select pp.ProductID, pp.Name, pp.Color, pp.ListPrice
from Production.Product pp
where pp.Name like '%Mountain' and pp.Color = 'Black'


-- 2.2 List employees born between January 1, 1970, and December 31, 1985, 
-- showing full name, birth date, and age in years.

select (pp.FirstName + ' ' + pp.MiddleName + ' ' + pp.LastName) as 'full_name',
he.BirthDate, Datediff(year,he.BirthDate, GETDATE()) as 'Age'
from HumanResources.Employee he
join Person.Person pp on pp.BusinessEntityID = he.BusinessEntityID
where he.BirthDate BETWEEN '1970-01-01' and '1985-12-31'


-- 2.3 List orders placed in the fourth quarter of 2013, 
-- showing order ID, order date, customer ID, and total due.

select ss.SalesOrderID, ss.OrderDate, ss.CustomerID, ss.Totaldue 
from Sales.SalesOrderHeader as ss
where MONTH(ss.OrderDate) >= 10 and MONTH(ss.OrderDate) <= 12 and YEAR(ss.OrderDate) = 2013


-- 2.4 List products with a null weight but a non-null size, 
-- showing product ID, name, weight, size, and product number.

select ProductID, Name, WeightUnitMeasureCode, Size, ProductNumber
from Production.Product
where WeightUnitMeasureCode is null and size is not NULL