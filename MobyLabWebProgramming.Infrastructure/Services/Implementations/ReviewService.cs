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

public class ReviewService : IReviewService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public ReviewService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }
    public async Task<ServiceResponse<ReviewDTO>> GetReview(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new ReviewProjectionSpec(id), cancellationToken);
        return result != null ?
            ServiceResponse<ReviewDTO>.ForSuccess(result) :
            ServiceResponse<ReviewDTO>.FromError(CommonErrors.ReservationNotFound);
    }

    public async Task<ServiceResponse<int>> GetReviewCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Review>(cancellationToken));

    public async Task<ServiceResponse> AddReview(ReviewAddDTO review, UserDTO requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Guest && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only guests or hosts or admins can add reviews!", ErrorCodes.CannotAdd));
        }

        await _repository.AddAsync(new Review
        {
            Rating = review.Rating,
            Description = review.Description,
            UserId = review.UserId,
        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateReview(ReviewUpdateDTO review, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only admins can update reviews!", ErrorCodes.CannotAdd));
        }

        var entity = await _repository.GetAsync(new ReviewSpec(review.Id), cancellationToken);

        if (entity != null)
        {
            if (review.Rating != 0)
            {
                entity.Rating = review.Rating;
            }
            entity.Description = review.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();

    }

    public async Task<ServiceResponse> DeleteReview(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the admin can delete the review!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Review>(id, cancellationToken);

        return ServiceResponse.ForSuccess();


    }

}
