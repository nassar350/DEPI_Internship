-- 16. Dynamic Product Search Procedure
-- Write a stored procedure named sp_SearchProducts that builds dynamic SQL based on optional parameters: 
--     product name search term, category ID, minimum price, maximum price, and sort column.

create PROCEDURE sp_SearchProducts
    @product_name nvarchar(255) = NULL,
    @category_id int = NULL,
    @minimum_price decimal(10, 2) = NULL,
    @maximum_price decimal(10, 2) = NULL,
    @sort_column nvarchar(255) = null,
    @sort_type NVARCHAR(5) = 'asce'
AS
BEGIN
    declare @query nvarchar(600) = 'select * from production.products'
    declare @is_where int = 0

    if @product_name is not NULL
    BEGIN
        set @query = @query + ' where product_name = ' + @product_name
        set @is_where = 1
    END

    if @category_id is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and category_id = ' + cast(@category_id as varchar(50))
    END

    if @category_id is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where category_id = ' + cast(@category_id as varchar(50))
        set @is_where = 1
    END

    if @minimum_price is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and list_price = ' + cast(@minimum_price as varchar(50))
    END

    if @minimum_price is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where list_price = ' + cast(@minimum_price as varchar(50))
        set @is_where = 1
    END

    if @maximum_price is not null and @is_where = 1
    BEGIN
        set @query = @query + ' and list_price = '+ cast(@maximum_price as varchar(50))
    END

    if @maximum_price is not NULL and @is_where = 0
    BEGIN
        set @query = @query + ' where list_price = '+ cast(@maximum_price as varchar(50))
        set @is_where = 1
    END

    if @sort_column is NOT NULL
    BEGIN
        set @query = @query + ' order by ' + @sort_column + ' ' + @sort_type
    END

    select @query as query 
END;
go

exec sp_SearchProducts @category_id = 2
