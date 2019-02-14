using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoEntities;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApi.Controllers
{
    /// <summary>
    /// Todo Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : Controller
    {
        private readonly TodoContext context;

        public TodoController(TodoContext context)
        {
            this.context = context;

            if (!context.TodoItems.Any())
            {
                context.TodoItems.Add(new TodoItem
                {
                    Name = "Item1"
                });
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Get all todoItems
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> Get()
        {
            return await context.TodoItems.ToListAsync();
        }

        /// <summary>
        /// Get item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TodoItem>> Get(long id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null)
                return NotFound();
            return todoItem;
        }

        /// <summary>
        /// Add new item
        /// </summary>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TodoItem>> Post(TodoItem todoItem)
        {
            context.TodoItems.Add(todoItem);
            await context.SaveChangesAsync();
            return CreatedAtAction("Get", new {id = todoItem.Id}, todoItem);
        }

        /// <summary>
        /// Update item
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Put(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            context.Entry(todoItem).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Delete item 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<TodoItem>> Delete(long id)
        {
            var todoItem = await context.TodoItems.FindAsync(id);
            if (todoItem == null)
                return NotFound();
            context.TodoItems.Remove(todoItem);
            await context.SaveChangesAsync();
            return todoItem;
        }
    }
}
