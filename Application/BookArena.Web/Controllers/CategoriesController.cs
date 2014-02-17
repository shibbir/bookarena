using System.Linq;
using System.Web.Mvc;
using BookArena.DAL.Interfaces;
using BookArena.Model;
using BookArena.Model.EntityModel;
using BookArena.Web.Helper;
using Newtonsoft.Json;

namespace BookArena.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ActionResult Index()
        {
            var model = _categoryRepository.FindAll().Select(category => new
            {
                category.CategoryId,
                category.Title,
                category.Books.Count
            }).ToList();
            return Content(JsonConvert.SerializeObject(new {Data = model}), "application/json");
        }

        [HttpPost]
        public ActionResult Add(Category category)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid category information!"
                    }
                }), "application/json");
            }
            var duplicate = _categoryRepository.Find(x => x.Title == category.Title);
            if (duplicate != null)
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The Title for this category is already exist in the record. Please check again."
                    }
                }), "application/json");
            _categoryRepository.InsertOrUpdate(category);
            _categoryRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Data = category,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Category created successfully."
                }
            }), "application/json");
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (!Request.IsAuthenticated)
                return Content(JsonConvert.SerializeObject(Utility.AccessDeniedResponse()), "application/json");
            if (!ModelState.IsValid)
            {
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "Invalid category information!"
                    }
                }), "application/json");
            }
            var duplicate =
                _categoryRepository.Find(x => x.Title == category.Title && x.CategoryId != category.CategoryId);
            if (duplicate != null)
                return Content(JsonConvert.SerializeObject(new
                {
                    PreserveInput = true,
                    Response = new Response
                    {
                        ResponseType = ResponseType.Error,
                        Message = "The Title for this category is already exist in the record. Please check again."
                    }
                }), "application/json");
            _categoryRepository.InsertOrUpdate(category);
            _categoryRepository.Save();
            return Content(JsonConvert.SerializeObject(new
            {
                Data = category,
                Response = new Response
                {
                    ResponseType = ResponseType.Success,
                    Message = "Category updated successfully."
                }
            }), "application/json");
        }
    }
}