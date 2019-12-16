using DeliveryFinder.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeliveryFinder
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ResultPage : ContentPage
    {
        private string company;
        private string invoice;

        public ResultPage()
        {
            InitializeComponent();
            BindingContext = new ResultVM(company, invoice);
        }

        public ResultPage(string company, string invoice)
        {
            this.company = company;
            this.invoice = invoice;

            //SmartDelivery DeliveryData = new SmartDelivery();
            //SearchResult Result = DeliveryData.InvoiceSearch(comboBox.SelectedItem.ToString(), Entry.Text);
            //if (Result.exception != null)
            //{
            //    await DisplayAlert("오류", Result.exception.Message, "OK");
            //}
        }
    }
}