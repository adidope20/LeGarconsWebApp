using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReservationService
{
    public Task<ServiceResponse<ReservationDTO>> GetReservation(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetReservationCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddReservation(ReservationAddDTO reservation, UserDTO requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateReservation(ReservationUpdateDTO reservation, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteReservation(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

}
