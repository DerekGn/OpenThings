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

using System.Collections.Generic;

namespace OpenThings
{
    public class Parameter
    {
        static Parameter()
        {
            Parameters = new List<Parameter>()
            {
                new Parameter(ParameterIdentifiers.AirPressure, "mbar" ),
                new Parameter(ParameterIdentifiers.Alarm, ""),
                new Parameter(ParameterIdentifiers.ApparentPower, "VA"),
                new Parameter(ParameterIdentifiers.BatteryLevel, "V"),
                new Parameter(ParameterIdentifiers.Closures, ""),
                new Parameter(ParameterIdentifiers.CODetector, ""),
                new Parameter(ParameterIdentifiers.Current, "A"),
                new Parameter(ParameterIdentifiers.Debug, ""),
                new Parameter(ParameterIdentifiers.DoorBell, ""),
                new Parameter(ParameterIdentifiers.DoorSensor, ""),
                new Parameter(ParameterIdentifiers.EmergencyPanicButton, ""),
                new Parameter(ParameterIdentifiers.Energy, "kWh"),
                new Parameter(ParameterIdentifiers.FallSensor, ""),
                new Parameter(ParameterIdentifiers.Frequency, "Hz"),
                new Parameter(ParameterIdentifiers.GasFlowRate, "m3/hr"),
                new Parameter(ParameterIdentifiers.GasPressure, "Pa"),
                new Parameter(ParameterIdentifiers.GasVolume, "m3"),
                new Parameter(ParameterIdentifiers.GlassBreakage, ""),
                new Parameter(ParameterIdentifiers.Identify, ""),
                new Parameter(ParameterIdentifiers.Illuminance, "Lux"),
                new Parameter(ParameterIdentifiers.Join, ""),
                new Parameter(ParameterIdentifiers.Level, ""),
                new Parameter(ParameterIdentifiers.LightLevel, ""),
                new Parameter(ParameterIdentifiers.MotionDetector, ""),
                new Parameter(ParameterIdentifiers.Occupancy, ""),
                new Parameter(ParameterIdentifiers.Phase1Power, "W"),
                new Parameter(ParameterIdentifiers.Phase2Power, "W"),
                new Parameter(ParameterIdentifiers.Phase3Power, "W"),
                new Parameter(ParameterIdentifiers.PowerFactor, ""),
                new Parameter(ParameterIdentifiers.Rainfall, "mm"),
                new Parameter(ParameterIdentifiers.ReactivePower, "VAR"),
                new Parameter(ParameterIdentifiers.RealPower, "W"),
                new Parameter(ParameterIdentifiers.RelativeHumidity, "%"),
                new Parameter(ParameterIdentifiers.ReportPeriod, "s"),
                new Parameter(ParameterIdentifiers.RFQuality, ""),
                new Parameter(ParameterIdentifiers.RotationSpeed, "RPM"),
                new Parameter(ParameterIdentifiers.SmokeDetector, ""),
                new Parameter(ParameterIdentifiers.SourceSelector, ""),
                new Parameter(ParameterIdentifiers.SwitchState, ""),
                new Parameter(ParameterIdentifiers.Temperature, "Celsius"),
                new Parameter(ParameterIdentifiers.ThreePhaseTotalPower, "W"),
                new Parameter(ParameterIdentifiers.TimeDate, ""),
                new Parameter(ParameterIdentifiers.Vibration, ""),
                new Parameter(ParameterIdentifiers.Voltage, "V"),
                new Parameter(ParameterIdentifiers.WaterDetector, ""),
                new Parameter(ParameterIdentifiers.WaterFlowRate, "l/hr"),
                new Parameter(ParameterIdentifiers.WaterPressure, "Pa"),
                new Parameter(ParameterIdentifiers.WaterVolume, "l"),
                new Parameter(ParameterIdentifiers.WindSpeed, "m/s")
            };
        }
        public Parameter(ParameterIdentifiers identifier, string units)
        {
            Identifier = identifier;
            Units = units;
        }
        public ParameterIdentifiers Identifier { get; }
        public string Units { get; set; }
        public static IList<Parameter> Parameters { get; private set; }
        public override string ToString()
        {
            return 
                $"Identifier:\t[{Identifier}]\r\n" +
                $"\t\tUnits:\t\t[{Units}]";
        }
    }
}
