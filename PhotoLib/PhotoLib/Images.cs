using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Editing;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Popups;
using Windows.UI.Xaml.Media.Imaging;

namespace PhotoLib
{
    class Images
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public BitmapImage Collection { get; set; }
        public StorageFile SourceImageFile { get; internal set; }
        private static int lastImageID = 0;
        private static string FILE_NAME = "ImageStorage.txt";
        public string imageFileName;
        public StorageFile Temp = null;
        public string videoFileName;
        public ObservableCollection<Images> ImageList { get; private set; }
        public ObservableCollection<Images> VideoImageList { get; private set; }
        public string AlbumName { get; set; }
        public BitmapImage AlbumCollection { get; set; }
        public ObservableCollection<Images> Albums { get; private set; }
        public ObservableCollection<Images> AlbumImageList { get; private set; }
        public StorageFile Temp1 { get; internal set; }

        public Images()
        {
            ImageList = new ObservableCollection<Images>();
            VideoImageList = new ObservableCollection<Images>();
            Albums = new ObservableCollection<Images>();
            AlbumImageList = new ObservableCollection<Images>();
        }

        public static async void AddImageAsync(Images image)
        {
            var imageFile = image.SourceImageFile;
            var localFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            ImageProperties imageProperties = await imageFile.Properties.GetImagePropertiesAsync();
            try
            {
                try
                {
                    Windows.Storage.StorageFile existingFile = await localFolder.GetFileAsync(imageFile.Name);
                }
                catch (FileNotFoundException)
                {
                    await imageFile.CopyAsync(localFolder);
                }

                image.Name = imageFile.Name;
                image.ID = ++lastImageID;
                FileHelper.WriteImagesToFileAsync(image, FILE_NAME);
            }
            catch (ArgumentException ex)
            {
                var messageDialog = new MessageDialog(ex.Message);
                await messageDialog.ShowAsync();
                return;
            }
        }

        public async void GetAllImagesAsync()
        {
            ImageList.Clear();
            VideoImageList.Clear();
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
                        Name = file.Name,
                        imageFileName = file.Name,
                        Collection = Picture
                    };
                    var imageid = await FileHelper.GetImageIDAsync(p, FILE_NAME);
                    p.ID = imageid;
                    ImageList.Add(p);
                }
                if (file.FileType.Equals(".mp4"))
                {
                    VideoProperties videoProperties = await file.Properties.GetVideoPropertiesAsync();
                    StorageItemThumbnail storageItemThumbnail = await file.GetThumbnailAsync(ThumbnailMode.VideosView, 200, ThumbnailOptions.UseCurrentScale);
                    var video = new BitmapImage();
                    video.SetSource(storageItemThumbnail);
                    Images v = new Images
                    {
                        Name = file.Name,
                        videoFileName = file.Name,
                        Collection = video
                    };
                    var videoid = await FileHelper.GetImageIDAsync(v, FILE_NAME);
                    v.ID = videoid;
                    VideoImageList.Add(v);
                }
            }
        }

        public void SearchImages(string str, int pageSize = 1, int currentPage = 0)
        {
            str = str.ToLower();
            var query = (from Images s in ImageList
                         where s.Name.ToLower().Contains(str)
                         select s);
            var query1 = (from Images s in VideoImageList
                          where s.Name.ToLower().Contains(str)
                          select s);
            ImageList = new ObservableCollection<Images>(query);
            VideoImageList = new ObservableCollection<Images>(query1);
        }

        public async void AddAlbum(Images a)
        {
            if (await FileHelper.IsDuplicateNewAlbumAsync(a) == false)
            {
                Albums.Add(a);
                FileHelper.WriteAlbumToFileAsync(a);
            }
            else
            {
                var messageDialog = new MessageDialog("The album name already exist.");
                await messageDialog.ShowAsync();
                return;
            }
        }

        public async void DisplayAllAlbums()
        {
            ObservableCollection<Images> tempalbums = new ObservableCollection<Images>(await FileHelper.GetAllAlbumsAsync());
            Albums.Clear();
            for (int i = 0; i < tempalbums.Count; i++)
            {
                Albums.Add(tempalbums[i]);
            }
        }
    }
}
