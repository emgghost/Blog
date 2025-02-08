using AutoMapper;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Mappings;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        CreateMap<BlogPost, BlogPostDto>()
            .ForMember(dest => dest.Categories, opt => 
                opt.MapFrom(src => src.BlogPostCategories.Select(pc => pc.Category)))
            .ForMember(dest => dest.Tags, opt => 
                opt.MapFrom(src => src.BlogPostTags.Select(pt => pt.Tag)))
            .ForMember(dest => dest.Author, opt => 
                opt.MapFrom(src => src.Author));
        
        CreateMap<BlogPostCreateDto, BlogPost>();
        CreateMap<BlogPostUpdateDto, BlogPost>();
        
        CreateMap<Category, CategoryDto>();
    }
}