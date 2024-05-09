namespace Appointify.Application.Queries.Plans.All
{
    public class GetAllPlansQueryResponse
    {
        public GetAllPlansQueryResponse(
            Guid id,
            string name,
            bool showExtraFields)
        {
            Id = id;
            Name = name;
            ShowExtraFields = showExtraFields;
        }

        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool ShowExtraFields { get; set; }
    }
}
