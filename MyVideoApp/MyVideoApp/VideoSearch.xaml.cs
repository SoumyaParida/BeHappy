using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace MyVideoApp
{
    public partial class VideoSearch : PhoneApplicationPage
    {
        string finalSearchString = "";
        public VideoSearch()
        {
            InitializeComponent();
        }
        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            string msg = "";

            if (NavigationContext.QueryString.TryGetValue("msg", out msg))
            {
                finalSearchString = msg;
            }
            var wc = new WebClient();
            wc.DownloadStringCompleted += DownloadStringCompleted;
            var searchUri = string.Format(
              "http://gdata.youtube.com/feeds/api/videos?q={0}&format=6&v=2&hd",
              HttpUtility.UrlEncode(finalSearchString));
            wc.DownloadStringAsync(new Uri(searchUri));

        }

        public class YouTubeVideo
        {
            public string Title { get; set; }
            public string VideoImageUrl { get; set; }
            public string VideoId { get; set; }
        }
        void DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            var atomns = System.Xml.Linq.XNamespace.Get("http://www.w3.org/2005/Atom");
            var medians = System.Xml.Linq.XNamespace.Get("http://search.yahoo.com/mrss/");
            var xml = System.Xml.Linq.XElement.Parse(e.Result);
            if (e.Result == null)
            {
                NoResultFound.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                NoResultFound.Visibility = System.Windows.Visibility.Collapsed;
                var videos = (
              from entry in xml.Descendants(atomns.GetName("entry"))
              select new YouTubeVideo
              {
                  VideoId = entry.Element(atomns.GetName("id")).Value,
                  VideoImageUrl = (
                    from thumbnail in entry.Descendants(medians.GetName("thumbnail"))
                    //where thumbnail.Attribute("height").Value == "240"
                    select thumbnail.Attribute("url").Value).FirstOrDefault(),
                  Title = entry.Element(atomns.GetName("title")).Value
              }).ToArray();
                //ResultsList.ItemsSource = videos;
                //int i = 0;

                SearchResults.ItemsSource = videos;
            }
            
        }

        private void VideoListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var video = SearchResults.SelectedItem as YouTubeVideo;
            if (video != null)
            {
               // var parsed = video.VideoId.Split('/');
                //var id = parsed[parsed.Length - 1];
                string[] id=video.VideoId.Split(':');
                NavigationService.Navigate(new Uri("/VideoPlayer.xaml?msg=" + id[3], UriKind.Relative));
            }
        }
    }
}
