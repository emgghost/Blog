using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"))
            .ReverseMap()
            .ForMember(dest => dest.FirstName, opt => opt.Ignore())
            .ForMember(dest => dest.LastName, opt => opt.Ignore());

        CreateMap<UserCreateDto, User>();
        CreateMap<UpdateUserDto, User>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<User, UserListDto>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        // Post mappings
        CreateMap<Post, PostDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
            .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.PostTags.Select(pt => pt.Tag.Name)))
            .ReverseMap();

        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Post, PostListDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

        // Category mappings
        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count))
            .ReverseMap();

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Category, CategoryListDto>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.Posts.Count));

        // Tag mappings
        CreateMap<Tag, TagDto>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.PostTags.Count))
            .ReverseMap();

        CreateMap<CreateTagDto, Tag>();
        CreateMap<UpdateTagDto, Tag>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Tag, TagListDto>()
            .ForMember(dest => dest.PostCount, opt => opt.MapFrom(src => src.PostTags.Count));

        // Comment mappings
        CreateMap<Comment, CommentDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"))
            .ReverseMap();

        CreateMap<CommentCreateDto, Comment>();
        CreateMap<CommentUpdateDto, Comment>()
            .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        CreateMap<Comment, CommentListDto>()
            .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.Author.FirstName} {src.Author.LastName}"));

        // PostTag mappings
        CreateMap<PostTag, PostTagDto>().ReverseMap();
    }
}
