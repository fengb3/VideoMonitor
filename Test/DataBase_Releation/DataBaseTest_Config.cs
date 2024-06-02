using Lib.DataManagement;

namespace Test.DataBase_Releation;

[TestFixture]
public class DataBaseTestConfig
{
    private MonitorDbContext _context;
    
    [SetUp]
    public void Setup()
    {
        _context = DataBaseInitailizer.GetContext();
    }
    
    [Test]
    public void TestGetConfig()
    {
        // Arrange
        var config = _context.Configs.Find("buvid3");
        
        // Assert
        Assert.IsNotNull(config);

        Console.WriteLine(config.ToJsonString());
    }
}