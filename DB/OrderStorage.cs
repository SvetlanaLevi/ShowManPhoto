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
                   dataModel.AlbumId,
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



        public async ValueTask<List<SelectedPhoto>> SelectedPhotoGetByOrderId(int orderId)
        {

            var result = await _connection.QueryAsync<SelectedPhoto>(
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
