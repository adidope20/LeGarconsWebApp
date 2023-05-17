using System.Net;
using System.Threading;
using MobyLabWebProgramming.Core.Constants;
using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Enums;
using MobyLabWebProgramming.Core.Errors;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;
using MobyLabWebProgramming.Core.Specifications;
using MobyLabWebProgramming.Infrastructure.Database;
using MobyLabWebProgramming.Infrastructure.Repositories.Interfaces;
using MobyLabWebProgramming.Infrastructure.Services.Interfaces;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class EventService : IEventService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public EventService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<EventDTO>> GetEvent(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new EventProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<EventDTO>.ForSuccess(result) :
            ServiceResponse<EventDTO>.FromError(CommonErrors.PropertyNotFound);
    }

    public async Task<ServiceResponse<int>> GetEventCount(CancellationToken cancellationToken = default) =>
    ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Event>(cancellationToken));
    
    public async Task<ServiceResponse> AddEvent(EventAddDTO eventAdd, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only a host or admin can add events!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Event
        {
            Start = eventAdd.Start,
            End = eventAdd.End,
            Available = eventAdd.Available,
            Booked = eventAdd.Booked,
            Blocked = eventAdd.Blocked,
            PropertyId = eventAdd.PropertyId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateEvent(EventUpdateDTO eventUpdate, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host can update the event!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new EventSpec(eventUpdate.Id), cancellationToken);

        if (entity != null)
        {
            entity.Start = eventUpdate.Start;
            entity.End = eventUpdate.End;
            entity.Available = eventUpdate.Available;
            entity.Blocked = eventUpdate.Blocked;
            entity.Booked = eventUpdate.Booked;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteEvent(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host and admin can delete the event!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Event>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}