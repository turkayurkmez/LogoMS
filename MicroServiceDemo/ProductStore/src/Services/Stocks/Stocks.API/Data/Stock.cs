namespace Stocks.API.Data
{
    public class Stock
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }

    public class StockInMemory
    {
        private List<Stock> stocks;
        public StockInMemory()
        {
            stocks = new List<Stock>()
            {
                new Stock(){ ProductId = 1, Quantity = 10 },
                new Stock(){ ProductId = 2, Quantity = 100 },
                new Stock(){ ProductId = 3, Quantity = 50 },
                new Stock(){ ProductId = 4, Quantity = 25 },
            };
        }

        public void ChangeStock(int id, int quantity)
        {
            stocks.Find(p => p.ProductId == id).Quantity -= quantity;
        }

        public bool GetAvailableStocks(int productId, int quantity)
        {
            return stocks.Where(p => p.ProductId == productId && p.Quantity>quantity).Any();
        }
    }
}
