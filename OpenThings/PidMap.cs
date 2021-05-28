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

namespace OpenThings
{
    /// <summary>
    /// A pip to manufacturer map 
    /// </summary>
    public class PidMap
    {
        /// <summary>
        /// The default constructor
        /// </summary>
        public PidMap()
        {
        }

        /// <summary>
        /// Create an instance of a <see cref="PidMap"/>
        /// </summary>
        /// <param name="manufacturerId">The manufacturer Id</param>
        /// <param name="pid">The PID</param>
        public PidMap(byte manufacturerId, byte pid)
        {
            ManufacturerId = manufacturerId;
            Pid = pid;
        }

        /// <summary>
        /// The manufacturer Id
        /// </summary>
        public byte ManufacturerId { get; set; }
        
        /// <summary>
        /// The pid
        /// </summary>
        public byte Pid { get; set; }
    }
}
