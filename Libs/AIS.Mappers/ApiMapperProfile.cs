using AIS.Commands.API;
using AutoMapper;

namespace AIS.Mappers
{
    class ApiMapperProfile : Profile
    {
        public ApiMapperProfile()
        {
            // Controller to Command
            CreateMap<Model.Models.Tender, TenderCommandCU>()
                .ForMember(c => c.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(c => c.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(c => c.ReferenceNumber, opt => opt.MapFrom(src => src.ReferenceNumber))
                .ForMember(c => c.ReleaseDate, opt => opt.MapFrom(src => src.ReleaseDate))
                .ForMember(c => c.ClosingDate, opt => opt.MapFrom(src => src.ClosingDate))
                .ForMember(c => c.Details, opt => opt.MapFrom(src => src.Details))
            ;

            CreateMap<Model.Models.User, UserCommand>()
                .ForMember(c => c.UserID, opt => opt.MapFrom(src => src.UserID))
                .ForMember(c => c.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(c => c.Password, opt => opt.MapFrom(src => src.Password))
            ;

            CreateMap<Model.Models.AuthenticateUser, AuthenticateCommand>();

            // Model to Entity
            CreateMap<Model.Models.Tender, Data.Entity.Tender>();
            CreateMap<Model.Models.User, Data.Entity.User>();

            // Reverse Entity to Model
            CreateMap<Data.Entity.Tender, Model.Models.Tender>();
            CreateMap<Data.Entity.User, Model.Models.User>();
        }
    }
}