/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.8.0
 *│　Time    ：2022/9/20 11:02:36
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;

namespace Tauren.Core.Runtime
{
	/// <summary>
	/// 服务管理器
	/// 
	/// 和单例管理器区别有两点：
	/// 1.支持自定义服务；
	/// 2.限制不能获取未添加的服务；
	/// </summary>
	public class ServiceManager : InstanceBase<ServiceManager>
	{
		/// <summary>
		/// 服务字典
		/// </summary>
		private readonly Dictionary<Type, IService> _serviceMap = new Dictionary<Type, IService>();

		/// <summary>
		/// 注册添加服务对象
		/// </summary>
		/// <typeparam name="T">使用限制只能为接口类型</typeparam>
		/// <param name="service"></param>
		public void Add<T>(T service) where T : IService
		{
			_serviceMap[typeof(T)] = service;
		}

		public bool Add(Type type, IService service)
		{
			if (type is not IService)
				return false;

			_serviceMap[type] = service;
			return true;
		}

		/// <summary>
		/// 获取服务对象
		/// </summary>
		/// <typeparam name="T">使用限制只能为接口类型</typeparam>
		/// <returns></returns>
		public T Get<T>() where T : IService
		{
			if (_serviceMap.TryGetValue(typeof(T), out var service) && service is T tService)
				return tService;

			return default(T);
		}
	}
}