using EFTest.DTOs;

namespace EFTest.Services;

public interface ITripService
{
    Task<PaginatedTripsDto> GetTrips(int page, int pageSize);
    Task<bool> DeleteClient(int idClient);
    Task<string> AddClientToTrip(int idTrip, AddClientToTripRequest request);
}