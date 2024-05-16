namespace Appointify.Application.Queries.Companies.ById
{
    public class GetCompanyByIdQueryResponse
    {
        public GetCompanyByIdQueryResponse(
            Guid id,
            string name,
            string open,
            string close)
        {
            Id = id;
            Name = name;
            Open = open;
            Close = close;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Open { get; set; }

        public string Close { get; set; }
    }
}
