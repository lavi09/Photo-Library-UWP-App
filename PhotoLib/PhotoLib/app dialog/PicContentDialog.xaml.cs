using PhotoLib.Model;
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



namespace PhotoLib.app_dialog
{
    public sealed partial class PicContentDialog : ContentDialog
    {
        private IReadOnlyList<StorageFile> pickedFileList;

        public PicContentDialog()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_OpenClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            foreach (StorageFile imageFile in pickedFileList)
            {

                var image = new Images
                {

                    SourceImageFile = imageFile,

                };

               Images.AddImageAsync(image);
            }
        }

        private void ContentDialog_CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private async void Browse_Click(object sender, RoutedEventArgs e)
        {
            var picker = new Windows.Storage.Pickers.FileOpenPicker
            {
                ViewMode = Windows.Storage.Pickers.PickerViewMode.List,
                SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary
            };

            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".gif");
            picker.FileTypeFilter.Add(".jpeg");
            pickedFileList = await picker.PickMultipleFilesAsync();

            if (pickedFileList != null && pickedFileList.Count > 0)
            {
                foreach (StorageFile file in pickedFileList)
                    Image.Text += file.Name;
                Image.TextWrapping = TextWrapping.Wrap;

                IsPrimaryButtonEnabled = true;

            }
        }
    }
}
