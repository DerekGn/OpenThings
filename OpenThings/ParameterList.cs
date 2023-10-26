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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace OpenThings
{
    /// <summary>
    /// A <see cref="IList{T}"/> of <see cref="Parameter"/> 
    /// </summary>
    public class ParameterList : IList<Parameter>
    {
        private readonly List<Parameter> _parameterList = new List<Parameter>()
        {
            new Parameter(ParameterIdentifier.AirPressure, nameof(ParameterIdentifier.AirPressure), "mbar" ),
            new Parameter(ParameterIdentifier.Alarm, nameof(ParameterIdentifier.Alarm), ""),
            new Parameter(ParameterIdentifier.ApparentPower, nameof(ParameterIdentifier.ApparentPower), "VA"),
            new Parameter(ParameterIdentifier.BatteryLevel, nameof(ParameterIdentifier.BatteryLevel), "V"),
            new Parameter(ParameterIdentifier.Closures, nameof(ParameterIdentifier.Closures), ""),
            new Parameter(ParameterIdentifier.CODetector, nameof(ParameterIdentifier.CODetector),""),
            new Parameter(ParameterIdentifier.Current,nameof(ParameterIdentifier.Current), "A"),
            new Parameter(ParameterIdentifier.Debug, nameof(ParameterIdentifier.Debug),""),
            new Parameter(ParameterIdentifier.DoorBell, nameof(ParameterIdentifier.DoorBell),""),
            new Parameter(ParameterIdentifier.DoorSensor,nameof(ParameterIdentifier.DoorSensor), ""),
            new Parameter(ParameterIdentifier.EmergencyPanicButton,nameof(ParameterIdentifier.EmergencyPanicButton), ""),
            new Parameter(ParameterIdentifier.Energy,nameof(ParameterIdentifier.Energy), "kWh"),
            new Parameter(ParameterIdentifier.FallSensor, nameof(ParameterIdentifier.FallSensor),""),
            new Parameter(ParameterIdentifier.Frequency,nameof(ParameterIdentifier.Frequency), "Hz"),
            new Parameter(ParameterIdentifier.GasFlowRate,nameof(ParameterIdentifier.GasFlowRate), "m3/hr"),
            new Parameter(ParameterIdentifier.GasPressure, nameof(ParameterIdentifier.GasPressure),"Pa"),
            new Parameter(ParameterIdentifier.GasVolume, nameof(ParameterIdentifier.GasVolume),"m3"),
            new Parameter(ParameterIdentifier.GlassBreakage, nameof(ParameterIdentifier.GlassBreakage),""),
            new Parameter(ParameterIdentifier.Identify, nameof(ParameterIdentifier.Identify),""),
            new Parameter(ParameterIdentifier.Illuminance,nameof(ParameterIdentifier.Illuminance), "Lux"),
            new Parameter(ParameterIdentifier.Join, nameof(ParameterIdentifier.Join),""),
            new Parameter(ParameterIdentifier.Level, nameof(ParameterIdentifier.Level),""),
            new Parameter(ParameterIdentifier.LightLevel, nameof(ParameterIdentifier.LightLevel),""),
            new Parameter(ParameterIdentifier.MotionDetector,nameof(ParameterIdentifier.MotionDetector), ""),
            new Parameter(ParameterIdentifier.Occupancy, nameof(ParameterIdentifier.Occupancy),""),
            new Parameter(ParameterIdentifier.Phase1Power, nameof(ParameterIdentifier.Phase1Power),"W"),
            new Parameter(ParameterIdentifier.Phase2Power,nameof(ParameterIdentifier.Phase2Power), "W"),
            new Parameter(ParameterIdentifier.Phase3Power,nameof(ParameterIdentifier.Phase3Power), "W"),
            new Parameter(ParameterIdentifier.PowerFactor, nameof(ParameterIdentifier.PowerFactor),""),
            new Parameter(ParameterIdentifier.Rainfall, nameof(ParameterIdentifier.Rainfall),"mm"),
            new Parameter(ParameterIdentifier.ReactivePower, nameof(ParameterIdentifier.ReactivePower),"VAR"),
            new Parameter(ParameterIdentifier.RealPower, nameof(ParameterIdentifier.RealPower),"W"),
            new Parameter(ParameterIdentifier.RelativeHumidity, nameof(ParameterIdentifier.RelativeHumidity),"%"),
            new Parameter(ParameterIdentifier.ReportPeriod,nameof(ParameterIdentifier.ReportPeriod), "s"),
            new Parameter(ParameterIdentifier.RFQuality, nameof(ParameterIdentifier.RFQuality),""),
            new Parameter(ParameterIdentifier.RotationSpeed, nameof(ParameterIdentifier.RotationSpeed),"RPM"),
            new Parameter(ParameterIdentifier.SmokeDetector, nameof(ParameterIdentifier.SmokeDetector),""),
            new Parameter(ParameterIdentifier.SourceSelector, nameof(ParameterIdentifier.SourceSelector),""),
            new Parameter(ParameterIdentifier.SwitchState, nameof(ParameterIdentifier.SwitchState),""),
            new Parameter(ParameterIdentifier.Temperature, nameof(ParameterIdentifier.Temperature),"Celsius"),
            new Parameter(ParameterIdentifier.ThreePhaseTotalPower,nameof(ParameterIdentifier.ThreePhaseTotalPower), "W"),
            new Parameter(ParameterIdentifier.TimeDate, nameof(ParameterIdentifier.TimeDate),""),
            new Parameter(ParameterIdentifier.Vibration, nameof(ParameterIdentifier.Vibration), ""),
            new Parameter(ParameterIdentifier.Voltage, nameof(ParameterIdentifier.Voltage),"V"),
            new Parameter(ParameterIdentifier.WaterDetector, nameof(ParameterIdentifier.WaterDetector),""),
            new Parameter(ParameterIdentifier.WaterFlowRate, nameof(ParameterIdentifier.WaterFlowRate),"l/hr"),
            new Parameter(ParameterIdentifier.WaterPressure, nameof(ParameterIdentifier.WaterPressure),"Pa"),
            new Parameter(ParameterIdentifier.WaterVolume, nameof(ParameterIdentifier.WaterVolume), "l"),
            new Parameter(ParameterIdentifier.WindSpeed, nameof(ParameterIdentifier.WindSpeed), "m/s"),
            new Parameter(ParameterIdentifier.IdentifyCommand, nameof(ParameterIdentifier.IdentifyCommand),""),
            new Parameter(ParameterIdentifier.JoinCommand, nameof(ParameterIdentifier.JoinCommand),"")
        };

        /// <inheritdoc/>
        public Parameter this[int index] { get => _parameterList[index]; set => _parameterList[index] = value; }

        /// <inheritdoc/>
        public int Count => _parameterList.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <summary>
        /// Adds the given object to the end of the <see cref="List{T}"/>
        /// </summary>
        /// <param name="item">The <see cref="Parameter"/> instance to add to the <see cref="List{T}"</param>
        /// <exception cref="OpenThingsParameterExistsException">Thrown if a <paramref name="item"/> with same id exists in the <see cref="List{T}"</exception>
        public void Add(Parameter item)
        {
            if (_parameterList.Any(_ => _.Identifier == item.Identifier))
            {
                throw new OpenThingsParameterExistsException($"Parameter with Identifier: [{item.Identifier}] exists");
            }
            
            _parameterList.Add(item);
        }

        /// <inheritdoc/>
        public void Clear()
        {
            _parameterList.Clear();
        }

        /// <inheritdoc/>
        public bool Contains(Parameter item)
        {
            return _parameterList.Contains(item);
        }

        /// <inheritdoc/>
        public void CopyTo(Parameter[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        /// <inheritdoc/>
        public IEnumerator<Parameter> GetEnumerator()
        {
            return _parameterList.GetEnumerator();
        }

        /// <inheritdoc/>
        public int IndexOf(Parameter item)
        {
            return _parameterList.IndexOf(item);
        }

        /// <inheritdoc/>
        public void Insert(int index, Parameter item)
        {
            _parameterList.Insert(index, item);
        }

        /// <inheritdoc/>
        public bool Remove(Parameter item)
        {
            return _parameterList.Remove(item);
        }

        /// <inheritdoc/>
        public void RemoveAt(int index)
        {
            _parameterList.RemoveAt(index);
        }

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Get a <see cref="Parameter"/> instance from the collection by identifier
        /// </summary>
        /// <param name="identifier">The identifier of the <see cref="Parameter"/></param>
        /// <returns>The <see cref="Parameter"/> instance if found in the list</returns>
        public Parameter GetParameter(byte identifier)
        {
            return _parameterList.FirstOrDefault(_ => _.Identifier == identifier);
        }
    }
}
