using AutoMapper;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.RequestModels;
using SchoolApplication.Web.Models.CreateRequestApiModels;
using SchoolApplication.Web.Models.ResponseModels;

namespace SchoolApplication.Web.Infrastructure
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
            CreateMap<ApplicationModel, ApplicationApiModel>(MemberList.Destination);
            CreateMap<ApplicationCreateRequestApiModel, ApplicationCreateModel>(MemberList.Destination);

            CreateMap<ParentModel, ParentApiModel>(MemberList.Destination);
            CreateMap<ParentCreateRequestApiModel, ParentCreateModel>(MemberList.Destination);

            CreateMap<StudentModel, StudentApiModel>(MemberList.Destination);
            CreateMap<StudentCreateRequestApiModel, StudentCreateModel>(MemberList.Destination);

            CreateMap<SchoolModel, SchoolApiModel>(MemberList.Destination);
            CreateMap<SchoolCreateRequestApiModel, SchoolCreateModel>(MemberList.Destination);
        }
    }
}
