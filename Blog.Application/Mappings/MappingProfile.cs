using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>();
        CreateMap<CreateUserDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Post mappings
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Category mappings
        CreateMap<Category, CategoryDto>();
        CreateMap<Category, CategoryListDto>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));
        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
        
        // Tag mappings
        CreateMap<Tag, TagDto>();
    }
}
