﻿using Azure.Messaging.ServiceBus;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Onatrix.ViewModels;
using System.Text;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace Onatrix.Controllers;

public class FormSurfaceController : SurfaceController
{
    private readonly string _serviceBusConnectionString;
    private readonly string _queueName;
    private readonly IConfiguration _configuration;

    public FormSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, 
                                IUmbracoDatabaseFactory databaseFactory, 
                                ServiceContext services, 
                                AppCaches appCaches, 
                                IProfilingLogger profilingLogger, 
                                IPublishedUrlProvider publishedUrlProvider,
                                IConfiguration configuration) : base(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        _configuration = configuration;
        _serviceBusConnectionString = _configuration["AzureServiceBus:ConnectionString"]!;
        _queueName = _configuration["AzureServiceBus:QueueName"]!;
       
    }

    [HttpPost]
	public async Task<IActionResult> RequestForm(RequestFormViewModel form)
	{
		if (!ModelState.IsValid)
			return CurrentUmbracoPage();

        var emailRequest = new EmailRequest
        {
            To = form.Email,
            Subject = "Request Form Confirmation",
            HtmlBody = $"<p>Dear <strong>{form.Name}</strong> thank you for your request! We will soon contact you. With best regards, Onatrix Team</p>", 
            PlainText = $"Dear {form.Name} thank you for your request! We will soon contact you. With best regards, Onatrix Team"
        };

        await SendMessageToServiceBusAsync(emailRequest);

        return RedirectToCurrentUmbracoPage();
	}

    [HttpPost]
    public async Task<IActionResult> QuestionForm(QuestionFormViewModel form)
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();
        
        var emailRequest = new EmailRequest
        {
            To = form.Email,
            Subject = "Question Form Confirmation",
            HtmlBody = $"<p>Dear <strong>{form.Name}</strong>, thank you for your question! We will soon contact you. With best regards, Onatrix Team</p>",
            PlainText = $"Dear {form.Name} thank you for your question! We will soon contact you. With best regards, Onatrix Team"
        };

        await SendMessageToServiceBusAsync(emailRequest);

        return RedirectToCurrentUmbracoPage();
    }

    [HttpPost]
    public async Task<IActionResult> ContactForm(ContactFormViewModel form)
    {
        if (!ModelState.IsValid)
            return CurrentUmbracoPage();

        var emailRequest = new EmailRequest
        {
            To = form.Email,
            Subject = "Contact Form Confirmation",
            HtmlBody = $"<p>Dear User, thank you for contacting us! We will soon provide help. With best regards, Onatrix Team</p>",
            PlainText = $"Dear User, thank you for contacting us! We will soon provide help. With best regards, Onatrix Team"
        };

        await SendMessageToServiceBusAsync(emailRequest);

        return RedirectToCurrentUmbracoPage();
    }


    private async Task SendMessageToServiceBusAsync(EmailRequest emailRequest)
    {

        await using var client = new ServiceBusClient(_serviceBusConnectionString);
        ServiceBusSender sender = client.CreateSender(_queueName);
        var jsonMessage = JsonConvert.SerializeObject(emailRequest);
        ServiceBusMessage message = new(Encoding.UTF8.GetBytes(jsonMessage));

        await sender.SendMessageAsync(message);
    }
}
public class EmailRequest
{
    public string To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string HtmlBody { get; set; } = null!;
    public string PlainText { get; set; } = null!;
}

