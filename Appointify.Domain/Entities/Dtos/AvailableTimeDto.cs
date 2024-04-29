namespace Appointify.Domain.Entities.Dtos
{
    public class AvailableTimeDto
    {
        public AvailableTimeDto(TimeSpan time, Guid userId)
        {
            Time = time;
            UserId = userId;
        }

        public TimeSpan Time { get; set; }

        public Guid UserId { get; set; }
    }
}
