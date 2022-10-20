# OpenThings

[![Build Status](https://dev.azure.com/DerekGn/GitHub/_apis/build/status/DerekGn.OpenThings?branchName=main)](https://dev.azure.com/DerekGn/GitHub/_build/latest?definitionId=4&branchName=main)

[![NuGet Badge](https://buildstats.info/nuget/OpenThings)](https://www.nuget.org/packages/OpenThings/)

A .net core library for encoding and decoding OpenThings messages. See [specification](https://github.com/DerekGn/OpenThings/blob/main/OpenThings%20Specification%5B2505%5D.pdf).

## Installing OpenThings

Install the OpenThings package via nuget package manager console:

``` cmd
Install-Package OpenThings
```

## Supported .Net Runtimes

The OpenThings package is compatible with the following runtimes:

* .NET Standard 2.0

## Encoding

``` csharp
var messageHeader = new MessageHeader(0x55, 0xAA, 0x0000, 0xFEEDED);

var message = new Message(messageHeader);

message.Records.Add(
    new MessageRecord(
        new Parameter(OpenThingsParameter.Temperature),
        new MessageRecordDataFloat(RecordType.UnsignedX4, 20.5f)));

message.Records.Add(
    new MessageRecord(
        new Parameter(OpenThingsParameter.Debug),
        new MessageRecordDataString("TEST")));

message.Records.Add(
    new MessageRecord(
        new Parameter(OpenThingsParameter.Frequency),
        new MessageRecordDataUInt(120)));

message.Records.Add(
    new MessageRecord(
        new Parameter(OpenThingsParameter.Level),
        new MessageRecordDataInt(-50)));

List<Byte> encodedMessageBytes = openthingsMessageEncoder.Encode(message);

// A random seed source for linear encrypt encryption
ushort seed = GetSeed();

// Alternatively apply linear shift encryption to message 
List<Byte> encodedMessageBytes = openthingsMessageEncoder.Encode(message, 0x45, seed);
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