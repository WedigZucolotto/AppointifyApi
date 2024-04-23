namespace Appointify.Infrastructure.Authentication
{
    public static class Permissions
    {
        public static class Companies
        {
            public const string GetAll = "companies:getAll";
            public const string GetById = "companies:getById";
            public const string Create = "companies:create";
            public const string Update = "companies:update";
            public const string Delete = "companies:delete";
        }

        //public static class Events
        //{
        //    public const string Create = "events:create";
        //}

        public static class Plans
        {
            public const string Options = "plans:options";
        }

        public static class Services
        {
            public const string GetAll = "services:getAll";
            public const string GetById = "services:getById";
            public const string Create = "services:create";
            public const string Update = "services:update";
            public const string Delete = "services:delete";
        }

        public static class Users
        {
            public const string View = "users:view";
            public const string Edit = "users:edit";
        }
    }
}
