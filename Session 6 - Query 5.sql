-- 5.Write a query that checks the inventory level for product ID 1 in store ID 1.
-- Use IF statements to display different messages based on stock levels:#
-- If quantity > 20: Well stocked
-- If quantity 10-20: Moderate stock
-- If quantity < 10: Low stock - reorder needed

declare @quantity INT

select @quantity = quantity from production.stocks
where product_id = 1 and store_id = 1

if @quantity > 20
BEGIN
    print 'Well Stocked'
END
ELSE IF @quantity <= 20 and @quantity >= 10
BEGIN
    print 'Moderate Stock'
END
ELSE IF @quantity < 10
BEGIN
    print 'Low Stock - reorder needed'
END