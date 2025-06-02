using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class Medicalrecord
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public string? Diagnosis { get; set; }

    public string? Treatment { get; set; }

    public string? Medications { get; set; }

    public string? Notes { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
