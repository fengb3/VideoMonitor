// using Worker;
//
// var builder = Host.CreateApplicationBuilder(args);
// builder.Services.AddHostedService<Worker.Worker>();
//
// var host = builder.Build();
// host.Run();

using Lib.API.DataConsumer.Clawer;

var consummer = new UploaderDataConsumer();
await consummer.Consume(1);