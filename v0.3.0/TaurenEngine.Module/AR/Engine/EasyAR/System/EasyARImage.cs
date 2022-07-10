/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System.Collections.Generic;
using System.IO;
#if EasyAR
using easyar;
#endif
using UnityEngine;

namespace TaurenEngine.AR
{
	internal class EasyARImage : EasyARSystemBase, ISystemImage
	{
#if EasyAR
		private ARImage _arImage;
		//private VideoCameraDevice _cameraDevice;
		private ImageTrackerFrameFilter _imageTracker;
#endif

		public EasyARImage(EasyAREx easyArEx) : base(easyArEx)
		{
			IsAvailable = true;
		}

		protected override void Init()
		{
#if EasyAR
			_arImage = AREngine.Instance.ArImage;

			//var oVideoCameraDevice = Unityx.GetObjectInstance("VideoCameraDevice");
			//oVideoCameraDevice.transform.SetParent(_arEngine.EasyAROrigin.transform);
			//_cameraDevice = Unityx.GetComponentInstance<VideoCameraDevice>(oVideoCameraDevice);

			var oImageTracker = UnityHelper.GetNewGameObject("ImageTracker");
			oImageTracker.transform.SetParent(_arEngine.EasyAROrigin.transform);
			_imageTracker = oImageTracker.GetNewComponent<ImageTrackerFrameFilter>();

			if (UseCloudRecognizer)
				InitCloudRecognizer();

			InitOfflineCache();
#endif

			base.Init();
		}

#if EasyAR
		public override void Enable()
		{
			base.Enable();

			//if (_cameraDevice != null)
			//	_cameraDevice.enabled = true;

			if (_imageTracker != null)
				_imageTracker.enabled = true;

			SetCloudRecognizerEnabled(true);
		}

		public override void Disable()
		{
			base.Disable();

			//if (_cameraDevice != null)
			//	_cameraDevice.enabled = false;

			if (_imageTracker != null)
				_imageTracker.enabled = false;

			SetCloudRecognizerEnabled(false);
		}

		public override void Destroy()
		{
			if (IsDestroyed)
				return;

			//if (_cameraDevice?.gameObject != null)
			//{
			//	GameObject.Destroy(_cameraDevice.gameObject);
			//	_cameraDevice = null;
			//}

			if (_imageTracker?.gameObject != null)
			{
				GameObject.Destroy(_imageTracker.gameObject);
				_imageTracker = null;
			}

			DestroyCloudRecognizer();

			ClearAllImages();

			base.Destroy();
		}
#endif

		#region 图像识别对象管理
#if EasyAR
		private readonly List<string> _targetUIds = new List<string>();
		private readonly List<ImageTargetController> _controllers = new List<ImageTargetController>();

		private bool IsCreateImageTarget(string uid)
		{
			return _targetUIds.Contains(uid);
		}

		private void AddImageTargetController(string uid, ImageTargetController controller)
		{
			if (IsCreateImageTarget(uid))
				return;

			_targetUIds.Add(uid);
			_controllers.Add(controller);
		}

		private void RemoveImageTargetController(string uid, ImageTargetController controller)
		{
			if (!IsCreateImageTarget(uid))
				return;

			GameObject.Destroy(controller.gameObject);

			_targetUIds.Remove(uid);
			_controllers.Remove(controller);
		}

		private void RemoveImageTargetController(string name)
		{
			var index = _controllers.FindIndex(item => item.name == name);
			if (index == -1)
				return;

			GameObject.Destroy(_controllers[index].gameObject);

			_targetUIds.RemoveAt(index);
			_controllers.RemoveAt(index);
		}

		private ImageTargetController CreateImageTarget(ImageTarget target)
		{
			var controller = CreateImageTargetController(target.uid());
			controller.SourceType = ImageTargetController.DataSource.Target;
			controller.TargetSource = target;

			return controller;
		}

		private ImageTargetController CreateDataFile(string path, PathType pathType)
		{
			return CreateDataFile(path, Path.GetFileNameWithoutExtension(path), pathType);
		}

		private ImageTargetController CreateDataFile(string path, string name, PathType pathType)
		{
			var controller = CreateImageTargetController(name);
			controller.SourceType = ImageTargetController.DataSource.TargetDataFile;
			controller.TargetDataFileSource.PathType = pathType;
			controller.TargetDataFileSource.Path = path;

			return controller;
		}

		private ImageTargetController CreateImageFile(string path, float scale, PathType pathType)
		{
			return CreateImageFile(path, Path.GetFileNameWithoutExtension(path), scale, pathType);
		}

		private ImageTargetController CreateImageFile(string path, string name, float scale, PathType pathType)
		{
			var controller = CreateImageTargetController(name);
			controller.SourceType = ImageTargetController.DataSource.ImageFile;
			controller.ImageFileSource.PathType = pathType;
			controller.ImageFileSource.Path = path;
			controller.ImageFileSource.Name = name;
			controller.ImageFileSource.Scale = scale;

			return controller;
		}

