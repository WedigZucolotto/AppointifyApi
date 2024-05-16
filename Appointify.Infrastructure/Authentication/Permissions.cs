namespace Appointify.Infrastructure.Authentication
{
    public static class Permissions
    {
        public static class Companies
        {
            public const string GetById = "companies:getById";
            public const string Update = "companies:update";
        }

        public static class Events
        {
            public const string GetById = "events:getById";
            public const string Delete = "events:delete";
        }

        public static class Services
        {
            public const string GetById = "services:getById";
            public const string Create = "services:create";
            public const string Update = "services:update";
            public const string Delete = "services:delete";
        }

        public static class Users
        {
            public const string GetById = "users:getById";
            public const string GetDay = "users:getDay";
            public const string GetWeek = "users:getWeek";
            public const string GetMonth = "users:getMonth";
            public const string Update = "users:update";
        }
    }
}
