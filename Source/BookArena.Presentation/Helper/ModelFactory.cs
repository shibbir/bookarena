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
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Book, BookViewModel>().ForMember(dest => dest.ImageFilePath, opt => opt.ResolveUsing<ImagePathResolver>().FromMember(src => src));
            });

            var mapper = config.CreateMapper();
            return mapper.Map<Book, BookViewModel>(book);
        }
    }

    public class ImagePathResolver : ValueResolver<Book, string>
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