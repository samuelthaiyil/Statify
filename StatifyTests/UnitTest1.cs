using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statify.Controllers;
using Statify.Data;
using StatTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StatifyTests
{
    [TestClass]
    public class UnitTest1
    {
        private ApplicationDbContext _context;

        PlayersController playerController;

        List<Player> players = new List<Player>();

        [TestInitialize]
        public void TestInit()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            _context = new ApplicationDbContext(options);

            players.Add(new Player { Position = "SF", PlayerName = "LeBron James", PlayerHeightCm = 210, PlayerId = 1 });
            players.Add(new Player { Position = "PG", PlayerName = "Russell Westbrook", PlayerHeightCm = 190, PlayerId = 2 });
            players.Add(new Player { Position = "C", PlayerName = "Anthony Davis", PlayerHeightCm = 215, PlayerId = 3 });

            foreach(var p in players)
            {
                _context.Players.Add(p);
            }

            _context.SaveChanges();

            playerController = new PlayersController(_context);

        }

        [TestMethod]
        public void IndexReturnsData()
        {
            var result = playerController.Index();
            var viewResult = (ViewResult)result.Result;

            List<Player> model = (List<Player>)viewResult.Model;

            CollectionAssert.AreEqual(players.OrderBy(player => player.PlayerName).ToList(), model);
        }

        [TestMethod]
        public void IndexLoads()
        {
            var result = playerController.Index();
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Index", viewResult.ViewName);
        }
    }
}
