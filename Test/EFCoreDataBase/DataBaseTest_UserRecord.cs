using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;

namespace Test;

[TestFixture]
public class DataBaseTest_UserRecord
{
    private MonitorDbContext _dbContext = null!;

    [SetUp]
    public void Setup()
    {
        _dbContext = DataBaseInitailizer.GetContext();
    }

    [Test]
    public void TestAddUserRecord()
    {
        // Arrange
        var user = new User
                   {
                       Uid     = 15L,
                       Name    = "TestUser",
                       FaceUrl = "https://www.example.com",
                   };

        var userRecord1 = new UserRecord
                          {
                              TimeStamp    = 1625140800L, // 2021-07-01 12:00:00
                              FollowerNum  = 100,
                              FollowingNum = 50,
                              ArchiveNum   = 20,
                              LikeNum      = 1000,
                              Uid          = user.Uid,
                              User         = user
                          };

        var userRecord2 = new UserRecord
                          {
                              TimeStamp    = 1625227200L, // 2021-07-02 12:00:00
                              FollowerNum  = 200,
                              FollowingNum = 100,
                              ArchiveNum   = 40,
                              LikeNum      = 2000,
                              Uid          = user.Uid,
                              User         = user
                          };

        user.UserRecords = new List<UserRecord> { userRecord1, userRecord2 };

        // Act
        _dbContext.Add(user);
        _dbContext.SaveChanges();

        // Assert
        var userFromDb = _dbContext.Users.Include(u => u.UserRecords).Single(u => u.Uid == user.Uid);
        Assert.That(userFromDb.UserRecords.Count, Is.EqualTo(2));
        Assert.IsTrue(userFromDb.UserRecords.Any(ur => ur.UrId == userRecord1.UrId));
        Assert.IsTrue(userFromDb.UserRecords.Any(ur => ur.UrId == userRecord2.UrId));

        Console.WriteLine(userFromDb.ToJsonString());
    }

    [Test]
    public void TestRemoveUserRecord()
    {
        TestAddUserRecord();
        // Arrange
        var user = _dbContext.Users
                             .Include(u => u.UserRecords)
                             .Single(u => u.Uid == 15L);
        Assert.That(user.UserRecords.Count, Is.GreaterThan(0)); // Ensure there are user records to delete

        // Act
        _dbContext.Users.Remove(user);
        _dbContext.SaveChanges();

        // Assert
        var userRecords = _dbContext.UserRecords.Where(ur => ur.Uid == user.Uid).ToList();
        Assert.That(userRecords, Is.Empty); // Assert that all user records related to the user are removed
    }
}