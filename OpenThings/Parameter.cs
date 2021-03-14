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

using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    public class Parameter
    {
        private static readonly List<Tuple<ParameterIdentifier, string>> parameterUnitsMap = new List<Tuple<ParameterIdentifier, string>>() 
        { 
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.AirPressure, "mbar" ),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Alarm, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.ApparentPower, "VA"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.BatteryLevel, "V"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Closures, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.CODetector, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Current, "A"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Debug, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.DoorBell, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.DoorSensor, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.EmergencyPanicButton, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Energy, "kWh"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.FallSensor, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Frequency, "Hz"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.GasFlowRate, "m3/hr"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.GasPressure, "Pa"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.GasVolume, "m3"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.GlassBreakage, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Identify, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Illuminance, "Lux"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Join, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Level, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.LightLevel, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.MotionDetector, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Occupancy, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Phase1Power, "W"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Phase2Power, "W"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Phase3Power, "W"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.PowerFactor, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Rainfall, "mm"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.ReactivePower, "VAR"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.RealPower, "W"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.RelativeHumidity, "%"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.ReportPeriod, "s"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.RFQuality, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.RotationSpeed, "RPM"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.SmokeDetector, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.SourceSelector, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.SwitchState, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Temperature, "Celsius"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.ThreePhaseTotalPower, "W"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.TimeDate, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Vibration, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.Voltage, "V"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.WaterDetector, ""),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.WaterFlowRate, "l/hr"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.WaterPressure, "Pa"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.WaterVolume, "l"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.WindSpeed, "m/s"),
            new Tuple<ParameterIdentifier, string>(ParameterIdentifier.JoinSlave, "")
        };

        public Parameter(ParameterIdentifier parameterIdentifier)
        {
            Identifier = parameterIdentifier;

            Units = parameterUnitsMap.First(_ => _.Item1 == parameterIdentifier).Item2;
        }

        public ParameterIdentifier Identifier { get; }
        public string Units { get; }
        public static Parameter GetParameter(byte identifier)
        {
            var parameter = parameterUnitsMap.First(_ => _.Item1 == (ParameterIdentifier) identifier);

            if (parameter == null)
            {
                throw new OpenThingsException($"Unable to find parameter id [0x{identifier:X}]");
            }

            return new Parameter(parameter.Item1);
        }

        public override string ToString()
        {
            return 
                $"Identifier:\t[{Identifier}]\r\n" +
                $"\t\tUnits:\t\t[{Units}]";
        }
    }
}
