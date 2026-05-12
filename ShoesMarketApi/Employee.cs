using System;
using System.Collections.Generic;

namespace ShoesMarketApi;

public partial class Employee
{
    public int IdEmployee { get; set; }

    public string EmployeeRole { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
