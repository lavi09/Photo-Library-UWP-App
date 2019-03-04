using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoLib
{

    public sealed partial class MainPage : Page
    {
        private Images pi;
        private MediaPlayer mediaPlayer;


      public MainPage()
        {
            this.InitializeComponent();
            pi = new Images();
            pi.GetAllImagesAsync();
            this.DataContext = pi;
        }


        private void MySplitView_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {
            Search.Visibility = Visibility.Visible;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
            Search.Visibility = MySplitView.IsPaneOpen ? Visibility.Collapsed : Visibility.Visible;
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            pi.GetAllImagesAsync();
            MySplitView.IsPaneOpen = true;
            Search.Visibility = Visibility.Collapsed;
            SearchAutoSuggestBox.Text = "";
        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

            if (SearchAutoSuggestBox.Text.Trim() != "")
            {
                pi.SearchImages(SearchAutoSuggestBox.Text);
                this.ImageGridView.ItemsSource = pi.ImageList;
            }
            else
            {
                pi.GetAllImagesAsync();
                this.ImageGridView.ItemsSource = pi.ImageList;
            }

            MySplitView.IsPaneOpen = false;
            Search.Visibility = Visibility.Visible;

        }

        private void DisplayImage_Click(object sender, RoutedEventArgs e)
        {
            pi.GetAllImagesAsync();
            this.DataContext = pi;
        }

        public async void Upload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BrowsePics();
            await dialog.ShowAsync();
            pi.GetAllImagesAsync();

        }


        private void Camera_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Camera));
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            //_mediaPlayerElement.SetMediaPlayer(mediaPlayer);

            //Pictures videoInContext = (Pictures)e.ClickedItem;

            //StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            //StorageFile file = await localFolder.GetFileAsync(videoInContext.videoFileName);
            //if (file != null && file.ContentType==".mp4" )
            //{
            //    IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
            //    //CapturedVideo.PosterSource(fileStream, file.ContentType);
            //    _mediaPlayerElement.Source= MediaSource.CreateFromStream(fileStream, file.ContentType);
            //    mediaPlayer = _mediaPlayerElement.MediaPlayer;
            //    mediaPlayer.Play();
            //}
            this.Frame.Navigate(typeof(FullsizeImage));

        }
        private void DisplayAlbumsList_Click(object sender, RoutedEventArgs e)
        {
            pi.DisplayAllAlbums();
            this.DataContext = pi;
            this.AlbumNames.ItemsSource = pi.Albums;
        }

        private async void Create_Click(object sender, RoutedEventArgs e)
        {
            var dialog1 = new CreateAlbum();
            var result = await dialog1.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var plname = dialog1.Content;

                try
                {
                    if (plname.ToString().Contains(","))
                    {
                        throw new ArgumentException("Commas cannot be used, Please try again.");
                    }
                }
                catch (ArgumentException ex)
                {
                    var messageDialog = new MessageDialog(ex.Message);
                    // Show the message dialog
                    await messageDialog.ShowAsync();
                    return;
                }

                pi.AddAlbum(new Images
                {
                    AlbumName = plname.ToString(),

                });
                this.Frame.Navigate(typeof(Album));
            }
            else if (result == ContentDialogResult.Secondary) //cancel was selected
            {
                dialog1.Hide();
            }
        }

        private void Albumname_Click(object sender, RoutedEventArgs e)
        {


        }


    }
}
