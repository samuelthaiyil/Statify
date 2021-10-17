using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StatTracker.Models;
using Statify.Data;

namespace Statify.Controllers
{
    public class GameDetailsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GameDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GameDetails
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.GameDetails.Include(g => g.Game).Include(g => g.Player);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: GameDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDetail = await _context.GameDetails
                .Include(g => g.Game)
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.GameDetailId == id);
            if (gameDetail == null)
            {
                return NotFound();
            }

            return View(gameDetail);
        }

        // GET: GameDetails/Create
        public IActionResult Create()
        {
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameDate");
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName");
            return View();
        }

        // POST: GameDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameDetailId,PlayerId,GameId,Points,Rebounds,Assists")] GameDetail gameDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gameDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameDate", gameDetail.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName", gameDetail.PlayerId);
            return View(gameDetail);
        }

        // GET: GameDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDetail = await _context.GameDetails.FindAsync(id);
            if (gameDetail == null)
            {
                return NotFound();
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameDate", gameDetail.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName", gameDetail.PlayerId);
            return View(gameDetail);
        }

        // POST: GameDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("GameDetailId,PlayerId,GameId,Points,Rebounds,Assists")] GameDetail gameDetail)
        {
            if (id != gameDetail.GameDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameDetailExists(gameDetail.GameDetailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GameId"] = new SelectList(_context.Games, "GameId", "GameDate", gameDetail.GameId);
            ViewData["PlayerId"] = new SelectList(_context.Players, "PlayerId", "PlayerName", gameDetail.PlayerId);
            return View(gameDetail);
        }

        // GET: GameDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameDetail = await _context.GameDetails
                .Include(g => g.Game)
                .Include(g => g.Player)
                .FirstOrDefaultAsync(m => m.GameDetailId == id);
            if (gameDetail == null)
            {
                return NotFound();
            }

            return View(gameDetail);
        }

        // POST: GameDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gameDetail = await _context.GameDetails.FindAsync(id);
            _context.GameDetails.Remove(gameDetail);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameDetailExists(int id)
        {
            return _context.GameDetails.Any(e => e.GameDetailId == id);
        }
    }
}
