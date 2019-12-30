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
        PickerVM ViewModel = new PickerVM();
        public MainPage()
        {
            InitializeComponent();

            BindingContext = ViewModel;
        }

        private async void searchButton_Clicked(object sender, EventArgs e)
        {
            if(comboBox.SelectedIndex != -1 && Entry.Text != null)
            {
                SmartDelivery DeliveryData = new SmartDelivery();
                try
                {
                    ViewModel.IsBusy = true;
                    SearchResult Result = DeliveryData.InvoiceSearch(comboBox.SelectedItem.ToString(), Entry.Text);
                    ViewModel.IsBusy = false;
                    await Navigation.PushAsync(new ResultPage(Result), true);
                }
                catch(Exception ex)
                {
                    await DisplayAlert("", "검색 오류", "OK");
                }
            }
            else
            {
                await DisplayAlert("", "모든 정보를 입력해 주세요", "OK");
            }
        }
    }
}
