-- 11.1 Show employees with their full name, age in years, years of service, hire date formatted as 'Mon DD, YYYY', 
-- and birth month name.

select (pp.FirstName + ' ' + pp.LastName) as 'Full_name', DATEDIFF(year, he.BirthDate, GETDATE()) as 'age', 
DATEDIFF(year, he.HireDate, GETDATE()) as 'years of service', 
(cast(MONTH(he.HireDate) as varchar(20)) + ', ' + cast(day(he.HireDate) as varchar(20)) + ', ' + cast(YEAR(he.HireDate) as varchar(20))) as 'hire_date', 
DATENAME(MONTH, he.BirthDate) as 'birth_month'
from Sales.Customer sc
join Person.Person pp on sc.CustomerID = pp.BusinessEntityID 
join HumanResources.Employee he on he.BusinessEntityID = pp.BusinessEntityID


-- 11.2 Format customer names as 'LAST, First M.' (with middle initial), extract the email domain, and apply proper case formatting.

select isnull((pp.LastName + ', ' + pp.FirstName + ' ' + upper(LEFT(pp.MiddleName,1)) + '.'), 'No name') as 'name',
right(he.EmailAddress, len(he.EmailAddress) - CHARINDEX('@',he.EmailAddress) + 1) as 'email_domain'
from Sales.Customer sc
join Person.Person pp on sc.CustomerID = pp.BusinessEntityID 
left join Person.EmailAddress he on he.BusinessEntityID = pp.BusinessEntityID


-- 11.3 For each product, show name, weight rounded to one decimal, weight in pounds (converted from grams), and price per pound.

select Name, round(Weight,2) as 'weight in gram',
round((Weight * 0.00220462),2) as 'weight in pound', (ListPrice / ((Weight * 0.00220462))) as 'price per pound'
from Production.Product
where Weight is not null;