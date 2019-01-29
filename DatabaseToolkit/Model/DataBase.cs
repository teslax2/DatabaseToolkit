using DatabaseToolkit.Inteface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DatabaseToolkit.Model
{
    public class Database : IDateBaseOperations, ISeedDataBase
    {
        private SqlConnectionStringBuilder _builder;
        private static readonly log4net.ILog _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Database(string dataSource, string userID, string password, string initialCatalog)
        {
            _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = dataSource;
            _builder.UserID = userID;
            _builder.Password = password;
            _builder.InitialCatalog = initialCatalog;
            _builder.ConnectTimeout = 2;
            _builder.ConnectRetryInterval = 2;
            _builder.AsynchronousProcessing = true;
        }

        public Database()
        {
            _builder = new SqlConnectionStringBuilder();
            _builder.DataSource = "localhost";
            _builder.UserID = "sa";
            _builder.Password = "Strong@Password1";
            _builder.InitialCatalog = "AssecoDB";
            _builder.ConnectTimeout = 2;
            _builder.ConnectRetryInterval = 2;
            _builder.AsynchronousProcessing = true;
        }

        public void Create()
        {
            Seed();
        }

        public async Task CreateAsync()
        {
            await SeedAsync();
        }

        public int Delete(List<AssecoStuff> data)
        {
            if (data == null)
                return 0;
            var existingItems = new List<AssecoStuff>();
            int affectedRows = 0;
            try
            {
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    existingItems = Read();

                    foreach (var item in data)
                    {
                        if (existingItems.Contains(item))
                            context.AssecoStuffs.Remove(item);
                    }
                    affectedRows = context.SaveChanges();
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Failed to Delete from database", ex);
                return affectedRows;
            }
        }

        public List<AssecoStuff> Read()
        {
            var itemsLoaded = new List<AssecoStuff>();
            try
            {
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    if (!context.Database.Exists())
                        return itemsLoaded;
                    var query = context.AssecoStuffs.ToList();
                    foreach (var item in query)
                    {
                        itemsLoaded.Add(item);
                    }

                }
                return itemsLoaded;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to Read from database", ex);
                return itemsLoaded;
            }
        }

        public async Task<List<AssecoStuff>> ReadAsync()
        {
            var itemsLoaded = new List<AssecoStuff>();
            try
            {
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    if (await CheckIfDatabaseExistsAsync(context) == false)
                        return itemsLoaded;
                    var query = await context.AssecoStuffs.ToListAsync();
                    foreach (var item in query)
                    {
                        itemsLoaded.Add(item);
                    }

                }
                return itemsLoaded;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to Read from database", ex);
                return itemsLoaded;
            }
        }

        private async Task<bool> CheckIfDatabaseExistsAsync(DbContext context)
        {
            await Task.Run(() => {
                return context.Database.Exists();
            });
            return true;
        }

        public int Seed()
        {
            try
            {
                var random = new Random();
                var image = System.IO.File.ReadAllBytes("Images/Bliss.bmp");
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    if (context.Database.Exists() == false)
                        return 0;
                    if (context.AssecoStuffs.Any())
                        return 0;
                    var items = new List<AssecoStuff>();
                    for (int i = 0; i < 100; i++)
                    {
                        var item = new AssecoStuff() { Mark = random.Next(1, 6), Ratio = random.NextDouble(), StartTime = DateTime.Now, Description = "Desription " + random.Next().ToString(), Note = "Note " + random.Next().ToString(), Attachment = image };
                        context.AssecoStuffs.Add(item);
                    }
                    var affectedRows = context.SaveChanges();
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Failed to Seed database", ex);
                return 0;
            }

        }

        public async Task<int> SeedAsync()
        {
            try
            {
                var random = new Random();
                var image = System.IO.File.ReadAllBytes("Images/Bliss.bmp");
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    if (await CheckIfDatabaseExistsAsync(context)==false)
                        return 0;
                    if (await context.AssecoStuffs.AnyAsync())
                        return 0;
                    var items = new List<AssecoStuff>();
                    for (int i = 0; i < 100; i++)
                    {
                        var item = new AssecoStuff() { Mark = random.Next(1, 6), Ratio = random.NextDouble(), StartTime = DateTime.Now, Description = "Desription " + random.Next().ToString(), Note = "Note " + random.Next().ToString(), Attachment = image };
                        context.AssecoStuffs.Add(item);
                    }
                    var affectedRows = await context.SaveChangesAsync();
                    return affectedRows;
                }
            }
            catch (Exception ex)
            {
                _log.Error("Failed to Seed database", ex);
                return 0;
            }

        }

        public int Update(List<AssecoStuff> data, bool AddOrUpdate)
        {
            int affectedRows;
            try
            {
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    foreach (var item in data)
                    {
                        if (AddOrUpdate)
                            context.AssecoStuffs.AddOrUpdate(item);
                        else
                            context.AssecoStuffs.Add(item);
                    }
                    affectedRows = context.SaveChanges();
                }
                return affectedRows;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to copy items", ex);
                return 0;
            }
        }

        public async Task<int> UpdateAsync(List<AssecoStuff> data, bool AddOrUpdate)
        {
            int affectedRows = 0;
            try
            {
                using (var context = new AssecoStuffContext(_builder.ConnectionString))
                {
                    if (await CheckIfDatabaseExistsAsync(context)==false)
                        return affectedRows;

                    foreach (var item in data)
                    {
                        if (AddOrUpdate)
                            context.AssecoStuffs.AddOrUpdate(item);
                        else
                            context.AssecoStuffs.Add(item);
                    }
                    affectedRows = await context.SaveChangesAsync();
                }
                return affectedRows;
            }
            catch (Exception ex)
            {
                _log.Error("Failed to copy items", ex);
                return 0;
            }
        }

    }
}
