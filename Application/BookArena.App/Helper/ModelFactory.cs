using System.Linq;
using System.Web;
using AutoMapper;
using BookArena.App.ViewModels;
using BookArena.Model;

namespace BookArena.App.Helper
{
    public class ModelFactory
    {
        public CategoryWithBooksViewModel Create(Category category)
        {
            return new CategoryWithBooksViewModel
            {
                Id = category.Id,
                Title = category.Title,
                Books = category.Books.Select(Create)
            };
        }

        public BookViewModel Create(Book book)
        {
            Mapper.CreateMap<Book, BookViewModel>()
                .ForMember(dest => dest.ImageFilePath,
                    opt =>
                        opt.ResolveUsing<ImageResolver>().FromMember(src => src));
            return Mapper.Map<Book, BookViewModel>(book);
        }
    }

    public class ImageResolver : ValueResolver<Book, string>
    {
        protected override string ResolveCore(Book book)
        {
            var baseUrl = HttpContext.Current.Request.Url.Scheme + System.Uri.SchemeDelimiter +
                          HttpContext.Current.Request.Url.Host +
                          (HttpContext.Current.Request.Url.IsDefaultPort
                              ? ""
                              : ":" + HttpContext.Current.Request.Url.Port);

            if (!string.IsNullOrEmpty(book.ImageFileName))
            {
                return baseUrl + "/Content/Images/" + book.ImageFileName;
            }
            return null;
        }
    }
}