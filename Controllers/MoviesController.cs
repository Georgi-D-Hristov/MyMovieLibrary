using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMovieLibrary.Data;
using MyMovieLibrary.Models;
using System.Threading.Tasks; // <-- 1. Добави using за твоите модели

namespace MyMovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MoviesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Този метод (Action) ще се изпълни, когато отидеш на /Movies
        public async Task<IActionResult> Index()
        {
            // 2. Вземаме всички филми от нашия "фалшив" списък
            var allMovies = await _context.Movies.ToListAsync();

            // 3. Подаваме ги към View-то
            return View(allMovies);
        }

        public async Task<IActionResult> Details(int id)
        {
            // Намираме филма по ID
            Movie movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound(); // Ако не е намерен, връщаме 404
            }
            // Подаваме филма към View-то
            return View(movie);
        }

        // GET: /Movies/Create
        // Този метод просто показва празната форма
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Movies/Create
        // Този метод приема данните от изпратената форма
        [HttpPost] // Този атрибут казва, че методът отговаря само на POST заявки
        [ValidateAntiForgeryToken] // Добавя защита срещу CSRF атаки
        public async Task<IActionResult> Create(Movie movie)
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            // 3. Пренасочваме потребителя обратно към списъка (Index)
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            _context.Update(movie);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: /Movies/Delete/5
        [HttpPost, ActionName("Delete")] // Казваме на ASP.NET: "Този метод отговаря на POST заявка към /Movies/Delete"
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie); // Маркираме го за изтриване
                await _context.SaveChangesAsync(); // Изпълняваме DELETE заявката към базата
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
