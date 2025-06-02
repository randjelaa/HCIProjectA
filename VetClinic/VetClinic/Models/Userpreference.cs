using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class Userpreference
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public string? Theme { get; set; }

    public string? Language { get; set; }

    public virtual User User { get; set; } = null!;
}
