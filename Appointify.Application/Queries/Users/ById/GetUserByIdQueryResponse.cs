namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryResponse
    {
        public GetUserByIdQueryResponse(
            Guid id,
            string name,
            string completeName,
            string type,
            Guid companyId)
        {
            Id = id;
            Name = name;
            CompleteName = completeName;
            Type = type;
            CompanyId = companyId;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public Guid CompanyId { get; set; }
    }
}
