using Forum.Data.Models;
using Forum.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Forum.Api.Controllers.Filters;

public class EnsureNewTopicIdIsUniqueFilter : IActionFilter
{
    private readonly ITopicsService _topics;

    public EnsureNewTopicIdIsUniqueFilter(ITopicsService topics)
    {
        _topics = topics;
    }

    public async void OnActionExecuting(ActionExecutingContext context)
    {
        var id = (int) (context.ActionArguments["id"] ?? throw new InvalidOperationException());
        var topic = (Topic) (context.ActionArguments["topic"] ?? throw new InvalidOperationException());
        if (!await _topics.NewIdIsUnique(id, topic.Id)) context.Result = new BadRequestResult();
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}