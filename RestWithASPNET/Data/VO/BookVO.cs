using System;

namespace RestWithASPNET.Data.VO
{
    public class BookVO
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime launchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
    }
}