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
        Alarm = 0x21,
        Debug = 0x2D,
        Identify = 0x3F,
        SourceSelector = 0xC0,
        WaterDetector = 0x41,
        GlassBreakage = 0x42,
        Closures = 0x43,
        DoorBell = 0x44,
        Energy = 0x45,
        FallSensor = 0x46,
        GasVolume = 0x47,
        AirPressure = 0x48,
        Illuminance = 0x49,
        Level = 0x4C,
        Rainfall = 0x4D,
        ApparentPower = 0x50,
        PowerFactor = 0x51,
        ReportPeriod = 0x52,
        SmokeDetector = 0x53,
        TimeDate = 0x54,
        Vibration = 0x56,
        WaterVolume = 0x57,
        WindSpeed = 0x58,
        GasPressure = 0x61,
        BatteryLevel = 0x62,
        CODetector = 0x63,
        DoorSensor = 0x64,
        EmergencyPanicButton = 0x65,
        Frequency = 0x66,
        GasFlowRate = 0x67,
        RelativeHumidity = 0x68,
        Current = 0x69,
        Join = 0x6A,
        RFQuality = 0x6B,
        LightLevel = 0x6C,
        MotionDetector = 0x6D,
        Occupancy = 0x6F,
        RealPower = 0x70,
        ReactivePower = 0x71,
        RotationSpeed = 0x72,
        SwitchState = 0x73,
        Temperature = 0x74,
        Voltage = 0x76,
        WaterFlowRate = 0x77,
        WaterPressure = 0x78,
        Phase1Power = 0x79,
        Phase2Power = 0x7A,
        Phase3Power = 0x7B,
        ThreePhaseTotalPower = 0x7C,
        IdentifyCommand = 0xBF,
        JoinCommand = 0xEA,
        StartOtaCommand = 0xFF
    }
}
