-- 6.1 Add a column named LastModifiedDate to the Production.Product table, with a default value of the current date and time.

alter table production.product
add lastModifiedDate date DEFAULT GETDATE


-- 6.2 Create a non-clustered index on the LastName column of the Person.Person table, including FirstName and MiddleName.

create NONCLUSTERED INDEX IX_lastname_search on person.person(LastName)
INCLUDE(FirstName, MiddleName)


-- 6.3 Add a check constraint to the Production.Product table to ensure that ListPrice is greater than StandardCost.

alter TABLE production.product
add CHECK(ListPrice > StandardCost)