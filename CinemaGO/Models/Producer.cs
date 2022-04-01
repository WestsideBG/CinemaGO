namespace CinemaGO.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Producer
    {
        [Key]
        public int Id { get; set; }

        public string AvatarURL { get; set; }

        public string Name { get; set; }

        public string Bio { get; set; }

        public List<Movie> Movies { get; set; }

    }
}
