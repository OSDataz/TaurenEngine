/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/8/1 21:48:03
 *└────────────────────────┘*/

using TaurenEngine.Runtime.Framework;
using TaurenEngine.Runtime.Unity;

namespace TaurenEngine.Runtime.Launch
{
	/// <summary>
	/// 启动运行组件
	/// </summary>
	public class LaunchComponent : MonoComponent
	{
		protected void Awake()
		{
			gameObject.GetOrAddComponent<TimerComponent>();// 添加Timer组件
		}

		protected void Start()
		{

		}
	}
}