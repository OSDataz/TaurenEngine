/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
	/// <summary>
	/// AR引擎生命周期代理
	/// </summary>
	public class AREngineProxy
	{
		private int _startupIndex;

		/// <summary>
		/// 启动引擎
		/// </summary>
		internal void Startup()
		{
			_startupIndex = 0;

			StartupLoop();
		}

		/// <summary>
		/// 关闭引擎
		/// </summary>
		internal void Close()
		{
			EngineBase?.Close();

			Destroy();
		}

		internal void StartupLoop()
		{
			AREngine.Instance.ClearSystem();
			EngineBase?.Close();

			Status = RunningStatus.Unsupported;

			var list = AREngine.Instance.startupSort;
			if (_startupIndex >= list.Count)
			{
				StartupFail();

				TDebug.Error("AR引擎启动失败 StartupSort End");
				return;
			}

			EngineBase = GetEngine(list[_startupIndex++]);
			if (EngineBase == null)
			{
				StartupFail();

				TDebug.Error("AR引擎启动失败 AREngineType Error");
				return;
			}

			if (EngineBase.IsAvailable)
			{
				Status = RunningStatus.Initializing;
				EngineBase.Startup();

				if (EngineBase.IsAvailable)
				{
					TDebug.Log($"启动AR引擎{EngineBase.EngineType}成功");
					StartupSuccess();
				}
				else
				{
					TDebug.Log($"启动AR引擎{EngineBase.EngineType}初始化失败");
					StartupLoop();
				}
			}
			else
			{
				TDebug.Log($"启动AR引擎{EngineBase.EngineType}不支持");
				StartupLoop();
			}
		}

		private void StartupSuccess()
		{
			Status = RunningStatus.Ready;

			if (AREngineSetting.Instance.useLocation)
				ARLocation.Instance.TryBind();
		}

		private void StartupFail()
		{
			EngineBase = null;

			AREngine.Instance.enabled = false;
		}

		internal void Destroy()
		{
			if (EngineBase != null)
			{
				EngineBase.OnDestroy();
				EngineBase = null;
			}

			Status = RunningStatus.None;
		}

		private AREngineBase GetEngine(AREngineType type)
		{
			if (type == AREngineType.ARFoundation)
				return new ARFoundationEx();

			if (type == AREngineType.EasyAR)
				return new EasyAREx();

			if (type == AREngineType.BaseAR)
				return new BaseAR();

			return null;
		}

		/// <summary>
		/// 重定位AR世界方位
		/// </summary>
		public void ResetDirection()
		{
			EngineBase?.ResetDirection();
		}

		/// <summary>
		/// 运行状态
		/// </summary>
		public RunningStatus Status { get; private set; }
		/// <summary>
		/// 运行的引擎
		/// </summary>
		internal AREngineBase EngineBase { get; private set; }
		/// <summary>
		/// AR引擎类型
		/// </summary>
		public AREngineType EngineType => EngineBase?.EngineType ?? AREngineType.None;
		/// <summary>
		/// AR摄像机
		/// </summary>
		public Camera Camera => EngineBase?.Camera ?? Camera.main;
		/// <summary>
		/// AR摄像机
		/// </summary>
		public Transform CameraTransform => Camera.transform;
	}

	/// <summary>
	/// 引擎运行状态
	/// </summary>
	public enum RunningStatus
	{
		/// <summary>
		/// 未启动
		/// </summary>
		None,
		/// <summary>
		/// 不支持，启动失败
		/// </summary>
		Unsupported,
		/// <summary>
		/// 启动中
		/// </summary>
		Initializing,
		/// <summary>
		/// 支持并启动成功
		/// </summary>
		Ready
	}
}