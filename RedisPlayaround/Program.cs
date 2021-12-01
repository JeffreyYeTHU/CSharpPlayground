// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;


ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(
            new ConfigurationOptions
            {
                EndPoints = { "localhost:6379" }
            });

var db = redis.GetDatabase();
var pong = await db.PingAsync();
Console.WriteLine(pong);