using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale;

/// <summary>
///     Profile for mapping between Application and API GetSale requests.
/// </summary>
public class GetSaleProfile : Profile
{
    /// <summary>
    ///     Initializes the mappings for GetSale feature.
    /// </summary>
    public GetSaleProfile()
    {
        CreateMap<Sale, GetSaleResult>();
    }
}