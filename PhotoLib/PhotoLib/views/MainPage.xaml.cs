using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Storage;
using Windows.Storage.Pickers;


using PhotoLib.Model;
using PhotoLib.app_dialog;



namespace PhotoLib
{
    public sealed partial class MainPage : Page
    {
        private Images pi;
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

        private async void Upload_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new PicContentDialog();
            await dialog.ShowAsync();
            pi.GetAllImagesAsync();
        }

        private void Camera_Click(object sender, RoutedEventArgs e)
        {
           // Frame.Navigate(typeof(camera));
        }

        private void ImageGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}


