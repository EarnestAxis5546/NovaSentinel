-- Atomic rate-limiting script for Redis
local key = KEYS[1]
local limit = tonumber(ARGV[1])
local window = tonumber(ARGV[2])
local now = tonumber(redis.call('TIME')[1])

local count = redis.call('GET', key)
if not count then
    redis.call('SET', key, 1, 'EX', window)
    return 0
end

count = tonumber(count)
if count >= limit then
    return 1
end

redis.call('INCR', key)
return 0