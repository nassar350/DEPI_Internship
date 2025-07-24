-- 16.1 Classify products by price as 'Premium' (greater than $500), 'Standard' ($100 to $500), or 'Budget' (less than $100), 
-- and show the count and average price for each category.

select pp.Name 'product', ppc.Name 'category', count(pp.ProductID) as 'product_count',
case
    when pp.ListPrice > 500 then 'Premium'
    when pp.ListPrice between 100 and 500 then 'Standard'
    when pp.ListPrice < 100 then 'Budget'
end as 'product class'
from Production.Product pp
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
group by pp.Name, ppc.Name, pp.ProductID, pp.ListPrice


-- 16.2 Classify employees by years of service as 'Veteran' (10+ years), 'Experienced' (5-10 years), 
-- 'Regular' (2-5 years), or 'New' (less than 2 years), and show salary statistics for each group.

select (pp.FirstName + ' ' + pp.LastName) as 'Full_name',
case 
    when DATEDIFF(year, he.HireDate, GETDATE()) > 10 then 'Veteran' 
    when DATEDIFF(year, he.HireDate, GETDATE()) between 5 and 10 then 'Experienced' 
    when DATEDIFF(year, he.HireDate, GETDATE()) between 2 and 5 then 'Regular'
    when DATEDIFF(year, he.HireDate, GETDATE()) < 2 then 'New'
end as 'employee class'
from Sales.Customer sc
join Person.Person pp on sc.CustomerID = pp.BusinessEntityID 
join HumanResources.Employee he on he.BusinessEntityID = pp.BusinessEntityID


-- 16.3 Classify orders by size as 'Large' (greater than $5000), 'Medium' ($1000 to $5000), or 'Small' (less than $1000), 
-- and show the percentage distribution.

select SalesOrderID, SubTotal,
case
    when SubTotal > 5000 then 'Large'
    when SubTotal between 1000 and 5000 then 'Medium'
    when SubTotal < 1000 then 'Small'
end as 'category'
from Sales.SalesOrderHeader
