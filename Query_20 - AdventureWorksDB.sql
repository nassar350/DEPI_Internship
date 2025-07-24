-- 20.1 Create a pivot table showing product categories as rows and years (2011-2014) as columns, 
-- displaying sales amounts with totals.

select * from (
    select ppc.name, sum(SubTotal) as 'total', year(OrderDate) as year
    from Production.Product p 
    join Sales.SalesOrderDetail so on p.ProductID = so.ProductID
    join Production.ProductSubcategory pc on p.ProductSubcategoryID = pc.ProductSubcategoryID
    join Production.ProductCategory ppc on pc.ProductCategoryID = ppc.ProductCategoryID
    join Sales.SalesOrderHeader sh on sh.SalesOrderID = so.SalesOrderID
    group by p.productid, ppc.name, year(OrderDate)
) t
PIVOT (
    sum(total) for
    [year] in (
        "2011",
        "2012",
        "2013",
        "2014"
    )
) as PIVOT_t


-- 20.2 Create a pivot table showing departments as rows and gender as columns, 
-- displaying employee count by department and gender.

SELECT * from (
    select d.Name, h.Gender, h.BusinessEntityID
    from HumanResources.Employee h
    join Person.Person p on h.BusinessEntityID = p.BusinessEntityID
    join HumanResources.EmployeeDepartmentHistory ed on ed.BusinessEntityID = h.BusinessEntityID
    join HumanResources.Department d on d.DepartmentID = ed.DepartmentID
    join Person.EmailAddress e on e.BusinessEntityID = h.BusinessEntityID 
    join Person.PersonPhone pp on pp.BusinessEntityID = h.BusinessEntityID
) t
pivot (
    count(BusinessEntityID) FOR
    gender in ("Male", "Female")
)as pivot_t


-- 20.3 Create a dynamic pivot table for quarterly sales, automatically handling an unknown number of quarters.

