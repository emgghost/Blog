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
        CreateMap<Post, PostDto>();
        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        // Category mappings
        CreateMap<Category, CategoryDto>();
        
        // Tag mappings
        CreateMap<Tag, TagDto>();
    }
}
