-- 4.1 Find all products launched in the same year as 'Mountain-100 Black, 42'. 
-- Show product ID, name, sell start date, and year.

select ProductID, Name, SellStartDate, YEAR(SellStartDate) as 'year'
from Production.Product
where year(SellStartDate) = (
    select YEAR(SellStartDate) from Production.Product where name = 'Mountain-100 Black, 42'
)


-- 4.2 Find employees who were hired on the same date as someone else. 
-- Show employee names, shared hire date, and the count of employees hired that day.

select (pp.FirstName + ' ' + pp.LastName) as 'Name', he.HireDate, 
COUNT(*) over (partition by HireDate) as 'No. of Employee'
from HumanResources.Employee as he
join Person.Person pp on pp.BusinessEntityID = he.BusinessEntityID
where he.HireDate in (
    select HireDate from HumanResources.Employee
    group by HireDate
    having count(*) > 1
)
group by pp.FirstName, pp.LastName, he.HireDate