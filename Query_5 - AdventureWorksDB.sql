-- 5.1 Create a table named Sales.ProductReviews with columns for 
-- review ID, product ID, customer ID, rating, review date, review text, verified purchase flag, and helpful votes. 
-- Include appropriate primary key, foreign keys, check constraints, defaults, and a unique constraint on product ID and customer ID.

create table Sales.ProductReviews(
    reviewID int IDENTITY(1,1) PRIMARY KEY,
    productID int not NULL unique,
    customerID int not NULL unique,
    rating decimal(8, 4) not NULL,
    review_date date not NULL default getdate(),
    review_text nvarchar(max),
    verified_purchase_flag bit check(verified_purchase_flag in (0, 1)),
    helpful_votes INT,
    FOREIGN key (productID) REFERENCES Production.Product(productID),
    FOREIGN key (customerID) REFERENCES Sales.customer(customerID)
)