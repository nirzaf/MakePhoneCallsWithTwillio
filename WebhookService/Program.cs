using Twilio.AspNet.Core.MinimalApi;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

var app = WebApplication.Create(args);

app.MapPost("/", () =>
{
    var voiceResponse = new VoiceResponse()
        .Pause(2)
        .Say("What is your favorite IDE?")
        .Pause(1)
        .Say("Press 1 for JetBrains Rider.")
        .Pause(1)
        .Say("Press 2 for vim.")
        .Pause(1)
        .Say("Press 3 for notepad.")
        .Gather(
            action: new Uri("/submit", UriKind.Relative),
            numDigits: 1,
            input: new List<Gather.InputEnum> {Gather.InputEnum.Dtmf}
        );

    return Results.Extensions.TwiML(voiceResponse);
});

app.MapPost("/submit", async (HttpRequest httpRequest) =>
{
    var form = await httpRequest.ReadFormAsync();
    var digit = form["digits"].Single();
    app.Logger.LogInformation("Someone voted {IdeNumber}", digit);

    var voiceResponse = new VoiceResponse()
        .Say($"You pressed {digit}. Good choice. Goodbye.");
    return Results.Extensions.TwiML(voiceResponse);
});

app.Run();

