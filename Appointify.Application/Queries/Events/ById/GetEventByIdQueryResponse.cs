﻿namespace Appointify.Application.Queries.Events.ById
{
    public class GetEventByIdQueryResponse
    {
        public GetEventByIdQueryResponse(
            Guid id,
            string title, 
            string description, 
            string date, 
            string serviceName)
        {
            Id = id;
            Title = title;
            Description = description;
            Date = date;
            ServiceName = serviceName;
        }

        public Guid Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Date { get; set; } = string.Empty;

        public string ServiceName { get; set; } = string.Empty;
    }
}
