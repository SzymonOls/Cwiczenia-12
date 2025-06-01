using EFTest.DTOs;

namespace EFTest.Services;

public interface ITripService
{
    Task<TripsDto> GetTrips(int page, int pageSize);
    Task<bool> DeleteClient(int idClient);
    Task<string> AddClientToTrip(int idTrip, AddClientToTripRequest request);
}