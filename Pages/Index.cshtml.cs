using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kadince_Todo_Ramanujam.Data;
using Kadince_Todo_Ramanujam.Models;
using Microsoft.AspNetCore.Http;
using System.Drawing;

namespace Kadince_Todo_Ramanujam.Pages
{
    public class IndexModel : PageModel
    {
        //Filters
        public const string _FilterAll = "_FilterAll";
        public const string _FilterComplete = "_FilterComplete";
        public const string _FilterIncomplete = "_FilterIncomplete";

        //Colors
        public string Purple = "#EADEFC";
        public string Blue = "#C9E8FF";
        public string Green = "#E3FFE9";
        public string Yellow = "#FFFFE3";
        public string Orange = "#FFE5C4";
        public string Red = "#FFCAC9";

        private readonly Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext _context;

        public IndexModel(Kadince_Todo_Ramanujam.Data.Kadince_Todo_RamanujamContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IList<TodoItem> TodoItems { get; set; } = default!;

        [BindProperty]
        public TodoItem TodoItem { get; set; }

        [BindProperty]
        public bool FilterAll { get; set; }

        [BindProperty]
        public bool FilterComplete { get; set; }

        [BindProperty]
        public bool FilterIncomplete { get; set; }

        public async Task OnGetAsync()
        {
            IQueryable<TodoItem> query = from x in _context.TodoItem select x;

            // If this is a new session, set the filter to all
            if (HttpContext.Session.GetInt32(_FilterAll) is null)
            {
                HttpContext.Session.SetInt32(_FilterAll, 1);
            }
            // If this is an existing session, retain filters between requests
            else if (HttpContext.Session.GetInt32(_FilterAll) == 0)
            {
                if (HttpContext.Session.GetInt32(_FilterComplete) == 1)
                {
                    query = query.Where(x => x.Complete == true);
                }
                if (HttpContext.Session.GetInt32(_FilterIncomplete) == 1)
                {
                    query = query.Where(x => x.Complete == false);
                }
            }
            TodoItems = query.ToList();
        }

        /// <summary>
        /// Posts filters to the session, so as to retain them between requests.
        /// </summary>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostFilterTodoItems()
        {
            // If filter all is ever selected, or every filter is selected, set all filters to 0
            if (FilterAll || (FilterComplete && FilterIncomplete))
            {
                HttpContext.Session.SetInt32(_FilterAll, 1);
                HttpContext.Session.SetInt32(_FilterComplete, 0);
                HttpContext.Session.SetInt32(_FilterIncomplete, 0);

            }
            // Otherwise, set each individual filter
            else
            {
                HttpContext.Session.SetInt32(_FilterAll, 0);
                if (FilterComplete)
                {
                    HttpContext.Session.SetInt32(_FilterComplete, 1);
                }
                else
                {
                    HttpContext.Session.SetInt32(_FilterComplete, 0);
                }

                if (FilterIncomplete)
                {
                    HttpContext.Session.SetInt32(_FilterIncomplete, 1);
                }
                else
                {
                    HttpContext.Session.SetInt32(_FilterIncomplete, 0);
                }
            }
            return Redirect("/Index");
        }

        /// <summary>
        /// Creates a new TODO item in the TODO List.
        /// CreationDate is set to Now, and the item is set to incomplete.
        /// </summary>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostCreateTodoItem()
        {
            TodoItem.Complete = false;
            TodoItem.CreationDate = DateTime.Now;
            TodoItem.Color = Yellow;

            if (ModelState.IsValid)
            {
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

        /// <summary>
        /// Saves edits to a todo item's Title or Details.
        /// </summary>
        /// <returns>Redirect to Index</returns>
        public async Task<IActionResult> OnPostEditTodoItem()
        {
            if (TodoItem == null) { return NotFound(); }
            _context.Attach(TodoItem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

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