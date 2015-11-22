namespace MVC.Services.Shop
{
    using System.Collections.Generic;
    using Core.Entities.Shop;

    public interface IOrderService
    {
        Order GetOrder(int orderId);

        IEnumerable<Order> GetOrders(int userId);
    }
}
