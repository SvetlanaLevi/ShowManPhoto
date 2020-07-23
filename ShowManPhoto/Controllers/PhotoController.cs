using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ShowManPhoto.Models;
using ShowManPhoto.Repositories;

namespace ShowManPhoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly PhotoRepository _repo;
        private readonly IMapper _mapper;
        public PhotoController(IConfiguration configuration, IMapper mapper)
        {
            string dbCon = configuration.GetConnectionString("DefaultConnection");
            _repo = new PhotoRepository(dbCon);
            _mapper = mapper;
        }

        [HttpPost("album")]
        public async ValueTask<ActionResult> AlbumCreate([FromBody] AlbumInputModel model)
        {
            var result = await _repo.AlbumCreate(_mapper.Map<Album>(model));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"Added album not found", statusCode: 520); }
                return Ok(result.RequestData);
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("album/{id}")]
        public async ValueTask<ActionResult> AlbumGetById(int id)
        {
            throw new Exception();
        }

        [HttpGet("album-by-user/{userId}")]
        public async ValueTask<ActionResult> AlbumGetByUserId(int userId)
        {
            throw new Exception();
        }

        [HttpPost]
        public async ValueTask<ActionResult> PhotoUploud([FromForm] PhotoInputModel model)
        {

            if (model.File == null || model.File.Length == 0) { return BadRequest(); }
            var result = await _repo.PhotoUploud(_mapper.Map<Photo>(model));
            if (result.IsOkay)
            {
                if (result.RequestData == null) { return Problem($"Added photo not found", statusCode: 520); }
                return Ok(result.RequestData);
            }
            return Problem($"Transaction failed {result.ExMessage}", statusCode: 520);
        }

        [HttpGet("by-album/{albumId}")]
        public async ValueTask<ActionResult> PhotoGetByAlbum(int albumId)
        {
            throw new Exception();
        }

        [HttpPost("select")]
        public async ValueTask<ActionResult> SelectPhotos([FromBody]OrderedPhotosInputModel model)
        {

            var i = _mapper.Map<OrderedPhotosInputModel, Order>(model);
            throw new Exception();
        }

        //создать заказ (ожидается выбор пользователя)
        //отметить фото как выбранные (в обработке)
        //отметить заказ как выполненный



    }
}