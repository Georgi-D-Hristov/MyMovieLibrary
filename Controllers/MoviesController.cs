using Microsoft.AspNetCore.Mvc;
using MyMovieLibrary.Models; // <-- 1. Добави using за твоите модели

namespace MyMovieLibrary.Controllers
{
    public class MoviesController : Controller
    {
        // --- Това е нашата "фалшива" база данни ---
        // "static" означава, че списъкът ще е един и същ за всички потребители
        // и няма да се нулира при всяка заявка.
        private static List<Movie> _movies = new List<Movie>
        {
            new Movie { Id = 1, Title = "The Shawshank Redemption", Director = "Frank Darabont", Year = 1994, Genre = "Drama", Rating = 9 },
            new Movie { Id = 2, Title = "The Godfather", Director = "Francis Ford Coppola", Year = 1972, Genre = "Crime", Rating = 9 },
            new Movie { Id = 3, Title = "The Dark Knight", Director = "Christopher Nolan", Year = 2008, Genre = "Action", Rating = 9 }
        };
        // ---------------------------------------------


        // Този метод (Action) ще се изпълни, когато отидеш на /Movies
        public IActionResult Index()
        {
            // 2. Вземаме всички филми от нашия "фалшив" списък
            List<Movie> allMovies = _movies;

            // 3. Подаваме ги към View-то
            return View(allMovies);
        }

        public IActionResult Details(int id)
        {
            // Намираме филма по ID
            Movie movie = _movies.FirstOrDefault(m => m.Id == id);
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
        public IActionResult Create(Movie movie)
        {
            // 1. Изчисляваме ново ID. (В истинска база данни, това става автоматично)
            // Взимаме най-голямото ID досега и добавяме 1.
            int newId = _movies.Any() ? _movies.Max(m => m.Id) + 1 : 1;
            movie.Id = newId;

            // 2. Добавяме новия филм към нашия "фалшив" списък
            _movies.Add(movie);

            // 3. Пренасочваме потребителя обратно към списъка (Index)
            return RedirectToAction("Index");
        }
    }
}
