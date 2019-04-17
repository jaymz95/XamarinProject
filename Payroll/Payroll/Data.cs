using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Text;
using Payroll;    // to access the payroll class
using Newtonsoft.Json;  // for json functions
using System.Diagnostics;

namespace Data
{
    class MyData
    {
        // JSON variable
        public const string JSON_EMPLOYEES_FILE = "employee.txt";
        public const string JSON_EMPLOYEES_EDIT_FILE = "employeeEdit.txt";
        //public const string MAINPAGE_IMAGE = "emp.jpg";
        public const string UWP_IMG_FOLDER = "Images/";
        //UWP_IMG_FOLDER + MAINPAGE_IMAGE

        public static ObservableCollection<Employees> ReadEmployeesListData()
        {
            // variables
            ObservableCollection<Employees> myList = new ObservableCollection<Employees>();
            string jsonText;

            //// TESTING CODE ////

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
                                "Payroll.data.employee.txt");
            using (var reader = new StreamReader(stream))
            {
                jsonText = reader.ReadToEnd();
                // include JSON library now
            }
            


            myList = JsonConvert.DeserializeObject<ObservableCollection<Employees>>(jsonText);

            return myList;
        }

        // SAVES EMPLOYEE DATA TO JSON when save button is clicked
        public static void SaveEmployeeListData(ObservableCollection<Employees> saveList)
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(
                                                typeof(MainPage)).Assembly;

            //From the assembly where this code lives!
            assembly.GetType().Assembly.GetManifestResourceNames();

            //or from the entry point to the application - there is a difference!
            Assembly.GetExecutingAssembly().GetManifestResourceNames();

            // create the stream
            Stream stream = assembly.GetManifestResourceStream(
                                "Payroll.data.employee.txt");

            // need the path to the file
            string path = Environment.GetFolderPath(
                Environment.SpecialFolder.LocalApplicationData);
            string filename = Path.Combine(path, JSON_EMPLOYEES_FILE);
            // use a stream writer to write the list
            using (StreamWriter writer = new StreamWriter(filename))
            {
                // stringify equivalent
                string jsonText = JsonConvert.SerializeObject(saveList);
                Debug.WriteLine(jsonText);
                writer.WriteLine(jsonText);
            }

            //// TESTING CODE ////

            /*
            string jsonText = JsonConvert.SerializeObject(saveList);
            System.IO.File.WriteAllText(@"C:/Users/James Mullarkey/source/repos/Payroll/Payroll/Payroll/data/employees.txt", "hi");

                        //open file stream
                        using (StreamWriter file = File.CreateText(filename))
                        {
                            JsonSerializer serializer = new JsonSerializer();
                            //serialize object directly into file stream
                            serializer.Serialize(file, saveList);
                        }*/

            /*
            var fileName = "reiksmes.json";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, fileName);

            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                var s = AssetManager.Open(fileName);
                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                // write to the stream
                ReadWriteStream(s, writeStream);
            }*/


        }
    }
}
