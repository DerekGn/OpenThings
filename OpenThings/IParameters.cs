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

namespace OpenThings
{
    /// <summary>
    /// Defines an interface for an openthings parameter set
    /// </summary>
    public interface IParameters
    {
        /// <summary>
        /// Adds a <see cref="Parameter"/> instance to the <see cref="IParameters"/>
        /// </summary>
        /// <param name="item">The <see cref="Parameter"/> instance to add to the <see cref="IParameters"/> instance</param>
        /// <exception cref="OpenThingsParameterExistsException">Thrown if a <paramref name="item"/> with same identifier exists</exception>
        void Add(Parameter item);

        /// <summary>
        /// Get a <see cref="Parameter"/> instance from the collection by identifier
        /// </summary>
        /// <param name="identifier">The identifier of the <see cref="Parameter"/></param>
        /// <returns>The <see cref="Parameter"/> instance if found in the list</returns>
        Parameter GetParameter(byte identifier);
    }
}