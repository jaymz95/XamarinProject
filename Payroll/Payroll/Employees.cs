using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Data; // referencing the Data class
using Xamarin.Forms;

namespace Payroll
{
    class Employees : INotifyPropertyChanged
    {

        // required event handler to implement the interface
        public event PropertyChangedEventHandler PropertyChanged;


        //Gets and sets for the JSON attributes
        public string empId { get; set; }

        private string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if (_name == value) return;   // no change
                _name = value;
                OnPropertyChanged(nameof(name));
            }
        }

        private string _salary;
        public string salary
        {
            get { return _salary; }
            set
            {
                if (_salary == value) return; // no change
                _salary = value;
                // notify the system of the change
                // there is a change, whatever is databound will update 
                OnPropertyChanged(nameof(salary));
            }
        }

        private string _wages;
        public string wages
        {
            get { return _wages; }
            set
            { 
                if (_wages == value) return; // no change
                _wages = value;
                OnPropertyChanged(nameof(wages));

                //SetValue(ref _wages, value);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        // constructor 
        public Employees()
        {

        }

        // constructor with parameters
        public Employees(string i, string n, string s, string w)
        {
            empId = i;
            name = n;
            salary = s;
            wages = w;
        }
    }
}
