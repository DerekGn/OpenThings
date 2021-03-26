# OpenThings

[![Build Status](https://dev.azure.com/DerekGn/GitHub/_apis/build/status/DerekGn.OpenThings?branchName=main)](https://dev.azure.com/DerekGn/GitHub/_build/latest?definitionId=4&branchName=main)

A .net core library for encoding and decoding OpenThings messages. See [specification](https://github.com/DerekGn/OpenThings/blob/main/OpenThings%20Specification%5B2505%5D.pdf).

## Installing OpenThings

Install the OpenThings package via nuget package manager console:

```
Install-Package OpenThings
```

## Supported .Net Runtimes

The OpenThings package is compatible with the following runtimes:

* .NET Standard 2.0

## Encoding

``` csharp
var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

var message = new Message(messageHeader);

var parameterTemperature = new Parameter(OpenThingsParameter.Temperature);
var dataTempTemperature = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);
var parameterHumidity = new Parameter(OpenThingsParameter.RelativeHumidity);
var dataHumidity = new MessageRecordDataUInt(RecordType.UnsignedX0, 2, 0xBEEF);

message.Records.Add(
    new MessageRecord(
        parameterTemp,
        dataTemp));

message.Records.Add(
    new MessageRecord(
        parameterHumidity,
        dataHumidity));

List<Byte> encodedMessageBytes = openthingsMessageEncoder.Encode(message);

// A random seed source for linear encrypt encryption
ushort seed = GetSeed();

// Alternatively apply linear shift encryption to message 
List<Byte> encodedMessageBytes = openthingsMessageEncoder.Encode(message, 0x45, );
```

## Decoding

``` csharp
// List of manufacturer Ids to PID's for linear shift decryption
List<PidMap> pidMaps = new List<PidMap>()
{
    new PidMap(0xAA, 1)
}

List<Byte> payload = GetPayloadFromSource();

var openthingsMessage = _decoder.Decode(payload, pidMaps);

Console.WriteLine($"Message: {openthingsMessage}");
```