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
    /// OpenThings record type
    /// </summary>
    public enum RecordType
    {
        /// <summary>
        /// x.0 unsigned normal integer
        /// </summary>
        UnsignedX0,
        /// <summary>
        /// x.4 unsigned fixed point integer
        /// </summary>
        UnsignedX4,
        /// <summary>
        /// x.8 unsigned fixed point integer
        /// </summary>
        UnsignedX8,
        /// <summary>
        /// x.4 unsigned fixed point integer
        /// </summary>
        UnsignedX12,
        /// <summary>
        /// x.16 unsigned fixed point integer
        /// </summary>
        UnsignedX16,
        /// <summary>
        /// x.20 unsigned fixed point integer
        /// </summary>
        UnsignedX20,
        /// <summary>
        /// x.24 unsigned fixed point integer
        /// </summary>
        UnsignedX24,
        /// <summary>
        /// A set of characters record type
        /// </summary>
        Chars,
        /// <summary>
        /// x.0 signed normal integer
        /// </summary>
        SignedX0,
        /// <summary>
        /// x.8 signed fixed point integer
        /// </summary>
        SignedX8,
        /// <summary>
        /// x.12 signed fixed point integer
        /// </summary>
        SignedX12,
        /// <summary>
        /// x.16 signed fixed point integer
        /// </summary>
        SignedX16,
        /// <summary>
        /// x.24 signed fixed point integer
        /// </summary>
        SignedX24,
        /// <summary>
        /// Enumeration
        /// </summary>
        Enum,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved1,
        /// <summary>
        /// Reserved
        /// </summary>
        Reserved2,
        /// <summary>
        /// A float record type
        /// </summary>
        Float
    }
}
