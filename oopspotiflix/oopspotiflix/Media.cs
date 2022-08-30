namespace OOPSpotiflixV2
{
    internal abstract class Media
    {
        public string? Title { get; set; }
        //TODO Change type to avoid AM/PM 12:00 (Make Converter instead)
        public DateTime Length { get; set; }
        public string? Genre { get; set; }
        public DateTime ReleaseDate { get; set; } = DateTime.Today;
        public string? WWW { get; set; }

        public string GetReleaseDate()
        {
            return ReleaseDate.ToString("D");

        }
    }
}
