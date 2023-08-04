using System;

namespace Kadince_Todo_Ramanujam.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public DateTime CreationDate { get; set; }
        public bool Complete { get; set; }
    }
}
