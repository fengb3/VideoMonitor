using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
// using SQLite;

namespace Lib.DataManagement;

public class DatabaseHandler
{
    // private readonly SQLiteConnection _dbConnection;
    
    private readonly MonitorDbContext _context;

    private static DatabaseHandler? _instance;

    private static readonly object Locker = new();

    public static DatabaseHandler Instance
    {
        get
        {
            if (_instance != null) return _instance;

            lock (Locker)
            {
                _instance ??= new DatabaseHandler();
            }

            return _instance;
        }
    }

    private DatabaseHandler()
    {
        var configuration = new ConfigurationBuilder()
                           .SetBasePath(Directory.GetCurrentDirectory())
                           .AddJsonFile("appsettings.json")
                           .Build();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        _context = new MonitorDbContext(new DbContextOptionsBuilder<MonitorDbContext>()
                                          .UseSqlServer(connectionString)
                                          .Options);

        // context.Database.EnsureDeleted();
        _context.Database.EnsureCreated();
    }

    public void AddData<T>(T data)
    {
        // var changed = _dbConnection.Insert(data);
        //
        // if (changed == 0)
        //     Log.Warning($"No data changed when insert - {data}");

        if (data == null)
            throw new ArgumentNullException(nameof(data));    
            
        _context.Add(data);

        _context.SaveChanges();
    }

    public void UpdateData<T>(T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        
        _context.Update(data);
        _context.SaveChanges();
    }

    public void DeleteData<T>(T data)
    {
        if (data == null)
            throw new ArgumentNullException(nameof(data));
        
        _context.Remove(data);
        _context.SaveChanges();
    }
    
    public bool TryGetData<T>(Func<T, bool> func, out T? data) where T : class
    {
        // data = _dbConnection.Table<T>().FirstOrDefault(func);
        // return data != null;

        data = _context.Set<T>().FirstOrDefault(func);

        // Console.WriteLine(data);
        
        return data != null;
    }
    
    public T? GetData<T>(Func<T, bool> func) where T : class
    {
        // return _dbConnection.Table<T>().FirstOrDefault(func);
        return _context.Set<T>().FirstOrDefault(func);
    }
    
    public T? GetData<T>(object key) where T : class
    {
        // return _dbConnection.Get<T>(key);
        return _context.Find<T>(key);
    }

    // public TableQuery<T> GetAllData<T>() where T : new()
    // {
    //     // return _dbConnection.Table<T>();
    //     return _context.Set<T>();
    // }

    // private void CreateTables()
    // {
    //     _dbConnection.CreateTable<Model.RunTimeVar.Config>();
    //     _dbConnection.CreateTable<Video>();
    //     _dbConnection.CreateTable<User>();
    //     _dbConnection.CreateTable<UserRecord>();
    //     _dbConnection.CreateTable<VideoRecord>();
    //     _dbConnection.CreateTable<VideoType>();
    // }
    
}