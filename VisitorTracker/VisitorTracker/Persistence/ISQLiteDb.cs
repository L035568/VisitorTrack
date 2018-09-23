using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace VisitorTracker.Persistence
{
    public interface ISQLiteDb
    {
        SQLiteAsyncConnection GetConnection();
    }
}
