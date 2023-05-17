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
using System.Net;
using System.Threading;

namespace MobyLabWebProgramming.Infrastructure.Services.Implementations;

public class ReservationService : IReservationService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ReservationService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ReservationProjectionSpec(id), cancellationToken);
        return result != null ?
            ServiceResponse<ReservationDTO>.ForSuccess(result) :
            ServiceResponse<ReservationDTO>.FromError(CommonErrors.ReservationNotFound);
    }

    public async Task<ServiceResponse<int>> GetReservationCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Reservation>(cancellationToken));

    public async Task<ServiceResponse> AddReservation(ReservationAddDTO reservation, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Guest && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only guests or admins can add reservations!", ErrorCodes.CannotAdd));
        }

        var newEvent = new Event
        {
            PropertyId = reservation.PropertyId,
            Available = false,
            Blocked = false,
            Booked = true,
            Start = reservation.Event.Start,
            End = reservation.Event.End
        };

        await _repository.AddAsync(newEvent, cancellationToken);
       
        await _repository.AddAsync(new Reservation
        {
            Price = reservation.Price,
            UserId = requestingUser.Id,
            PropertyId = reservation.PropertyId,
            EventId = newEvent.Id,
            Event = newEvent

        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateReservation(ReservationUpdateDTO reservation, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Guest && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only guests or admins can update reservations!", ErrorCodes.CannotAdd));
        }

        var entity = await _repository.GetAsync(new ReservationSpec(reservation.Id), cancellationToken);

        if (entity != null)
        {
            if (reservation.Event.Start != null)
            {
                entity.Event.Start = reservation.Event.Start;
            }
            if (reservation.Event.End != null)
            {
                entity.Event.End = reservation.Event.End;
            }

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();

    }
    
    public async Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the reservation!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Reservation>(id, cancellationToken);
        
        return ServiceResponse.ForSuccess();


    }

}
