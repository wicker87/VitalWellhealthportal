using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalWellhealthportal.Shared.Domain
{
    public class Patient:BaseDomainModel
    {
        public string? Name { get; set; }
        public string? NRIC {  get; set; }
        public string? Address { get; set; }
        public DateTime? DOB {  get; set; }
        public int? Age { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual List<Appointment>? Appointments { get; set; }

    }
}
