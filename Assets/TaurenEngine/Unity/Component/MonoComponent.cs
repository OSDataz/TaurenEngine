/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/4/16 10:47:27
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.Unity
{
    public abstract class MonoComponent : MonoBehaviour
    {
        /// <summary>
        /// 脚本是否已销毁
        /// </summary>
        public bool IsDestroyed { get; private set; }

        /// <summary>
        /// 脚本销毁
        /// </summary>
        protected virtual void OnDestroy()
		{
            IsDestroyed = true;
        }

        /// <summary>
        /// 销毁对象
        /// <para>子类销毁逻辑写在 <c>OnDestroy</c> 中</para>
        /// </summary>
        /// <param name="immediate">是否立即销毁</param>
        public void Destroy(bool immediate = false)
        {
            if (IsDestroyed) return;

            IsDestroyed = true;

            if (immediate) DestroyImmediate(this);
            else GameObject.Destroy(this);
        }
    }
}