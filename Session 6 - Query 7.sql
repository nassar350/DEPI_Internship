-- 7. Product Price Categorization
-- Write a query that categorizes all products using CASE WHEN based on their list price:
-- Under $300: Budget
-- $300-$800: Mid-Range
-- $801-$2000: Premium
-- Over $2000: Luxury

select product_id, product_name, list_price,
case
    when list_price < 300 then 'Budget'
    when list_price >= 300 and list_price <= 800 then 'Mid-Range'
    when list_price >= 801 and list_price <= 2000 then 'Premium'
    when list_price > 2000 then 'Luxury'
end as 'Category'
from production.products