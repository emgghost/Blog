namespace Blog.Application.Utilities;

public class Slug
{
    public static string GenerateSlug(string title)
    {
        // پیاده‌سازی تولید Slug (مثال ساده)
        return title.ToLower().Replace(" ", "-").Replace(".", "");
    }
}