using AuthService.Entities;
using AutoMapper;

namespace AuthService.Dto
{
    public class StudentDto
    {
        public string Id { get; set; }
        public string studentName { get; set; } = null!;
        public string mobNo { get; set; } = null!;

    }
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<StudentDto, User>().ForMember(e => e.Id, s => s.MapFrom(d => d.Id))
                .ForMember(e => e.Username, s => s.MapFrom(d => d.studentName));
        }
    }


}
