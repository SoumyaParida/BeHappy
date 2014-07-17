using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Tasks;
using Microsoft.Phone.Shell;

namespace VideoApp
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        string listfile = "";
        public MainPage()
        {
            InitializeComponent();
            Loaded += NetworkCheck;
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
        }

       /* private void ToggleSwitch_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("The application will run under lock screen!");
            PhoneApplicationService.Current.ApplicationIdleDetectionMode = IdleDetectionMode.Disabled;
            PhoneApplicationFrame rootFrame = Application.Current.RootVisual as PhoneApplicationFrame;
          
           /* if (rootFrame != null)
            {
                rootFrame.Obscured += new EventHandler<ObscuredEventArgs>(rootFrame_Obscure);
                rootFrame.Unobscured += new EventHandler(rootFrame_Unobscured);
            }*/
        //}

       /* private void ToggleSwitch_Unchecked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("You can’t alter the change until restart the application!");
            FluxToggle.IsChecked = true;
        }*/
        /*
        void rootFrame_Unobscured(object sender, EventArgs e)
        {

            textBlockStatus.Text = "coming back from lock screen!";
            // resume the application

        }

        void rootFrame_Obscure(object sender, ObscuredEventArgs e)
        {

            textBlockStatus.Text = "going under lock screen!";
            //minimize CPU usage, and  save the settings.
        }*/

        private void NetworkCheck(object sender, EventArgs e)
        {
           /* if (DeviceNetworkInformation.IsCellularDataEnabled == false || DeviceNetworkInformation.IsWiFiEnabled == false)
            {
                MessageBoxResult m = MessageBox.Show("An Active Internet Connection is required to use this app. Check Network Settings !", "Network error", MessageBoxButton.OKCancel);
                if (m == MessageBoxResult.OK)
                {
                    ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
                    connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                    connectionSettingsTask.Show();
                }
                else if (m == MessageBoxResult.Cancel)
                {
                    NavigationService.RemoveBackEntry();
                }
            }*/
            //Dispatcher.BeginInvoke(() => NetworkCheck(null, EventArgs.Empty));
            if (DeviceNetworkInformation.IsWiFiEnabled == false)
            {
                if (DeviceNetworkInformation.IsCellularDataEnabled == false)
                {
                    MessageBoxResult m = MessageBox.Show("An Active Internet Connection is required to use this app. Check Network Settings !", "Network error", MessageBoxButton.OKCancel);
                    if (m == MessageBoxResult.OK)
                    {
                        ConnectionSettingsTask connectionSettingsTask = new ConnectionSettingsTask();
                        connectionSettingsTask.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                        connectionSettingsTask.Show();
                    }
                    else if (m == MessageBoxResult.Cancel)
                    {
                        NavigationService.RemoveBackEntry();
                    }
                }
            }

        }

        private void Button_Bollywood(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPageBollywoodtoTypeOfSong.xaml", UriKind.Relative));
        }

        private void Tamil_Click(object sender, RoutedEventArgs e)
        {
            listfile = "tamilSongs.xml";
            NavigationService.Navigate(new Uri("/BollyWood.xaml?msg=" + listfile, UriKind.Relative));
            
        }

        private void Punjabi_Click(object sender, RoutedEventArgs e)
        {
            string Album = "JukeBox";
            listfile = "Punjabi.xml";
            NavigationService.Navigate(new Uri("/SongsNameList.xaml?msg=" + Album + "-" + listfile, UriKind.Relative));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string finalSearchString = "";
            finalSearchString = SearchBox.Text + "in 720p";
            NavigationService.Navigate(new Uri("/VideoSearch.xaml?msg=" + finalSearchString, UriKind.Relative));
        }

        public EventHandler<ObscuredEventArgs> rootFrame_Obscured { get; set; }

        
    }
}