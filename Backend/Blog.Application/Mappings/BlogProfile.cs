using System.Text.RegularExpressions;
using Blog.Application.DTOs;
using Blog.Domain.Entities;

namespace Blog.Application.Mappings;

using AutoMapper;

public class BlogProfile : Profile
{
    public BlogProfile()
    {
        // تبدیل BlogPost به BlogPostReadDto
        CreateMap<BlogPost, BlogPostReadDto>()
            .ForMember(dest => dest.Categories, opt => 
                opt.MapFrom(src => src.BlogPostCategories.Select(pc => pc.Category)))
            .ForMember(dest => dest.Tags, opt => 
                opt.MapFrom(src => src.BlogPostTags.Select(pt => pt.Tag)));

        // تبدیل BlogPostCreateDto به BlogPost
        CreateMap<BlogPostCreateDto, BlogPost>()
            .ForMember(dest => dest.Slug, opt => 
                opt.MapFrom(src => GenerateSlug(src.Title))); // تابع تولید Slug

        CreateMap<Category, CategoryReadDto>();
        CreateMap<Tag, TagReadDto>();
    }

    public static string GenerateSlug(string title)
    {
        Console.WriteLine("eeeeeeeeeeeeee");
        var slug = title.ToLower().Replace(" ", "-");
        slug = Regex.Replace(slug, @"[^a-z0-9\-]", "");
        return slug;
    }
}