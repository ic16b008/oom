using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace MusicCollection_Test
{
    [TestFixture]

    public class Tests
    {
        [Test]
        public void SimpleTest()
        {
            Assert.IsTrue(1 == 1);
        }

        [Test]
        public void AddInvalidArtistName()
        {
            Assert.Catch(() =>
            {
                var a = new MusicCollection("", "testAlbum", 2000, 1, eFileType.FLAC);
            });
        }

        [Test]
        public void AddInvalidAlbumName()
        {
            Assert.Catch(() =>
            {
                var a = new MusicCollection("testArtist", "", 2000, 1, eFileType.FLAC);
            });
        }

        [Test]
        public void AddInvalidYear()
        {
            Assert.Catch(() =>
            {
                var a = new MusicCollection("testArtist", "testAlbum", 1837, 1, eFileType.FLAC);
            });
        }
    }
}
