using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Payroll
{
    class Employees : INotifyPropertyChanged
    {
        // required event handler to implement the interface
        public event PropertyChangedEventHandler PropertyChanged;

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
                // if PC == null, do nothing, else Invoke the
                // PC event handler with the two arguments
                //PropertyChanged?.Invoke(this,
                //    new PropertyChangedEventArgs(nameof(Size)));
                OnPropertyChanged(nameof(salary));
            }

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }
        
        public Employees()
        {

        }

        public Employees(string i, string n, string s)
        {
            empId = i;
            name = n;
            salary = s;
        }
    }
}
