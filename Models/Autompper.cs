using AutoMapper;
using NZwalker.Models.Domain;
using NZwalker.Models.DTO;

namespace NZwalker.Models.AutoMapper;

public class AutoMapperProfiles : Profile {
    public AutoMapperProfiles(){

        // Region
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<Region, AddRegionRequestDto>().ReverseMap();
        CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
        CreateMap<Region, DeleteRegionRequestDto>().ReverseMap();

        // Walks
        CreateMap<Walks, WalksDTO>().ReverseMap();
        CreateMap<Walks, AddRequestWalksDTO>().ReverseMap();
        CreateMap<Walks, DeleteResponseWalksDTO>().ReverseMap();
    
    }
}
