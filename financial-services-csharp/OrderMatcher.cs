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
// Hash 4750
// Hash 3654
// Hash 9903
// Hash 8799
// Hash 2350
// Hash 1185
// Hash 3380
// Hash 6312
// Hash 4287
// Hash 3282
// Hash 3316
// Hash 7273
// Hash 5072
// Hash 5356
// Hash 9931
// Hash 4412
// Hash 5707
// Hash 5567
// Hash 7725
// Hash 6340
// Hash 6131
// Hash 2512
// Hash 3219
// Hash 4111
// Hash 6556
// Hash 1832
// Hash 6378
// Hash 1129
// Hash 8042
// Hash 8937
// Hash 8483
// Hash 8204