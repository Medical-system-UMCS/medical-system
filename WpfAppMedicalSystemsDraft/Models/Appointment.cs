using System;
using System.Collections.Generic;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class Appointment
    {
        public Appointment()
        {
            Examinations = new HashSet<Examination>();
        }

        public int Id { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public string ExaminRoom { get; set; } = null!;
        public string AppointmentType { get; set; } = null!;

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Patient Patient { get; set; } = null!;
        public virtual ICollection<Examination> Examinations { get; set; }
    }
}
