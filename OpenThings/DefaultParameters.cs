/*
* MIT License
*
* Copyright (c) 2022 Derek Goslin
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

using OpenThings.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// The set of default openthings <see cref="Parameter"/> 
    /// </summary>
    public class DefaultParameters : IParameters
    {
        private readonly List<Parameter> _parameterList = new List<Parameter>()
        {
            new Parameter(ParameterIdentifiers.AirPressure, nameof(ParameterIdentifiers.AirPressure), "mbar" ),
            new Parameter(ParameterIdentifiers.Alarm, nameof(ParameterIdentifiers.Alarm), string.Empty),
            new Parameter(ParameterIdentifiers.ApparentPower, nameof(ParameterIdentifiers.ApparentPower), "VA"),
            new Parameter(ParameterIdentifiers.BatteryLevel, nameof(ParameterIdentifiers.BatteryLevel), "V"),
            new Parameter(ParameterIdentifiers.Closures, nameof(ParameterIdentifiers.Closures), string.Empty),
            new Parameter(ParameterIdentifiers.CODetector, nameof(ParameterIdentifiers.CODetector),string.Empty),
            new Parameter(ParameterIdentifiers.Current,nameof(ParameterIdentifiers.Current), "A"),
            new Parameter(ParameterIdentifiers.Debug, nameof(ParameterIdentifiers.Debug),string.Empty),
            new Parameter(ParameterIdentifiers.DoorBell, nameof(ParameterIdentifiers.DoorBell),string.Empty),
            new Parameter(ParameterIdentifiers.DoorSensor,nameof(ParameterIdentifiers.DoorSensor), string.Empty),
            new Parameter(ParameterIdentifiers.EmergencyPanicButton,nameof(ParameterIdentifiers.EmergencyPanicButton), string.Empty),
            new Parameter(ParameterIdentifiers.Energy,nameof(ParameterIdentifiers.Energy), "kWh"),
            new Parameter(ParameterIdentifiers.FallSensor, nameof(ParameterIdentifiers.FallSensor),string.Empty),
            new Parameter(ParameterIdentifiers.Frequency,nameof(ParameterIdentifiers.Frequency), "Hz"),
            new Parameter(ParameterIdentifiers.GasFlowRate,nameof(ParameterIdentifiers.GasFlowRate), "m3/hr"),
            new Parameter(ParameterIdentifiers.GasPressure, nameof(ParameterIdentifiers.GasPressure),"Pa"),
            new Parameter(ParameterIdentifiers.GasVolume, nameof(ParameterIdentifiers.GasVolume),"m3"),
            new Parameter(ParameterIdentifiers.GlassBreakage, nameof(ParameterIdentifiers.GlassBreakage),string.Empty),
            new Parameter(ParameterIdentifiers.Identify, nameof(ParameterIdentifiers.Identify),string.Empty),
            new Parameter(ParameterIdentifiers.Illuminance,nameof(ParameterIdentifiers.Illuminance), "Lux"),
            new Parameter(ParameterIdentifiers.Join, nameof(ParameterIdentifiers.Join),string.Empty),
            new Parameter(ParameterIdentifiers.Level, nameof(ParameterIdentifiers.Level),string.Empty),
            new Parameter(ParameterIdentifiers.LightLevel, nameof(ParameterIdentifiers.LightLevel),string.Empty),
            new Parameter(ParameterIdentifiers.MotionDetector,nameof(ParameterIdentifiers.MotionDetector), string.Empty),
            new Parameter(ParameterIdentifiers.Occupancy, nameof(ParameterIdentifiers.Occupancy),string.Empty),
            new Parameter(ParameterIdentifiers.Phase1Power, nameof(ParameterIdentifiers.Phase1Power),"W"),
            new Parameter(ParameterIdentifiers.Phase2Power,nameof(ParameterIdentifiers.Phase2Power), "W"),
            new Parameter(ParameterIdentifiers.Phase3Power,nameof(ParameterIdentifiers.Phase3Power), "W"),
            new Parameter(ParameterIdentifiers.PowerFactor, nameof(ParameterIdentifiers.PowerFactor),string.Empty),
            new Parameter(ParameterIdentifiers.Rainfall, nameof(ParameterIdentifiers.Rainfall),"mm"),
            new Parameter(ParameterIdentifiers.ReactivePower, nameof(ParameterIdentifiers.ReactivePower),"VAR"),
            new Parameter(ParameterIdentifiers.RealPower, nameof(ParameterIdentifiers.RealPower),"W"),
            new Parameter(ParameterIdentifiers.RelativeHumidity, nameof(ParameterIdentifiers.RelativeHumidity),"%"),
            new Parameter(ParameterIdentifiers.ReportPeriod,nameof(ParameterIdentifiers.ReportPeriod), "s"),
            new Parameter(ParameterIdentifiers.RFQuality, nameof(ParameterIdentifiers.RFQuality),string.Empty),
            new Parameter(ParameterIdentifiers.RotationSpeed, nameof(ParameterIdentifiers.RotationSpeed),"RPM"),
            new Parameter(ParameterIdentifiers.SmokeDetector, nameof(ParameterIdentifiers.SmokeDetector),string.Empty),
            new Parameter(ParameterIdentifiers.SourceSelector, nameof(ParameterIdentifiers.SourceSelector),string.Empty),
            new Parameter(ParameterIdentifiers.SwitchState, nameof(ParameterIdentifiers.SwitchState),string.Empty),
            new Parameter(ParameterIdentifiers.Temperature, nameof(ParameterIdentifiers.Temperature),"Celsius"),
            new Parameter(ParameterIdentifiers.ThreePhaseTotalPower,nameof(ParameterIdentifiers.ThreePhaseTotalPower), "W"),
            new Parameter(ParameterIdentifiers.TimeDate, nameof(ParameterIdentifiers.TimeDate),string.Empty),
            new Parameter(ParameterIdentifiers.Vibration, nameof(ParameterIdentifiers.Vibration), string.Empty),
            new Parameter(ParameterIdentifiers.Voltage, nameof(ParameterIdentifiers.Voltage),"V"),
            new Parameter(ParameterIdentifiers.WaterDetector, nameof(ParameterIdentifiers.WaterDetector),string.Empty),
            new Parameter(ParameterIdentifiers.WaterFlowRate, nameof(ParameterIdentifiers.WaterFlowRate),"l/hr"),
            new Parameter(ParameterIdentifiers.WaterPressure, nameof(ParameterIdentifiers.WaterPressure),"Pa"),
            new Parameter(ParameterIdentifiers.WaterVolume, nameof(ParameterIdentifiers.WaterVolume), "l"),
            new Parameter(ParameterIdentifiers.WindSpeed, nameof(ParameterIdentifiers.WindSpeed), "m/s"),
            new Parameter(ParameterIdentifiers.JoinCommand, nameof(ParameterIdentifiers.JoinCommand), string.Empty),
            new Parameter(ParameterIdentifiers.IdentifyCommand, nameof(ParameterIdentifiers.IdentifyCommand), string.Empty),
            new Parameter(ParameterIdentifiers.ReportPeriodCommand, nameof(ParameterIdentifiers.ReportPeriodCommand), string.Empty)
        };

        /// <summary>
        /// Instantiate instance of <see cref="DefaultParameters"/>
        /// </summary>
        public DefaultParameters()
        {
            _parameterList.Sort();
        }

        /// <inheritdoc/>
        public void Add(Parameter item)
        {
            if (_parameterList.Any(_ => _.Identifier == item.Identifier))
            {
                throw new OpenThingsParameterExistsException($"Parameter with Identifier: [0x{item.Identifier:X2}] exists");
            }
            
            _parameterList.Add(item);
        }

        /// <inheritdoc/>
        public Parameter GetParameter(byte identifier)
        {
            var parameter = _parameterList.FirstOrDefault(_ => _.Identifier == identifier);

            if(parameter == null)
            {
                throw new OpenThingsParameterNotFoundException($"Parameter with Identifier: [0x{identifier:X2}] not found");
            }

            return parameter;
        }
    }
}
