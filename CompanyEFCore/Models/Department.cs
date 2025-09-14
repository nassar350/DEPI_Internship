using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Department
{
    public int Dnum { get; set; }

    public string Dname { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual ICollection<Management> Managements { get; set; } = new List<Management>();

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
