using RedCrossBingo.Models; 
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using GraphQL.Types;

namespace  RedCrossBingo.Repositories
{
    class RoomsRepository
    {
        private readonly DataBaseContext _context;
        public RoomsRepository(DataBaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Rooms> All(ResolveFieldContext<object> context){
            var results = from rooms in _context.Rooms select rooms;
            return results;
        }

        public Rooms Find(long id){
           return  _context.Rooms.Find(id); 
        }

        public async Task<Rooms> Add(Rooms b) {
            _context.Rooms.Add(b);
            await _context.SaveChangesAsync();
            return b;
        }

       public IEnumerable<BingoCards> cardsFromRooms(long cardId){
            var results = from cards in _context.BingoCards select cards;
            results = results.Where(a => a.Id == cardId); 
            return results;
        }

      public IEnumerable<BingoNumbers> numberFromRooms(long numberId){
            var results = from number in _context.BingoNumbers select number;
            results = results.Where(a => a.Id == numberId); 
            return results;
        }



  public async Task<Rooms> Update(long id, Rooms b) {
            b.Id = id;
            var updated = (_context.Rooms.Update(b)).Entity;
            if (updated == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return updated;
        }

        public async Task<Rooms> Remove(long id) {
            var b = await _context.Rooms.FindAsync(id);
            if (b == null)
            {
                return null;
            }
            _context.Rooms.Remove(b);
            await _context.SaveChangesAsync();
            return b;
        }

    }
}