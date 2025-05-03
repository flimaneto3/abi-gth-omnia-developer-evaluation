using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

/// <summary>
///     Profile for mapping between Application and API DeleteSale requests.
/// </summary>
public class DeleteSaleProfile : Profile
{
    /// <summary>
    ///     Initializes the mappings for DeleteSale feature.
    /// </summary>
    public DeleteSaleProfile()
    {
        CreateMap<Guid, DeleteSaleCommand>()
            .ConstructUsing(id => new DeleteSaleCommand(id));
    }
}