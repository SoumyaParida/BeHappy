using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Linq;

namespace MyVideoApp
{
    public partial class Playlist : PhoneApplicationPage
    {
        XDocument xmlDoc;


        public Playlist()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string msg = "";
            string message = "";


            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                message = msg;
            }
            xmlDoc = XDocument.Load(@message);

            var q = from c in xmlDoc.Descendants("VideoSong")

                    select new
                    {
                        songName = c.Attribute("name").Value,
                        name = c.Element("SongName").Value,
                        url = c.Element("SongPath").Value
                    };
            int i = 0;

            List<string> songsNameList = new List<string>();
            foreach (var obj in q)
            {
                //string videoSong = obj.;

                Grid subGrid = new Grid();
                HyperlinkButton txtRun = new HyperlinkButton();

                txtRun.Name = obj.name;
                if (!songsNameList.Contains(obj.name))
                {
                    songsNameList.Add(obj.songName);
                    HorizontalAlignment = HorizontalAlignment.Left;
                    HorizontalContentAlignment = HorizontalAlignment.Left;

                    txtRun.Margin = new Thickness(0, i * 30 + 100, 0, 0);

                    txtRun.FontSize = 24;
                    txtRun.VerticalAlignment = VerticalAlignment.Top;
                    txtRun.HorizontalAlignment = HorizontalAlignment.Left;
                    txtRun.HorizontalContentAlignment = HorizontalAlignment.Left;

                    txtRun.Content = obj.name;
                    txtRun.Click += new RoutedEventHandler((sender, eventarg) => Onb2Click(sender, eventarg, obj.url));
                    try
                    {
                        // ArgumentException is thrown because 7 is not an even number.
                        ContentPanel.Children.Add(txtRun);
                    }
                    catch (ArgumentException)
                    {
                        // Show the user that 7 cannot be divided by 2.
                        NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
                    }
                    
                    i++;
                    i++;
                }
            }

            

            // }


        }

        void Onb2Click(object sender, RoutedEventArgs e, string url)
        {
            NavigationService.Navigate(new Uri("/VideoPlayer.xaml?msg=" + url, UriKind.Relative));
        }

        
    }
}