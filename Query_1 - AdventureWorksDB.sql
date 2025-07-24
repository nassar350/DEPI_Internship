-- 1.1 List all employees hired after January 1, 2012, 
-- showing their ID, first name, last name, and hire date, ordered by hire date descending.

select pp.BusinessEntityID, pp.FirstName, pp.LastName, he.HireDate
from HumanResources.Employee he
join Person.Person pp on pp.BusinessEntityID = he.BusinessEntityID
where he.HireDate > '2012-01-01'
order by he.HireDate DESC


-- 1.2 List products with a list price between $100 and $500, 
-- showing product ID, name, list price, and product number, ordered by list price ascending.

select ProductID, Name, ListPrice, ProductNumber 
from Production.Product
where ListPrice between 100 and 500
order by ListPrice asc


-- 1.3 List customers from the cities 'Seattle' or 'Portland', 
-- showing customer ID, first name, last name, and city, using appropriate joins.

select pp.BusinessEntityID, pp.FirstName, pp.LastName, a.City
from Person.Person as pp
join Person.BusinessEntity pe on pp.BusinessEntityID = pe.BusinessEntityID
join Person.BusinessEntityAddress pa on pa.BusinessEntityID = pp.BusinessEntityID
join Person.Address a on a.AddressID = pa.AddressID
where a.City in ('Seattle', 'Portland')


-- 1.4 List the top 15 most expensive products currently being sold, 
-- showing name, list price, product number, and category name, excluding discontinued products.

select top 15 pp.Name, pp.ListPrice, pp.ProductNumber, pc.Name as category
from Production.Product pp
join Production.ProductSubcategory ps on pp.ProductSubcategoryID = ps.ProductSubcategoryID
join Production.ProductCategory pc on pc.ProductCategoryID = ps.ProductCategoryID
where ListPrice is not null
order by ListPrice DESC