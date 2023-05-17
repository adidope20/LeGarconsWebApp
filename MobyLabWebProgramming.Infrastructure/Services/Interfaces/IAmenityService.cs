using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IAmenityService
{
    public Task<ServiceResponse<AmenityDTO>> GetAmenity(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<AmenityDTO>>> GetAmenities(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetAmenityCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddAmenity(AmenityAddDTO amenity, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateAmenity(AmenityUpdateDTO amenity, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteAmenity(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}