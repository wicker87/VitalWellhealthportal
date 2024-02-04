using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace VitalWellhealthportal.Client.Static
{
    public static class Endpoints
    {
        private static readonly string Prefix = "api";

        public static readonly string AppointmentsEndpoint = $"{Prefix}/appointments";
        public static readonly string BranchesEndpoint = $"{Prefix}/branches";
        public static readonly string HomeCareBookingsEndpoint = $"{Prefix}/homeCareBookings";
        public static readonly string MedicationsEndpoint = $"{Prefix}/medications";
        public static readonly string PatientsEndpoint = $"{Prefix}/patients";
        public static readonly string PaymentsEndpoint = $"{Prefix}/payments";
        public static readonly string StaffsEndpoint = $"{Prefix}/Staffs";
    }
}

