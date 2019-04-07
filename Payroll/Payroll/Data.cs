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
        public const string JSON_EMPLOYEES_FILE = "employee.json";
        public const string MAINPAGE_IMAGE = "emp.png";
        public const string UWP_IMG_FOLDER = "Images/";
        //UWP_IMG_FOLDER + MAINPAGE_IMAGE

        public static ObservableCollection<Employees> ReadEmployeesListData()
        {
            ObservableCollection<Employees> myList = new ObservableCollection<Employees>();
            string jsonText;

            /*string path = Environment.GetFolderPath(
                            Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine("C:/Users/James Mullarkey/source/repos/Payroll/Payroll/Payroll/data/", JSON_EMPLOYEES_FILE);
            using (var reader = new StreamReader(filename))
            {
                jsonText = reader.ReadToEnd();
                // need json library
            }*/

            var assembly = IntrospectionExtensions.GetTypeInfo(
                                                typeof(MainPage)).Assembly;

            //From the assembly where this code lives!
            assembly.GetType().Assembly.GetManifestResourceNames();

            //or from the entry point to the application - there is a difference!
            Assembly.GetExecutingAssembly().GetManifestResourceNames();

            // create the stream
            Stream stream = assembly.GetManifestResourceStream(
                                "Payroll.data.employee.json");
            using (var reader = new StreamReader(stream))
            {
                jsonText = reader.ReadToEnd();
                // include JSON library now
            }



            myList = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(jsonText);

            return myList;
        }

        public static void SaveEmployeeListData(ObservableCollection<Employees> saveList)
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
