using AutoMapper;
using GameStore.Service;
using GameStore.Service.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GameStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService genreService;
        private readonly IMapper mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            this.genreService = genreService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var genreList = await genreService.GetAllGenresAsync();

            var model = genreList
                .Where(genre => genre.ParentId == null)
                .Select(x => new GenreModel
                {                     
                    Id = x.Id,
                    GenreName = x.GenreName,                                     
                    ParentId = x.ParentId,
                    Description = x.Description,
                    ChildGenres = x.ChildGenres?.Adapt<List<GenreModel>>()
                }).ToList();

            // test

            return Ok(model);
        }
    }
}
