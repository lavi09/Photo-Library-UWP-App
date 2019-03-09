using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Camera : Page
    {
        public StorageFile TemporaryFile = null;
        private static string FILE_NAME = "ImageStorage.txt";
        private int camlastImageID = 0;
        private int camlastVideoID = 0;

        public Camera()
        {
            this.InitializeComponent();
            ElementSoundPlayer.State = ElementSoundPlayerState.On;
        }

        private async void Capture_Click(object sender, RoutedEventArgs e)
        {
            ElementSoundPlayer.Play(ElementSoundKind.Invoke);
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.AllowCropping = false;
            StorageFile photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);
            StorageFile fileCopy = await photo.CopyAsync(Windows.Storage.ApplicationData.Current.LocalFolder);

            if (photo == null)
            {                
                return;
            }

            if (TemporaryFile != null)
            {
                await TemporaryFile.DeleteAsync();
            }
            TemporaryFile = photo;
            var camImage = new Images
            {
                Temp = photo,

            };
            var camImageFile = camImage.Temp;
            camImage.Name = camImageFile.Name;
            camImage.ID = ++camlastImageID;              
            BitmapImage bitmapImage = new BitmapImage();
            FileRandomAccessStream stream = (FileRandomAccessStream)await TemporaryFile.OpenAsync(FileAccessMode.Read);
            bitmapImage.SetSource(stream);
            image.Source = bitmapImage;
            FileHelper.WriteImagesToFileAsync(camImage, FILE_NAME);
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private async void VideoButton_Click(object sender, RoutedEventArgs e)
        {
            CameraCaptureUI dialog = new CameraCaptureUI();
            dialog.VideoSettings.Format = CameraCaptureUIVideoFormat.Mp4;
            StorageFile file = await dialog.CaptureFileAsync(CameraCaptureUIMode.Video);
            StorageFile fileCopy = await file.CopyAsync(Windows.Storage.ApplicationData.Current.LocalFolder);
            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                CapturedVideo.SetSource(fileStream, "video/mp4");

            }
            var camVideo = new Images
            {
                Temp1 = file,

            };
            var camVideoFile = camVideo.Temp1;
            camVideo.Name = camVideoFile.Name;
            camVideo.ID = ++camlastVideoID;
            FileHelper.WriteImagesToFileAsync(camVideo, FILE_NAME);
        }

    }
}


