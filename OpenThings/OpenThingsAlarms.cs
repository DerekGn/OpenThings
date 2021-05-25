namespace OpenThings
{
    /// <summary>
    /// OpenThings Alarm events
    /// </summary>
    public enum OpenThingsAlarms
    {
        /// <summary>
        /// The low battery alarm set
        /// </summary>
        LowBatterySet = 0x42,
        /// <summary>
        /// The low battery alarm cleared
        /// </summary>
        LowBatteryClear = 0x72,
        /// <summary>
        /// The power fail alarm set
        /// </summary>
        PowerFailSet = 0x50,
        /// <summary>
        /// The power fail alarm cleared
        /// </summary>
        PowerFailClear = 0x70,
        /// <summary>
        /// The over temperature alarm set
        /// </summary>
        OverTemperatureSet = 0x54,
        /// <summary>
        /// The over temperature alarm cleared
        /// </summary>
        OverTemperatureClear = 0x74,
        /// <summary>
        /// The tamper alarm is set
        /// </summary>
        TamperSet = 0x5A,
        /// <summary>
        /// The tamper alarm is cleared
        /// </summary>
        TamperClear = 0x7A
    }
}
