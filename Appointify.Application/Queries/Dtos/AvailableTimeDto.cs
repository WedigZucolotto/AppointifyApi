namespace Appointify.Application.Queries.Dtos
{
    public class AvailableTimeDto
    {
        public AvailableTimeDto(string time, Guid userId)
        {
            Time = time;
            UserId = userId;
        }

        public string Time { get; set; } = string.Empty;

        public Guid UserId { get; set; }
    }
}
