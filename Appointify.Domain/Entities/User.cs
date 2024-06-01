namespace Appointify.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; } = string.Empty;

        public string CompleteName { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public bool IsOwner { get; set; }

        public Guid CompanyId { get; set; }
        
        public Company Company { get; set; } = new Company();

        public List<Event> Events { get; set; } = new List<Event>();

        public List<Permission> Permissions { get; set; } = new List<Permission>();

        public List<TimeSpan> GetAvailableTimes(DateTime initialDate, TimeSpan serviceInterval)
        {
            var availableTimes = new List<TimeSpan>();
            var interval = new TimeSpan();

            for (var time = Company.Open; time <= Company.Close; time += TimeSpan.FromMinutes(1))
            {
                var date = initialDate.Add(time);
                var _event = Events.FirstOrDefault(e => e.Date == date);

                if (_event != null)
                {
                    time += _event.Service.Interval;
                    time -= new TimeSpan(0, 1, 0);
                    interval = _event.Service.Interval;
                    continue;
                }

                if (interval == serviceInterval)
                {
                    availableTimes.Add(date.TimeOfDay);
                    interval = new TimeSpan();
                }

                interval += TimeSpan.FromMinutes(1);
            }

            var SAMTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            var now = TimeZoneInfo.ConvertTime(DateTime.Now, SAMTimeZone);

            if (initialDate == now.Date)
            {
                return availableTimes
                    .Where(time => time > now.TimeOfDay)
                    .ToList();
            }
            return availableTimes;
        }

        public bool IsAvailable(DateTime initialDate, TimeSpan serviceInterval)
        {
            for (var date = initialDate; date < initialDate + serviceInterval; date += TimeSpan.FromMinutes(1))
            {
                return !Events.Any(e => e.Date == date);
            }
            return true;
        }

        public User() { }
    }
}
