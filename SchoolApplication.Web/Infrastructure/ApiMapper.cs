using AutoMapper;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Web
{
    /// <summary>
    /// Настройка автомаппера
    /// </summary>
    public class ApiMapper : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ApiMapper()
        {
            CreateMap<ApplicationRequestApiModel, ApplicationCreateModel>(MemberList.Destination);
            CreateMap<ParentRequestApiModel, ParentCreateModel>(MemberList.Destination);
            CreateMap<StudentRequestApiModel, StudentCreateModel>(MemberList.Destination);
            CreateMap<SchoolRequestApiModel, SchoolCreateModel>(MemberList.Destination);

            CreateMap<ApplicationRequestApiModel, ApplicationModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<ParentRequestApiModel, ParentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());
            CreateMap<StudentRequestApiModel, StudentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());            
            CreateMap<SchoolRequestApiModel, SchoolModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore());

            CreateMap<SchoolApiModel, SchoolCreateModel>(MemberList.Destination);
            CreateMap<ParentApiModel, ParentCreateModel>(MemberList.Destination);

            CreateMap<ApplicationModel, ApplicationApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<StudentModel, StudentApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<ParentModel, ParentApiModel>(MemberList.Destination).ReverseMap();
            CreateMap<SchoolModel, SchoolApiModel>(MemberList.Destination).ReverseMap();
        }
    }
}
