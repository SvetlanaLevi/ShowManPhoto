using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShowManPhoto.Repositories
{
    public class OrderRepository
    {
        private readonly OrderStorage _storage;
        public OrderRepository(string DBcon)
        {
            _storage = new OrderStorage(DBcon);
        }

        public async ValueTask<RequestResult<Order>> OrderCreate(Order model)
        {
            var result = new RequestResult<Order>();
            result.RequestData.OrderedPhotos = new List<OrderedPhoto>();
            try
            {
                _storage.TransactionStart();
                result.RequestData = await _storage.OrderAdd(model);
                foreach(OrderedPhoto orderedPhoto in model.OrderedPhotos)
                {
                    result.RequestData.OrderedPhotos.Add(await _storage.OrderedPhotoAdd(orderedPhoto));
                }
                _storage.TransactionCommit();
                result.IsOkay = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
                _storage.TransactioRollBack();
            }
            return result;
        }
    }

}
