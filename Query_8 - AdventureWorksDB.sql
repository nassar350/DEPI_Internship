-- 8.1 Update the ModifiedDate to the current date for all products where ListPrice is greater than $1000 and SellEndDate is null.

update Production.Product
set ModifiedDate = GETDATE()
where ListPrice > 1000 and SellEndDate is NULL


-- 8.2 Increase the ListPrice by 15% for all products in the 'Bikes' category and update the ModifiedDate.

UPDATE pp
set ListPrice = ListPrice + (ListPrice * 0.15), ModifiedDate = GETDATE()
from Production.Product pp 
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on ppc.ProductCategoryID = pc.ProductCategoryID
where ppc.Name = 'Bikes'


-- 8.3 Update the JobTitle to 'Senior' plus the existing job title for employees hired before January 1, 2010.

update HumanResources.Employee
set JobTitle = 'Senior'
where HireDate < '2010-01-01'