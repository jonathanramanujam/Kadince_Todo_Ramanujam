using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kadince_Todo_Ramanujam.Data;
using Kadince_Todo_Ramanujam.Models;

namespace Kadince_Todo_Ramanujam.Pages.Todo
{
    public class EditModel : PageModel
    {
        private readonly Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext _context;

        public EditModel(Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TodoItem TodoItem { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.TodoItem == null)
            {
                return NotFound();
            }

            var todoitem =  await _context.TodoItem.FirstOrDefaultAsync(m => m.Id == id);
            if (todoitem == null)
            {
                return NotFound();
            }
            TodoItem = todoitem;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TodoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoItemExists(TodoItem.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TodoItemExists(int id)
        {
          return (_context.TodoItem?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
