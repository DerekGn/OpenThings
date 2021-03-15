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

namespace OpenThings
{
    /// <summary>
    /// An OpenThings message header
    /// </summary>
    public class MessageHeader
    {
        /// <summary>
        /// Create an instance of an OpenThings message
        /// </summary>
        /// <param name="manufacturerId">The manufacturer Id</param>
        /// <param name="productId">The product Id</param>
        /// <param name="pip">The pip value</param>
        public MessageHeader(byte manufacturerId, byte productId, ushort pip, uint sensorId)
        {
            if ((manufacturerId & 0x80) == 0x80)
            {
                throw new ArgumentOutOfRangeException(nameof(manufacturerId));
            }

            if(sensorId > 0xFFFFFF)
            {
                throw new ArgumentOutOfRangeException(nameof(sensorId));
            }

            ManufacturerId = manufacturerId;
            ProductId = productId;
            SensorId = sensorId;
            Pip = pip;
        }

        /// <summary>
        /// The length in bytes of the OpenThings message payload
        /// </summary>
        public byte Length { get; }
        /// <summary>
        /// The Manufacturer Id
        /// </summary>
        public byte ManufacturerId { get; }
        /// <summary>
        /// The Product Id
        /// </summary>
        public byte ProductId { get; }
        /// <summary>
        /// The pip
        /// </summary>
        public ushort Pip { get; }
        /// <summary>
        /// The sensor Id
        /// </summary>
        public uint SensorId { get; private set; }

        public override string ToString()
        {
            return 
                $"\tLength:\t\t[0x{Length:X2}]\r\n" +
                $"\tManufacturerID:\t[0x{ManufacturerId:X2}]\r\n" +
                $"\tProductId:\t[0x{ProductId:X2}]\r\n" +
                $"\tPip:\t\t[0x{Pip:X4}]\r\n" +
                $"\tSensorId:\t[0x{SensorId:X8}]";
        }

        internal void SetSensorId(List<byte> sensorId)
        {
            if (sensorId == null)
                throw new ArgumentNullException(nameof(sensorId));

            if (sensorId.Count < 3)
                throw new ArgumentOutOfRangeException(nameof(sensorId));

            SensorId = (uint)(sensorId[0] << 16);
            SensorId += (uint)(sensorId[1] << 8);
            SensorId += sensorId[2];
        }
    }
}