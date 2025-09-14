using System;
using System.Collections.Generic;

namespace CompanyEFCore.Models;

public partial class WorkHour
{
    public int Essn { get; set; }

    public int Pnumber { get; set; }

    public int WorkingHours { get; set; }

    public virtual Employee EssnNavigation { get; set; } = null!;

    public virtual Project PnumberNavigation { get; set; } = null!;
}
