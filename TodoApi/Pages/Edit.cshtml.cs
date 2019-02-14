using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoEntities;

namespace TodoApi.Pages
{
    public class EditModel : PageModel
    {
        private readonly TodoContext context;

        public EditModel(TodoContext context)
        {
            this.context = context;
        }

        [BindProperty]
        public TodoItem Item { get; set; }

        [TempData]
        public string Message { get; set; }

        public async Task OnGetAsync(long id)
        {
            Item = await context.TodoItems.FindAsync(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var item = context.TodoItems.Attach(Item);
            item.State = EntityState.Modified;
            await context.SaveChangesAsync();
            Message = "Задача изменена";
            return RedirectToPage("Index");
        }
    }
}
