namespace Crawlers.Application.Services.Async.Supporters
{
    internal class ThreadCounter
    {
        private static int _counter = 0;
        public int Counter { get; private set; }

        public ThreadCounter()
        {
            Counter = _counter++;
        }
    }
}
