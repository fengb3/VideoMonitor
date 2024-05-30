using Lib.Tool;
using SQLite;
using Model;

namespace Lib.DataManagement;

public class DatabaseHandler
{
    private readonly SQLiteConnection _dbConnection;

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
        var databaseDir = Path.Combine(RunTimeVarHelper.PathHome, "data");
        
        if (!Directory.Exists(databaseDir))
        {
            Directory.CreateDirectory(databaseDir);
        }
        
        var databasePath = Path.Combine(databaseDir, "Data Source=database.db;Version=3;");
        
        // if (!File.Exists(databasePath))
        // {
        //     Log.Warning($"找不到数据库, 创建一个新的在 {databasePath}");
        //     File.Create(databasePath).Close();
        // }
        
        _dbConnection = new SQLiteConnection(databasePath);
        CreateTables();
    }

    public void AddData<T>(T data)
    {
        var changed = _dbConnection.Insert(data);
        
        if (changed == 0)
            Log.Warning($"No data changed when insert - {data}");
    }

    public void UpdateData<T>(T data)
    {
        
        var changed = _dbConnection.Update(data);

        if (changed == 0)
            Log.Warning($"No data changed when update - {data}");
    }

    public void DeleteData<T>(T data)
    {
        var changed =  _dbConnection.Delete(data);
        
        if (changed == 0)
            Log.Warning($"No data changed when delete - {data}");
    }
    
    public bool TryGetData<T>(Func<T, bool> func, out T data) where T : new()
    {
        data = _dbConnection.Table<T>().FirstOrDefault(func);
        return data != null;
    }
    
    public T? GetData<T>(Func<T, bool> func) where T : new()
    {
        return _dbConnection.Table<T>().FirstOrDefault(func);
    }
    
    public T GetData<T>(object key) where T : new()
    {
        return _dbConnection.Get<T>(key);
    }

    public TableQuery<T> GetAllData<T>() where T : new()
    {
        return _dbConnection.Table<T>();
    }

    private void CreateTables()
    {
        _dbConnection.CreateTable<Model.RunTimeVar.Config>();
        _dbConnection.CreateTable<Video>();
        _dbConnection.CreateTable<User>();
        _dbConnection.CreateTable<UserRecord>();
        _dbConnection.CreateTable<VideoRecord>();
        _dbConnection.CreateTable<VideoType>();
    }
    
}