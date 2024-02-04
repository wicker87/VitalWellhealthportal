using VitalWellhealthportal.Server.Data;
using VitalWellhealthportal.Server.IRepository;
using VitalWellhealthportal.Server.Models;
using VitalWellhealthportal.Shared.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace VitalWellhealthportal.Server.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Appointment> _appointments;
        private IGenericRepository<Branch> _branches;
        private IGenericRepository<HomeCareBooking> _homeCareBookings;
        private IGenericRepository<Medication> _medications;
        private IGenericRepository<Patient> _patients;
        private IGenericRepository<Payment> _payments;
        private IGenericRepository<Staff> _staffs;

        private UserManager<ApplicationUser> _userManager;

        public UnitOfWork(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IGenericRepository<Appointment> Appointments
            => _appointments ??= new GenericRepository<Appointment>(_context);
        public IGenericRepository<Branch> Branches
            => _branches ??= new GenericRepository<Branch>(_context);
        public IGenericRepository<HomeCareBooking> HomeCareBookings
            => _homeCareBookings ??= new GenericRepository<HomeCareBooking>(_context);
        public IGenericRepository<Medication> Medications
            => _medications ??= new GenericRepository<Medication>(_context);
        public IGenericRepository<Patient> Patients
            => _patients ??= new GenericRepository<Patient>(_context);
        public IGenericRepository<Payment> Payments
            => _payments ??= new GenericRepository<Payment>(_context);
        public IGenericRepository<Staff> Staffs
            => _staffs ??= new GenericRepository<Staff>(_context);

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save(HttpContext httpContext)
        {
            //To be implemented
            string user = "System";

            var entries = _context.ChangeTracker.Entries()
                .Where(q => q.State == EntityState.Modified ||
                    q.State == EntityState.Added);

            foreach (var entry in entries)
            {
                ((BaseDomainModel)entry.Entity).DateUpdated = DateTime.Now;
                ((BaseDomainModel)entry.Entity).UpdatedBy = user;
                if (entry.State == EntityState.Added)
                {
                    ((BaseDomainModel)entry.Entity).DateCreated = DateTime.Now;
                    ((BaseDomainModel)entry.Entity).CreatedBy = user;
                }
            }

            await _context.SaveChangesAsync();
        }
    }
}