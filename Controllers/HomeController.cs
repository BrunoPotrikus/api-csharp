using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhaApi.Data;
using MinhaApi.Models;

namespace MinhaApi.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        [Route("/hello")]
        public IActionResult Get([FromServices] DataContext context)
        {
            return Ok(context.TodoModels.ToList());
        }

        [HttpPost]
        [Route("/hello")]
        public IActionResult Post(
            [FromBody]TodoModel model,
            [FromServices] DataContext context)
        {
            context.TodoModels.Add(model);
            context.SaveChanges();

            return Created($"/{model.Id}", model);
        }

        [HttpGet]
        [Route("/hello/{id:int}")]
        public IActionResult GetItem(
            [FromRoute] int id,
            [FromServices] DataContext context)
        {
            var item = context.TodoModels.AsNoTracking().FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPut]
        [Route("/hello/{id:int}")]
        public IActionResult Put(
            [FromRoute] int id,
            [FromBody] TodoModel model,
            [FromServices] DataContext context)
        {
            var item = context.TodoModels.FirstOrDefault(x => x.Id == id);

            if(item == null)
            {
                return NotFound();
            }

            item.Title = model.Title;
            item.Done = model.Done;

            context.TodoModels.Update(item);
            context.SaveChanges();

            return Ok(item);
        }

        [HttpDelete]
        [Route("/hello/{id:int}")]
        public IActionResult Delete(
            [FromRoute] int id,
            [FromServices] DataContext context)
        {
            var item = context.TodoModels.FirstOrDefault(x => x.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            context.TodoModels.Remove(item);
            context.SaveChanges();

            return Ok(item);
        }
    }
}
