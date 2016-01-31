﻿namespace YAMP.Exceptions
{
    using System;

    /// <summary>
    /// The numeric overflow.
    /// </summary>
    public class YAMPNumericOverflowException : YAMPRuntimeException
    {
        public YAMPNumericOverflowException(String function)
            : base("Numeric overflow in the {0} function.", function)
        {
        }
    }
}
