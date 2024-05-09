namespace Appointify.Application.Queries.Users.All
{
    public class GetAllUsersQueryResponse
    {
        public GetAllUsersQueryResponse(
            Guid id,
            string name,
            string completeName,
            string type,
            string companyName)
        {
            Id = id;
            Name = name;
            CompleteName = completeName;
            Type = type;
            CompanyName = companyName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Type { get; set; }

        public string CompanyName { get; set; }
    }
}
