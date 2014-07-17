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
using System.ComponentModel;

namespace VideoApp
{
    public partial class SongsList : PhoneApplicationPage
    {
        XDocument xmlDoc;
        
        

        public SongsList()
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
            //subGrid.Margin = new Thickness(12, 0, 0, 0);
            /*Grid grdbooltype = new Grid();
            grdbooltype.Name = "ContentPanel";
            grdbooltype.Margin = new Thickness(5, 20, 200, 0);
            grdbooltype.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
            grdbooltype.RowDefinitions.Add(new RowDefinition());
            
           

            TextBlock objtextblock = new TextBlock();
            objtextblock.Text = "Choose Movie";
            objtextblock.FontSize = 36;
          //  Margin="10,41,0,0"
            objtextblock.Margin = new Thickness(10, 41, 0, 0);
            grdbooltype.Children.Add(objtextblock);
            Grid.SetRow(objtextblock, 0);*/

            
            foreach (var obj in q)
            {
                //string videoSong = obj.;

                //Grid subGrid = new Grid();
                //subGrid.Margin = new Thickness(12, 0, 12, 0);
                HyperlinkButton txtRun = new HyperlinkButton();
                //HyperlinkButton abc = new HyperlinkButton();
                //StackPanel panel = new StackPanel();
                //panel.Orientation = System.Windows.Controls.Orientation.Vertical;
              
                    txtRun.Name = obj.songName;
                    if (!songsNameList.Contains(obj.songName))
                    {
                        songsNameList.Add(obj.songName);
                        HorizontalAlignment = HorizontalAlignment.Left;
                        HorizontalContentAlignment = HorizontalAlignment.Left;

                        txtRun.Margin = new Thickness(0, i * 30 + 100, 0, 0);

                        txtRun.FontSize = 24;
                        txtRun.VerticalAlignment = VerticalAlignment.Top;
                        txtRun.HorizontalAlignment = HorizontalAlignment.Left;
                        txtRun.HorizontalContentAlignment = HorizontalAlignment.Left;

                        txtRun.Content = obj.songName;
                        txtRun.Click += new RoutedEventHandler((sender, eventarg) => Onb2Click(sender, eventarg, obj.songName, message));
                        //panel.Children.Add(txtRun);
                        //ContentPanel.Children.Add(panel);
                        
                        //Name = Guid.NewGuid().ToString());
                       //ontentPanel.Name=ContentPanel.
                        //grdbooltype.Children.Add(panel);
                    //    txtRun.Name = obj.songName;
                        try
                        {
                            // ArgumentException is thrown because 7 is not an even number.
                            ContentPanel.Children.Add(txtRun);
                        }
                        catch (ArgumentException)
                        {
                            // Show the user that 7 cannot be divided by 2.
                            //NavigationService.Navigate(new Uri("/MainPage.xaml?", UriKind.Relative));
                        }
                        
                        i++;
                        i++;
                    }

                    //subGrid.Children.Add(panel);
                    //subGrid.Children.Add(panel);
                }
           
            
               
           // }

            //LayoutRoot.Children.Add(grdbooltype); 
        }
        void Onb2Click(object sender, RoutedEventArgs e, string name ,string message)
        {
            NavigationService.Navigate(new Uri("/SongsNameList.xaml?msg=" + name +"-"+ message, UriKind.Relative));
        }
    }
}