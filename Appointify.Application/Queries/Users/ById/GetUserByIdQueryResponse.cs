namespace Appointify.Application.Queries.Users.ById
{
    public class GetUserByIdQueryResponse
    {
        public GetUserByIdQueryResponse(
            Guid id,
            string name,
            string completeName)
        {
            Id = id;
            Name = name;
            CompleteName = completeName;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;
    }
}
