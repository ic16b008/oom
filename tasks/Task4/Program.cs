using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public enum eFileType
{
    MP3,
    FLAC,
    WEBM
}

public class MusicCollection : Task4.IMusicCollection
{
    private string my_ArtistName;
    private string my_AlbumTitle;
    private int my_ReleaseYear;
    private int my_NumOfTracks;
    private eFileType my_FileType;


    // Das hier ist ein Konstruktor (muss genauso heissen wie die Klasse)!
    public MusicCollection(string Artist, string Album, int Year, int NumTracks, eFileType FileType)
    {
        if (Artist == null || Artist.Length == 0)
            throw new Exception("No artist name!");
        if (Album == null || Album.Length == 0)
            throw new Exception("No album name!");
        if (Year < 1900 || Year > 2017)
            throw new Exception("Invalid release year!");
        if (NumTracks < 1 || NumTracks > 1000)
            throw new Exception("Invalid number of tracks!");

        my_ArtistName = Artist;
        my_AlbumTitle = Album;
        my_ReleaseYear = Year;
        my_NumOfTracks = NumTracks;
        my_FileType = FileType;
    }

    public string ArtistName
    {
        get { return my_ArtistName; }
        set
        {
            if (ArtistName == null || ArtistName.Length == 0)
                throw new Exception("No artist name!");

            my_ArtistName = value;
        }
    }

    public string AlbumTitle
    {
        get { return my_AlbumTitle; }
        set
        {
            if (AlbumTitle == null || AlbumTitle.Length == 0)
                throw new Exception("No album name!");

            my_AlbumTitle = value;
        }
    }

    public int ReleaseYear
    {
        get { return my_ReleaseYear; }
        set
        {
            if (ReleaseYear < 1900 || ReleaseYear > DateTime.Today.Year)
                throw new Exception("Invalid release year!");

            my_ReleaseYear = value;
        }
    }

    public int NumOfTracks
    {
        get { return my_NumOfTracks; }
        set
        {
            if (NumOfTracks < 1 || NumOfTracks > 1000)
                throw new Exception("Invalid number of tracks!");

            my_NumOfTracks = value;
        }
    }

    public eFileType FileType
    {
        get { return my_FileType; }
        set { my_FileType = value; }
    }

    public int AlbumAge()
    {
        return DateTime.Today.Year - this.ReleaseYear;
    }
}

namespace Task4
{
    interface IMusicCollection
    {
        string ArtistName { get; set; }
        string AlbumTitle { get; set; }
        int ReleaseYear { get; set; }
        int NumOfTracks { get; set; }
        eFileType FileType { get; set; }
        int AlbumAge();
    }

    public class MusicCollection2 : IMusicCollection
    {
        private string my_ArtistName;
        private string my_AlbumTitle;
        private int my_ReleaseYear;
        private int my_NumOfTracks;
        private eFileType my_FileType;

        public MusicCollection2(string Artist, string Album, int Year, int NumTracks, eFileType FileType)
        {
            if (Artist == null || Artist.Length == 0)
                throw new Exception("No artist name!");
            if (Album == null || Album.Length == 0)
                throw new Exception("No album name!");
            if (Year< 1900 || Year> 2017)
                throw new Exception("Invalid release year!");
            if (NumTracks< 1 || NumTracks> 1000)
                throw new Exception("Invalid number of tracks!");

            my_ArtistName = Artist;
            my_AlbumTitle = Album;
            my_ReleaseYear = Year;
            my_NumOfTracks = NumTracks;
            my_FileType = FileType;
        }

        public string ArtistName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string AlbumTitle { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int ReleaseYear { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int NumOfTracks { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public eFileType FileType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public int AlbumAge()
        {
            throw new NotImplementedException();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                MusicCollection a = new MusicCollection("Tristan Prettyman", "Hello...x", 2007, 14, eFileType.MP3);
                MusicCollection b = new MusicCollection("Tommy Stinson", "One Man Mutiny", 2011, 10, eFileType.FLAC);
                MusicCollection c = new MusicCollection("The Palms", "Untitled", 2013, 6, eFileType.WEBM);


                a.ArtistName = "Xyz";

                Console.WriteLine($"Album: {a.ArtistName}, {a.AlbumTitle}, {a.ReleaseYear}, {a.NumOfTracks}, {a.FileType}; age: {a.AlbumAge()} years");
                Console.WriteLine($"Album: {b.ArtistName}, {b.AlbumTitle}, {b.ReleaseYear}, {b.NumOfTracks}, {b.FileType}; age: {b.AlbumAge()} years");
                Console.WriteLine($"Album: {c.ArtistName}, {c.AlbumTitle}, {c.ReleaseYear}, {c.NumOfTracks}, {c.FileType}; age: {c.AlbumAge()} years");

                var vMusicCollection = new IMusicCollection[]
                {
                    new MusicCollection("Tristan Prettyman", "Hello...x", 2007, 14, eFileType.MP3),
                    new MusicCollection2("Tommy Stinson", "One Man Mutiny", 2011, 10, eFileType.FLAC)
                };
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ups, something happened ({e.Message})");
            }
        }
    }
}
