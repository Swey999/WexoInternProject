namespace DAO
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int numberOfMovies { get; set; }

        public List<Movie> Movies { get; set; } = new List<Movie>(); // Add movie list

    }
}
