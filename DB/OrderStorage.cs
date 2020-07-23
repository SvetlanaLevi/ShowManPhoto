using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DB
{

    public class OrderStorage
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        public OrderStorage(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public async ValueTask<Order> OrderAdd(Order dataModel)
        {
            var result = await _connection.QueryAsync(
               "Order_Insert",
               param: new
               {
                   dataModel.UserId,
                   dataModel.StatusId
               },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            var order = new Order() { Id = result.FirstOrDefault() };
            order = await OrderGetById((int)order.Id);
            return order;
        }

        public async ValueTask<Order> OrderGetById(int Id)
        {

            var result = await _connection.QueryAsync(
               "Order_SelectById",
               param: new { Id },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.FirstOrDefault();
        }



        public async ValueTask<List<OrderedPhoto>> SelectedPhotoGetByOrderId(int orderId)
        {

            var result = await _connection.QueryAsync<OrderedPhoto>(
               "SelectdedPhoto_SelectByOrderId",
               param: new { orderId },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
            return result.ToList();
        }

        public async ValueTask OrderChangeStatus(int statusId)
        {

            var result = await _connection.QueryAsync(
               "Order_ChangeStatus",
               param: new { statusId },
               transaction: _transaction,
               commandType: CommandType.StoredProcedure
               );
        }
        public async ValueTask<List<int>> OrderedPhotosAdd(List<OrderedPhoto> dataModels)
        {
            var resultList = new List<int>();
            foreach (var model in dataModels)
            {
                var result = await _connection.QueryAsync<int>(
                   "SelectedPhoto_Insert",
                   param: new
                   {
                       model.OrderId,
                       model.PhotoId,
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

        public async ValueTask<OrderedPhoto> OrderedPhotoAdd(OrderedPhoto model)
        {
                var result = await _connection.QueryAsync<OrderedPhoto>(
                   "SelectedPhoto_Insert",
                   param: new
                   {
                       model.OrderId,
                       model.PhotoId,
                       model.IsForPrint,
                       model.PrintFormatId,
                       model.Comment
                   },
                   transaction: _transaction,
                   commandType: CommandType.StoredProcedure
                   );
            return result.FirstOrDefault();
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
