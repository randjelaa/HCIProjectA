using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class User
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int? RoleId { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Pet> Pets { get; set; } = new List<Pet>();

    public virtual Role? Role { get; set; }

    public virtual ICollection<Unpaidservice> Unpaidservices { get; set; } = new List<Unpaidservice>();

    public virtual ICollection<Userpreference> Userpreferences { get; set; } = new List<Userpreference>();
}
