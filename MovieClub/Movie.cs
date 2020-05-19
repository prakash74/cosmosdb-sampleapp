using System;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MovieDocumentsClub
{
    public class Movie
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [Display(Name = "Movie Name")]
        [JsonProperty(PropertyName = "movieName")]
        public string MovieName { get; set; }

      
        [JsonProperty(PropertyName = "rating")]
        public string Rating { get; set; }

      
        [Display(Name = "Release Date")]
        [JsonProperty(PropertyName = "releaseDate")]
        public DateTime ReleaseDate { get; set; }
    }

}
