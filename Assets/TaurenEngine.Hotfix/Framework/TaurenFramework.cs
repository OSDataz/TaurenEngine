﻿/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/10/3 20:40:00
 *└────────────────────────┘*/

using System;

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

			// 注意：以下顺序切勿随意修改 ======================
			addServiceFunc(typeof(IPoolService), typeof(PoolService));// 对象池服务
			addServiceFunc(typeof(ILogService), typeof(LogService));// 日志服务
			addServiceFunc(typeof(ITimerService), typeof(TimerService));// 计时器服务

			//// 资源服务
			//addServiceFunc(typeof(IResourceService), typeof(ResourceService));
			//ResourceHelper.resourceService = serviceMgr.Get<IResourceService>();

			addServiceFunc(typeof(IAudioService), typeof(AudioService));// 音频服务
			addServiceFunc(typeof(INetworkService), typeof(NetworkService));// 网络服务
		}
	}
}