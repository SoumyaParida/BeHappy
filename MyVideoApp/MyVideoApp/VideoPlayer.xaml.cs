using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using MyVideoApp.Resources;

namespace MyVideoApp
{
    public partial class VideoPlayer : PhoneApplicationPage
    {
        // Constructor
        string PlayerSource="";
        public VideoPlayer()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                PlayerSource = msg;
            }
           
        }
        private async void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            var videoUri = await MyToolkit.Multimedia.YouTube.GetVideoUriAsync(PlayerSource, MyToolkit.Multimedia.YouTubeQuality.Quality720P, MyToolkit.Multimedia.YouTubeQuality.Quality720P);
            if (videoUri != null)
                player.Source = videoUri.Uri;
            player.Play();
        


            /*var url = await YouTube.GetVideoUriAsync(youTubeId, YouTubeQuality.Quality720P);
            if (url != null)
            {
                player.Source = url.Uri;
                player.Play();
            }*/
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}