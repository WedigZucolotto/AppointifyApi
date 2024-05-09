namespace Appointify.Application.Queries.Plans.ById
{
    public class GetPlanByIdQueryResponse
    {
        public GetPlanByIdQueryResponse(
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
