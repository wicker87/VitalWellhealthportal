namespace VitalWellhealthportal.Shared.Domain
{
    public class Appointment:BaseDomainModel
    {
        public string? AppointmentType {  get; set; }
        public DateTime? DateAndTime {  get; set; }
        public string? Room {  get; set; }
        public string? Diagnosis { get; set; }
        public string? DiagnosisDescription { get; set; }
        //public int BranchID { get; set; }
        //public virtual Branch? Branch { get; set; }
        public int PatientID { get; set; }
        public virtual Patient? Patient { get; set; }
        public int StaffID { get; set; }
        public virtual Staff? Staff { get; set; }

    }
}