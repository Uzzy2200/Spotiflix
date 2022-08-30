namespace OOPSpotiflixV2
{
    internal class Gui
    {
        private Data data = new Data();
        private string path = @"/Users/uzzy/Projects/oopspotiflix/Data/Saveddata.rtf";
        public Gui()
        {
            while (true)
            {
                Menu();
            }
        }
        private void Menu()
        {
            Console.WriteLine("\nMenu\n1 Movies\n2 Series\n3 Music\n4 Save Data\n5 Load Data");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    MovieMenu();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SeriesMenu();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    MusicMenu();
                    break;
                case ConsoleKey.NumPad4:
                case ConsoleKey.D4:
                    SaveData();
                    break;
                case ConsoleKey.NumPad5:
                case ConsoleKey.D5:
                    LoadData();
                    break;
                default:
                    break;
            }
        }
        #region DataHandling
        private void SaveData()
        {
            string json = System.Text.Json.JsonSerializer.Serialize(data);
            File.WriteAllText(path, json);
            Console.WriteLine("File saved succesfully at " + path);
        }

        private void LoadData()
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Creating new file at: " + path);
                SaveData();
            }
            string json = File.ReadAllText(path);
            data = System.Text.Json.JsonSerializer.Deserialize<Data>(json);
            Console.WriteLine("File loaded succesfully from " + path);
        }
        #endregion
        #region Get input
        private DateTime GetLength()
        {
            DateTime time;
            do
            {
                Console.Write("Length (hh:mm:ss): ");
            }
            while (!DateTime.TryParse("0001-01-01 " + Console.ReadLine(), out time));
            return time;
        }

        private DateTime GetReleaseDate()
        {
            DateTime date;
            string input = "";
            do
            {
                Console.Write("Release Date (dd/mm/yyyy): ");
                input = Console.ReadLine();
                if (input == "") return DateTime.Today;
            }
            while (!DateTime.TryParse(input, out date));
            return date;
        }

        private string GetString(string type)
        {
            string? input;
            do
            {
                Console.Write(type);
                input = Console.ReadLine();
                if (input == "") return "Unknown";
            }
            while (input == null);
            return input;
        }

        private int GetInt(string request)
        {
            int i;
            do
            {
                Console.Write(request);
            }
            while (!int.TryParse(Console.ReadLine(), out i));
            return i;
        }
        #endregion        
        #region Movies
        private void MovieMenu()
        {
            Console.WriteLine("\nMovie Menu\n1 Movies\n2 Search Movies\n3 Add new Movie");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowMovieList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchMovie();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddMovie();
                    break;
                default:
                    break;
            }
        }

        private void AddMovie()
        {
            Movie movie = new Movie();
            movie.Title = GetString("Title: ");
            movie.Length = GetLength();
            movie.Genre = GetString("Genre: ");
            movie.ReleaseDate = GetReleaseDate();
            movie.WWW = GetString("WWW: ");

            ShowMovie(movie);
            Console.WriteLine("Confirm adding to list (Y/N)");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.MovieList.Add(movie);
        }
        private void SearchMovie()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine().ToLower();
            foreach (Movie movie in data.MovieList)
            {
                if (search != null)
                {
                    //TODO ToLower or ToUpper to match any case
                    if (movie.Title.ToLower().Contains(search) || movie.Genre.ToLower().Contains(search))
                        ShowMovie(movie);
                }
            }
        }
        private void ShowMovie(Movie m)
        {
            Console.WriteLine($"{m.Title} {m.GetLength()} {m.Genre} {m.GetReleaseDate()} {m.WWW}");
        }

        private void ShowMovieList()
        {
            if (data.MovieList == null || data.MovieList.Count == 0) return;
            foreach (Movie m in data.MovieList)
            {
                ShowMovie(m);
            }
        }
        #endregion
        #region Series
        private void SeriesMenu()
        {
            Console.WriteLine("\nSeries Menu\n1 Series\n2 Search series\n3 Add new Series");

            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowSeriesList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    SearchSeries();
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddSeries();
                    break;
                default:
                    break;
            }
        }
        private void AddSeries()
        {
            Series series = new();
            //TODO Ask for input on series
            series.Title = GetString("Title: ");
            //series.Length = GetLength();
            series.Genre = GetString("Genre: ");
            series.ReleaseDate = GetReleaseDate();
            series.WWW = GetString("WWW: ");

            Console.WriteLine("Add series (Y/N)?");
            if (Console.ReadKey().Key == ConsoleKey.Y) data.SerieList.Add(series);

            Console.WriteLine("Add episode?");
            if (Console.ReadKey().Key == ConsoleKey.Y) AddEpisode(series);
        }
        private void AddEpisode(Series series)
        {
            do
            {
                Episode episode = new();
                episode.Title = GetString("Title: ");
                episode.Season = GetInt("Season: ");
                episode.EpisodeNum = GetInt("Episode number: ");
                episode.Length = GetLength();
                episode.ReleaseDate = GetReleaseDate();

                Console.WriteLine("Add episode (Y/N)?");
                if (Console.ReadKey().Key == ConsoleKey.Y) series.Episodes.Add(episode);
                Console.WriteLine("Add another episode?");
            }
            while (Console.ReadKey().Key == ConsoleKey.Y);
        }
        private void SearchSeries()
        {
            Console.Write("Search: ");
            string? search = Console.ReadLine();
            foreach (Series series in data.SerieList)
            {
                if (search != null)
                {
                    if (series.Title.Contains(search) || series.Genre.Contains(search))
                        ShowSeries(series);
                }
            }
        }
        private void ShowSeries(Series s)
        {
            Console.WriteLine($"{s.Title}  {s.Genre} {s.GetReleaseDate()} {s.WWW}");
            foreach (Episode e in s.Episodes)
            {
                //TODO Show episode
                Console.WriteLine($"{e.Title}");
            }
        }

        private void ShowSeriesList()
        {
            foreach (Series s in data.SerieList)
            {
                ShowSeries(s);
            }
        }
        #endregion
        #region Music
        private void MusicMenu()
        {
            Console.WriteLine("\nMusic Menu\n1 Music\n2 Search Music\n3 Add Music");
            switch (Console.ReadKey().Key)
            {

                case ConsoleKey.NumPad1:
                case ConsoleKey.D1:
                    ShowMusicList();
                    break;
                case ConsoleKey.NumPad2:
                case ConsoleKey.D2:
                    break;
                case ConsoleKey.NumPad3:
                case ConsoleKey.D3:
                    AddMusic();
                    break;


                default:
                    break;
            }

        }
        private void AddMusic()
        {
            Album album = new Album();
            album.Title = GetString("Album Title: ");
            album.Artist = GetString("Artist: ");
            album.Genre = GetString("Genre: ");
            album.ReleaseDate = GetReleaseDate();
            album.WWW = GetString("WWW: ");

            ShowAlbum(album);
            Console.WriteLine("Add album to list?");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
                data.MusicList.Add(album);

            Console.WriteLine("Add new song to album?");
            while (Console.ReadKey(true).Key == ConsoleKey.Y)
            {
                AddSong(album);
                Console.WriteLine("Add another song to album? (y/n)");
            };
        }

        private void AddSong(Album album)
        {
            Song song = new Song();
            song.Title = GetString("Song Title: ");
            song.Artist = GetInputOrParent(album.Artist, "Artist: ");
            song.Genre = GetInputOrParent(album.Genre, "Genre: ");
            song.ReleaseDate = GetReleaseDate();
            song.Length = GetLength();
            Console.WriteLine("Add this song to album? (y/n)");
            if (Console.ReadKey(true).Key == ConsoleKey.Y)
                album.Songs.Add(song);
        }
        private string GetInputOrParent(string parent, string type)
        {
            Console.Write(type);
            string input = Console.ReadLine();
            if (input != "") parent = input;
            return parent;
        }

        private void ShowMusicList()
        {
            foreach (Album album in data.MusicList)
            {
                ShowAlbum(album, true);
            }
        }

        private void ShowAlbum(Album album, bool showSongs = false)
        {
            //TODO Show album details
            Console.WriteLine($"Album title: {album.Title}");
            if (showSongs)
            {
                foreach (Song song in album.Songs)
                {
                    ShowSong(song);
                }
            }
        }
        private void ShowSong(Song song)
        {
            //TODO Show song details
            Console.Write($"\tSong Title: {song.Title}\tLenght: {GetLength()}\t");
            //if (song.Artist != al)
        }

        #endregion
    }
}