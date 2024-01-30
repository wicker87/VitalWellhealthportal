namespace VitalWellhealthportal.Shared.Domain
{
    public class Staff:BaseDomainModel
    {
        public string? Name { get; set; }
        public string? Gender { get; set; }
        public string? Position { get; set; }
        public int BranchID { get; set; }
        public virtual Branch? Branch { get; set; }
    }
}