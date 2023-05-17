using MobyLabWebProgramming.Core.DataTransferObjects;
using MobyLabWebProgramming.Core.Entities;
using MobyLabWebProgramming.Core.Requests;
using MobyLabWebProgramming.Core.Responses;

namespace MobyLabWebProgramming.Infrastructure.Services.Interfaces;

public interface IReviewService
{
    public Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default);
    public Task<ServiceResponse<int>> GetReviewCount(CancellationToken cancellationToken = default);
    public Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);
    public Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default);

}
