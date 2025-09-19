using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using SchoolApplication.Entities;
using SchoolApplication.Repositories.Contracts.Models;
using SchoolApplication.Services.Contracts;
using SchoolApplication.Services.Contracts.Models.CreateModels;
using SchoolApplication.Services.Contracts.Models.Enums;
using SchoolApplication.Services.Contracts.Models.RequestModels;

namespace SchoolApplication.Services.Infrastructure
{
    /// <summary>
    /// Сервисный маппер
    /// </summary>
    public class ServiceProfile : Profile
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ServiceProfile()
        {
            CreateMap<Gender, GenderModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Application, ApplicationModel>(MemberList.Destination);
            CreateMap<Parent, ParentModel>(MemberList.Destination);
            CreateMap<School, SchoolModel>(MemberList.Destination);
            CreateMap<Student, StudentModel>(MemberList.Destination);

            CreateMap<ApplicationCreateModel, Application>(MemberList.Source);
            CreateMap<ParentCreateModel, Parent>(MemberList.Source);
            CreateMap<SchoolCreateModel, School>(MemberList.Source);
            CreateMap<StudentCreateModel, Student>(MemberList.Source);

            CreateMap<ApplicationDbModel, Application>(MemberList.Source).ReverseMap();
            CreateMap<ApplicationDbModel, ApplicationModel>(MemberList.Source).ReverseMap();
        }
    }
}
