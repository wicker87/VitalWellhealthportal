using VitalWellhealthportal.Shared.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VitalWellhealthportal.Server.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save(HttpContext httpContext);
        IGenericRepository<Appointment> Appointments { get; }
        IGenericRepository<Branch> Branches { get; }
        IGenericRepository<HomeCareBooking> HomeCareBookings { get; }
        IGenericRepository<Medication> Medications { get; }
        IGenericRepository<Patient> Patients { get; }
        IGenericRepository<Payment> Payments { get; }
        IGenericRepository<Staff> Staffs { get; }
    }
}