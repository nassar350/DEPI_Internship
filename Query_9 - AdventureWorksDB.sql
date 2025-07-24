-- 9.1 Delete all product reviews with a rating of 1 and helpful votes equal to 0.

delete from Sales.ProductReviews where rating = 1 and helpful_votes = 0


-- 9.2 Delete products that have never been ordered, using a NOT EXISTS condition with Sales.SalesOrderDetail.

delete from Production.Product where NOT EXISTS (select 1 from Sales.SalesOrderDetail where ProductID = Production.Product.ProductID)


-- 9.3 Delete all purchase orders from vendors that are no longer active.

delete from Purchasing.PurchaseOrderHeader where vendorID in (
    select BusinessEntityID from Purchasing.Vendor where ActiveFlag = 0
)

