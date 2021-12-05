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
            players.Add(new Player { Position = "PG", PlayerName = "Rajon Rondo", PlayerHeightCm = 186, PlayerId = 4 });

            foreach (var p in players)
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

        [TestMethod]
        public void EditRedirectsToIndexAfterSave()
        {
            var player = players[3];
            player.PlayerHeightCm = 209;
            var result = playerController.Edit(player.PlayerId, player);
            var redirectResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", redirectResult.ActionName);
        }

        [TestMethod]
        public void EditReturnsErrorWhenNullId()
        {
            var result = playerController.Edit(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void EditReturnsErrorWhenInvalidId()
        {
            var result = playerController.Edit(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void EditViewLoads()
        {
            var result = playerController.Edit(1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Edit", viewResult.ViewName);
        }

        [TestMethod]
        public void EditViewLoadsCorrectPlayerModel()
        {
            var result = playerController.Edit(3);
            var viewResult = (ViewResult)result.Result;
            Player playerModel = (Player)viewResult.Model;

            Assert.AreEqual(_context.Players.Find(3), playerModel);
        }

        [TestMethod]
        public void CreateViewLoads()
        {
            var result = playerController.Create();
            var viewResult = (ViewResult)result;

            Assert.AreEqual("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void CreatePlayerSavesToDb()
        {
            var newPlayer = new Player { Position = "SF", PlayerName = "Markieff Morris", PlayerHeightCm = 208, PlayerId = 5 };

            _context.Players.Add(newPlayer);
            _context.SaveChanges();

            Assert.AreEqual(newPlayer, _context.Players.ToArray()[4]);
        }

        [TestMethod]
        public void PostCreatePlayerReturnsCreate()
        {    
            var player = new Player { };
            playerController.ModelState.AddModelError("Error Code 1", "Didn't return create or create is null");
            var result = playerController.Create(player);
            var viewResult = (ViewResult)result.Result;
     
            Assert.AreEqual("Create", viewResult.ViewName);
            Assert.IsNotNull("Create", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteReturnsErrorWhenNullId()
        {
            var result = playerController.Delete(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteReturnsErrorWhenInvalidId()
        {
            var result = playerController.Delete(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DeleteRedirectsToIndexAfterConfirmation()
        {
            var result = playerController.DeleteConfirmed(2);  
            var actionResult = (RedirectToActionResult)result.Result;

            Assert.AreEqual("Index", actionResult.ActionName);
        }

        [TestMethod]
        public void DeleteSuccessfulAfterConfirmation()
        {
            var result = playerController.DeleteConfirmed(3); 
            var product = _context.Players.Find(3);

            Assert.AreEqual(product, null);
        }

        [TestMethod]
        public void DeleteViewLoads()
        {
            var result = playerController.Delete(1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Delete", viewResult.ViewName);
        }

        [TestMethod]
        public void DeletedPlayerIsCorrectPlayer()
        {
            var result = playerController.Delete(2); 
            var viewResult = (ViewResult)result.Result;
            Player player = (Player)viewResult.Model;

            Assert.AreEqual(players[1], player);
        }

        [TestMethod]
        public void DetailsViewLoadsCorrectPlayer()
        {
            var result = playerController.Details(players[0].PlayerId);
            var viewResult = (ViewResult)result.Result;
     
            Assert.AreEqual(players[0], viewResult.Model);
        }

        [TestMethod]
        public void DetailsReturnsErrorWhenNullId()
        {
            var result = playerController.Details(null);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DetailsReturnsErrorWhenInvalidId()
        {
            var result = playerController.Details(-1);
            var viewResult = (ViewResult)result.Result;

            Assert.AreEqual("Error", viewResult.ViewName);
        }

        [TestMethod]
        public void DetailsViewLoads()
        {
            var result = playerController.Details(players[0].PlayerId);
            var viewResult = (ViewResult)result.Result;
            
            Assert.AreEqual("Details", viewResult.ViewName);
        }
    }
}
