using System;
using System.Collections.Generic;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class Examination
    {
        public int Id { get; set; }
        public string Symptoms { get; set; } = null!;
        public string Diagnosis { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public int AppointmentId { get; set; }

        public virtual Appointment Appointment { get; set; } = null!;
    }
}
