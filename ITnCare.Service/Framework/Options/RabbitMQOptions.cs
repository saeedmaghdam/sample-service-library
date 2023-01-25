﻿namespace ITnCare.Service.Framework.Options
{
    public class RabbitMQOptions
    {
        public string Host { get; set; } = default!;
        public int Port { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
