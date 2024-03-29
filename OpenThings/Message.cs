﻿/*
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
using System.Collections.Generic;
using System.Text;

namespace OpenThings
{
    /// <summary>
    /// An OpenThings message
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Create an instance of an <see cref="OpenThings"/> message
        /// </summary>
        /// <param name="messageHeader">The <see cref="MessageHeader"/> to add to the <see cref="Message"/></param>
        public Message(MessageHeader messageHeader)
        {
            Header = messageHeader ?? throw new ArgumentNullException(nameof(messageHeader));

            Records = new List<MessageRecord>();
        }

        /// <summary>
        /// The <see cref="MessageHeader"/>
        /// </summary>
        public MessageHeader Header { get; private set; }

        /// <summary>
        /// The <see cref="IList{T}"/> of <see cref="MessageRecord"/>
        /// </summary>
        public IList<MessageRecord> Records { get; private set; }

        /// <summary>
        /// Convert the <see cref="Message"/> to a string representation
        /// </summary>
        /// <returns>A string representation of the message</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("Header->");
            sb.AppendLine(Header.ToString());
            sb.AppendLine("Records->");

            foreach (var r in Records)
            {
                sb.AppendLine(r.ToString());
            }

            return sb.ToString();
        }
    }
}