-- 6.Create a WHILE loop that updates low-stock items (quantity < 5) in batches of 3 products at a time. 
-- Add 10 units to each product and display progress messages after each batch.

declare @rows_affected int = 1

while @rows_affected > 0
BEGIN
    UPDATE top (3) production.stocks
    set quantity = quantity + 10
    where quantity < 5

    set @rows_affected = @@rowcount
    
    print cast(@rows_affected as varchar(20)) + ' Row Affected'
END