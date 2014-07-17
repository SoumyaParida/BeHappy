using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace VideoApp
{
    public partial class MainPageBollywoodtoTypeOfSong : PhoneApplicationPage
    {
        string listfile = "";

        public MainPageBollywoodtoTypeOfSong()
        {
            InitializeComponent();
        }

        /*private void NewReleases(object sender, RoutedEventArgs e)
        {
            
        }*/

        private void Playlist_Click(object sender, RoutedEventArgs e)
        {
            listfile="Playlist.xml";
            NavigationService.Navigate(new Uri("/Playlist.xaml?msg="+listfile, UriKind.Relative));
            
        }

        private void PlayList(object sender, RoutedEventArgs e)
        {
            listfile = "NewReleasesSongs.xml";
            NavigationService.Navigate(new Uri("/BollyWood.xaml?msg="+listfile, UriKind.Relative));
        }

        private void Artists_Click(object sender, RoutedEventArgs e)
        {
            listfile="Artists.xml";
            NavigationService.Navigate(new Uri("/Playlist.xaml?msg=" + listfile, UriKind.Relative));
        }

        private void Albums_Click(object sender, RoutedEventArgs e)
        {
            //listfile = "Album.xml";
            //NavigationService.Navigate(new Uri("/BollyWood.xaml?msg=" + listfile, UriKind.Relative));
            string Album = "JukeBox";
            listfile = "Album.xml";
            NavigationService.Navigate(new Uri("/SongsNameList.xaml?msg=" + Album + "-" + listfile, UriKind.Relative));
        }
        

        


    }
}