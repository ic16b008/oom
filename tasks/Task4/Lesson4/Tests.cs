using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Task4;


// T4.2 Add at least 8 unit tests to the project
//   -> added 13 tests, should be sufficient ;-)

namespace Test
{
    [TestFixture]

    public class Tests
    {
        [Test]
        public void CanCreateNewAlbum()
        {
            var x = new Album("testArtist", "testAlbum", 2000, 9, eFileType.FLAC);

            Assert.IsTrue(x.ArtistName == "testArtist");
            Assert.IsTrue(x.AlbumTitle == "testAlbum");
            Assert.IsTrue(x.ReleaseYear == 2000);
            Assert.IsTrue(x.NumOfTracks == 9);
            Assert.IsTrue(x.FileType == eFileType.FLAC);
        }

        [Test]
        public void CannotCreateInvalidArtistName()
        {
            Assert.Catch(() =>
            {
                var a = new Album("", "testAlbum", 2000, 1, eFileType.FLAC);  // invalid artist name ""
            });
        }

        [Test]
        public void CannotCreateInvalidAlbumName()
        {
            Assert.Catch(() =>
            {
                var a = new Album("testArtist", "", 2000, 1, eFileType.FLAC);  // invalid album name ""
            });
        }

        [Test]
        public void CannotCreateInvalidYear()
        {
            Assert.Catch(() =>
            {
                var a = new Album("testArtist", "testAlbum", 1837, 1, eFileType.FLAC);  // invalid erlease year "1837"
            });
        }

        [Test]
        public void CannotCreateInvalidNumOfTracks()
        {
            Assert.Catch(() =>
            {
                var a = new Album("testArtist", "testAlbum", 2000, -3, eFileType.FLAC);  // invalid num. of tracks "-3"
            });
        }

        [Test]
        public void CalculateCorrectAge()
        {
            var a = new Album("testArtist", "testAlbum", 2000, 1, eFileType.FLAC);

            Assert.IsTrue(a.AlbumAge() == (DateTime.Today.Year - 2000));
        }

        [Test]
        public void CanUpdateNumOfTracks()
        {
            var a = new Album("testArtist", "testAlbum", 2000, 10, eFileType.FLAC);

            a.updateNumOfTracks(12);

            Assert.IsTrue(a.NumOfTracks == 12);
        }

        [Test]
        public void CanCreateNewFish()
        {
            var x = new Fish("testFish", eWaterType.salt);

            Assert.IsTrue(x.species == "testFish");
            Assert.IsTrue(x.water == eWaterType.salt);
        }

        [Test]
        public void ReturnCorrectAnimalFamilyFish()
        {
            var x = new Fish("testFish", eWaterType.salt);

            Assert.IsTrue(x.family == "Fish");
        }

        [Test]
        public void CanCountFish()
        {
            var f1 = new Fish("testFish1", eWaterType.salt);
            Assert.IsTrue(f1.count == 1);
            var f2 = new Fish("testFish2", eWaterType.sweet);
            Assert.IsTrue(f1.count == 2);
            Assert.IsTrue(f2.count == 2);
            var f3 = new Fish("testFish3", eWaterType.brack);
            var b1 = new Bird("testBird1", eBirdSkills.canRunFly);  // must not be counted
            Assert.IsTrue(f1.count == 3);
            Assert.IsTrue(f2.count == 3);
            Assert.IsTrue(f3.count == 3);
        }

        [Test]
        public void CanCreateNewBird()
        {
            var x = new Bird("testBird", eBirdSkills.canRunFly);

            Assert.IsTrue(x.species == "testBird");
            Assert.IsTrue(x.skills == eBirdSkills.canRunFly);
        }

        [Test]
        public void ReturnCorrectAnimalFamilyBird()
        {
            var x = new Bird("testBird", eBirdSkills.canRunFly);

            Assert.IsTrue(x.family == "Bird");
        }

        [Test]
        public void CanCountBirds()
        {
            var b1 = new Bird("testBird1", eBirdSkills.canRun);
            Assert.IsTrue(b1.count == 1);
            var b2 = new Bird("testBird2", eBirdSkills.canRunFly);
            Assert.IsTrue(b1.count == 2);
            Assert.IsTrue(b2.count == 2);
            var b3 = new Bird("testBird3", eBirdSkills.canRunFlySwim);
            Assert.IsTrue(b1.count == 3);
            Assert.IsTrue(b2.count == 3);
            Assert.IsTrue(b3.count == 3);
            var b4 = new Bird("testBird4", eBirdSkills.canRun);
            Assert.IsTrue(b4.count == 4);
            var b5 = new Bird("testBird5", eBirdSkills.canRunFly);
            Assert.IsTrue(b1.count == 5);
            Assert.IsTrue(b5.count == 5);
        }
    }
}
