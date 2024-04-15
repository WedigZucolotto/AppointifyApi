﻿using Appointify.Application.Queries.Companies.GetById;
using FluentValidation;

namespace Appointify.Application.Queries.Services.Options
{
    public class GetServiceOptionsQueryValidator : AbstractValidator<GetServiceOptionsQuery>
    {
        public GetServiceOptionsQueryValidator()
        {
        }
    }
}