using Lib.DataManagement;
using Microsoft.EntityFrameworkCore;

namespace Test;

[TestFixture]
public class DatabaseTest_Video
{
    private MonitorDbContext _context;

    [SetUp]
    public void Setup()
    {
        _context = DataBaseInitailizer.GetContext();
    }

    [Test]
    public void TestUserVideoRelationship()
    {
        // Arrange
        var user = new User
                   {
                       Uid     = 16L,
                       Name    = "TestUser2",
                       FaceUrl = "https://www.example2.com",
                   };

        var video1 = new Video
                     {
                         BvId      = "BV1",
                         Title     = "Test Video 1",
                         AuthorUid = user.Uid,
                     };

        var video2 = new Video
                     {
                         BvId      = "BV2",
                         Title     = "Test Video 2",
                         AuthorUid = user.Uid,
                     };

        user.Videos = new List<Video> { video1, video2 };

        // Act
        _context.Add(user);
        _context.SaveChanges();

        // Assert
        var userFromDb = _context.Users.Include(u => u.Videos).Single(u => u.Uid == user.Uid);
        Assert.That(userFromDb.Videos.Count, Is.EqualTo(2));
        Assert.IsTrue(userFromDb.Videos.Any(v => v.BvId == video1.BvId));
        Assert.IsTrue(userFromDb.Videos.Any(v => v.BvId == video2.BvId));
    }
}