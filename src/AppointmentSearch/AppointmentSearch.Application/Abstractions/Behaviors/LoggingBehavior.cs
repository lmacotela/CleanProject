
using System.Diagnostics;
using AppointmentSearch.Application.Abstractions.Messaging;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppointmentSearch.Application.Abstractions.Behaviors;


public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IBaseCommand
{
    private readonly ILogger<TRequest> _logger;

    public LoggingBehavior(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var responseName = typeof(TResponse).Name;
        try{
            _logger.LogInformation("Handling {Request} ({Response})", requestName, responseName);
            var result = await next();
            _logger.LogInformation("Handled {Request} ({Response})", requestName, responseName);

            return result;

        }catch(Exception ex){
            _logger.LogError(ex, "Error handling {Request} ({Response})", requestName, responseName);
            throw;
        }
    }
}
