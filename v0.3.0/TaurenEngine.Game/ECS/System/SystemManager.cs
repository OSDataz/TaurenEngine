/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 9:21:36
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;

namespace TaurenEngine.ECS
{
	/// <summary>
	/// 系统管理，考虑不适用SingletonBehaviour
	/// </summary>
	public class SystemManager : SingletonBehaviour<SystemManager>
	{
		private readonly List<SystemBase> _systems = new List<SystemBase>();

		public void AddSystem(SystemBase system)
		{
			_systems.Add(system);
		}

		void Update()
		{
			_systems.ForEach(system => system.Update());
		}
	}
}