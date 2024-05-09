namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryResponse
    {
        public GetUserByIdQueryResponse(
            Guid id,
            string name,
            string completeName,
            bool isOwner,
            Guid companyId)
        {
            Id = id;
            Name = name;
            CompleteName = completeName;
            IsOwner = isOwner;
            CompanyId = companyId;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public bool IsOwner { get; set; }

        public Guid CompanyId { get; set; }
    }
}
