using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestWithASPNET.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Author { get; set; }
        [Column("launch_date")]
        public DateTime launchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}