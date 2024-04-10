namespace Appointify.Domain
{
    public class ErrorResponse
    {
        public string Title { get; set; }

        public int Status { get; set; }

        public IReadOnlyCollection<string> Errors { get; set; }

        public ErrorResponse(string title, int status, IReadOnlyCollection<string> errors)
        {
            Title = title;
            Status = status;
            Errors = errors;
        }
    }
}
