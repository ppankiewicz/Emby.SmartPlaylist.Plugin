using System;
using System.Threading;

namespace SmartPlaylist.Extensions
{
    public static class SemaphoreSlimExtensions
    {
        public static void SafeExecute(this SemaphoreSlim semaphore, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static T SafeExecute<T>(this SemaphoreSlim semaphore, Func<T> func)
        {
            try
            {
                return func();
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}