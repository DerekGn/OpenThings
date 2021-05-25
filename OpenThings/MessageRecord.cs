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

namespace OpenThings
{
    /// <summary>
    /// An OpenThings message record
    /// </summary>
    public class MessageRecord
    {
        /// <summary>
        /// Create an instance of a <see cref="MessageRecord"/>
        /// </summary>
        /// <param name="parameter">The <see cref="Parameter"/> instance</param>
        /// <param name="data">The <see cref="BaseMessageRecordData"/> instance for this <see cref="MessageRecord"/></param>
        public MessageRecord(Parameter parameter, BaseMessageRecordData data)
        {
            Parameter = parameter ?? throw new ArgumentNullException(nameof(parameter));
            Data = data ?? throw new ArgumentNullException(nameof(data));
        }
        /// <summary>
        /// The <see cref="Parameter"/> instance
        /// </summary>
        public Parameter Parameter { get; }
        /// <summary>
        /// The <see cref="BaseMessageRecordData"/> instance for this <see cref="MessageRecord"/>
        /// </summary>
        public BaseMessageRecordData Data { get; }

        /// <summary>
        /// Convert the <see cref="MessageRecord"/> to a string representation
        /// </summary>
        /// <returns>A string representation of the <see cref="MessageRecord"/></returns>
        public override string ToString()
        {
            return
                $"\tParameter:[{Parameter}] Data: [{Data}]";
        }
    }
}
