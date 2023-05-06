namespace SeleniumNUnitFramework.Utils
{
    internal class ThreadManager
    {
        private static Semaphore _semaphore;

        public static void SetupThreads() {
            _semaphore = new Semaphore(4, 4);
        }

        public static Semaphore GetSemaphore() {
            return _semaphore;
        }
    }
}
