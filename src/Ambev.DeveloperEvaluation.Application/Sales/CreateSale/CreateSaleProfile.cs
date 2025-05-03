using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
///     Profile for mapping between Sale entity and CreateSaleResult.
/// </summary>
public class CreateSaleProfile : Profile
{
    /// <summary>
    ///     Initializes the mappings for CreateSale operation.
    /// </summary>
    public CreateSaleProfile()
    {
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // UUID is generated automatically
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.Branch, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.SaleId, opt => opt.MapFrom(src => src.Id));
    }
}