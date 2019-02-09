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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PhotoLib
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        //private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        //{
        //    // Only get results when it was a user typing, 
        //    // otherwise assume the value got filled in by TextMemberPath 
        //    // or the handler for SuggestionChosen.
        //    if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
        //    {
        //        //Set the ItemsSource to be your filtered dataset
        //        //sender.ItemsSource = dataset;
        //    }
        //}
        //private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        //{
        //    // Set sender.Text. You can use args.SelectedItem to build your text string.
        //}


        //private void AutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        //{
        //    if (args.ChosenSuggestion != null)
        //    {
        //        // User selected an item from the suggestion list, take an action on it here.
        //    }
        //    else
        //    {
        //        // Use args.QueryText to determine what to do.
        //    }
        //}

        //private async void PickAFileButton_Click(object sender, RoutedEventArgs e)
        //{
        //    // Clear previous returned file name, if it exists, between iterations of this scenario
        //    OutputTextBlock.Text = "";

        //    FileOpenPicker openPicker = new FileOpenPicker();
        //    openPicker.ViewMode = PickerViewMode.Thumbnail;
        //    openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
        //    openPicker.FileTypeFilter.Add(".jpg");
        //    openPicker.FileTypeFilter.Add(".jpeg");
        //    openPicker.FileTypeFilter.Add(".png");
        //    StorageFile file = await openPicker.PickSingleFileAsync();
        //    if (file != null)
        //    {
        //        // Application now has read/write access to the picked file
        //        OutputTextBlock.Text = "Picked photo: " + file.Name;
        //    }
        //    else
        //    {
        //        OutputTextBlock.Text = "Operation cancelled.";
        //    }
        //}

        private void TextBlock_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void FoldersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void MySplitView_PaneClosing(SplitView sender, SplitViewPaneClosingEventArgs args)
        {

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DisplayPicsList_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddPicButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FoldersButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void AddFoldersButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {

        }

        private void RootPivot_PivotItemLoading(Pivot sender, PivotItemEventArgs args)
        {

        }

        private void RootPivot_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
