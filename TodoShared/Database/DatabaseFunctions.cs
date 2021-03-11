using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoLocalized.Model;
using TodoLocalized.Model.DbModel;

namespace TodoLocalized.Database
{
    public class DatabaseFunctions
    {
        readonly SQLiteAsyncConnection database;

        public DatabaseFunctions(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            
            database.CreateTableAsync<Ipharmacy>().Wait();
            database.CreateTableAsync<IMedicalAlarm>().Wait();
            database.CreateTableAsync<IFirstRun>().Wait();
            database.CreateTableAsync<IHospitals>().Wait();
            database.CreateTableAsync<ILab>().Wait();
            database.CreateTableAsync<IManualForm>().Wait();


            database.CreateTableAsync<IAccounts>().Wait();

        }
        ///////////////////////////////////////////////Lab/////////////////////////////////////////////////////////////
  
        public Task<List<ILab>> GetLabAsyncByAccountID(int account_id)
        {
            return database.QueryAsync<ILab>("SELECT * FROM [lab] WHERE [account_id] = " + account_id);
        }
        public Task<ILab> GetLabAsync(int ID)
        {
            return database.Table<ILab>().Where(i => i.id == ID).FirstOrDefaultAsync();
        }
        public Task<int> SaveLabAsync(ILab item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteLabAsync(ILab item)
        {
            return database.DeleteAsync(item);
        }
        ///////////////////////////////////////////////FirstRun////////////////////////////////////////////////////////

        public Task<List<IFirstRun>> GetAllFirst( ) {
            return database.Table<IFirstRun>().ToListAsync();
        }
        public Task<IFirstRun> GetFirst(String key)
        {
            return database.Table<IFirstRun>().Where(i => i.key.Equals(key)).FirstOrDefaultAsync();
        }
        public Task<int> SaveFirst(IFirstRun item) {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                
                return database.InsertAsync(item);
            }
        }

        //////////////////////////////////////////////////Accounts////////////////////////////////////////////////////

        public Task<List<IAccounts>> GetAccountsAsync()
        {
            return database.Table<IAccounts>().ToListAsync();
        }

        public Task<int> SaveAccountAsync(IAccounts item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        /////////////////////////////////////////////////pharmacy////////////////////////////////////////////////////


        public Task<List<Ipharmacy>> GetItemsAsync()
        {
            return database.Table<Ipharmacy>().ToListAsync();
        }

        public Task<List<Ipharmacy>> GetItemsNotDoneAsync()
        {
            return database.QueryAsync<Ipharmacy>("SELECT * FROM [pharmacy] WHERE [Done] = 0");
        }
        public Task<List<Ipharmacy>> GetItemAsyncByAccountID(int account_id)
        {
            return database.QueryAsync<Ipharmacy>("SELECT * FROM [pharmacy] WHERE [account_id] = "+account_id);
        }
        public Task<Ipharmacy> GetItemAsync(int ID)
        {
            return database.Table<Ipharmacy>().Where(i => i.id == ID).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Ipharmacy item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteItemAsync(Ipharmacy item)
        {
            return database.DeleteAsync(item);
        }

        /////////////////////////////////  ALARM   /////////////////////////////////////////////

        public Task<List<IMedicalAlarm>> GetAlarmAsync()
        {
            return database.Table<IMedicalAlarm>().ToListAsync();
        }

        public Task<List<IMedicalAlarm>> GetAlarmActiveAsync()
        {
            return database.QueryAsync<IMedicalAlarm>("SELECT * FROM [MedicalAlarm] WHERE [alarm_status] = 1");
        }
        public Task<List<IMedicalAlarm>> GetAlarmByAccount(int accountId)
        {
            return database.QueryAsync<IMedicalAlarm>("SELECT * FROM [MedicalAlarm] WHERE [account_id] = "+accountId);
        }

        public Task<IMedicalAlarm> GetAlarmAsync(int id)
        {
            return database.Table<IMedicalAlarm>().Where(i => i.id == id).FirstOrDefaultAsync();
        }
        public Task<IMedicalAlarm> GetAlarmByMedical(int MID)
        {
            return database.Table<IMedicalAlarm>().Where(i => i.M_id == MID).FirstOrDefaultAsync();
        }
        public Task<IMedicalAlarm> GetAlarmByMedicalAndAccount(int account_id,int MID)
        {
            return database.Table<IMedicalAlarm>().Where(i => i.M_id == MID && i.account_id == account_id).FirstOrDefaultAsync();
        }
        public Task<int> SaveAlarmAsync(IMedicalAlarm item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteAlarmAsync(IMedicalAlarm item)
        {
            return database.DeleteAsync(item);
        }
        //////////////////////////////////////////////////////////////Manual Form///////////////////////////////////////////////////////////

        public Task<List<IManualForm>> GetFormAsync()
        {
            return database.Table<IManualForm>().ToListAsync();
        }
        public Task<List<IManualForm>> GetFormAsyncByAccountID(int account_id)
        {
            return database.QueryAsync<IManualForm>("SELECT * FROM [manual_form] WHERE [account_id] = " + account_id);
        }
        public Task<IManualForm> GetFormAsync(int ID)
        {
            return database.Table<IManualForm>().Where(i => i.id == ID).FirstOrDefaultAsync();
        }

        public Task<int> SaveFormAsync(IManualForm item)
        {
            if (item.id != 0)
            {
                return database.UpdateAsync(item);
            }
            else
            {
                return database.InsertAsync(item);
            }
        }

        public Task<int> DeleteFormAsync(IManualForm item)
        {
            return database.DeleteAsync(item);
        }
    }
}
