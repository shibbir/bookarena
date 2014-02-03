using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Web.Helper;

namespace BookArena.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public JsonResult Index()
        {
            var model = _categoryRepository.FindAll().Select(category => new
            {
                category.CategoryId,
                category.Title,
                category.Books.Count
            }).ToList();
            return Json(new {Data = model}, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Add(Category category)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid category information!"
                    }
                });
            }
            var duplicate = _categoryRepository.Find(x => x.Title == category.Title);
            if (duplicate != null)
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The Title for this category is already exist in the record. Please check again."
                    }
                });
            _categoryRepository.InsertOrUpdate(category);
            _categoryRepository.Save();
            return Json(new
            {
                Data = category,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Category created successfully."
                }
            });
        }

        [HttpPost]
        public JsonResult Edit(Category category)
        {
            if (!Request.IsAuthenticated)
                return Json(Utility.AccessDeniedResponse());
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid category information!"
                    }
                });
            }
            var duplicate =
                _categoryRepository.Find(x => x.Title == category.Title && x.CategoryId != category.CategoryId);
            if (duplicate != null)
                return Json(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The Title for this category is already exist in the record. Please check again."
                    }
                });
            _categoryRepository.InsertOrUpdate(category);
            _categoryRepository.Save();
            return Json(new
            {
                Data = category,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Category updated successfully."
                }
            });
        }
    }
}