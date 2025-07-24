-- 13.1 List all products with their total sales, including those never sold. 
-- Show product name, category, total quantity sold (zero if never sold), and total revenue (zero if never sold).

select pp.Name 'product', ppc.Name 'category', isnull(sum(o.OrderQty), 0) as 'total_quantity',
isnull(sum(o.UnitPrice), 0) as 'total_revenue'
from Production.Product pp
left join Sales.SalesOrderDetail as o on o.ProductID = pp.ProductID 
join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
group by pp.ProductID, pp.Name, ppc.Name



-- 13.2 Show all sales territories with their assigned employees, including unassigned territories. 
-- Show territory name, employee name (null if unassigned), and sales year-to-date.

select st.Name 'territory name', isnull((pp.FirstName + ' ' + pp.LastName), 'Null') as 'employee name',
OrderDate as 'sales'
from Sales.SalesTerritory ST
left join Sales.SalesPerson sp on sp.TerritoryID = st.TerritoryID
left join HumanResources.Employee e on e.BusinessEntityID = sp.BusinessEntityID
join Person.Person pp on pp.BusinessEntityID = sp.BusinessEntityID
join Sales.SalesOrderHeader so on so.TerritoryID = st.TerritoryID


-- 13.3 Show the relationship between vendors and product categories, 
-- including vendors with no products and categories with no vendors.

select pp.Name 'product', ppc.Name 'category', v.Name as 'Vendor name'
from Production.Product pp
full join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
full join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
full join Purchasing.ProductVendor pv on pv.ProductID = pp.ProductID
full join Purchasing.Vendor v on v.BusinessEntityID = pv.BusinessEntityID