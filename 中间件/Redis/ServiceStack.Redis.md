# 高并发下异常处理

加锁

~~~C#
public static bool SetKey<T>(String key, T value, int exp)
        {
            RemoveKey(key + "lock");
            var r = false;

            if (!RedisHelper.redisStarted)
            {
                return r;
            }
            try
            {
                using (IRedisClient irc = RedisHelper.GetRedisClient())
                {
                    if (irc != null && value != null)
                    {
                        try
                        {
                            using (irc.AcquireLock(key + "lock"))
                            {
                                if (exp > 0)
                                {
                                    irc.Set<T>(key, value, DateTime.Now.AddSeconds(exp));
                                    r = true;
                                }
                                else
                                {
                                    irc.Set<T>(key, value);
                                    r = true;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            RemoveKey(key + "lock");
                            RemoveKey(key);
                            Logger.Error("RedisSetKey4<T>：", ex.Message);
                        }
                        finally
                        {
                            irc.Dispose();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error("RedisSetKey3<T>：", ex.Message);
            }

            return r;
        }
~~~



