using System.Collections.ObjectModel;
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

public class AmenityService : IAmenityService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public AmenityService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<AmenityDTO>> GetAmenity(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new AmenityProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<AmenityDTO>.ForSuccess(result) :
            ServiceResponse<AmenityDTO>.FromError(CommonErrors.PropertyNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<AmenityDTO>>> GetAmenities(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new AmenityProjectionSpec(pagination.Search), cancellationToken);

        return ServiceResponse<PagedResponse<AmenityDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetAmenityCount(CancellationToken cancellationToken = default) =>
    ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Amenity>(cancellationToken));

    public async Task<ServiceResponse> AddAmenity(AmenityAddDTO amenity, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only a host or admin can add amenities!", ErrorCodes.CannotAdd));
        }

        var properties = new Collection<Property>();
        if (amenity.PropertiesId  != null)
        {
            foreach (Guid id in amenity.PropertiesId) 
            {
                var property = await _repository.GetAsync(new PropertySpec(id));
                if (property == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The id provided does not exists!", ErrorCodes.EntityNotFound));
                }
                properties.Add(property);
            }
        }

            await _repository.AddAsync(new Amenity
        {
            Name = amenity.Name,
            Description = amenity.Description,
            Properties = properties

        }, cancellationToken);

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateAmenity(AmenityUpdateDTO amenity, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host or admin can update the event!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new AmenitySpec(amenity.Id), cancellationToken);

        if (entity != null)
        {
            entity.Name = amenity.Name ?? entity.Name;
            entity.Description = amenity.Description ?? entity.Description;

            await _repository.UpdateAsync(entity, cancellationToken);
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteAmenity(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host can delete the amenity!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Amenity>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}