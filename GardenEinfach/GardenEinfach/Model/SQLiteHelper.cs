using Dapper;
using GemBox.Spreadsheet;
using SQLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GardenEinfach.Model
{
    public  class SQLiteHelper
    {
        public SQLiteAsyncConnection db;
        public SQLiteHelper(string dbPath)
        {
            try
            {
                db = new SQLiteAsyncConnection(dbPath);
                db.CreateTableAsync<PostZahl>().Wait();
            }
            catch (Exception ex)
            {

                string s = ex.Message;
            }
            
            
        }
        //Read All Items  
        public Task<List<PostZahl>> GetItemsAsync()
        {
            return db.Table<PostZahl>().ToListAsync();
        }

        public static  List<PostZahl> readPlz()
        {
            using (IDbConnection cnn = new System.Data.SQLite.SQLiteConnection("AustriaPlz_Plz_Anhang.sql"))
            {
                var output = cnn.Query<PostZahl>("select * from AustriaPlz_Plz_Anhang", new DynamicParameters());
                return output.ToList();
            }
        }
       
    }
}
