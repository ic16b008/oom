using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Task4_3;
using System.Reactive.Linq;
using System.Reactive.Subjects;


public enum eFileType { MP3, FLAC, WEBM }
public enum eWaterType { salt, sweet, brack }
public enum eBirdSkills { canRun, canRunFly, canRunFlySwim }


// T3.1: Implement an interface
interface IAlbum  // an interface only contains the specification of its members, but no definitions!!
{
    string ArtistName { get; }
    string AlbumTitle { get; }
    int ReleaseYear { get; }
    int NumOfTracks { get; }
    eFileType FileType { get; }
    int AlbumAge();
    void updateNumOfTracks(int numOfTracks);
}

// T3.2 Change your existing class (from last week) so that it implements your newly defined interface.
// Remark: Last week's implementation was: public class MusicCollection { ... }
//         Changing to "public class MusicCollection : Task3.IMusicCollection" references to the interface (T3.1)
public class Album : IAlbum
{
    private string my_ArtistName;
    private string my_AlbumTitle;
    private int my_ReleaseYear;
    private int my_NumOfTracks;
    private eFileType my_FileType;


    // Das hier ist ein Konstruktor (muss genauso heissen wie die Klasse)!
    public Album(string Artist, string Album, int Year, int NumTracks, eFileType FileType)
    {
        if (Artist == null || Artist.Length == 0)
            throw new Exception("No artist name!");
        if (Album == null || Album.Length == 0)
            throw new Exception("No album name!");
        if (Year < 1900 || Year > DateTime.Today.Year)
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
        get => my_ArtistName;
    }

    public string AlbumTitle
    {
        get => my_AlbumTitle;
    }

    public int ReleaseYear
    {
        get => my_ReleaseYear;
    }

    public int NumOfTracks
    {
        get => my_NumOfTracks;
    }

    public eFileType FileType
    {
        get => my_FileType;
    }

    public int AlbumAge() => DateTime.Today.Year - this.ReleaseYear;
    public void updateNumOfTracks(int numOfTracks)
    {
        if (numOfTracks < 1 || numOfTracks > 1000)
            throw new Exception("Invalid number of tracks!");
        else
            my_NumOfTracks = numOfTracks;
    }
}

namespace Task4
{
    // T3.1: Implement another interface (vertebrate = Wirbeltiere)
    public interface IVertebrate
    {
        int count { get; }
        string family { get; }
        string species { get; }
    }

    // T3.3: Add a class that implements the interface
    // derive members of interface IAnimal
    class Fish : IVertebrate
    {
        static private int my_numOfFish;
        private string my_speciesOfFish;
        private eWaterType my_waterType;
        private const string my_family = "Fish";

        public Fish(string species, eWaterType water)  // constructor of class Fish
        {
            if ((species.Trim()).Length == 0)
                throw new Exception("Invalid fish species!");
            else
            {
                my_speciesOfFish = species;
                my_waterType = water;
                my_numOfFish++;
            }
        }
        public int count
        {
            get => my_numOfFish;
        }
        public string family
        {
            get => my_family;
        }
        public string species
        {
            get => my_speciesOfFish;
         }
        public eWaterType water
        {
            get => my_waterType;
        }
    }

    // T3.3: Add one more class that implements the interface
    // derive members of interface IAnimal
    public class Bird : IVertebrate
    {
        static private int my_numOfBirds;
        private string my_speciesOfBird;
        private eBirdSkills my_skills;
        private const string my_family = "Bird";

        // new code begin
        private DateTime? _addDateTime;  // the ? after DateTime makes Datetime nullable!

        private readonly Subject<Bird> _addSubj = new Subject<Bird>();
        public IObservable<Bird> BirdAdded { get => _addSubj.AsObservable(); }

        public void MarkAdded(DateTime addDateTime)
        {
            _addDateTime = addDateTime;
            _addSubj.OnNext(this); // Raise "BirdAdded" event
        }
        // new code end

        public Bird(string species, eBirdSkills skills)  // constructor of class Bird
        {
            if ((species.Trim()).Length == 0)
                throw new Exception("Invalid bird species!");
            else
            {
                my_speciesOfBird = species;
                my_skills = skills;
                my_numOfBirds++;
            }
        }
        public int count
        {
            get => my_numOfBirds;
        }
        public string family
        {
            get => my_family;
        }
        public string species
        {
            get => my_speciesOfBird;
        }
        public eBirdSkills skills
        {
            get => my_skills;
        }
    }

