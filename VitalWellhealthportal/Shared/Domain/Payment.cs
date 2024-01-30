namespace VitalWellhealthportal.Shared.Domain
{
    public class Payment:BaseDomainModel
    {
        public float? Amount { get; set; }
        public DateTime? Date { get; set; }
        public int AppointmentID { get; set; }   
        public virtual Appointment? Appointment { get; set; }
    }
}