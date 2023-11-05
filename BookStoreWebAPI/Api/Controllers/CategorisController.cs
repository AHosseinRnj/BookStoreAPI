using Application.Commands.CreateCategory;
using Application.Commands.DeleteCategory;
using Application.Commands.UpdateCategory;
using Application.Query.GetCategories;
using Application.Query.GetCategory;
using Application.Query.GetCategoryBooks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategorisController : ControllerBase
    {
        private readonly ISender _sender;
        public CategorisController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(CreateCategoryCommand request)
        {
            await _sender.Send(request);
            return CreatedAtRoute("GetCategoryById", new { id = request.id }, request);
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var result = await _sender.Send(new GetCategoriesQuery());

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}", Name = "GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await _sender.Send(new GetCategoryQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet("{id}/Books", Name = "GetCategoryBooks")]
        public async Task<IActionResult> GetCategoryBooks(int id)
        {
            var result = await _sender.Send(new GetCategoryBooksQuery(id));

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryCommand request)
        {
            await _sender.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            await _sender.Send(new DeleteCategoryCommand(id));
            return NoContent();
        }
    }
}