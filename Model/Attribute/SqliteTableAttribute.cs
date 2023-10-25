namespace Model.Attribute;

[AttributeUsage(AttributeTargets.Class)]
public class SqliteTableAttribute : System.Attribute
{
    public SqliteTableAttribute(string name)
    {
        Name = name;
    }

    public SqliteTableAttribute()
    {
    }

    public string Name { get; } = "";
}