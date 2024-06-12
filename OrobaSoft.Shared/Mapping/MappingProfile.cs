using AutoMapper;
using OrobaSoft.Shared.DTOs;
using OrobaSoft.Shared.Models;

namespace OrobaSoft.Shared.Mapping;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Invoice, InvoiceDto>().ReverseMap();
        CreateMap<InvoiceItem, InvoiceItemDto>().ReverseMap();
    }
}
