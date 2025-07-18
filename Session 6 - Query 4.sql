-- Global Variables Information
-- Create a query that displays the current server name, SQL Server version, and the number of rows affected by the last statement.
-- Use appropriate global variables.

select @@SERVERNAME as server_name,
    @@VERSION as server_version,
    @@ROWCOUNT as last_row_affected