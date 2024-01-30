using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalWellhealthportal.Shared.Domain
{
    public class Medication:BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Dosage { get; set; }
        public int AppointmentID { get; set; }
        public virtual Appointment? Appointment { get; set; }
        //public int PatientID { get; set; }
        //public virtual Patient? Patient { get; set; }
        //public int PaymentID { get; set; }
        //public virtual Payment? Payment { get; set; }


    }
}
