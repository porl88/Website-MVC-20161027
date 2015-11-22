namespace MVC.Services.Shop
{
    using MVC.Core.Entities.Shop;

    public interface IBasketService
    {
        // basket storage service
        void CreateBasket();// ??

        void DeleteBasket(int basketId);

        void AddItem(BasketItem item);

        void UpdateItem(BasketItem item);

        void RemoveItem(int itemId);
    }
}
