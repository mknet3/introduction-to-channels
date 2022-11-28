## Introduction to Producer Consumer pattern using Channels

[manuelcanete.com](https://manuelcanete.com/posts/introduccion_patron_consumidor_channels/)

Run the project in one terminal:
```bash
dotnet run --project ProducerConsumerChannels
```

Make a request with `curl` in another terminal:
```bash
curl --location --request POST 'http://localhost:5014/messages' --header 'Content-Type: application/json' --data-raw '"Hello World"'
```

And you will see the logs from the consumer like:

```log
info: MessageConsumer[0]
      Message received: Hello World
```
