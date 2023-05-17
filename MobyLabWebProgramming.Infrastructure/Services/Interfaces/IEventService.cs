using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;
public interface IEventService
{
    public Task<ServiceResponse<EventDTO>> GetEvent(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetEventCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddEvent(EventAddDTO eventadd, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateEvent(EventUpdateDTO eventupdate, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteEvent(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
}