using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VisitorTracker.Models;
using SQLite;
using VisitorTracker.Persistence;
using Microsoft.WindowsAzure.MobileServices;

namespace VisitorTracker.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VisitorDetailsPage : ContentPage
	{
        public event EventHandler<Visitor> VisitorAdded;
        public event EventHandler<Visitor> VisitorUpdated;

        //private SQLiteAsyncConnection _connection;
        private MobileServiceClient _client;
        private IMobileServiceTable<Visitor> _visitorTable;
        public VisitorDetailsPage (Visitor visitor)
		{
            if (visitor == null)
                throw new ArgumentNullException(nameof(visitor));
			InitializeComponent ();

            //_connection = DependencyService.Get<ISQLiteDb>().GetConnection();

            this._client = this._client = new MobileServiceClient("https://mai-visitortracker.azurewebsites.net");
            this._visitorTable = _client.GetTable<Visitor>();

            BindingContext = new Visitor
            {
                Id = visitor.Id,
                VehicleNumber = visitor.VehicleNumber,
                Name = visitor.Name,
                FlatNumber = visitor.FlatNumber,
                PhoneNumber = visitor.PhoneNumber,
                TimeIn = visitor.TimeIn,
                Signature = visitor.Signature,
            };

		}

        private async void OnVisitorSaved(object sender, EventArgs e)
        {
            var visitor = BindingContext as Visitor;
            if(visitor.Id == null)
            {
                //await _connection.InsertAsync(visitor);
                await _visitorTable.InsertAsync(visitor);
                VisitorAdded?.Invoke(this, visitor);
            }
            else
            {
                //await _connection.UpdateAsync(visitor);
                await _visitorTable.UpdateAsync(visitor);
                VisitorUpdated?.Invoke(this, visitor);
            }

            await Navigation.PopAsync();
        }
    }
}