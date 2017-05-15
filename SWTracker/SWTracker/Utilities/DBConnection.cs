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
            try
            {
                var connection = new SQLiteAsyncConnection(path);
                {
                    connection.CreateTableAsync<SummonType>();
                    connection.CreateTableAsync<SummonSession>();
                    connection.CreateTableAsync<Summon>();
                    return "Database created";
                }
            }

            catch (SQLiteException ex)
            {
                return ex.Message;
            }
            //try
            //{
            //    var connection = new SQLiteAsyncConnection(path);
            //    {
            //        connection.CreateTableAsync<MyClass>();
            //        return "Database created";
            //    }
            //}
        }
        public async Task<string> deleteDatabase(string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                await db.DropTableAsync<MyClass>();
                await db.DropTableAsync<Summon>();
                await db.DropTableAsync<SummonType>();
                await db.DropTableAsync<SummonSession>();
                return "Deleted database";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }


        //public string createTable(string path, Type x)
        //{
        //    try
        //    {
        //        var connection = new SQLiteAsyncConnection(path);
        //        {
        //            connection.CreateTableAsync<x>();
        //            return "Database created";
        //        }
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        return ex.Message;
        //    }
        //}

        public async Task<string> insertUpdateData<T>(T data, string path)
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

        public async Task<string> removeMyClass<T>(T data, string path)
        {
            try
            {
                var db = new SQLiteAsyncConnection(path);
                await db.DeleteAsync(data);
                return "Class successfully removed.";
            }
            catch (SQLiteException ex)
            {
                return ex.Message;
            }
        }

        //************************************************************************
        //SUMMONS
        //************************************************************************
        public async Task<List<Summon>> getSummonsList(string path)
        {
            var db = new SQLiteAsyncConnection(path);
            List<Summon> summonsList = await db.Table<Summon>().ToListAsync();
            return summonsList;
        }
        
        public async Task<SummonSession> getSummonSession(string path, int ID)
        {
            var db = new SQLiteAsyncConnection(path);
            return await db.Table<SummonSession>().Where(f => f.ID == ID).FirstOrDefaultAsync();
        }

        public async Task<int> getNumOfSummons(string path, int SummonSessionID, int? starNumber)
        {
            var db = new SQLiteAsyncConnection(path);
            await db.Table<SummonSession>().Where(f => f.ID == SummonSessionID).FirstOrDefaultAsync();
            await db.Table<Summon>().Where(f => f.SummonSessionID == SummonSessionID).CountAsync();
            return 1;
        }


        //************************************************************************
        //SUMMONSESSIONS
        //************************************************************************
        public async Task<List<SummonSession>> getSummonSessionList(string path)
        {
            var db = new SQLiteAsyncConnection(path);
            List<SummonSession> summonSessionList = await db.Table<SummonSession>().ToListAsync();
            return summonSessionList;
        }
        



        //public async Task<int> getNumberFromMyClass(string path)
        //{
        //    try
        //    {
        //        var db = new SQLiteAsyncConnection(path);
        //        // this counts all records in the database, it can be slow depending on the size of the database
        //        List<MyClass> myClassList = await db.Table<MyClass>().ToListAsync();
        //        MyClass firstMyClass = myClassList.FirstOrDefault();
        //        if (firstMyClass != null)
        //        {
        //            return firstMyClass.testNum;
        //        }
        //        // for a non-parameterless query
        //        // var count = db.ExecuteScalar<int>("SELECT Count(*) FROM Person WHERE FirstName="Amy");


        //        return 1;
        //    }
        //    catch (SQLiteException ex)
        //    {
        //        return -1;
        //    }
        //}
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
        
    }
}
