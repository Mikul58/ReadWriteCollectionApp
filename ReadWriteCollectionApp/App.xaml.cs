using ReadWriteCollectionApp.Service;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ReadWriteCollectionApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            SaveLoadService service = new SaveLoadService();
            MainPage = new MainPage(service);
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
