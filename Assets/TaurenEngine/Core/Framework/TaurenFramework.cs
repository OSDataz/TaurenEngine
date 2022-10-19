/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/3 20:40:00
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace TaurenEngine
{
	/// <summary>
	/// 框架启动器
	/// </summary>
	public static class TaurenFramework
	{
		/// <summary>
		/// 启动服务
		/// </summary>
		/// <param name="customServices">外部扩展的服务</param>
		public static void StartupService(IService[] customServices = null)
		{
			var serviceMgr = ServiceManager.Instance;
			var len = customServices?.Length ?? 0;
			Action<Type, Type> addServiceFunc = (iType, oType) => 
			{
				if (len > 0)
				{
					for (int i = 0; i < len; ++i)
					{
						if (iType.IsInstanceOfType(customServices[i]))
						{
							serviceMgr.Add(iType, customServices[i]);
							return;
						}
					}
				}

				serviceMgr.Add(iType, (IService)Activator.CreateInstance(oType));
			};

			// 注意：以下顺序切勿随意修改
			addServiceFunc(typeof(IPoolService), typeof(PoolService));// 对象池服务器
			PoolHelper.poolService = serviceMgr.Get<IPoolService>();
			addServiceFunc(typeof(ILogService), typeof(LogService));// 日志服务
			addServiceFunc(typeof(ITimerService), typeof(TimerService));// 计时器服务
			TimerHelper.timerService = serviceMgr.Get<ITimerService>();
		}
	}
}