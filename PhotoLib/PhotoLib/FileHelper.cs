using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;

namespace PhotoLib
{
    static class FileHelper
    {
        public static string FILE_NAME1 = "AlbumNames.txt";

        public static async void WriteImagesToFileAsync(Images image, String FILE_NAME)
        {
            StorageFolder Folder = ApplicationData.Current.LocalFolder;
            StorageFile File = await Folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.OpenIfExists);
            var lines = await FileIO.ReadLinesAsync(File);
            bool exist = false;
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var check = line.Split(',');
                    if (check[0] == image.Name)
                    {
                        exist = true;
                    }
                }
            }
            if (exist == false)
            {
                var Data = $"{image.Name},";
                Data = Data + $"{image.ID.ToString()},";
                Data = Data + Environment.NewLine;
                await FileIO.AppendTextAsync(File, Data);
            }
        }

        public static async Task<int> GetImageIDAsync(Images picture, string FILE_NAME)
        {
            int imageID = -1;
            StorageFolder Folder = ApplicationData.Current.LocalFolder;
            StorageFile File = await Folder.CreateFileAsync(FILE_NAME, CreationCollisionOption.OpenIfExists);
            var lines = await FileIO.ReadLinesAsync(File);
            foreach (var line in lines)
            {
                if (line != "")
                {
                    var lineparts = line.Split(',');
                    if (lineparts[0] == picture.Name)
                    {
                        imageID = Convert.ToInt32(lineparts[1]);
                    }
                }
            }
            return imageID;
        }

        public static async void WriteAlbumToFileAsync(Images album)
        {
            StorageFolder allPLfolder = ApplicationData.Current.LocalFolder;
            StorageFile allPLFile = await allPLfolder.CreateFileAsync(FILE_NAME1, CreationCollisionOption.OpenIfExists);
            var allplines = await FileIO.ReadLinesAsync(allPLFile);
            var AData = $"{album.AlbumName}";
            bool exist = false;
            foreach (var pline in allplines)
            {
                if (pline != null)
                {
                    if (pline == album.AlbumName)
                    {
                        exist = true;
                    }
                }
            }
            if (exist == false)
            {
                var albumfilename = album.AlbumName + Environment.NewLine;
                await FileIO.AppendTextAsync(allPLFile, albumfilename);
            }
        }

        public static async Task<ICollection<Images>> GetAllAlbumsAsync()
        {
            List<Images> albums = new List<Images>();
            StorageFolder allPLfolder = ApplicationData.Current.LocalFolder;
            StorageFile allPLFile = await allPLfolder.CreateFileAsync(FILE_NAME1, CreationCollisionOption.OpenIfExists);

            var allplines = await FileIO.ReadLinesAsync(allPLFile);

            foreach (var pline in allplines)
            {
                if (pline != "")
                {
                    var album = new Images();
                    album.AlbumName = pline;
                    albums.Add(album);
                }
            }
            return albums;
        }

        public static async Task<bool> IsDuplicateNewAlbumAsync(Images album)
        {
            var isduplicate = false;
            bool rewrite = false;
            StorageFolder allPLfolder = ApplicationData.Current.LocalFolder;
            StorageFile allPLFile = await allPLfolder.CreateFileAsync(FILE_NAME1, CreationCollisionOption.OpenIfExists);
            var allplines = await FileIO.ReadLinesAsync(allPLFile);
            bool exist = false;
            foreach (var pline in allplines)
            {
                if (pline != null)
                {
                    if (pline == album.AlbumName)
                    {
                        exist = true;
                    }
                }
            }
            if (exist == true)
            {
                if (rewrite == false)
                {
                    isduplicate = true; ;
                }
            }
            return isduplicate;
        }
    }
}
