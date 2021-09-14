using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteCollectionApp.Service
{
    public class SaveLoadService
    {
        private readonly string filePath = null;

        public SaveLoadService()
        {
            filePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "items.txt");
        }

        public async Task<ObservableCollection<string>> ReadItemsAsync()
        {
            ObservableCollection<string> oc = new ObservableCollection<string>();
            if (filePath != null || File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath, true))
                {
                    string line;
                    while ((line = await reader.ReadLineAsync()) != null)
                    {
                        oc.Add(line);
                    }
                }
            }
            return oc;
        }

        public async Task WriteItemsAsync(ObservableCollection<string> oc)
        {
            if(filePath != null)
            {
                using (var writer = File.CreateText(filePath))
                {
                    foreach(string item in oc)
                    {
                        await writer.WriteLineAsync(item);
                    }
                }
            }
        }


    }
}
