using PhotoLib.Model;
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
                    if (check[0] == image.Title)
                    {
                        exist = true;
                    }
                }
            }
            if (exist == false)
            {

                var Data = $"{image.Title},";
                Data = Data + $"{image.ID.ToString()},";
                Data = Data + Environment.NewLine;
                await FileIO.AppendTextAsync(File, Data);

            }

        }

        public static async Task<string> ReadTextFileAsync(string FILE_NAME)
        {
            var localfolder = ApplicationData.Current.LocalFolder;
            var textFile = await localfolder.GetFileAsync(FILE_NAME);
            var textStream = await textFile.OpenReadAsync();
            var textReader = new DataReader(textStream);
            var textLength = textStream.Size;
            await textReader.LoadAsync((uint)textLength);
            return textReader.ReadString((uint)textLength);

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
                    if (lineparts[0] == picture.Title)
                    {
                        imageID = Convert.ToInt32(lineparts[1]);
                    }
                }
            }
            return imageID;
        }

    }
}


