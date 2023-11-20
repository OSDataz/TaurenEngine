/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/8/28 17:06:26
 *└────────────────────────┘*/

using TaurenEngine.Core;

namespace TaurenEngine.ModHardware
{
    /// <summary>
    /// 方向角度稳定器
    /// </summary>
    public class AngleStabilizer
    {
        private float[] _angles;
        private int _count;
        private int _index;
        private bool _isLengthMax;

        private bool _excludeMax;

        private int _tempCount;
        private int _tempCalcCount;
        private bool _tempExclude;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count">缓存数（最小数量为2）</param>
        /// <param name="excludeMax">剔除最大值和最小值</param>
        public AngleStabilizer(int count, bool excludeMax)
        {
            Reset(count, excludeMax);
        }

        /// <summary>
        /// 重置数据
        /// </summary>
        /// <param name="count"></param>
        /// <param name="excludeMax"></param>
        public void Reset(int count, bool excludeMax)
        {
            if (count < 2) count = 2;

            _count = count;
            _excludeMax = excludeMax && count > 2;

            Clear();
        }

        public void Clear()
        {
            _angles = new float[_count];

            _index = 0;
            _isLengthMax = false;

            _tempCount = 0;
            _tempCalcCount = 0;
            _tempExclude = false;
        }

        /// <summary>
        /// 添加角度
        /// </summary>
        /// <param name="angle">默认角度已标准格式化</param>
        /// <returns></returns>
        public float AddAngle(float angle)
        {
            _angles[_index++] = MathUtils.NormalizeDegrees(angle);

            if (_isLengthMax)
            {
                if (_index >= _count)
                {
                    _index = 0;
                }
            }
            else
            {
                if (_index >= _count)
                {
                    _index = 0;
                    _isLengthMax = true;

                    _tempCount = _count;
                    _tempExclude = _excludeMax;
                }
                else
                {
                    _tempCount = _index;
                    _tempExclude = _excludeMax && _index > 2;
                }

                _tempCalcCount = _tempExclude ? _tempCount - 2 : _tempCount;
            }

            int firstCount = 0;
            int fourthCount = 0;
            float total = 0;

            if (_tempExclude)
            {
                angle = _angles[0];
                float max = angle;
                float min = angle;

                for (int i = 0; i < _tempCount; ++i)
                {
                    angle = _angles[i];
                    if (angle < 45.0f) firstCount += 1;
                    else if (angle > 315.0f) fourthCount += 1;

                    if (angle > max) max = angle;
                    else if (angle < min) min = angle;

                    total += angle;
                }

                if (max < 45.0f) firstCount -= 1;
                else if (max > 315.0f) fourthCount -= 1;

                if (min < 45.0f) firstCount -= 1;
                else if (min > 315.0f) fourthCount -= 1;

                total = total - max - min;
            }
            else
            {
                for (int i = 0; i < _tempCount; ++i)
                {
                    angle = _angles[i];
                    if (angle < 45.0f) firstCount += 1;
                    else if (angle > 315.0f) fourthCount += 1;

                    total += angle;
                }
            }

            if (firstCount > 0 && fourthCount > 0)
            {
                total += (firstCount * 360.0f);
            }

            Angle = MathUtils.NormalizeDegrees(total / _tempCalcCount);

            return Angle;
        }

        public float Angle { get; private set; }
    }
}