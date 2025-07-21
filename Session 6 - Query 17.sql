-- 17. Staff Bonus Calculation System
-- Create a complete solution that calculates quarterly bonuses for all staff members. 
-- Use variables to store date ranges and bonus rates. 
-- Apply different bonus percentages based on sales performance tiers.




declare @start_date date = '2022-01-01'
declare @end_date date = '2023-12-31'
declare @bonus_rate1 decimal(4, 4) = 0.05
declare @bonus_rate2 decimal(4,4) = 0.10
declare @bonus_rate3 DECIMAL (4, 4) = 0.15

select ss.staff_id, ss.first_name, ss.last_name, sum(soi.quantity * soi.list_price * (1 - soi.discount)) as 'total_sale',
case
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) <= 10000
        then sum(soi.quantity * soi.list_price * (1 - soi.discount) * @bonus_rate1)
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) > 10000 and sum(soi.quantity * soi.list_price * (1 - soi.discount)) <= 50000
        then sum(soi.quantity * soi.list_price * (1 - soi.discount) * @bonus_rate2)
    when sum(soi.quantity * soi.list_price * (1 - soi.discount)) > 50000
        then sum(soi.quantity * soi.list_price * (1 - soi.discount) * @bonus_rate3)
end as 'Bouns'
from sales.staffs as ss 
join sales.orders as so on ss.staff_id = so.staff_id
join sales.order_items as soi on soi.order_id = so.order_id
where so.order_date BETWEEN @start_date and @end_date
group by ss.staff_id, ss.first_name, ss.last_name

