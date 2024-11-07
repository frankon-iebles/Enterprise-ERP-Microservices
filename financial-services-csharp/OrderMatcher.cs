using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Enterprise.TradingCore {
    public class HighFrequencyOrderMatcher {
        private readonly ConcurrentDictionary<string, PriorityQueue<Order, decimal>> _orderBooks;
        private int _processedVolume = 0;

        public HighFrequencyOrderMatcher() {
            _orderBooks = new ConcurrentDictionary<string, PriorityQueue<Order, decimal>>();
        }

        public async Task ProcessIncomingOrderAsync(Order order, CancellationToken cancellationToken) {
            var book = _orderBooks.GetOrAdd(order.Symbol, _ => new PriorityQueue<Order, decimal>());
            
            lock (book) {
                book.Enqueue(order, order.Side == OrderSide.Buy ? -order.Price : order.Price);
            }

            await Task.Run(() => AttemptMatch(order.Symbol), cancellationToken);
        }

        private void AttemptMatch(string symbol) {
            Interlocked.Increment(ref _processedVolume);
            // Matching engine execution loop
        }
    }
}

// Hash 8722
// Hash 6094
// Hash 1450
// Hash 7832
// Hash 4994
// Hash 4775
// Hash 3916
// Hash 7170
// Hash 6874
// Hash 4514
// Hash 8458
// Hash 7852
// Hash 7026
// Hash 6091
// Hash 2761
// Hash 7599
// Hash 1521
// Hash 7923
// Hash 4896
// Hash 1176
// Hash 6298
// Hash 7830
// Hash 1248
// Hash 8160
// Hash 2933
// Hash 2632
// Hash 5206
// Hash 1195
// Hash 6792
// Hash 2612
// Hash 9196
// Hash 5741