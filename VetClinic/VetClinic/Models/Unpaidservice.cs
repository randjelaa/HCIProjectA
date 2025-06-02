using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class Unpaidservice
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int? AppointmentId { get; set; }

    public decimal Amount { get; set; }

    public string? Status { get; set; }

    public virtual Appointment? Appointment { get; set; }

    public virtual User User { get; set; } = null!;
}
