using AIS.Commands.API;
using AutoMapper;

namespace AIS.Mappers
{
    class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            // Controller to Command
            CreateMap<Model.Models.Tender, TenderCommandCUD>()
                .ForMember(c => c.ID, opt => opt.MapFrom(o => o.ID))
                .ForMember(c => c.Tender, opt => new Data.Entity.Tender())
                .ForPath(c => c.Tender.Name, opt => opt.MapFrom(src => src.Name))
                .ForPath(c => c.Tender.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
                .ForPath(c => c.Tender.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForPath(c => c.Tender.ClosingDate, opt => opt.MapFrom(src => src.ClosingDate))
                .ForPath(c => c.Tender.Details, opt => opt.MapFrom(src => src.Details))
                .ForPath(c => c.Tender.CreatorID, opt => opt.MapFrom(src => src.CreatorID))
            ;

            // Model to Entity
            CreateMap<Model.Models.Tender, Data.Entity.Tender>();
        }
    }
}