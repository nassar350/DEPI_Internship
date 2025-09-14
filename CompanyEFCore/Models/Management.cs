using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class Management
{
    public int Essn { get; set; }

    public int Dnum { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual Department DnumNavigation { get; set; } = null!;

    public virtual Employee EssnNavigation { get; set; } = null!;
}
