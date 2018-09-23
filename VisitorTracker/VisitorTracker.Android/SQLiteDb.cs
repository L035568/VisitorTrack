using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using VisitorTracker.Droid;
using VisitorTracker.Persistence;
using Xamarin.Forms;

[assembly:Dependency(typeof(SQLiteDb))]

namespace VisitorTracker.Droid
{
    public class SQLiteDb : ISQLiteDb
    {
        public SQLiteAsyncConnection GetConnection()
        {
            var documentPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
            var path = Path.Combine(documentPath, "VisitorSQLite.db3");

            return new SQLiteAsyncConnection(path);
        }
    }
}