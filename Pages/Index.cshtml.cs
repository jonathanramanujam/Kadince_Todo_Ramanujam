using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kadince_Todo_Ramanujam.Data;
using Kadince_Todo_Ramanujam.Models;

namespace Kadince_Todo_Ramanujam.Pages
{
    public class IndexModel : PageModel
    {
        private readonly Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext _context;

        public IndexModel(Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<TodoItem> TodoItems { get; set; } = default!;

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.TodoItem != null)
            {
                TodoItems = await _context.TodoItem.ToListAsync();
            }
        }

        /// <summary>
        /// Creates a new TODO item in the TODO List.
        /// CreationDate is set to Now, and the item is set to incomplete.
        /// </summary>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostCreateTodoItem()
        {
            if (ModelState.IsValid)
            {
                TodoItem.Complete = false;
                TodoItem.CreationDate = DateTime.Now;

                _context.TodoItem.Add(TodoItem);
                await _context.SaveChangesAsync();
            }
            return Redirect("/Index");
        }

        /// <summary>
        /// Deletes the selected TODO item in the TODO List.
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostDeleteTodoItem(int? id)
        {
            if (id == null) { return NotFound(); }

            TodoItem = await _context.TodoItem.FirstOrDefaultAsync(m => m.Id == id);
            if (TodoItem == null) {  return NotFound(); }

            _context.TodoItem.Remove(TodoItem);
            await _context.SaveChangesAsync();
            return Redirect("/Index");
        }

        /// <summary>
        /// Updates the selected TODO item's status.
        /// Alternates Complete between True and False
        /// </summary>
        /// <param name="id">The id of the item to be updated</param>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostCompleteTodoItem(int? id)
        {
            if (id == null) { return NotFound(); }

            TodoItem = await _context.TodoItem.FirstOrDefaultAsync(m => m.Id == id);
            if (TodoItem == null) { return NotFound(); }

            TodoItem.Complete = !TodoItem.Complete;
            _context.Attach(TodoItem).State=EntityState.Modified;
            await _context.SaveChangesAsync();

            return Redirect("/Index");
        }

        public async Task<IActionResult> OnPostEditTodoItem(int? id)
        {
            return Redirect("/Index");
        }

        /// <summary>
        /// Calculates the time that the item has been alive.
        /// </summary>
        /// <param name="DateCreated">CreationDate of the TODO item</param>
        /// <returns>Minutes, Hours, or Days that an item has been in the list.</returns>
        public string TimeAlive(DateTime DateCreated)
        {
            TimeSpan span = DateTime.Now - DateCreated;
            if (span.Days > 0)
            {
                if (span.Days == 1)
                {
                    return $"Added {span.Days} day ago";
                }
                return $"Added {span.Days} days ago";
            }
            else if (span.Hours > 0)
            {
                if (span.Hours == 1)
                {
                    return $"Added {span.Hours} hour ago";
                }
                return $"Added {span.Hours} hours ago";
            }
            else if (span.Minutes > 0)
            {
                if (span.Minutes == 1)
                {
                    return $"Added {span.Minutes} minute ago";
                }
                return $"Added {span.Minutes} minutes ago";
            }
            return $"Just added";
        }
    }
}