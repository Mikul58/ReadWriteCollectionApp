using MvvmHelpers.Commands;
using ReadWriteCollectionApp.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ReadWriteCollectionApp
{
    class MainPageVM :INotifyPropertyChanged
    {
        private ObservableCollection<string> collection;

        public ObservableCollection<string> Collection
        {
            get { return collection; }

            set { collection = value; OnPropertyChanged(); }
        }

        private SaveLoadService service = null;
        public SaveLoadService Service
        {
            get { return service; }
            set
            {
                service = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand SaveToFileCommand { get; set; }
        public ICommand ReadFromFileCommand { get; set; }

        public MainPageVM(SaveLoadService injectedService)
        {
            Service = injectedService;
            //Initialize Collection with some items
            Collection = new ObservableCollection<string>()
            {
                "First item",
                "second item",
                "third item"
            };

            AddItemCommand = new AsyncCommand(AddCollection);
            SaveToFileCommand = new AsyncCommand(SaveMethod);
            ReadFromFileCommand = new AsyncCommand(ReadMethod);
        }

        //Adds new string to Collection
        public async Task AddCollection()
        {
            await Task.Run( () => collection.Add("Added string"));
        }

        public async Task SaveMethod()
        {
            await service.WriteItemsAsync(collection);
        }

        public async Task ReadMethod()
        {
            Collection = await service.ReadItemsAsync();
        }



       

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
