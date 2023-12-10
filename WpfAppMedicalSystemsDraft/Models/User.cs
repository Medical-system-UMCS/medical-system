using System;
using System.Collections.Generic;

namespace WpfAppMedicalSystemsDraft.Models
{
    public partial class User
    {
        public User()
        {
            Doctors = new HashSet<Doctor>();
            Patients = new HashSet<Patient>();
        }

        public int Id { get; set; }
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string AccountType { get; set; } = null!;
        public bool Verified { get; set; }
        public string Email { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Patient> Patients { get; set; }
    }
}
