using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryFinder
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PickerMVVM();
        }

        private void searchButton_Clicked(object sender, EventArgs e)
        {
            SmartDelivery DeliveryData = new SmartDelivery();
            DeliveryData.InvoiceSearch(comboBox.SelectedItem.ToString(), Entry.Text);
            SearchResult Result = DeliveryData.GetSearchResult;
        }
    }
}
