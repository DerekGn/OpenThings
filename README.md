# OpenThings

![GitHub Actions](https://github.com/DerekGn/OpenThings/actions/workflows/build.yml/badge.svg)

[![NuGet](https://img.shields.io/nuget/v/OpenThings.svg?style=flat-square)](https://www.nuget.org/packages/OpenThings/)

A .net core library for encoding and decoding OpenThings messages. See [specification](https://github.com/DerekGn/OpenThings/blob/main/OpenThings%20Specification%5B2505%5D.pdf).

## Installing OpenThings

Install the OpenThings package via nuget package manager console:

``` cmd
Install-Package OpenThings
```

## Supported .Net Runtimes

The OpenThings package is compatible with the following runtimes:

* .NET Core 8.0

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

## Adding Extended Parameters And Commands

The OpenThings specification specifies a number of parameter types out of the box, however there are situations when new parameters are required to be added to support a custom sensor type. There are a number of steps required to support additional parameter types encoding and decoding.

### Extend Parameter Identifiers

To support the encoding and decoding of custom parameter types first extend the **ParameterIdentifiers** base class and add the additional parameters.

``` csharp
internal class ExtendedParameterIdentifiers : ParameterIdentifiers
{
    /// <summary>
    /// The devices battery voltage
    /// </summary>
    public const byte BatteryVoltage = 0x7D;

    /// <summary>
    /// The Indoor air quality
    /// </summary>
    public const byte Iaq = 0x7E;

    /// <summary>
    /// The estimated carbon dioxide level
    /// </summary>
    public const byte eCo2 = 0x7F;

    /// <summary>
    /// The equivalent ethanol level
    /// </summary>
    public const byte EtOH = 0x80;

    /// <summary>
    /// The total volatile organic compounds
    /// </summary>
    public const byte TVOC = 0x81;
}
```

### Extend Commands

To support encoding of command type parameters extend the **DefaultCommands** type.

``` csharp
internal class ExtendedCommands : DefaultCommands
{
    /// <summary>
    /// A command to execute the devices boot loader
    /// </summary>
    public const byte ExecuteBootLoaderCommand = 0xFF;
}
```

### Extend Parameter Definitions

To support the encoding and decoding of custom parameter types the OpenThings library contains a collection of the default parameters called **DefaultParameters**. This class implements the interface **IParameters** interface.

Create a derived type from the **DefaultParameters** type and add the custom parameters to the collection via the **Add** method.

``` csharp
internal class ExtendedParameters : DefaultParameters
{
    public ExtendedParameters()
    {
        Add(new Parameter(
            ExtendedParameterIdentifiers.BatteryVoltage, 
            nameof(ExtendedParameterIdentifiers.BatteryVoltage), "V"));

        Add(new Parameter(
            ExtendedParameterIdentifiers.Iaq,
            nameof(ExtendedParameterIdentifiers.Iaq), string.Empty));

        Add(new Parameter(
            ExtendedParameterIdentifiers.eCo2,
            nameof(ExtendedParameterIdentifiers.eCo2), "ppm"));

        Add(new Parameter(
            ExtendedParameterIdentifiers.EtOH,
            nameof(ExtendedParameterIdentifiers.EtOH), "ppm"));

        Add(new Parameter(
            ExtendedParameterIdentifiers.TVOC,
            nameof(ExtendedParameterIdentifiers.TVOC), "mg/m^3"));

        Add(new Parameter(
            ExtendedCommands.ExecuteBootLoaderCommand,
            nameof(ExtendedCommands.ExecuteBootLoaderCommand), string.Empty));
    }
}
```

An instance of this custom ExtendedParameters type is then injected into the **OpenThingsDecoder** instance either directly or via a DI framework.
