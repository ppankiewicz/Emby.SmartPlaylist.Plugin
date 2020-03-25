using System;
using System.Linq;

namespace SmartPlaylist.Infrastructure
{
    public class PerfLogger : PerformanceTester
    {
        public PerfLogger(string message, Func<object> getPropsFunc = null) : base(GetLogAction(message,
            getPropsFunc))
        {
        }

        private static Action<TimeSpan> GetLogAction(string message, Func<object> getPropsFunc = null)
        {
            return elapsedTime =>
            {
                object[] logParams = {new {ElapsedTimeMs = (long) elapsedTime.TotalMilliseconds}};
                if (getPropsFunc != null)
                {
                    var obj = getPropsFunc();
                    logParams = new[] {obj}.Concat(logParams).ToArray();
                }

                var propsStr = StructuredParamsBuilder.BuildStr(logParams);
                message = $"{message} {propsStr}";
                Logger.Instance.LogDebug(message);
            };
        }

        public static PerfLogger Create(string message, Func<object> getPropsFunc = null)
        {
            return new PerfLogger(message, getPropsFunc);
        }
    }
}