using System.Linq;
using System.Web.Http;
using BookArena.App.Helper;
using BookArena.Data.Interfaces;
using BookArena.Model;

namespace BookArena.App.Controllers
{
    [Authorize]
    [RoutePrefix("api/Categories")]
    public class CategoriesController : BaseApiController
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ModelFactory _modelFactory;

        public CategoriesController(ICategoryRepository categoryRepository, ModelFactory modelFactory)
        {
            _categoryRepository = categoryRepository;
            _modelFactory = modelFactory;
        }

        [AllowAnonymous]
        public IHttpActionResult Get()
        {
            var model = _categoryRepository.FindAll().Select(category => new
            {
                category.Id,
                category.Title,
                category.Books.Count
            }).ToList();
            return Ok(model);
        }

        [AllowAnonymous]
        [Route("{categoryId}/books")]
        public IHttpActionResult Get(int categoryId)
        {
            var model =
                _categoryRepository.FindAll()
                    .Where(x => x.Id == categoryId)
                    .ToList()
                    .Select(category => _modelFactory.Create(category));

            return Ok(model);
        }

        public IHttpActionResult Post(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var duplicate = _categoryRepository.Find(x => x.Title == category.Title);
            if (duplicate != null)
            {
                return BadRequest("The Title for this category is already exist in the record. Please check again.");
            }

            _categoryRepository.Insert(category);
            _categoryRepository.Save();

            return Ok(category);
        }

        public IHttpActionResult Put(Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var duplicate =
                _categoryRepository.Find(x => x.Title == category.Title && x.Id != category.Id);

            if (duplicate != null)
            {
                return BadRequest("The Title for this category is already exist in the record. Please check again.");
            }

            _categoryRepository.Update(category);
            _categoryRepository.Save();

            return Ok(category);
        }
    }
}