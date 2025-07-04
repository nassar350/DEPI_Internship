# Company Database Project

This project defines and populates a relational database schema for a **company management system**, including employees, departments, projects, dependents, work assignments, and management structures.

---

## 🖼 ERD and Mapping Diagram

### 1. ER-Diagram
![ER-Diagram](.\ER-Diagram&mapping\Company_ER-Diagram.png)

---

### 2. Mapping
![ER-Diagram](.\ER-Diagram&mapping\company_mapping.png)

---

> This images shows:
>
> * Tables and their attributes
> * Primary and foreign keys
> * Relationships between entities (one-to-many, many-to-many)
> * Mapping lines and cardinality

---

## 📊 DDL Statements (Schema)

### 1. Department

```sql
CREATE TABLE Department (
    DNum INT IDENTITY(1,1) PRIMARY KEY,
    DName NVARCHAR(255) NOT NULL,
    Location NVARCHAR(300) NOT NULL
);
```

---

### 2. Employee

```sql
CREATE TABLE Employee (
    SSN INT IDENTITY(1,1) PRIMARY KEY,
    Fname NVARCHAR(255) NOT NULL,
    Lname NVARCHAR(255) NOT NULL,
    Gender CHAR NOT NULL CHECK (Gender IN ('M','F')),
    BirthDate DATE NOT NULL,
    MSSN INT NOT NULL,
    DNum INT NOT NULL,
    FOREIGN KEY (MSSN) REFERENCES Employee(SSN),
    FOREIGN KEY (DNum) REFERENCES Department(DNum)
);
```

---

### 3. Project

```sql
CREATE TABLE Project (
    PNumber INT PRIMARY KEY,
    PName NVARCHAR(255) NOT NULL,
    City NVARCHAR(100) NOT NULL,
    DNum INT NOT NULL,
    FOREIGN KEY (DNum) REFERENCES Department(DNum)
);
```

---

### 4. Dependent

```sql
CREATE TABLE Dependent (
    DName NVARCHAR(255) PRIMARY KEY,
    Gender CHAR NOT NULL CHECK (Gender IN ('M','F')),
    BirthDate DATE NOT NULL,
    ESSN INT NOT NULL,
    FOREIGN KEY (ESSN) REFERENCES Employee(SSN) ON DELETE CASCADE
);
```

---

### 5. Work\_Hours

```sql
CREATE TABLE Work_Hours (
    ESSN INT NOT NULL,
    PNumber INT NOT NULL,
    Working_hours INT DEFAULT 0 NOT NULL,
    FOREIGN KEY (ESSN) REFERENCES Employee(SSN) ON DELETE CASCADE,
    FOREIGN KEY (PNumber) REFERENCES Project(PNumber) ON DELETE CASCADE,
    PRIMARY KEY (ESSN, PNumber)
);
```

---

### 6. Management

```sql
CREATE TABLE Management (
    ESSN INT NOT NULL,
    DNum INT NOT NULL,
    Hire_date DATE NOT NULL,
    FOREIGN KEY (ESSN) REFERENCES Employee(SSN) ON DELETE CASCADE,
    FOREIGN KEY (DNum) REFERENCES Department(DNum) ON DELETE CASCADE,
    PRIMARY KEY (ESSN, DNum)
);
```

---


## 🦖 Sample Data

Each table is populated with 10 rows that demonstrate:

* Department hierarchy
* Employee-supervisor relationships
* Employee work hours on projects
* Dependent linkage
* Management assignments

---

## 💡 Notes

* `IDENTITY(1,1)` is used in `Department` and `Employee` to auto-generate primary keys.
* Referential integrity is preserved through foreign keys.
* `ON DELETE CASCADE` ensures automatic cleanup of dependents and work-hour records.

---

## 🛠 Requirements

* Microsoft SQL Server (or compatible engine with slight syntax modifications)
* SQL execution environment (SSMS, Azure Data Studio, etc.)

---

## 🗾 License

This project is open for academic and educational purposes.
