using AutoMapper;
using AutoMapper.Extensions.EnumMapping;
using SchoolApplication.Entities;
using SchoolApplication.Services.Contracts;

namespace SchoolApplication.Services
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile()
        {
            CreateMap<Gender, GenderModel>().ConvertUsingEnumMapping(opt => opt.MapByName()).ReverseMap();

            CreateMap<Application, ApplicationModel>(MemberList.Destination).ReverseMap();
            CreateMap<Parent, ParentModel>(MemberList.Destination).ReverseMap();
            CreateMap<School, SchoolModel>(MemberList.Destination).ReverseMap();
            CreateMap<Student, StudentModel>(MemberList.Destination).ReverseMap();

            CreateMap<ApplicationCreateModel, Application>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.Student, opt => opt.Ignore())
                .ForMember(x => x.Parent, opt => opt.Ignore())
                .ForMember(x => x.School, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore());
            CreateMap<ParentCreateModel, Parent>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore());
            CreateMap<SchoolCreateModel, School>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore());
            CreateMap<StudentCreateModel, Student>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.DeletedAt, opt => opt.Ignore());

            CreateMap<ApplicationCreateModel, ApplicationModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<ParentCreateModel, ParentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<SchoolCreateModel, SchoolModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
            CreateMap<StudentCreateModel, StudentModel>(MemberList.Destination)
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ReverseMap();
        }
    }
}
