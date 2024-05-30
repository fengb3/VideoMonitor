using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;

namespace Test;

[TestFixture]
public class DataBaseTest_User
{
    private MonitorDbContext _context;

    [SetUp]
    public void Setup()
    {
        _context = DataBaseInitailizer.GetContext();
    }

    [Test]
    public void TestAddUser()
    {
        // arrange
        var user = new User
                   {
                       Uid     = 14L,
                       Name    = "TestUser",
                       FaceUrl = "https://www.example.com",
                   };
        
        // act
        _context.Add(user);
        _context.SaveChanges();
        
        // assert
        var userFromDb = _context.Find<User>(14L);
        Console.WriteLine(userFromDb.ToJsonString());
        Assert.IsNotNull(userFromDb);
        
    }

    [Test]
    public void TestRemoveUser()
    {
        // arrange
        var user = new User
                   {
                       Uid     = 24L,
                       Name    = "TestUser",
                       FaceUrl = "https://www.example.com",
                   };
        
        _context.Add(user);
        _context.SaveChanges();
        
        // act
        _context.Remove(user);
        
        // assert
        var userFromDb = _context.Find<User>(14L);
        Assert.IsNull(userFromDb);
    }
    
    [Test]
    public void UpdateUser()
    {
        // arrange
        var user = new User
                   {
                       Uid     = 14L,
                       Name    = "TestUser",
                       FaceUrl = "https://www.example.com",
                   };
        
        _context.Add(user);
        _context.SaveChanges();
        
        // act
        user.Name = "TestUser2";
        _context.Update(user);
        _context.SaveChanges();
        
        // assert
        var userFromDb = _context.Find<User>(14L);
        Assert.AreEqual("TestUser2", userFromDb.Name);
    }
    
    public void QueryUser()
    {
        // arrange
        var user = new User
                   {
                       Uid     = 14L,
                       Name    = "TestUser",
                       FaceUrl = "https://www.example.com",
                   };
        
        _context.Add(user);
        _context.SaveChanges();
        
        // act
        var userFromDb = _context.Users.Where(u => u.Name=="TestUser").FirstOrDefault();
        
        // assert
        Assert.IsNotNull(userFromDb);
    }
}