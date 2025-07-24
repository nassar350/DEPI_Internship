-- 7.1 Insert three sample records into Sales.ProductReviews using existing product and customer IDs, 
-- with varied ratings and meaningful review text.

insert into Sales.productreviews (productID, customerID, rating, review_date, review_text, verified_purchase_flag, helpful_votes)
VALUES  (2, 5, 7.5, GETDATE(), 'nice', 1, 9),
        (7, 10, 5.9, GETDATE(), 'like', 0, 4),
        (9, 1, 4.98, GETDATE(), 'good', 1, 2)


-- 7.2 Insert a new product category named 'Electronics' and a corresponding product subcategory named 'Smartphones' under Electronics.

insert into Production.ProductCategory(ProductCategoryID, Name)
values(1001, 'Electronics')

insert into Production.ProductSubcategory(ProductCategoryID, Name)
values(1001, 'Smartphones')

-- 7.3 Copy all discontinued products (where SellEndDate is not null) into a new table named Sales.DiscontinuedProducts.

insert into Sales.DiscontinuedProducts
select * from Production.Product where SellEndDate is not NULL