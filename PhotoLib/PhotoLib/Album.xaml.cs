using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Album : Page
    {
        private Images pi;
        private static int lastAlbumImageID = 0;
        public StorageFile SourceFile { get; internal set; }

        public Album()
        {
            this.InitializeComponent();
            pi = new Images();
            pi.GetAllImagesAsync();
            this.DataContext = pi;
        }

        private void AlbumImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // AlbumImage
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
            // //Image img = sender as Image;
            //StorageFile SourceFile = 
            // await SourceFile.CopyAsync(ApplicationData.Current.LocalFolder);

        }

        private void AlbumImagesGridview_ItemClick(object sender, ItemClickEventArgs e)
        {

           Images s = (e.OriginalSource as FrameworkElement)?.DataContext as Images;
            //var Albumfilesname = imagecontext.Name;
            //var AlbumFileID = ++lastAlbumImageID;
            //var AlbumData = $"{Albumfilesname},{AlbumFileID}";

            //FileHelper.WriteAlbumImagesToFileAsync(AlbumData);

        }

        private void MenuFlyoutItem_Click(object sender, RoutedEventArgs e)
        {


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