    /*
    class Something
    {
        private int my_ID;
        static private int my_numOfSomethings;
        public Something(int ID)    // constructor of class Fish
        {
            my_ID = ID;
            my_numOfSomethings++;
        }
        public int count
        {
            get => my_numOfSomethings;
        }
    }
    */

    class Program
    {
        static public class TaskExamples
        {
            static int TaskCounter = 0;
            public const int DefaultSleepTime = 3000;
            static int sleepTime = DefaultSleepTime;

            static public Task<string> StringTaskDotRunAsync() => Task.Run(() => SubTaskForTask(nameof(StringTaskDotRunAsync))); //Non-Blocking

            static private async Task<string> SubTaskForTask(string taskName)
            {
                Thread.Sleep(sleepTime / 3);        //UI thread
                await Task.Delay(sleepTime / 3);    //New thread
                Thread.Sleep(sleepTime / 3);        //UI thread

                return $"Task {++TaskCounter}:  {taskName}";
            }

            static public int SleepTime
            {
                get => sleepTime; set { sleepTime = value; }
            }

            static public async Task TestTask()
            {
                await StringTaskDotRunAsync();
            }
        }

        public class TaskExamples2
        {
            static private int ComplexCalculation()
            {
                double x = 2;
                for (int i=0; i < 10000000; i++)
                    x += Math.Sqrt(x) / i;
                return (int)x;
            }

            public Task<int> ComplexCalculationAsync()
            {
                return Task.Run(() => ComplexCalculation());
            }
        }

        static void Main(string[] args)
        {
            try
            {
                Album a = new Album("Tristan Prettyman", "Hello...x", 2007, 14, eFileType.MP3);
                Album b = new Album("Tommy Stinson", "One Man Mutiny", 2011, 10, eFileType.FLAC);
                Album c = new Album("The Palms", "Untitled", 2013, 6, eFileType.WEBM);

                Console.WriteLine($"Album: {a.ArtistName}, {a.AlbumTitle}, {a.ReleaseYear}, {a.NumOfTracks}, {a.FileType}; age: {a.AlbumAge()} years");
                Console.WriteLine($"Album: {b.ArtistName}, {b.AlbumTitle}, {b.ReleaseYear}, {b.NumOfTracks}, {b.FileType}; age: {b.AlbumAge()} years");
                Console.WriteLine($"Album: {c.ArtistName}, {c.AlbumTitle}, {c.ReleaseYear}, {c.NumOfTracks}, {c.FileType}; age: {c.AlbumAge()} years");

                // T3.4: create array of interface type, loop over the array:
                var Vertebrate = new IVertebrate[]
                {
                    new Fish("Shark", eWaterType.salt),
                    new Fish("Clown Fish", eWaterType.salt),
                    new Fish("Scalare", eWaterType.sweet),
                    new Fish("Plated Catfish", eWaterType.sweet),
                    new Fish("Reedfish", eWaterType.brack),
                    new Bird("Sparrow", eBirdSkills.canRunFly),
                    new Bird("Ostrich", eBirdSkills.canRun),  // = Strauss
                    new Bird("Duck", eBirdSkills.canRunFlySwim),
                };

                Console.WriteLine("\n\nAnimals:\n");
                foreach (var x in Vertebrate)
                {
                    Console.WriteLine($"{x.family}: {x.species} ({x.count})");
                }
                SerializationExample.Run(Vertebrate);

                // T6.1
                //var producer = new Subject<Something>();

                for (int i = 0; i < 100; i++)
                {
                    var Bird_ = new Bird($"Swan_{i}", eBirdSkills.canRunFlySwim);
                    System.Threading.Thread.Sleep(TimeSpan.FromMilliseconds(10));
                    Bird_.BirdAdded.Subscribe(_ => Console.WriteLine($"Added {Bird_.species} ({Bird_.count})"));  // Subscribe
                    Bird_.MarkAdded(DateTime.Now);
                }

                Console.WriteLine($"Current time 1: {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}");
                
                Console.WriteLine($"Current time 2: {DateTime.Now.Hour}:{DateTime.Now.Minute}:{DateTime.Now.Second}.{DateTime.Now.Millisecond}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops, something happened ({e.Message})");
            }
        }
    }
}
