using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

var configuration = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var twilioAccountSid = configuration["Twilio:AccountSid"];
var twilioApiKeySid = configuration["Twilio:ApiKeySid"];
var twilioApiKeySecret = configuration["Twilio:ApiKeySecret"];

TwilioClient.Init(twilioApiKeySid, twilioApiKeySecret, twilioAccountSid);

Console.Write("From? ");
var from = (PhoneNumber)Console.ReadLine();

Console.Write("To? ");
var to = (PhoneNumber)Console.ReadLine();

var call = CallResource.Create(
    from: from,
    to: to,
    url: new Uri("http://demo.twilio.com/docs/voice.xml")
);