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

        public static class Events
        {
            public const string View = "events:view";
            public const string Edit = "events:edit";
        }

        public static class Plans
        {
            public const string View = "plans:view";
            public const string Edit = "plans:edit";
        }

        public static class Services
        {
            public const string View = "services:view";
            public const string Edit = "services:edit";
        }

        public static class Users
        {
            public const string View = "users:view";
            public const string Edit = "users:edit";
        }
    }
}
