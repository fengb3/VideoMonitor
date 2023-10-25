using Lib;
using Lib.Tool;
using SQLite;

namespace Model.DataManage;

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
        var databasePath = Path.Combine(Config.PathHome, Config.Get("path_data")?.ToString() ?? "data",
                                        Config.Get("path_database_file")?.ToString()         ?? "database.db");
        _dbConnection = new SQLiteConnection(databasePath);
        CreateTables();
    }

    public void AddData<T>(T data)
    {
        _dbConnection.Insert(data);
    }

    public void UpdateData<T>(T data)
    {
        var changed = _dbConnection.Update(data);

        if (changed == 0)
            Log.Warning($"No data changed - {data}");
    }

    public void DeleteData<T>(T data)
    {
        _dbConnection.Delete(data);
    }

    public TableQuery<T> GetData<T>() where T : new()
    {
        return _dbConnection.Table<T>();
    }

    private void CreateTables()
    {
        _dbConnection.CreateTable<Video>();
        _dbConnection.CreateTable<User>();
        _dbConnection.CreateTable<UserRecord>();
        _dbConnection.CreateTable<VideoRecord>();
        _dbConnection.CreateTable<VideoType>();
    }
}