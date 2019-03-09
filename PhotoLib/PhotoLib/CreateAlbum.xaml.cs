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

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PhotoLib
{
    public sealed partial class CreateAlbum : ContentDialog
    {
        private Images pi;

        public CreateAlbum()
        {
            this.InitializeComponent();
            pi = new Images();
        }

        private void ContentDialog_AddClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Content = this.AlbumName.Text;
        }

        private void ContentDialog_CancelClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

    }

}
