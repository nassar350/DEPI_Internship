-- 26.1 Create a filtered index for active products only (SellEndDate IS NULL) and for recent orders (last 2 years),
-- and measure performance impact.

CREATE NONCLUSTERED INDEX IX_Active_Products ON Production.Product (ProductID, Name)
WHERE SellEndDate IS NULL;
