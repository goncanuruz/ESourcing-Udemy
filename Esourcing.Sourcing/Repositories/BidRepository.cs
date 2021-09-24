using Esourcing.Sourcing.Data.Interfaces;
using Esourcing.Sourcing.Entities;
using Esourcing.Sourcing.Repositories.Interfaces;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Esourcing.Sourcing.Repositories
{
    public class BidRepository : IBidRepository
    {
        private readonly ISourcingContext _context;

        public BidRepository(ISourcingContext context)
        {
            _context = context;
        }

        public async Task<List<Bid>> GetBidsByAuctionId(string id)
        {
            FilterDefinition<Bid> filter = Builders<Bid>.Filter.Eq(a => a.AuctionId, id);
            List<Bid> bids = await _context.Bids.Find(filter).ToListAsync();
            bids = bids.OrderByDescending(x => x.CreatedAt).GroupBy(x => x.SellerUserName).Select(a => new Bid
            {
                AuctionId = a.FirstOrDefault().AuctionId,
                Price = a.FirstOrDefault().Price,
                CreatedAt = a.FirstOrDefault().CreatedAt,
                SellerUserName = a.FirstOrDefault().SellerUserName,
                ProductId = a.FirstOrDefault().ProductId,
                Id = a.FirstOrDefault().Id
            })
                .ToList();
            return bids;
        }

        public async Task<Bid> GetWinnerBid(string id)
        {
            var bids = await GetBidsByAuctionId(id);
            return bids.OrderByDescending(x => x.Price).FirstOrDefault();
            
        }
        public async Task<Bid> GetBid(string id)
        {
            var bid = await _context.Bids.Find(x => x.Id == id).FirstOrDefaultAsync();
            return bid;

        }
        public async Task SendBid(Bid bid)
        {
            await _context.Bids.InsertOneAsync(bid);
        }
    }
}
