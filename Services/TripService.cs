using EFTest.Data;
using EFTest.DTOs;
using EFTest.Models;
using Microsoft.EntityFrameworkCore;

namespace EFTest.Services;

public class TripService : ITripService
    {
        private readonly AppDbContext _context;
        public TripService(AppDbContext context) => _context = context;

        public async Task<TripsDto> GetTrips(int page, int pageSize)
        {
            var totalCount = await _context.Trips.CountAsync();
            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var trips = await _context.Trips
                .Include(t => t.CountryTrips).ThenInclude(ct => ct.Country)
                .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
                .OrderByDescending(t => t.DateFrom)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(t => new TripDto
                {
                    Name = t.Name,
                    Description = t.Description,
                    DateFrom = t.DateFrom,
                    DateTo = t.DateTo,
                    MaxPeople = t.MaxPeople,
                    Countries = t.CountryTrips.Select(ct => new CountryDto { Name = ct.Country.Name }).ToList(),
                    Clients = t.ClientTrips.Select(ct => new ClientDto { FirstName = ct.Client.FirstName, LastName = ct.Client.LastName }).ToList()
                }).ToListAsync();

            return new TripsDto
            {
                PageNum = page,
                PageSize = pageSize,
                AllPages = totalPages,
                Trips = trips
            };
        }

        public async Task<bool> DeleteClient(int idClient)
        {
            var client = await _context.Clients
                .Include(c => c.ClientTrips)
                .FirstOrDefaultAsync(c => c.IdClient == idClient);

            if (client == null || client.ClientTrips.Any()) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<string> AddClientToTrip(int idTrip, AddClientToTripRequest request)
        {
            var existing = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == request.Pesel);
            if (existing != null && await _context.ClientTrips.AnyAsync(ct => ct.IdTrip == idTrip && ct.IdClient == existing.IdClient))
                return "Client already registered to this trip";

            var trip = await _context.Trips.FirstOrDefaultAsync(t => t.IdTrip == idTrip);
            if (trip == null || trip.DateFrom <= DateTime.Now) return "Trip doesnt exist";

            var client = existing ?? new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Telephone = request.Telephone,
                Pesel = request.Pesel
            };

            if (existing == null) _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            _context.ClientTrips.Add(new ClientTrip
            {
                IdClient = client.IdClient,
                IdTrip = idTrip,
                PaymentDate = request.PaymentDate?.ToString("yyyy-MM-dd")
            });
            await _context.SaveChangesAsync();
            return "Client added";
        }
    }
