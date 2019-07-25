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
using ConsoleAppEngine.Globals;

namespace Course_Record_v2._0
{
    public sealed partial class SignIn : Page
    {
       readonly ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
        public SignIn()
        {
            this.InitializeComponent();
            Id.Text = Hash.Instance.DisplayHash;
            Id.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Hash.Instance.VerifyHashFromConsole(Key.Text))
            {
                ApplicationDataCompositeValue composite = new ApplicationDataCompositeValue
                {
                    ["Id"] = Hash.Instance.DisplayHash,
                    ["Key"] = Key.Text,
                    ["Unregister"] = Hash.ComputeSha256Hash(Key.Text)
                };

                localSettings.Values["exampleCompositeSetting"] = composite;
                this.Frame.Navigate(typeof(ExtendedSplash));
            }
            else
                (sender as Button).Content = "Invalid Hash";
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ApplicationDataCompositeValue composite = (ApplicationDataCompositeValue)localSettings.Values["exampleCompositeSetting"];
            if (composite != null)
            {
                string idhash = composite["Id"] as string;
                string KeyHash = composite["Key"] as string;
                string UnRegister = composite["Unregister"] as string;

                if (KeyHash == Hash.ComputeSha256Hash(idhash) && UnRegister == Hash.ComputeSha256Hash(KeyHash))
                    (Window.Current.Content as Frame).Navigate(typeof(ExtendedSplash));
            }
        }
    }
}
