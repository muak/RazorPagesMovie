using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace RazorPagesmovie.Pages.Movies
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.MovieContext _context;

        public IndexModel(RazorPagesMovie.Models.MovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get;set; }
        public SelectList Genres;
        public string MovieGenre { get; set; }

        public async Task OnGetAsync(string movieGenre,string searchString)
        {
            var genreQuery = _context.Movie.OrderBy(x => x.Genre).Select(x => x.Genre);

            var movies = _context.Movie.Select(x=>x);

            if(!string.IsNullOrEmpty(searchString)){
                movies = movies.Where(x => x.Title.Contains(searchString));
            }

            if(!string.IsNullOrEmpty(movieGenre)){
                movies = movies.Where(x => x.Genre == movieGenre);
            }

            Genres = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}
