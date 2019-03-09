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
using Windows.Storage.FileProperties;
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
        ObservableCollection<BitmapImage> ImgList = new ObservableCollection<BitmapImage>();

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
                this.VideoImageGridView.ItemsSource = pi.VideoImageList;
            }
            MySplitView.IsPaneOpen = false;
            Search.Visibility = Visibility.Visible;
            VideosText.Visibility = Visibility.Collapsed;
            AlbumsText.Visibility = Visibility.Collapsed;
            AlGridView.Visibility = Visibility.Collapsed;
        }

        private void DisplayImage_Click(object sender, RoutedEventArgs e)
        {
            pi.GetAllImagesAsync();
            this.DataContext = pi;
            VideosText.Visibility = Visibility.Visible;
            AlbumsText.Visibility = Visibility.Visible;
            AlGridView.Visibility = Visibility.Visible;
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

        private async void ImageGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Images imageInContext = (Images)e.ClickedItem;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.GetFileAsync(imageInContext.imageFileName);
            this.Frame.Navigate(typeof(FullsizeImage), file);
        }

        private void DisplayAlbumsList_Click(object sender, RoutedEventArgs e)
        {
            pi.DisplayAllAlbums();
            this.DataContext = pi;
            this.AlbumNames.ItemsSource = pi.Albums;
            MySplitView.IsPaneOpen = true;
            if (pi.Albums.Count > 0)
            {
                AlbumNames.Visibility = Visibility.Visible;
                MySplitView.IsPaneOpen = true;
            }
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
                    await messageDialog.ShowAsync();
                    return;
                }
                pi.AddAlbum(new Images
                {
                    AlbumName = plname.ToString(),

                });
            }
            else if (result == ContentDialogResult.Secondary)
            {
                dialog1.Hide();
            }
        }

        private void Albumname_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Album));
        }

        private async void VideoImageGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            MyMediaElement.Visibility = Visibility.Visible;
            Images videoInContext = (Images)e.ClickedItem;
            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile file = await localFolder.GetFileAsync(videoInContext.videoFileName);
            if (file != null)
            {
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                MyMediaElement.SetSource(fileStream, file.ContentType);
            }
            MyMediaElement.AutoPlay = true;
        }

        private async void VideoImageGridView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var count = e.AddedItems.Count;
            if (count > 0)
            {
                Images videoInContext = (Images)e.AddedItems.ElementAt(count - 1);
                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile file = await localFolder.GetFileAsync(videoInContext.videoFileName);
                if (file != null)
                {
                    IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                    MyMediaElement.SetSource(fileStream, file.ContentType);
                }
                MyMediaElement.AutoPlay = true;
            }
            
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            StorageFile f = e.Parameter as StorageFile;
            if (f != null)
            {
                StorageItemThumbnail storageItemThumbnail = await f.GetThumbnailAsync(ThumbnailMode.PicturesView, 200, ThumbnailOptions.UseCurrentScale);
                var Picture = new BitmapImage();
                Picture.SetSource(storageItemThumbnail);
                Images a = new Images
                {
                    AlbumCollection = Picture,

                };

                pi.AlbumImageList.Add(a);
            }
        }

        private void MyMediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            {
                MyMediaElement.Source = null;
                MyMediaElement.Visibility = Visibility.Collapsed;
            }
        }
    }

}