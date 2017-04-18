using SQLite;
using SWTracker.Classes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace SWTracker.Utilities
{
    class DBConnection
    {
        public string createDatabase(string path)
        {
            //try
            //{
            //    var connection = new SQLiteAsyncConnection(path);
            //    {
            //        connection.CreateTableAsync<SummonType>();
            //        connection.CreateTableAsync<SummonSession>();
            //        connection.CreateTableAsync<Summon>();
            //        return "Database created";
            //    }
            //}
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                    connection.CreateTableAsync<MyClass>();
                    return "Database created";
                }
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        private async Task<string> insertUpdateData(Summon data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    await db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        public async Task<string> myClassInsertUpdateData(MyClass data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                if (await db.InsertAsync(data) != 0)
                    await db.UpdateAsync(data);
                return "Single data file inserted or updated";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }
        public async Task<int> getNumberFromMyClass(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                // this counts all records in the database, it can be slow depending on the size of the database
                List<MyClass> myClassList = await db.Table<MyClass>().ToListAsync();
                MyClass firstMyClass = myClassList.FirstOrDefault();
                if (firstMyClass != null)
                {
                    return firstMyClass.testNum;
                }
                // for a non-parameterless query
                // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");
                

                return 1;
            }
            catch (SQLiteException ex)
            {
                return -1;
            }
        }
        private async Task<int> findNumberRecords(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                // this counts all records in the database, it can be slow depending on the size of the database
                List<Summon> test = await db.Table<Summon>().Where(v => v.Name.StartsWith("a")).ToListAsync();

                // for a non-parameterless query
                // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");
                foreach (var x in test)
                {

                }

                return 3;
            }
            catch (SQLiteException ex)
            {
                return -1;
            }
        }
        public async Task<int> removeMyClass(MyClass data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                List<MyClass> myClassList = await db.Table<MyClass>().ToListAsync();
                await db.DeleteAsync(data);
                return 1;
            }
            catch (SQLiteException ex)
            {
                return -1;
            }
        }
    }
}
