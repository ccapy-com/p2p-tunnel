﻿using System;
using System.Collections.Concurrent;

namespace common.libs.rateLimit
{
    /// <summary>
    /// 令牌桶算法
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public sealed class TokenBucketRatelimit<TKey> : IRateLimit<TKey>
    {
        int rate = 0;
        double ms = 1000.0d;
        int timeout = 2000;
        private readonly ConcurrentDictionary<TKey, TokenBucketRateInfo> limits = new ConcurrentDictionary<TKey, TokenBucketRateInfo>();
        private readonly WheelTimer<TKey> wheelTimer = new WheelTimer<TKey>();

        public TokenBucketRatelimit(int rate)
        {
            this.rate = rate;
        }

        public void SetRate(TKey key, int rate)
        {
            if (limits.TryGetValue(key, out TokenBucketRateInfo info) == false)
            {
                info = new TokenBucketRateInfo { Rate = rate, CurrentRate = rate, Token = rate / ms, LastTime = DateTimeHelper.GetTimeStamp(), TimeoutMs = timeout };
                limits.TryAdd(key, info);
            }
            else
            {
                info.Rate = rate;
                info.CurrentRate = rate;
                info.Token = rate / ms;
                info.Timeout.Cancel();
            }
            info.Timeout = wheelTimer.NewTimeout(new WheelTimerTimeoutTask<TKey> { State = key, Callback = Timeout }, 1000, true);
        }
        public int Try(TKey key, int num)
        {
            TokenBucketRateInfo info = Get(key);
            if (info.Rate == 0)
            {
                return num;
            }
            AddToken(info);
            //消耗掉能消耗的
            int canEat = Math.Min(num, (int)info.CurrentRate);
            info.CurrentRate -= canEat;
            return canEat;
        }
        public void Remove(TKey key)
        {
            if (limits.TryRemove(key, out TokenBucketRateInfo info))
            {
                info.Timeout.Cancel();
            }
        }
        public void Clear()
        {
            foreach (var item in limits.Values)
            {
                item.Timeout.Cancel();
            }
            limits.Clear();
        }
        private TokenBucketRateInfo Get(TKey key)
        {
            if (limits.TryGetValue(key, out TokenBucketRateInfo info) == false)
            {
                info = new TokenBucketRateInfo { Rate = rate, CurrentRate = rate, Token = rate / ms, LastTime = DateTimeHelper.GetTimeStamp(), TimeoutMs = timeout };
                limits.TryAdd(key, info);
                info.Timeout = wheelTimer.NewTimeout(new WheelTimerTimeoutTask<TKey> { State = key, Callback = Timeout }, 1000, true);
            }
            return info;
        }


        private void Timeout(WheelTimerTimeout<TKey> timeout)
        {
            if (limits.TryGetValue(timeout.Task.State, out TokenBucketRateInfo info))
            {
                if (DateTimeHelper.GetTimeStamp() - info.LastTime > info.TimeoutMs)
                {
                    info.Timeout.Cancel();
                    limits.TryRemove(timeout.Task.State, out _);
                }
            }
        }
        private void AddToken(TokenBucketRateInfo info)
        {
            long time = DateTimeHelper.GetTimeStamp();
            long times = (time - info.LastTime);

            info.CurrentRate = Math.Min(info.CurrentRate + times * info.Token, info.Rate);

            info.LastTime = time;
        }


        class TokenBucketRateInfo
        {
            public double Rate { get; set; }
            public double CurrentRate { get; set; }
            public double Token { get; set; }
            public int TimeoutMs { get; set; }
            public WheelTimerTimeout<TKey> Timeout { get; set; }
            public long LastTime { get; set; }

        }
    }
}