		private ImageTargetController CreateImageTargetController(string targetName)
		{
			GameObject go = new GameObject(targetName);
			var controller = go.AddComponent<ImageTargetController>();
			controller.Tracker = _imageTracker;

			controller.TargetLoad += (target, status) =>
			{
				TDebug.Log($"TargetLoad: {targetName}");

				if (!status)
				{
					TDebug.Error($"EasyAR Target Load Fail: {target.name()} {target.uid()}");
					return;
				}

				AddImageTargetController(target.uid(), controller);
			};

			// controller.Tracker == null 触发
			controller.TargetUnload += (target, status) =>
			{
				TDebug.Log($"TargetUnload: {targetName}");

				if (!status)
				{
					TDebug.Error($"EasyAR Target Unload Fail: {target.name()} {target.uid()}");
					return;
				}

				RemoveImageTargetController(target.uid(), controller);
			};

			controller.TargetFound += () =>
			{
				TDebug.Log($"TargetFound: {targetName}");

				_arImage.TrackedImage(controller.name, true, controller.transform);
			};

			controller.TargetLost += () =>
			{
				TDebug.Log($"TargetLost: {targetName}");

				_arImage.TrackedImage(controller.name, false, controller.transform);
			};

			return controller;
		}
#endif

		public void AddTrackImage(ARImageData data)
		{
#if EasyAR
			CreateImageFile(data.Url, data.Name, 1, PathType.Absolute);
#endif
		}

		public void RemoveTrackImage(string name)
		{
#if EasyAR
			RemoveImageTargetController(name);
#endif
		}

		public void ClearAllImages()
		{
#if EasyAR
			foreach (var controller in _controllers)
			{
				GameObject.Destroy(controller.gameObject);
			}

			_targetUIds.Clear();
			_controllers.Clear();
#endif
		}
		#endregion

#if EasyAR
#region 云识别检测
		private bool _useCloudRecognizer = false;
		/// <summary>
		/// 是否使用云识别检测
		/// </summary>
		public bool UseCloudRecognizer
		{
			get => _useCloudRecognizer;
			set
			{
				if (_useCloudRecognizer == value)
					return;

				_useCloudRecognizer = value;

				if (_useCloudRecognizer)
				{
					InitCloudRecognizer();
				}
				else
				{
					DestroyCloudRecognizer();
				}
			}
		}

		private CloudRecognizerFrameFilter _cloudRecognizer;

		private void InitCloudRecognizer()
		{
			TDebug.Log("EasyAR初始化云识别检测");

			var oCloudRecognizer = UnityHelper.GetNewGameObject("CloudRecognizer");
			oCloudRecognizer.transform.SetParent(_arEngine.EasyAROrigin.transform);
			_cloudRecognizer = oCloudRecognizer.GetNewComponent<CloudRecognizerFrameFilter>();
		}

		private void DestroyCloudRecognizer()
		{
			if (_cloudRecognizer?.gameObject != null)
			{
				GameObject.Destroy(_cloudRecognizer.gameObject);
				_cloudRecognizer = null;
			}
		}

		private void SetCloudRecognizerEnabled(bool enable)
		{
			if (_cloudRecognizer != null)
				_cloudRecognizer.enabled = enable;
		}

		/// <summary>
		/// 云识别检测
		/// </summary>
		public void Resolve()
		{
			_cloudRecognizer.Resolve(ResolveInputFrame, ResolveResult);
		}

		private void ResolveInputFrame(InputFrame frame)
		{

		}

		private void ResolveResult(CloudRecognizationResult result)
		{
			var status = result.getStatus();
			if (status == CloudRecognizationStatus.FoundTarget)// 识别到target
			{
				var targetOptional = result.getTarget();
				if (targetOptional.OnSome)
				{
					using (var target = targetOptional.Value)
					{
						if (IsCreateImageTarget(target.uid()))
							return;

						var controller = CreateImageTarget(target.Clone());

						AddImageTargetController(target.uid(), controller);
					}
				}
			}
			else if (status == CloudRecognizationStatus.TargetNotFound)// 未识别到target
			{
			}
			else if (status == CloudRecognizationStatus.UnknownError)// 未知错误
			{
				TDebug.Error(result.getUnknownErrorMessage().Value);
			}
			else if (status == CloudRecognizationStatus.ReachedAccessLimit)// 到达访问额度
			{
				TDebug.Error("EasyAR云识别到达访问额度");
			}
			else if (status == CloudRecognizationStatus.RequestIntervalTooLow)// 请求间隔过低
			{
			}
		}
#endregion

#region 离线缓存
		private bool _useOfflineCache = false;
		/// <summary>
		/// 离线缓存路径
		/// </summary>
		private string _offlineCachePath;

		/// <summary>
		/// 是否使用离线缓存
		/// </summary>
		public bool UseOfflineCache
		{
			get => _useOfflineCache;
			set
			{
				if (_useOfflineCache == value)
					return;

				_useOfflineCache = value;
				if (_useOfflineCache)
				{
					InitOfflineCache();
				}
			}
		}

		private void InitOfflineCache()
		{
			if (UseOfflineCache)
			{
				if (string.IsNullOrEmpty(_offlineCachePath))
				{
					_offlineCachePath = Application.persistentDataPath + "/CloudRecognizerCache";

					if (!Directory.Exists(_offlineCachePath))
					{
						Directory.CreateDirectory(_offlineCachePath);
					}

					var files = Directory.GetFiles(_offlineCachePath, "*.etd");
					foreach (var file in files)
					{
						var controller = CreateDataFile(file, PathType.Absolute);
					}
				}
			}
		}

		public void ClearOfflineCache()
		{
			if (Directory.Exists(_offlineCachePath))
			{
				var files = Directory.GetFiles(_offlineCachePath, "*.etd");
				foreach (var file in files)
				{
					File.Delete(file);
				}
			}

			ClearAllImages();
		}
#endregion
#endif
	}
}