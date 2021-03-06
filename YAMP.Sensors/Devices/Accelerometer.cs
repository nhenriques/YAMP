﻿namespace YAMP.Sensors.Devices
{
    using System;
    using Windows.Devices.Sensors;
    using Sensor = Windows.Devices.Sensors.Accelerometer;

    /// <summary>
    /// The accelerometer device.
    /// </summary>
    public class Accelerometer : BaseDevice
    {
        static readonly Sensor sensor = GetSensor();

        static Sensor GetSensor()
        {
            try
            {
                return Sensor.GetDefault();
            }
            catch
            {
                return null;
            }
        }

        private event EventHandler<AccelerometerEventArgs> changed;

        /// <summary>
        /// Listens to the changed event.
        /// </summary>
        public event EventHandler<AccelerometerEventArgs> Changed
        {
            add 
            {
                InstallHandler(sensor);
                changed += value;
            }
            remove
            {
                changed -= value;
                UninstallHandler(sensor);
            }
        }

        /// <summary>
        /// Installs the reading handler.
        /// </summary>
        protected override void InstallReadingChangedHandler()
        {
            sensor.ReadingChanged += OnReadingChanged;
        }

        /// <summary>
        /// Uninstalls the reading handler.
        /// </summary>
        protected override void UninstallReadingChangedHandler()
        {
            sensor.ReadingChanged -= OnReadingChanged;
        }

        void OnReadingChanged(Sensor sender, AccelerometerReadingChangedEventArgs args)
        {
            var handler = changed;

            if (handler != null)
            {
                var value = ConvertToVector(args.Reading);
                var e = new AccelerometerEventArgs(value);
                handler.Invoke(this, e);
            }
        }

        /// <summary>
        /// Gets the aceleration value.
        /// </summary>
        public Vector CurrentAcceleration
        {
            get
            {
                if (sensor != null)
                {
                    var acc = sensor.GetCurrentReading();
                    return ConvertToVector(acc);
                }

                return new Vector();
            }
        }

        static Vector ConvertToVector(AccelerometerReading reading)
        {
            return new Vector
            {
                X = reading.AccelerationX,
                Y = reading.AccelerationY,
                Z = reading.AccelerationZ
            };
        }
    }
}
