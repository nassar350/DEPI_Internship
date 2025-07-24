-- 15.1 Create a view named vw_ProductCatalog with product ID, name, product number, category, 
-- subcategory, list price, standard cost, profit margin percentage, inventory level, and status (active/discontinued).

create view vw_productCatalog as (
    select pp.ProductID, pp.Name 'product', ppc.Name 'category', pc.Name 'subcategory',
    pp.ListPrice, pp.StandardCost, pi.Quantity, 
    case
        when pp.DiscontinuedDate is null then 'active'
        else 'discontinued'
    end as 'status'
    from Production.Product pp
    join Production.ProductSubcategory pc on pp.ProductSubcategoryID = pc.ProductSubcategoryID
    join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
    join Production.ProductInventory pi on pp.ProductID = pi.ProductID
    
)
GO


-- 15.2 Create a view named vw_SalesAnalysis with year, month, territory, total sales, order count, 
-- average order value, and top product name.

create view vw_salesAnalysis as (
    select YEAR(so.OrderDate) as 'year', MONTH(so.OrderDate) 'month', st.Name 'territory name', 
    sum(so.SubTotal) as 'total sales', count(so.SalesOrderID) as 'order_count',
    avg(SubTotal) as 'average_oeder_value', p.Name 'product'
    from Sales.SalesTerritory st
    join Sales.SalesOrderHeader so on st.TerritoryID = so.TerritoryID
    join Sales.SalesOrderDetail sd on so.SalesOrderID = sd.SalesOrderID
    join Production.Product p on p.ProductID = sd.ProductID
    group by YEAR(so.OrderDate), MONTH(so.OrderDate), st.Name, p.Name
)
go

-- 15.3 Create a view named vw_EmployeeDirectory with full name, job title, department, 
-- manager name, hire date, years of service, email, and phone.

create view vw_EmployeeDirectory as (
    select (p.FirstName + ' ' + p.LastName) as 'full_name', h.JobTitle, d.Name 'department',
    h.HireDate, datediff(year,h.HireDate, getdate()) as 'years of service', e.EmailAddress, pp.PhoneNumber
    from HumanResources.Employee h
    join Person.Person p on h.BusinessEntityID = p.BusinessEntityID
    join HumanResources.EmployeeDepartmentHistory ed on ed.BusinessEntityID = h.BusinessEntityID
    join HumanResources.Department d on d.DepartmentID = ed.DepartmentID
    join Person.EmailAddress e on e.BusinessEntityID = h.BusinessEntityID 
    join Person.PersonPhone pp on pp.BusinessEntityID = h.BusinessEntityID
)


-- 15.4 Write three different queries using the views you created, demonstrating practical business scenarios.

