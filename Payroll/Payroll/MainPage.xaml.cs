using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Data;
using System.Collections.ObjectModel;
using Plugin.Media;


namespace Payroll
{
    public partial class MainPage : ContentPage
    {
        /*public MainPage()
        {
            InitializeComponent();
        }

        private void Employees_Clicked(object sender, EventArgs e)
        {

        }

        private void AddEmployee_Clicked(object sender, EventArgs e)
        {

        }

        private void EditEmployeeSalary_Clicked(object sender, EventArgs e)
        {

        }

        private void CalculateMonthlyWages_Clicked(object sender, EventArgs e)
        {

        }*/

        /*
        * if we want listview to automatically update on removal, 
        * addition to the underlying list, List class won't do it.
        * To make it happen, need to use an ObservableCollection
        * Same behaviour, but with some additions - main one is that
        * there is a INotifyCollectionChanged event built in
        * change List<Employees> to ObserverableCollection<Employees> everywhere
        */
        private ObservableCollection<Employees> empList;
        private Employees selectedEmp;


        public MainPage()
        {
            InitializeComponent();
            SetDefaultStuffMethod();
            CameraButton.Clicked += CameraButton_Clicked;
        }

        private async void CameraButton_Clicked(object sender, EventArgs e)
        {
            var photo = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions() { });

            if (photo != null)
                PhotoImage.Source = ImageSource.FromStream(() => { return photo.GetStream(); });

            /*if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == (int)Permission.Granted)
            {
                // We have permission, go ahead and use the camera.
            }
            else
            {
                // Camera permission is not granted. If necessary display rationale & request.
            }*/


        }

        private void SetDefaultStuffMethod()
        {
            // add the image on the main page
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                case Device.Android:
                    //imgMainPage.Source = (ImageSource)ImageSource.FromFile(MyData.MAINPAGE_IMAGE);
                    break;
                case Device.UWP:
                    /*imgMainPage.Source = (ImageSource)
                        ImageSource.FromFile(
                            MyData.UWP_IMG_FOLDER +
                            MyData.MAINPAGE_IMAGE);*/
                    break;
                default:
                    break;
            }
            
            if (empList == null) empList = new ObservableCollection<Employees>();

            empList = MyData.ReadEmployeesListData();
            //System.Diagnostics.Debug.WriteLine("STop STOP stOP");

            //System.Diagnostics.Debug.WriteLine(dogsList);

            //System.Diagnostics.Debug.WriteLine(MyData.ReadDogListData());
            // set the data context for the list view
            lvEmps.ItemsSource = empList;
        }

        private void lvEmps_ItemSelected(object sender,
                                    SelectedItemChangedEventArgs e)
        {
            // set the binding context for slOneElement to be the 
            // selected item on the listview
            slOneElement.BindingContext = (Employees)e.SelectedItem;
        }

        private void btnSaveFile_Clicked(object sender, EventArgs e)
        {
            // save the list of dogs to the local application folder
            MyData.SaveEmployeeListData(empList);
        }

        private void btnUpdateOne_Clicked(object sender, EventArgs e)
        {
            // use a foreach statement to check the elements of the
            // list.  if a match with the current one (slOneElement)
            // then update that one on the empList
            foreach (var emp in empList)
            {
                if (emp.empId == lblOneEmpId.Text)
                {
                    //dog.empId = entOrigin.Text;
                    //dog.name = entSize.Text;
                    //dog.salary = entExercise.Text;
                    empList.Remove(emp as Employees);
                    // clear the one element box
                    slOneElement.BindingContext = null;
                    lblCount.Text = empList.Count().ToString();
                    break;
                }
            } // end of foreach
            //RefreshEmpsListView();
            // going to implement the INotifyPropertyChanged
            // the list will automatically update (I hope)
        }

        private void RefreshEmpsListView()
        {
            lvEmps.ItemsSource = null;
            lvEmps.ItemsSource = empList;
        }

        private void DeleteContext_Clicked(object sender,
                                           EventArgs e)
        {
            // really nice to get the object that was clicked.
            Employees emp = (sender as MenuItem).CommandParameter as Employees;
            empList.Remove(emp);
            //< MenuItem
        }
    }
}
