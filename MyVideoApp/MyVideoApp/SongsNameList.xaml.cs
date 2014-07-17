using System;
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
    public partial class SongsNameList : PhoneApplicationPage
    {
        XDocument xmlDoc1;
        //int i = 0;
        List<YouTubeVideo> videoItem = new List<YouTubeVideo>();
        public SongsNameList()
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
            string[] parts = message.Split(new string[] { "-" }, StringSplitOptions.None);

            xmlDoc1 = XDocument.Load(@parts[1]);

      

            var q = from c in xmlDoc1.Descendants("VideoSong")

                    select new
                    {
                        songName = c.Attribute("name").Value,
                        name = c.Element("SongName").Value,
                        url = c.Element("SongPath").Value
                    };
            int i = 0;
            /*foreach (var obj in q)
            {
                var wc = new WebClient();
                wc.DownloadStringCompleted += DownloadStringCompleted;

                var searchUri = string.Format(
                  "http://gdata.youtube.com/feeds/api/videos?q={0}&format=6&v=2&hd",
                  HttpUtility.UrlEncode(obj.url));
                wc.DownloadStringAsync(new Uri(searchUri));
            }*/

            //SearchResults.ItemsSource = videoItem;
            foreach (var obj in q)
            {
                //string videoSong = obj.;
                Grid subGrid = new Grid();


                if (parts[0] == obj.songName)
                {

                    var wc = new WebClient();
                    wc.DownloadStringCompleted += DownloadStringCompleted;

                    //var searchUri = string.Format(
                      //"http://gdata.youtube.com/feeds/api/videos?q={0}&format=6&v=2&hd",
                      //HttpUtility.UrlEncode(obj.url));
                    var searchUri = string.Format(
                    "http://gdata.youtube.com/feeds/api/videos?q={0}&v=2&hd",
                    HttpUtility.UrlEncode(obj.url));
                    wc.DownloadStringAsync(new Uri(searchUri));
                    
                    /*HyperlinkButton txtRun = new HyperlinkButton();
                    txtRun.Name = obj.name;
                    HorizontalAlignment = HorizontalAlignment.Left;
                    HorizontalContentAlignment = HorizontalAlignment.Left;

                    txtRun.Margin = new Thickness(0, i * 30 + 100, 0, 0);

                    txtRun.FontSize = 24;
                    txtRun.VerticalAlignment = VerticalAlignment.Top;
                    txtRun.HorizontalAlignment = HorizontalAlignment.Left;
                    txtRun.HorizontalContentAlignment = HorizontalAlignment.Left;

                    txtRun.Content = obj.name;
                   // txtRun.Click += new RoutedEventHandler((sender, eventarg) => Onb2ClicktoVideo(sender, eventarg, obj.url));
                    ContentPanel.Children.Add(txtRun);
                    i++;
                    i++;*/
                }
                

                
            }


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
           /* if (e.Result == null)
            {
                NoResultFound.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {*/
               // NoResultFound.Visibility = System.Windows.Visibility.Collapsed;
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
                
                videoItem.Add(videos.FirstOrDefault());
                //videoItem.Add
                
                if (videoItem != null)
                {
                    SearchResults.ItemsSource = videoItem;
                    SearchResults.ItemTemplate.LoadContent();
                    SearchResults.UpdateLayout();
                    //SearchResults.ItemsSource.Insert(videoItem);
                }
                
                
                //ResultsList.ItemsSource = videos;
                //int i = 0;
                //source.Add(VideoId, VideoImageUrl, Title);
                
               // SearchResults.ItemsSource=videos;
                //SearchResults.ItemsSource.Add(videos);
                //i++;
            //}

        }
        /*void Onb2ClicktoVideo(object sender, RoutedEventArgs e, string url)
        {
            NavigationService.Navigate(new Uri("/VideoPlayer.xaml?msg=" + url, UriKind.Relative));
        }*/
        private void VideoListSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var video = SearchResults.SelectedItem as YouTubeVideo;
            if (video != null)
            {
                // var parsed = video.VideoId.Split('/');
                //var id = parsed[parsed.Length - 1];
                string[] id = video.VideoId.Split(':');
                NavigationService.Navigate(new Uri("/VideoPlayer.xaml?msg=" + id[3], UriKind.Relative));
            }
        }
    }
 }
  