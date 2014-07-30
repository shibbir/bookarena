using System.Linq;
using AutoMapper;
using BookArena.App.ViewModels;
using BookArena.Model;

namespace BookArena.App.Helper
{
    public class ModelFactory
    {
        public CategoryViewModel Create(Category category)
        {
            return new CategoryViewModel
            {
                CategoryId = category.CategoryId,
                Title = category.Title,
                Books = category.Books.Select(Create)
            };
        }

        public BookViewModel Create(Book book)
        {
            Mapper.CreateMap<Book, BookViewModel>();
            return Mapper.Map<Book, BookViewModel>(book);
        }
    }
}