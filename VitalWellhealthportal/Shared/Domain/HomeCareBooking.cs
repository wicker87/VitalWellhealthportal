using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VitalWellhealthportal.Shared.Domain
{
    public class HomeCareBooking:BaseDomainModel
    {
        public DateTime? Booking {  get; set; }
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        public int StaffID { get; set; }
        public virtual Staff? Staff { get; set; }

    }
}
