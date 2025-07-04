
-- insert data into the Company database
INSERT INTO Department (DName, Location) VALUES
('HR', 'New York'),
('IT', 'San Francisco'),
('Marketing', 'Chicago'),
('Finance', 'Boston'),
('R&D', 'Seattle'),
('Sales', 'Dallas'),
('Support', 'Denver'),
('Legal', 'Miami'),
('Security', 'Austin'),
('Admin', 'Atlanta');


-- Managers
INSERT INTO Employee (Fname, Lname, Gender, BirthDate, MSSN, DNum) VALUES
('Alice', 'Smith', 'F', '1980-01-01', 1, 1),
('Bob', 'Brown', 'M', '1982-02-02', 2, 2),
('Charlie', 'Davis', 'M', '1979-03-03', 3, 3),
('Diana', 'Evans', 'F', '1985-04-04', 4, 4),
('Ethan', 'Fox', 'M', '1983-05-05', 5, 5);
-- Employees
INSERT INTO Employee (Fname, Lname, Gender, BirthDate, MSSN, DNum) VALUES
('Fiona', 'Green', 'F', '1990-06-06', 1, 6),
('George', 'Hill', 'M', '1991-07-07', 2, 7),
('Hannah', 'Irwin', 'F', '1992-08-08', 3, 8),
('Ian', 'Jones', 'M', '1989-09-09', 4, 9),
('Julia', 'King', 'F', '1993-10-10', 5, 10);


INSERT INTO Project (PNumber, PName, City, DNum) VALUES
(101, 'Recruiting System', 'New York', 1),
(102, 'Intranet Portal', 'San Francisco', 2),
(103, 'Ad Campaign', 'Chicago', 3),
(104, 'Budget Tool', 'Boston', 4),
(105, 'AI Research', 'Seattle', 5),
(106, 'CRM Upgrade', 'Dallas', 6),
(107, 'Helpdesk Migration', 'Denver', 7),
(108, 'Policy Update', 'Miami', 8),
(109, 'CCTV Upgrade', 'Austin', 9),
(110, 'Inventory Tool', 'Atlanta', 10);


INSERT INTO Dependent (DName, Gender, BirthDate, ESSN) VALUES
('Anna', 'F', '2010-01-01', 1),
('Ben', 'M', '2011-02-02', 2),
('Cathy', 'F', '2012-03-03', 3),
('David', 'M', '2013-04-04', 4),
('Eva', 'F', '2014-05-05', 5),
('Frank', 'M', '2015-06-06', 6),
('Grace', 'F', '2016-07-07', 7),
('Henry', 'M', '2017-08-08', 8),
('Isla', 'F', '2018-09-09', 9),
('Jack', 'M', '2019-10-10', 10);


INSERT INTO Work_Hours (ESSN, PNumber, Working_hours) VALUES
(1, 101, 40),
(2, 102, 35),
(3, 103, 30),
(4, 104, 25),
(5, 105, 20),
(6, 106, 15),
(7, 107, 10),
(8, 108, 5),
(9, 109, 8),
(10, 110, 12);


INSERT INTO Management (ESSN, DNum, Hire_date) VALUES
(1, 1, '2010-01-01'),
(2, 2, '2011-02-02'),
(3, 3, '2012-03-03'),
(4, 4, '2013-04-04'),
(5, 5, '2014-05-05'),
(6, 6, '2015-06-06'),
(7, 7, '2016-07-07'),
(8, 8, '2017-08-08'),
(9, 9, '2018-09-09'),
(10, 10, '2019-10-10');


-- Update an employee's department
update Employee set DNum = 9
where SSN = 8;

-- Delete a dependent record
delete from Dependent
where DName = 'Jack';

-- Retrieve all employees working in a specific department
select * from Employee
where DNum = 9;

-- Find all employees and their project assignments with working hours
select emp.Fname, emp.Lname, p.PName, w.Working_hours from Employee as emp
join Work_Hours as w on emp.SSN = w.ESSN 
join Project as p on w.PNumber = p.PNumber;