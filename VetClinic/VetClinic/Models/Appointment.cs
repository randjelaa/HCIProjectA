using System;
using System.Collections.Generic;

namespace VetClinic.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public DateTime Date { get; set; }

    public int PetId { get; set; }

    public int VetId { get; set; }

    public int ServiceId { get; set; }

    public virtual ICollection<Medicalrecord> Medicalrecords { get; set; } = new List<Medicalrecord>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual Pet Pet { get; set; } = null!;

    public virtual Service Service { get; set; } = null!;

    public DateTime? Deleted { get; set; }

    public virtual ICollection<Unpaidservice> Unpaidservices { get; set; } = new List<Unpaidservice>();

    public virtual User Vet { get; set; } = null!;
}
