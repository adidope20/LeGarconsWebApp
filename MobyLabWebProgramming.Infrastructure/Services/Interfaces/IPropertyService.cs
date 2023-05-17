using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IPropertyService
{
    public Task<ServiceResponse<PropertyDTO>> GetProperty(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<PagedResponse<PropertyDTO>>> GetProperties(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetPropertyCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddProperty(PropertyAddDTO property, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateProperty(PropertyUpdateDTO property, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteProperty(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}