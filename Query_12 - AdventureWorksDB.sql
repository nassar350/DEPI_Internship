-- 12.1 List product name, category, subcategory, and vendor name for products that have been purchased from vendors.

select pp.Name 'product', ppc.Name 'category', pc.Name 'subcategory', v.Name as 'Vendor name'
from Production.Product pp
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
join Purchasing.ProductVendor pv on pv.ProductID = pp.ProductID
join Purchasing.Vendor v on v.BusinessEntityID = pv.BusinessEntityID


-- 12.2 Show order details including order ID, customer name, salesperson name, territory name, product name, quantity, 
-- and line total.

select soh.SalesOrderID, (pp.FirstName + ' ' + pp.LastName) as 'customer name',
st.Name as 'territory name', p.Name as 'product name', sd.OrderQty as 'quantity'
from Sales.SalesOrderHeader soh
join Sales.Customer sc on sc.CustomerID = soh.CustomerID
join Person.Person pp on sc.PersonID = pp.BusinessEntityID
join Sales.SalesTerritory st on st.TerritoryID = soh.TerritoryID
join Sales.SalesOrderDetail sd on sd.SalesOrderID = soh.SalesOrderID
join Production.Product p on p.ProductID = sd.ProductID 


-- 12.3 List employees with their sales territories, 
-- including employee name, job title, territory name, territory group, and sales year-to-date.

select (pp.FirstName + ' ' + pp.LastName) as 'employee name', he.JobTitle, st.Name 'territory name',
so.OrderDate as 'sales'
from HumanResources.Employee he
join Sales.SalesPerson ssp on ssp.BusinessEntityID = he.BusinessEntityID
join Sales.SalesTerritory st on st.TerritoryID = ssp.TerritoryID
join Person.Person pp on pp.BusinessEntityID = he.BusinessEntityID
join Sales.SalesOrderHeader so on so.TerritoryID  = st.TerritoryID