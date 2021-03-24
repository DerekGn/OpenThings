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
    /// <summary>
    /// An OpenThings parameter
    /// </summary>
    public class Parameter
    {
        private static readonly List<Tuple<OpenThingsParameter, string>> parameterUnitsMap = new List<Tuple<OpenThingsParameter, string>>() 
        { 
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.AirPressure, "mbar" ),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Alarm, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.ApparentPower, "VA"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.BatteryLevel, "V"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Closures, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.CODetector, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Current, "A"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Debug, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.DoorBell, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.DoorSensor, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.EmergencyPanicButton, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Energy, "kWh"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.FallSensor, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Frequency, "Hz"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.GasFlowRate, "m3/hr"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.GasPressure, "Pa"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.GasVolume, "m3"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.GlassBreakage, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Identify, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Illuminance, "Lux"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Join, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Level, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.LightLevel, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.MotionDetector, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Occupancy, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Phase1Power, "W"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Phase2Power, "W"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Phase3Power, "W"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.PowerFactor, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Rainfall, "mm"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.ReactivePower, "VAR"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.RealPower, "W"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.RelativeHumidity, "%"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.ReportPeriod, "s"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.RFQuality, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.RotationSpeed, "RPM"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.SmokeDetector, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.SourceSelector, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.SwitchState, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Temperature, "Celsius"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.ThreePhaseTotalPower, "W"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.TimeDate, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Vibration, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.Voltage, "V"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.WaterDetector, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.WaterFlowRate, "l/hr"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.WaterPressure, "Pa"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.WaterVolume, "l"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.WindSpeed, "m/s"),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.IdentifyCommand, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.JoinCommand, ""),
            new Tuple<OpenThingsParameter, string>(OpenThingsParameter.StartOtaCommand, "")
        };

        /// <summary>
        /// Create an instance of a <see cref="Parameter"/>
        /// </summary>
        /// <param name="parameterIdentifier">The <see cref="OpenThingsParameter"/></param>
        public Parameter(OpenThingsParameter parameterIdentifier)
        {
            Identifier = parameterIdentifier;

            Units = parameterUnitsMap.First(_ => _.Item1 == parameterIdentifier).Item2;
        }
        /// <summary>
        /// The <see cref="OpenThingsParameter"/>
        /// </summary>
        public OpenThingsParameter Identifier { get; }
        /// <summary>
        /// The unit for this <see cref="Parameter"/>
        /// </summary>
        public string Units { get; }
        /// <summary>
        /// Get the <see cref="Parameter"/> from the identifier
        /// </summary>
        /// <param name="identifier">The identifier to lookup the <see cref="Parameter"/></param>
        /// <returns>The <see cref="Parameter"/> instance</returns>
        public static Parameter GetParameter(byte identifier)
        {
            var parameter = parameterUnitsMap.FirstOrDefault (_ => _.Item1 == (OpenThingsParameter) identifier);

            if (parameter == null)
            {
                throw new OpenThingsException($"Unable to find parameter id [0x{identifier:X}]");
            }

            return new Parameter(parameter.Item1);
        }

        public override string ToString()
        {
            return $"Identifier: [{Identifier}] Units: [{Units}]";
        }
    }
}
