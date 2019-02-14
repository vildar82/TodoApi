using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoEntities;

namespace TodoApi.Pages
{
    public class IndexModel : PageModel
    {
        private TodoContext _context;

        public IndexModel(TodoContext context)
        {
            Debug.WriteLine("IndexModel.ctor()");
            _context = context;
        }
        
        public List<TodoItem> Items { get; set; }

        [BindProperty]
        public TodoItem Item { get; set; }
        
        [TempData]
        public string Message { get; set; }
        public bool HasMessage => !string.IsNullOrEmpty(Message);

        public async Task OnGetAsync()
        {
            Debug.WriteLine("IndexModel.OnGetAsync()");
            Items = await _context.TodoItems.ToListAsync();
        }

        public async Task<IActionResult> OnPostDeleteAsync(long id)
        {
            Debug.WriteLine($"IndexModel.OnPostDeleteAsync() id={id}");
            _context.TodoItems.Attach(new TodoItem {Id = id}).State = EntityState.Deleted;
            await _context.SaveChangesAsync();
            Message = "Задача удалена";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            Debug.WriteLine($"IndexModel.OnPostAddAsync() text={Item.Name}");
            Item.Created = DateTime.Now;
            _context.TodoItems.Add(Item);
            await _context.SaveChangesAsync();
            Message = "Задача добавлена";
            return RedirectToPage();
        }
    }
}
