using DB;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


namespace ShowManPhoto.Repositories
{
    public class PhotoRepository
    {
        private readonly PhotoStorage _storage;
        public PhotoRepository(string DBcon)
        {
            _storage = new PhotoStorage(DBcon);
        }

        public async ValueTask<RequestResult<Album>> AlbumCreate(Album model)
        {
            var result = new RequestResult<Album>();
            try
            {
                result.RequestData = await _storage.AlbumAdd(model);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }


        public async ValueTask<RequestResult<Album>> AlbumGetById(int id)
        {
            var result = new RequestResult<Album>();
            try
            {
                result.RequestData = await _storage.AlbumGetById(id);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }


        public async ValueTask<RequestResult<List<Album>>> AlbumGetByUserId(int userId)
        {
            var result = new RequestResult<List<Album>>();
            try
            {
                result.RequestData = await _storage.AlbumGetByUserId(userId);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }


        public async ValueTask<RequestResult<List<int>>> PhotoUploud(List<Photo> models)//form
        {
            var result = new RequestResult<List<int>>();
            try
            {
                result.RequestData = await _storage.PhotoAdd(models);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
        public async ValueTask<RequestResult<Photo>> PhotoUploud(Photo model)//form
        {
            var result = new RequestResult<Photo>();
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await model.File.CopyToAsync(memoryStream);
                    using (var image = Image.FromStream(memoryStream))
                    {
                        Servises.SetWatermark(image, "ShowManPhoto", model.FilePath);
                    }
                }
                result.RequestData = await _storage.PhotoAdd(model);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Photo>>> PhotoGetByAlbum(int albumId)
        {
            var result = new RequestResult<List<Photo>>();
            try
            {
                result.RequestData = await _storage.PhotoGetByAlbumId(albumId);
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }


        //public async ValueTask<RequestResult<List<int>>> SelectedPhotoAdd(List<SelectedPhoto> models)
        //{
        //    var result = new RequestResult<List<int>>();
        //    try
        //    {
        //        result.RequestData = await _storage.SelectedPhotoAdd(models);
        //        result.IsOkay = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.ExMessage = ex.Message;
        //    }
        //    return result;
        //}
    }
}
