/*
* MIT License
*
* Copyright (c) 2021 Derek Goslin
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

namespace OpenThings
{
    /// <summary>
    /// The OpenThings parameter identifiers
    /// </summary>
    public enum OpenThingsParameter : byte
    {
        /// <summary>
        /// An alarm parameter
        /// </summary>
        Alarm = 0x21,
        /// <summary>
        /// A debug parameter
        /// </summary>
        Debug = 0x2D,
        /// <summary>
        /// Identify parameter
        /// </summary>
        Identify = 0x3F,
        /// <summary>
        /// Source selection parameter
        /// </summary>
        SourceSelector = 0xC0,
        /// <summary>
        /// Water detection parameter
        /// </summary>
        WaterDetector = 0x41,
        /// <summary>
        /// A glass break parameter
        /// </summary>
        GlassBreakage = 0x42,
        /// <summary>
        /// A closure parameter
        /// </summary>
        Closures = 0x43,
        /// <summary>
        /// A door bell parameter
        /// </summary>
        DoorBell = 0x44,
        /// <summary>
        /// Energy parameter
        /// </summary>
        Energy = 0x45,
        /// <summary>
        /// Fall sensor parameter
        /// </summary>
        FallSensor = 0x46,
        /// <summary>
        /// Gas volume
        /// </summary>
        GasVolume = 0x47,
        /// <summary>
        /// Air pressure
        /// </summary>
        AirPressure = 0x48,
        /// <summary>
        /// Luminance
        /// </summary>
        Illuminance = 0x49,
        /// <summary>
        /// Level
        /// </summary>
        Level = 0x4C,
        /// <summary>
        /// Rainfall
        /// </summary>
        Rainfall = 0x4D,
        /// <summary>
        /// Apparent power
        /// </summary>
        ApparentPower = 0x50,
        /// <summary>
        /// Power factor
        /// </summary>
        PowerFactor = 0x51,
        /// <summary>
        /// Report period
        /// </summary>
        ReportPeriod = 0x52,
        /// <summary>
        /// Smoke detector
        /// </summary>
        SmokeDetector = 0x53,
        /// <summary>
        /// Time Date
        /// </summary>
        TimeDate = 0x54,
        /// <summary>
        /// Vibration
        /// </summary>
        Vibration = 0x56,
        /// <summary>
        /// Water volume
        /// </summary>
        WaterVolume = 0x57,
        /// <summary>
        /// Wind speed
        /// </summary>
        WindSpeed = 0x58,
        /// <summary>
        /// Gas pressure
        /// </summary>
        GasPressure = 0x61,
        /// <summary>
        /// Battery level
        /// </summary>
        BatteryLevel = 0x62,
        /// <summary>
        /// CO Detector
        /// </summary>
        CODetector = 0x63,
        /// <summary>
        /// Door Sensor
        /// </summary>
        DoorSensor = 0x64,
        /// <summary>
        /// Emergency Panic Button
        /// </summary>
        EmergencyPanicButton = 0x65,
        /// <summary>
        /// Frequency
        /// </summary>
        Frequency = 0x66,
        /// <summary>
        /// Gas flow rate
        /// </summary>
        GasFlowRate = 0x67,
        /// <summary>
        /// Relative humidity
        /// </summary>
        RelativeHumidity = 0x68,
        /// <summary>
        /// Current
        /// </summary>
        Current = 0x69,
        /// <summary>
        /// Join
        /// </summary>
        Join = 0x6A,
        /// <summary>
        /// Rf Quality
        /// </summary>
        RFQuality = 0x6B,
        /// <summary>
        /// Light level
        /// </summary>
        LightLevel = 0x6C,
        /// <summary>
        /// Motion detector
        /// </summary>
        MotionDetector = 0x6D,
        /// <summary>
        /// Occupancy
        /// </summary>
        Occupancy = 0x6F,
        /// <summary>
        /// Real power
        /// </summary>
        RealPower = 0x70,
        /// <summary>
        /// Reactive power
        /// </summary>
        ReactivePower = 0x71,
        /// <summary>
        /// Rotation speed
        /// </summary>
        RotationSpeed = 0x72,
        /// <summary>
        /// Switch state
        /// </summary>
        SwitchState = 0x73,
        /// <summary>
        /// Temperature
        /// </summary>
        Temperature = 0x74,
        /// <summary>
        /// Voltage
        /// </summary>
        Voltage = 0x76,
        /// <summary>
        /// Water flow
        /// </summary>
        WaterFlowRate = 0x77,
        /// <summary>
        /// Water pressure
        /// </summary>
        WaterPressure = 0x78,
        /// <summary>
        /// Phase 1 Power
        /// </summary>
        Phase1Power = 0x79,
        /// <summary>
        /// Phase 2 Power
        /// </summary>
        Phase2Power = 0x7A,
        /// <summary>
        /// Phase 3 Power
        /// </summary>
        Phase3Power = 0x7B,
        /// <summary>
        /// THree phase total power
        /// </summary>
        ThreePhaseTotalPower = 0x7C,
        /// <summary>
        /// The Voc index
        /// </summary>
        VocIndex = 0x7D,
        /// <summary>
        /// Phase angle
        /// </summary>
        PhaseAngle = 0x7E,
        /// <summary>
        /// Active power
        /// </summary>
        ActivePower = 0x7F,
        /// <summary>
        /// Forward active energy
        /// </summary>
        ForwardActiveEnergy = 0x80,
        /// <summary>
        /// Reverse active energy
        /// </summary>
        ReverseActiveEnergy = 0x81,
        /// <summary>
        /// Absolute active energy
        /// </summary>
        AbsoluteActiveEnergy = 0x82,
        /// <summary>
        /// Forward reactive energy
        /// </summary>
        ForwardReactiveEnergy = 0x83,
        /// <summary>
        /// Reverse reactive energy
        /// </summary>
        ReverseReactiveEnergy = 0x84,
        /// <summary>
        /// Absolute reactive energy
        /// </summary>
        AbsoluteReactiveEnergy = 0x85,
        /// <summary>
        /// An Identify command
        /// </summary>
        IdentifyCommand = 0xBF,
        /// <summary>
        /// A Join command
        /// </summary>
        JoinCommand = 0xEA,
        /// <summary>
        /// Start an OTA update
        /// </summary>
        StartOtaCommand = 0xFF
    }
}
