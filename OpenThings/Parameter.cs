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

using System;

namespace OpenThings
{
    /// <summary>
    /// An OpenThings parameter
    /// </summary>
    public class Parameter : IComparable<Parameter>
    {
        /// <summary>
        /// Initialize a new instance of a <see cref="Parameter"/>
        /// </summary>
        /// <param name="identifier">The identifier for this parameter</param>
        public Parameter(byte identifier) : this(identifier, string.Empty, string.Empty)
        {
        }

        /// <summary>
        /// Initialize a new instance of a <see cref="Parameter"/>
        /// </summary>
        /// <param name="identifier">The identifier for this parameter</param>
        /// <param name="label">The <see cref="Parameter"/> label</param>
        /// <param name="units">The <see cref="Parameter"/> units</param>
        public Parameter(byte identifier, string label, string units)
        {
            Identifier = identifier;
            Label = label ?? throw new ArgumentNullException(nameof(label));
            Units = units ?? throw new ArgumentNullException(nameof(units));
        }

        /// <summary>
        /// The <see cref="Parameter"/> identifier
        /// </summary>
        public byte Identifier { get; }

        /// <summary>
        /// The <see cref="Parameter"/>
        /// </summary>
        public string Label { get; }

        /// <summary>
        /// The unit for this <see cref="Parameter"/>
        /// </summary>
        public string Units { get; }
        
        /// <inheritdoc/>
        public int CompareTo(Parameter other)
        {
            if (other == null)
            {
                return 1;
            }
            else
            {
                return Identifier.CompareTo(other.Identifier);
            }
        }

        /// <summary>
        /// Convert the <see cref="Parameter"/> to a string representation
        /// </summary>
        /// <returns>A string representation of the <see cref="Parameter"/></returns>
        public override string ToString()
        {
            return $"Identifier: [0x{Identifier:X2}] Label: [{Label}] Units: [{Units}]";
        }
    }
}