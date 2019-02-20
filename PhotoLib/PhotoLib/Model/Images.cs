using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoLib.Model
{
    class Images
   
    {
        public int ID { get; set; }
        public uint Height { get; set; }
        public uint Width { get; set; }
        public string Title { get; set; }
        public BitmapImage Collection { get; set; }
        public StorageFile SourceImageFile { get; internal set; }
        private static int lastImageID = 0;
        private static string FILE_NAME = "ImageStorage.txt";
        private string imageFileName;

        public ObservableCollection<Images> ImageList { get; private set; }
        public Images()
        {
            ImageList = new ObservableCollection<Images>();

        }

        // Dummy Method for testing
        //public static Pictures GetPictures()
        //{
        //    var picture = new Pictures
        //    {
        //        Title = "BlueRose"
        //    };
        //    return picture;
        //}

        public static async void AddImageAsync(Images image)
        {
            var imageFile = image.SourceImageFile;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            ImageProperties imageProperties = await imageFile.Properties.GetImagePropertiesAsync();
            try
            {

                if (imageProperties.Title == null || imageProperties.Title == "")
                {

                    throw new ArgumentException("Cannot add image file without Title");

                }
                try
                {
                    Windows.Storage.StorageFile existingFile = await localFolder.GetFileAsync(imageFile.Name);


                }
                catch (FileNotFoundException)
                {
                    await imageFile.CopyAsync(localFolder);
                }

                image.Title = imageProperties.Title;

                image.ID = ++lastImageID;
                FileHelper.WriteImagesToFileAsync(image, FILE_NAME);


            }
            catch (ArgumentException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                // Show the message dialog
                await messageDialog.ShowAsync();
                return;
            }

        }

        public async void GetAllImagesAsync()
        {
            ImageList.Clear();
            var folder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var allFiles = await folder.GetFilesAsync();
            foreach (var file in allFiles)
            {
                if (file.FileType.Equals(".jpg") || file.FileType.Equals(".png") || file.FileType.Equals(".jpeg"))
                {

                    ImageProperties imageProperties = await file.Properties.GetImagePropertiesAsync();

                    StorageItemThumbnail storageItemThumbnail = await file.GetThumbnailAsync(ThumbnailMode.PicturesView, 200, ThumbnailOptions.UseCurrentScale);
                    var Picture = new BitmapImage();
                    Picture.SetSource(storageItemThumbnail);

                    Images p = new Images
                    {
                        Title = imageProperties.Title,
                        Height = imageProperties.Height,
                        Width = imageProperties.Width,
                        imageFileName = file.Name,
                        Collection = Picture
                    };
                    var imageid = await FileHelper.GetImageIDAsync(p, FILE_NAME);
                    p.ID = imageid;
                    ImageList.Add(p);

                }
            }
        }

        public void SearchImages(string str, int pageSize = 1, int currentPage = 0)
        {
            str = str.ToLower();
            var query = (from Images s in ImageList
                         where s.Title.ToLower().Contains(str)
                         select s);
            ImageList = new ObservableCollection<Images>(query);

        }



    }
}
