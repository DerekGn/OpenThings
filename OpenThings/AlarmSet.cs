﻿/*
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
    public enum AlarmSet
    {
        /// <summary>
        /// The device’s battery is failing and must be replaced soon
        /// </summary>
        LowBattery = 0x42,
        /// <summary>
        /// A mains-powered device is currently running off its battery backup
        /// </summary>
        PowerFail = 0x50,
        /// <summary>
        /// The device is running unexpectedly hot and needs attention
        /// </summary>
        OverTemperature = 0x54,
        /// <summary>
        /// The device’s anti-tamper mechanism has been triggered
        /// </summary>
        Tamper = 0x5A
    }
}