using MobyLabWebProgramming.Core.Enums;

namespace MobyLabWebProgramming.Core.DataTransferObjects;

public record PropertyUpdateDTO(Guid Id, string Name, string Description, decimal NightPrice, PropertyTypeEnum Type);
