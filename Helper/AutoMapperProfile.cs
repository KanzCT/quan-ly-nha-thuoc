using AutoMapper;
using QuanLyThuoc.Data;
using QuanLyThuoc.ViewModel;

namespace QuanLyThuoc.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>()
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src =>
                    src.NgaySinh.HasValue ? DateOnly.FromDateTime(src.NgaySinh.Value) : (DateOnly?)null
                ))
                .ForMember(dest => dest.TenKh, opt => opt.MapFrom(src => src.HoTen));


            CreateMap<KhachHang, RegisterVM>()
                .ForMember(dest => dest.NgaySinh, opt => opt.MapFrom(src =>
                    src.NgaySinh.HasValue ? src.NgaySinh.Value.ToDateTime(TimeOnly.MinValue) : (DateTime?)null
                ));

        }
    }
}
