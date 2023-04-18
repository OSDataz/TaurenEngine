/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 17:05:49
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Unity
{
    /// <summary>
    /// 指北针管理器（带防抖稳定器）
    /// </summary>
    public class NorthDevice : DeviceControl<NorthDevice>
    {
        private readonly AngleStabilizer _stabilizer;
        private readonly Compass _compass;
        private Gyroscope _gyro;

        public NorthDevice()
        {
            _stabilizer = new AngleStabilizer(10, true);

            _compass = CompassDevice.Instance.Compass;

            if (GyroDevice.Instance.IsAvailable)
                _gyro = GyroDevice.Instance.Gyro;
        }

        protected override void UpdateEnabled()
        {
            if (Enabled)
            {
                CompassDevice.Instance.SetEnabled(this, true);
                GyroDevice.Instance.SetEnabled(this, true);

                _stabilizer.Clear();
            }
            else
            {
                CompassDevice.Instance.SetEnabled(this, false);
                GyroDevice.Instance.SetEnabled(this, false);
            }
        }

        /// <summary>
        /// 重置稳定器数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="excludeMax"></param>
        public void ResetStabilizer(int count, bool excludeMax)
        {
            _stabilizer.Reset(count, excludeMax);
        }

        /// <summary>
        /// 北方角度计算更新（需每帧调用执行）
        /// </summary>
        /// <returns></returns>
        public float Update()
        {
            if (_gyro == null)
            {
                if (GyroDevice.Instance.IsAvailable)
                    _gyro = GyroDevice.Instance.Gyro;

                return _stabilizer.AddAngle(_compass.trueHeading);
            }

            var cy = _compass.trueHeading;
            var gy = 360.0f - _gyro.attitude.eulerAngles.z;
            if (cy > gy ? cy - gy > 180.0f : gy - cy > 180.0f)
            {
                gy += 360.0f;
            }

            return _stabilizer.AddAngle((cy + gy) * 0.5f);
        }

        public float NorthAngle => _stabilizer.Angle;
    }
}