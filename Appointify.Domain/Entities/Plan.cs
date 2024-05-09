using System.Collections.ObjectModel;

namespace Appointify.Domain.Entities
{
    public class Plan : Entity
    {
        public Plan(string name, bool showExtraFields)
        {
            Name = name;
            ShowExtraFields = showExtraFields;
        }

        public string Name { get; set; } = string.Empty;

        public bool ShowExtraFields { get; set; }

        public ICollection<Company> Companies { get; set; } = new Collection<Company>();
    }
}
