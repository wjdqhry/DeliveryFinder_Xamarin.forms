﻿using DeliveryFinder;
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
        public ResultPage(SearchResult searchResult)
        {
            InitializeComponent();
            BindingContext = new ResultVM(searchResult);
        }
    }
}