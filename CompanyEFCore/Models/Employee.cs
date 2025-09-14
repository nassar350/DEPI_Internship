using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Employee
{
    public int Ssn { get; set; }

    public string Fname { get; set; } = null!;

    public string Lname { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly BirthDate { get; set; }

    public int Mssn { get; set; }

    public int Dnum { get; set; }

    public virtual ICollection<Dependent> Dependents { get; set; } = new List<Dependent>();

    public virtual Department DnumNavigation { get; set; } = null!;

    public virtual ICollection<Employee> InverseMssnNavigation { get; set; } = new List<Employee>();

    public virtual ICollection<Management> Managements { get; set; } = new List<Management>();

    public virtual Employee MssnNavigation { get; set; } = null!;

    public virtual ICollection<WorkHour> WorkHours { get; set; } = new List<WorkHour>();
}
