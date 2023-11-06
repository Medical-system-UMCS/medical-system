using System;
using System.Collections.Generic;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Specialization { get; set; } = null!;
        public string ScientificDegree { get; set; } = null!;
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
    }
}
