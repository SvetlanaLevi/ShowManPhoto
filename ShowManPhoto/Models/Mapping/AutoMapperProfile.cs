using AutoMapper;
using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowManPhoto.Models.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            AllowNullCollections = true;
            CreateMap<AlbumInputModel, Album>();
            CreateMap<PhotoInputModel, Photo>()
                .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.File.FileName))
                .ForMember(dest => dest.FilePath, opt => opt.MapFrom(src => 
                    $@"C:\Users\Покемон\Desktop\ShowManPhoto\images\{src.File.FileName}"));
            CreateMap<SelectedItem, OrderedPhoto>();
            CreateMap<OrderedPhotosInputModel, Order>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.OrderedPhotos, opt => opt.MapFrom(src => src.SelectedItems))
                .ForMember(dest => dest.StatusId, opt => opt.MapFrom(src => 1));
        }
    }
}
