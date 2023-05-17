using System.Collections.ObjectModel;
using System.Net;
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

public class PropertyService : IPropertyService
{
    private readonly IRepository<WebAppDatabaseContext> _repository;

    public PropertyService(IRepository<WebAppDatabaseContext> repository)
    {
        _repository = repository;
    }

    public async Task<ServiceResponse<PropertyDTO>> GetProperty(Guid id, CancellationToken cancellationToken = default)
    {
        var result = await _repository.GetAsync(new PropertyProjectionSpec(id), cancellationToken);

        return result != null ?
            ServiceResponse<PropertyDTO>.ForSuccess(result) :
            ServiceResponse<PropertyDTO>.FromError(CommonErrors.PropertyNotFound);
    }

    public async Task<ServiceResponse<PagedResponse<PropertyDTO>>> GetProperties(PaginationSearchQueryParams pagination, CancellationToken cancellationToken = default)
    {
        var result = await _repository.PageAsync(pagination, new PropertyProjectionSpec(pagination.Search), cancellationToken);
        
        return ServiceResponse<PagedResponse<PropertyDTO>>.ForSuccess(result);
    }

    public async Task<ServiceResponse<int>> GetPropertyCount(CancellationToken cancellationToken = default) =>
        ServiceResponse<int>.ForSuccess(await _repository.GetCountAsync<Property>(cancellationToken));

    public async Task<ServiceResponse> AddProperty(PropertyAddDTO property, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only a host or admin can add properties!", ErrorCodes.CannotAdd));
        }

        var result = await _repository.GetAsync(new PropertySpec(property.Name), cancellationToken);

        if (result != null)
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The name you chose already exists!", ErrorCodes.PropertyNameAlreadyExists));
        }

        var amenities = new Collection<Amenity>();
        if (property.AmenitiesId != null)
        {
            foreach (Guid id in property.AmenitiesId)
            {
                var amenity = await _repository.GetAsync(new AmenitySpec(id));
                if (amenity == null)
                {
                    return ServiceResponse.FromError(new(HttpStatusCode.Conflict, "The id provided does not exists!", ErrorCodes.EntityNotFound));
                }
                amenities.Add(amenity);
            }
        }

        await _repository.AddAsync(new Property
        {
            Name = property.Name,
            Rooms = property.Rooms,
            Bedrooms = property.Bedrooms,
            Bathrooms = property.Bathrooms,
            Description = property.Description,
            Address = property.Address,
            NightPrice = property.NightPrice,
            Type = property.Type,
            UserId = requestingUser.Id,
            Amenities = amenities
        }, cancellationToken); 

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> UpdateProperty(PropertyUpdateDTO property, UserDTO? requestingUser, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host && requestingUser.Role != UserRoleEnum.Admin) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host or admin can update the property!", ErrorCodes.CannotUpdate));
        }

        var entity = await _repository.GetAsync(new PropertySpec(property.Id), cancellationToken);

        if (!((entity.UserId == requestingUser.Id && requestingUser.Role == UserRoleEnum.Host) || requestingUser.Role == UserRoleEnum.Admin))
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host of the property or the admin can update it!", ErrorCodes.CannotUpdate));
        }

        if (entity != null) 
        {
            entity.Name = property.Name ?? entity.Name;
            entity.Description = property.Description ?? entity.Description;
            if (property.NightPrice > 0)
            {
                entity.NightPrice = property.NightPrice;
            }
            entity.Type = property.Type ?? entity.Type;

            await _repository.UpdateAsync(entity, cancellationToken); 
        }

        return ServiceResponse.ForSuccess();
    }

    public async Task<ServiceResponse> DeleteProperty(Guid id, UserDTO? requestingUser = default, CancellationToken cancellationToken = default)
    {
        if (requestingUser != null && requestingUser.Role != UserRoleEnum.Host) 
        {
            return ServiceResponse.FromError(new(HttpStatusCode.Forbidden, "Only the host can delete the property!", ErrorCodes.CannotDelete));
        }

        await _repository.DeleteAsync<Property>(id, cancellationToken);

        return ServiceResponse.ForSuccess();
    }
}

