using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using Payroll;    // to access the payroll class
using Newtonsoft.Json;  // for json functions

namespace Data
{
    class MyData
    {
        public const string JSON_EMPLOYEES_FILE = "employees.txt";
        //public const string MAINPAGE_IMAGE = "panda1.png";
        //public const string UWP_IMG_FOLDER = "Images/";
        //UWP_IMG_FOLDER + MAINPAGE_IMAGE

        public static ObservableCollection<Dogs> ReadDogListData()
        {
            ObservableCollection<Dogs> myList = new ObservableCollection<Dogs>();
            string jsonText;
            
            string path = Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine("C:/Users/James Mullarkey/source/repos/Payroll/Payroll/Payroll/data/", JSON_EMPLOYEES_FILE);
            using (var reader = new StreamReader(filename))
            {
                jsonText = reader.ReadToEnd();
                // need json library
            }

            myList = JsonConvert.DeserializeObject<ObservableCollection<Dogs>>(jsonText);

            return myList;
        }

        public static void SaveDogListData(ObservableCollection<Dogs> saveList)
        {
            // need the path to the file
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, JSON_EMPLOYEES_FILE);
            // use a stream writer to write the list
            using (var writer = new StreamWriter(filename, false))
            {
                // stringify equivalent
                string jsonText = JsonConvert.SerializeObject(saveList);
                writer.WriteLine(jsonText);
            }
        }
    }
}
