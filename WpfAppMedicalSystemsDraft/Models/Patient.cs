using System;
using System.Collections.Generic;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class Patient
    {
        public Patient()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateOnly Dayofbirth { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
