-- 17.1 Show products with name, weight (display 'Not Specified' if null), 
-- size (display 'Standard' if null), and color (display 'Natural' if null).

select Name 'product', isnull(cast(Weight as varchar(50)), 'Not Specified') 'weight', 
isnull(cast([Size] as varchar(50)), 'Standard') 'size',
isnull(cast(Color as varchar(50)), 'Natural') 'color' 
from Production.Product


-- 17.2 For each customer, display the best available contact method, 
-- prioritizing email address, then phone, then address line.

select (pp.FirstName + ' ' + pp.LastName) as 'name',
COALESCE(e.EmailAddress, p.phonenumber, a.Addressline1, 'no contact')
from Sales.Customer SC
join Person.Person pp on sc.PersonID = pp.BusinessEntityID
join Person.EmailAddress e on pp.BusinessEntityID = e.BusinessEntityID
join Person.PersonPhone p on p.BusinessEntityID = pp.BusinessEntityID
join Person.BusinessEntityAddress ba on ba.BusinessEntityID = pp.BusinessEntityID
join Person.Address a on a.AddressID = ba.AddressID


-- 17.3 Find products where weight is null but size is not null, and also 
-- find products where both weight and size are null. Discuss the impact on inventory management.

select ProductID, Name
from Production.Product
where Weight is null and size is not NULL

select ProductID, Name
from Production.Product
where Weight is null and size is NULL