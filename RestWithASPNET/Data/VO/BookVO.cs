using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using RestWithASPNET.Hypermedia;
using RestWithASPNET.Hypermedia.Abstract;

namespace RestWithASPNET.Data.VO
{
    public class BookVO : ISupportsHyperMedia
    {
        public int Id { get; set; }
        public string Author { get; set; }
        public DateTime launchDate { get; set; }
        public decimal Price { get; set; }
        public string Title { get; set; }
        public bool Enabled { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}