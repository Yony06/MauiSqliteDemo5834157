using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MauiSqliteDemo5834157
{
    public class LocalDbService
    {
        private const string DB_NAME = "demo_local_db.db3";
        private readonly SQLiteAsyncConnection _connection;
        public LocalDbService()
        {
            _connection= new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory,DB_NAME));

            _connection.CreateTableAsync<Cliente>(); 
        }

        public async Task<List<Cliente>> GetCliente()
        {
            return await _connection.Table<Cliente>().ToListAsync();
        }

        public async Task<Cliente> GetById(int id)
        {
            return await _connection.Table<Cliente>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task Create(Cliente cliente)
        {
            await _connection.InsertAsync(cliente);
        }
        public async Task Update (Cliente cliente)
        {
            await _connection.UpdateAsync(cliente);
        }
        public async Task Delete(Cliente cliente)
        {
            await _connection.DeleteAsync(cliente);
        }

    }
}
