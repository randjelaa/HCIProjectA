using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class Pet
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Species { get; set; }

    public string? Breed { get; set; }

    public int OwnerId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual User Owner { get; set; } = null!;
}
