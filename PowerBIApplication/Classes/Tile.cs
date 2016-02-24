using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PowerBIApplication.Classes
{
    public class Tile
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string EmbedUrl { get; set; }
    }

    public class TilesFromJson
    {
        public Tile[] Value { get; set; }
    }
}