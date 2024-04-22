namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQueryResponse
    {
        public GetAllUsersQueryResponse(
            string name,
            string completeName,
            string type,
            string companyName)
        {
            Name = name;
            CompleteName = completeName;
            Type = type;
            CompanyName = companyName;
        }

        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string CompanyName { get; set; }
    }
}
