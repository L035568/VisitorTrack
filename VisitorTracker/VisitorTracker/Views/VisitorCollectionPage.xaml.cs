using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VisitorTracker.Models;
using System.Collections.ObjectModel;
using SQLite;
using VisitorTracker.Persistence;
using Microsoft.WindowsAzure.MobileServices;

namespace VisitorTracker.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VisitorCollectionPage : ContentPage
    {
        private ObservableCollection<Visitor> _visitors;
        private SQLiteAsyncConnection _connection;
        private bool _isloaded;

        MobileServiceClient _client;
        IMobileServiceTable<Visitor> _visitorTable;

        public VisitorCollectionPage()
        {
            InitializeComponent();

            //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            //https://mai-visitortracker.azurewebsites.net

            this._client = this._client = new MobileServiceClient("https://mai-visitortracker.azurewebsites.net");
            this._visitorTable = _client.GetTable<Visitor>();

        }

        protected override async void OnAppearing()
        {
            if (_isloaded)
                return;

            _isloaded = true;

            await LoadData();
      
            base.OnAppearing();
        }

        private async Task LoadData()
        {
            //await _connection.CreateTableAsync<Visitor>();

            var visitors = await _visitorTable.ToListAsync();

            _visitors = new ObservableCollection<Visitor>(visitors);

            VisitorList.ItemsSource = _visitors;
        }

        private void OnCallVisitor(object sender, EventArgs e)
        {
            var menuitem = sender as MenuItem;
            var visitor = menuitem.CommandParameter as Visitor;

            DisplayAlert("Call", visitor.VehicleNumber, "OK");

        }

        private async void OnDeleteVisitor(object sender, EventArgs e)
        {
            var visitor = (sender as MenuItem).CommandParameter as Visitor;

            if (await DisplayAlert("Warning", $"Are you sure to remove Vehicle {visitor.VehicleNumber}", "Yes", "No"))
            {
                _visitors.Remove(visitor);
                //await _connection.DeleteAsync(visitor);
                await _visitorTable.DeleteAsync(visitor);
            }
            

        }

        private async void OnNewVisitor(object sender, EventArgs e)
        {
            var page = new VisitorDetailsPage(new Visitor());

            page.VisitorAdded += (source, visitor) =>
            {
                _visitors.Add(visitor);
            };

            await Navigation.PushAsync(page);
        }

        private async void OnVisitorSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (VisitorList.SelectedItem == null)
                return;

            var selectedvisitor = e.SelectedItem as Visitor;

            VisitorList.SelectedItem = null;

            var page = new VisitorDetailsPage(selectedvisitor);
            page.VisitorUpdated += (source, visitor) =>
            {
                selectedvisitor.Id = visitor.Id;
                selectedvisitor.VehicleNumber = visitor.VehicleNumber;
                selectedvisitor.Name = visitor.Name;
                selectedvisitor.FlatNumber = visitor.FlatNumber;
                selectedvisitor.PhoneNumber = visitor.PhoneNumber;
                selectedvisitor.TimeIn = visitor.TimeIn;
                selectedvisitor.Signature = visitor.Signature;

            };

            await Navigation.PushAsync(page);

        }
    }
}