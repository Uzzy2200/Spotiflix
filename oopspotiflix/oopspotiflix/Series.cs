namespace OOPSpotiflixV2
{
    internal class Series : Media
    {
        public List<Episode> Episodes { get; set; } = new();
    }
    internal class Episode : Media
    {
        public int Season { get; set; }
        public int EpisodeNum { get; set; }
        public string GetLength()
        {
            return Length.ToString("mm");
        }
    }
}