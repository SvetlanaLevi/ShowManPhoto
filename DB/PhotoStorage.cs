using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DB
{
    public class PhotoStorage
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;
        public PhotoStorage(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async ValueTask<Album> AlbumGetById(int Id)
        {

            var result = await _connection.QueryAsync(
               "Album_SelectById",
               param: new { Id },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.FirstOrDefault();

        }

        public async ValueTask<List<Album>> AlbumGetByUserId(int userId)
        {

            var result = await _connection.QueryAsync<Album>(
               "Album_SelectByUserId",
               param: new { userId },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.ToList();

        }

        public async ValueTask<Album> AlbumAdd(Album dataModel)
        {
            var result = await _connection.QueryAsync(
               "Album_Insert",
               param: new
               {
                   dataModel.Name,
                   dataModel.Comment
               },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.FirstOrDefault();
        }

        public async ValueTask<List<Photo>> PhotoGetByAlbumId(int Id)
        {

            var result = await _connection.QueryAsync<Photo>(
               "Photo_SelectByAlbumId",
               param: new { Id },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.ToList();

        }

        public async ValueTask<List<int>> PhotoAdd(List<Photo> dataModels)
        {
            var resultList = new List<int>();
            foreach (var model in dataModels)
            {
                var result = await _connection.QueryAsync(
                   "Photo_Insert",
                   param: new
                   {
                       model.AlbumId,
                       model.FilePath,
                       model.FileName
                   },
                   transaction: _transaction,
                   commandType: CommandType.StoredProcedure
                   );
                resultList.Add(result.FirstOrDefault());
            }
            return resultList;
        }

        public async ValueTask<List<int>> SelectedPhotoAdd(List<SelectedPhoto> dataModels)
        {
            var resultList = new List<int>();
            foreach (var model in dataModels)
            {
                var result = await _connection.QueryAsync<int>(
                   "SelectedPhoto_Insert",
                   param: new
                   {
                       model.OrderId,
                       PhotoId = model.Photo.Id,
                       model.IsForPrint,
                       model.PrintFormatId,
                       model.Comment
                   },
                   transaction: _transaction,
                   commandType: CommandType.StoredProcedure
                   );
                resultList.Add(result.FirstOrDefault());
            }
            return resultList;
        }
        public void TransactionStart()
        {
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public void TransactionCommit()
        {
            _transaction?.Commit();
            _connection.Close();
        }

        public void TransactioRollBack()
        {
            _transaction?.Rollback();
            _connection.Close();
        }

    }
}


