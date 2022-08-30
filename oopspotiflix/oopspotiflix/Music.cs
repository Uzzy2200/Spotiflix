namespace oopspotiflix
{
        internal class Music
        {
            public string? Album { get; set; }
            public string? Title { get; set; }
            public DateTime Length { get; set; }
            public DateTime ReleaseDate { get; set; }
            public string GetReleaseDate()
        {
            return ReleaseDate.ToString("D");
        }
    }
}