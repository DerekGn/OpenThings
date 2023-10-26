/*
* MIT License
*
* Copyright (c) 2022 Derek Goslin
*
* Permission is hereby granted; free of charge; to any person obtaining a copy
* of this software and associated documentation files (the "Software"); to deal
* in the Software without restriction; including without limitation the rights
* to use; copy; modify; merge; publish; distribute; sublicense; and/or sell
* copies of the Software; and to permit persons to whom the Software is
* furnished to do so; subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in all
* copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS"; WITHOUT WARRANTY OF ANY KIND; EXPRESS OR
* IMPLIED; INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY;
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM; DAMAGES OR OTHER
* LIABILITY; WHETHER IN AN ACTION OF CONTRACT; TORT OR OTHERWISE; ARISING FROM;
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
* SOFTWARE.
*/

namespace OpenThings
{
    /// <summary>
    /// The OpenThings parameter identifiers
    /// </summary>
    public partial class ParameterIdentifiers
    {
        /// <summary>
        /// Air pressure
        /// </summary>
        public const byte AirPressure = 0x48;

        /// <summary>
        /// An alarm parameter
        /// </summary>
        public const byte Alarm = 0x21;

        /// <summary>
        /// Apparent power
        /// </summary>
        public const byte ApparentPower = 0x50;

        /// <summary>
        /// Battery level
        /// </summary>
        public const byte BatteryLevel = 0x62;

        /// <summary>
        /// A closure parameter
        /// </summary>
        public const byte Closures = 0x43;

        /// <summary>
        /// CO Detector
        /// </summary>
        public const byte CODetector = 0x63;

        /// <summary>
        /// Current
        /// </summary>
        public const byte Current = 0x69;

        /// <summary>
        /// A debug parameter
        /// </summary>
        public const byte Debug = 0x2D;

        /// <summary>
        /// A door bell parameter
        /// </summary>
        public const byte DoorBell = 0x44;

        /// <summary>
        /// Door Sensor
        /// </summary>
        public const byte DoorSensor = 0x64;

        /// <summary>
        /// Emergency Panic Button
        /// </summary>
        public const byte EmergencyPanicButton = 0x65;

        /// <summary>
        /// Energy parameter
        /// </summary>
        public const byte Energy = 0x45;

        /// <summary>
        /// Fall sensor parameter
        /// </summary>
        public const byte FallSensor = 0x46;

        /// <summary>
        /// Frequency
        /// </summary>
        public const byte Frequency = 0x66;

        /// <summary>
        /// Gas flow rate
        /// </summary>
        public const byte GasFlowRate = 0x67;

        /// <summary>
        /// Gas pressure
        /// </summary>
        public const byte GasPressure = 0x61;

        /// <summary>
        /// Gas volume
        /// </summary>
        public const byte GasVolume = 0x47;

        /// <summary>
        /// A glass break parameter
        /// </summary>
        public const byte GlassBreakage = 0x42;

        /// <summary>
        /// Identify parameter
        /// </summary>
        public const byte Identify = 0x3F;

        /// <summary>
        /// Luminance
        /// </summary>
        public const byte Illuminance = 0x49;

        /// <summary>
        /// Join
        /// </summary>
        public const byte Join = 0x6A;

        /// <summary>
        /// Level
        /// </summary>
        public const byte Level = 0x4C;

        /// <summary>
        /// Light level
        /// </summary>
        public const byte LightLevel = 0x6C;

        /// <summary>
        /// Motion detector
        /// </summary>
        public const byte MotionDetector = 0x6D;

        /// <summary>
        /// Occupancy
        /// </summary>
        public const byte Occupancy = 0x6F;

        /// <summary>
        /// Phase 1 Power
        /// </summary>
        public const byte Phase1Power = 0x79;

        /// <summary>
        /// Phase 2 Power
        /// </summary>
        public const byte Phase2Power = 0x7A;

        /// <summary>
        /// Phase 3 Power
        /// </summary>
        public const byte Phase3Power = 0x7B;

        /// <summary>
        /// Power factor
        /// </summary>
        public const byte PowerFactor = 0x51;

        /// <summary>
        /// Rainfall
        /// </summary>
        public const byte Rainfall = 0x4D;

        /// <summary>
        /// Reactive power
        /// </summary>
        public const byte ReactivePower = 0x71;

        /// <summary>
        /// Real power
        /// </summary>
        public const byte RealPower = 0x70;

        /// <summary>
        /// Relative humidity
        /// </summary>
        public const byte RelativeHumidity = 0x68;

        /// <summary>
        /// Report period
        /// </summary>
        public const byte ReportPeriod = 0x52;

        /// <summary>
        /// Rf Quality
        /// </summary>
        public const byte RFQuality = 0x6B;

        /// <summary>
        /// Rotation speed
        /// </summary>
        public const byte RotationSpeed = 0x72;

        /// <summary>
        /// Smoke detector
        /// </summary>
        public const byte SmokeDetector = 0x53;

        /// <summary>
        /// Source selection parameter
        /// </summary>
        public const byte SourceSelector = 0xC0;

        /// <summary>
        /// Switch state
        /// </summary>
        public const byte SwitchState = 0x73;

        /// <summary>
        /// Temperature
        /// </summary>
        public const byte Temperature = 0x74;

        /// <summary>
        /// THree phase total power
        /// </summary>
        public const byte ThreePhaseTotalPower = 0x7C;

        /// <summary>
        /// Time Date
        /// </summary>
        public const byte TimeDate = 0x54;

        /// <summary>
        /// Vibration
        /// </summary>
        public const byte Vibration = 0x56;

        /// <summary>
        /// Voltage
        /// </summary>
        public const byte Voltage = 0x76;

        /// <summary>
        /// Water detection parameter
        /// </summary>
        public const byte WaterDetector = 0x41;

        /// <summary>
        /// Water flow
        /// </summary>
        public const byte WaterFlowRate = 0x77;

        /// <summary>
        /// Water pressure
        /// </summary>
        public const byte WaterPressure = 0x78;

        /// <summary>
        /// Water volume
        /// </summary>
        public const byte WaterVolume = 0x57;

        /// <summary>
        /// Wind speed
        /// </summary>
        public const byte WindSpeed = 0x58;

        /// <summary>
        /// An Identify command
        /// </summary>
        public const byte IdentifyCommand = DefaultCommands.IdentifyCommand;

        /// <summary>
        /// A Join command
        /// </summary>
        public const byte JoinCommand = DefaultCommands.JoinCommand;

        /// <summary>
        /// Report period command
        /// </summary>
        public const byte ReportPeriodCommand = DefaultCommands.ReportPeriodCommand;
    }
}