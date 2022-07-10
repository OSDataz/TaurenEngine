/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

namespace TaurenEngine.AR
{
    public class SubState
    {
        private bool _parentState = false;
        private bool _currentState = false;

        public bool Enabled { get; private set; } = false;

        /// <summary>
        /// 设置父对象状态
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns>返回状态是否改变</returns>
        public bool SetParentState(bool enabled)
        {
            if (_parentState == enabled)
                return false;

            _parentState = enabled;

            return CheckState();
        }

        /// <summary>
        /// 设置当前对象状态
        /// </summary>
        /// <param name="enabled"></param>
        /// <returns>返回状态是否改变</returns>
        public bool SetCurrentState(bool enabled)
        {
            if (_currentState == enabled)
                return false;

            _currentState = enabled;

            return CheckState();
        }

        /// <summary>
        /// 检测状态
        /// </summary>
        /// <returns></returns>
        private bool CheckState()
        {
            if (Enabled == (_parentState && _currentState))
                return false;

            Enabled = !Enabled;
            return true;
        }
    }
}