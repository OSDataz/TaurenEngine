/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;
#if ARFoundation
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
#endif

namespace TaurenEngine.AR
{
    /// <summary>
    /// 图像识别并追踪
    /// </summary>
    internal class ARFoundationImage : ARFoundationSystemBase, ISystemImage
    {
#if ARFoundation
	    private ARImage _arImage;
        private ARTrackedImageManager _arTrackedImageManager;
#endif

	    public ARFoundationImage(ARFoundationEx arFoundationEx) : base(arFoundationEx)
        {
            IsAvailable = true;
        }

        protected override void Init()
        {
#if ARFoundation
	        _arImage = AREngine.Instance.ArImage;

            GameObject oARSessionOrigin = _arEngine.arSessionOrigin.gameObject;
            _arTrackedImageManager = oARSessionOrigin.GetNewComponent<ARTrackedImageManager>();
            
			IsAvailable = (_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsImageValidation ?? false)
                          && (_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsMovingImages ?? false)
                          && (_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsMutableLibrary ?? false);

            if (IsAvailable)
            {
	            _arTrackedImageManager.enabled = false;

				var setting = AREngineSetting.Instance.arFoundationSetting;
	            _arTrackedImageManager.requestedMaxNumberOfMovingImages = setting.trackImageMaxNum;// 可同时识别图片数量

				if (setting.serializedLibrary)
		            _arTrackedImageManager.referenceLibrary = setting.serializedLibrary;
	            else
		            _arTrackedImageManager.referenceLibrary = _arTrackedImageManager.CreateRuntimeLibrary();

	            _arTrackedImageManager.enabled = IsEnable;
            }

			TDebug.Log($"ARFoundation图像识别 " +
			           $"ImageValidation：{_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsImageValidation} " +
			           $"MovingImages：{_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsMovingImages} " +
			           $"MutableLibrary：{_arTrackedImageManager.subsystem?.subsystemDescriptor?.supportsMutableLibrary}");
#endif

			base.Init();
		}

#if ARFoundation
		public override void Enable()
        {
            base.Enable();

            if (_arTrackedImageManager != null)
            {
                _arTrackedImageManager.enabled = true;
                _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
                _arTrackedImageManager.SetTrackablesActive(true);
            }

            ExecuteWaitList();
		}

		public override void Disable()
        {
			base.Disable();

            if (_arTrackedImageManager != null)
            {
                _arTrackedImageManager.enabled = false;
                _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
                _arTrackedImageManager.SetTrackablesActive(false);
            }
        }

        public override void Destroy()
        {
            if (IsDestroyed)
                return;

	        ClearWaitList();

            _arTrackedImageManager = null;
			_arImage = null;

            base.Destroy();
        }

	    private void AddImageDataAux(ARFoundationImageData imageData)
	    {
		    if (_arTrackedImageManager.referenceLibrary is MutableRuntimeReferenceImageLibrary mutableLibrary)
		    {
			    try
			    {
				    _arTrackedImageManager.enabled = false;

				    if (imageData.LoadImage())
                    {
						// 在添加图像时耗时较长
	                    imageData.JobHandle = mutableLibrary
		                    .ScheduleAddImageWithValidationJob(imageData.Texture, imageData.Name, imageData.Width)
		                    .jobHandle;

	                    //mutableLibrary.ScheduleAddImageJob(imageData.Texture, imageData.Name, imageData.Width);
					}
					else
                    {
	                    TDebug.Error($"图片资源获取失败：{imageData.Name} {imageData.Url}");
                    }

				    _arTrackedImageManager.enabled = IsEnable;

					TDebug.Log($"ARTrackedImageManager: {IsEnable}");
			    }
			    catch (InvalidOperationException e)
			    {
				    TDebug.Error($"ScheduleAddImageJob threw exception: {e.Message}");
			    }
		    }
		    else
		    {
			    TDebug.Error($"The reference image library is not mutable.");
		    }
	    }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {
			TDebug.Log($"OnTrackedImagesChanged {eventArgs.added.Count} {eventArgs.removed.Count} {eventArgs.updated.Count}");

	        foreach (var trackedImage in eventArgs.added)
	        {
		        _arImage.TrackedImage(trackedImage.name, true, trackedImage.transform);
	        }

	        foreach (var trackedImage in eventArgs.removed)
	        {
                _arImage.TrackedImage(trackedImage.name, false, trackedImage.transform);
	        }

			foreach (var trackedImage in eventArgs.updated)
			{
				_arImage.TrackedImage(trackedImage.name, trackedImage.trackingState == TrackingState.Tracking, trackedImage.transform);
			}
		}

		public void UpdateReferenceLibrary(IReferenceImageLibrary library)
		{
			if (!_arTrackedImageManager)
				return;

			_arTrackedImageManager.enabled = false;
			library ??= _arTrackedImageManager.CreateRuntimeLibrary();
			_arTrackedImageManager.referenceLibrary = library;
			_arTrackedImageManager.enabled = IsEnable;
		}
#endif

		#region 图片追踪
#if ARFoundation
		public class ARFoundationImageData : ARImageData
        {
	        // The source texture for the image. Must be marked as readable.
	        public Texture2D Texture { get; set; }

	        // The width, in meters, of the image in the real world.
	        public float Width { get; set; } = 1;

	        public JobHandle JobHandle { get; set; }

	        public bool LoadImage()
	        {
				TDebug.Log("LoadImage 1");

		        if (Texture)
			        return true;

		        TDebug.Log("LoadImage 2");

		        Texture = LoadManager.Instance.LoadImage(Url, LoadImageType.Resources, new LoadSetting(), null);
				return Texture;
	        }
        }
#endif

		public void AddTrackImage(ARImageData data)
        {
#if ARFoundation
			ARFoundationImageData imageData;
	        if (data is ARFoundationImageData)
	        {
		        imageData = data as ARFoundationImageData;
            }
            else
            {
	            imageData = new ARFoundationImageData();
                imageData.Copy(data);
            }

	        if (_arTrackedImageManager != null)
	        {
		        AddImageDataAux(imageData);
            }
	        else
	        {
		        AddToWaitList(imageData);
	        }
#endif
		}

		public void RemoveTrackImage(string name)
        {
#if ARFoundation
	        if (_arTrackedImageManager == null)
		        return;

	        foreach (var arImage in _arTrackedImageManager.trackables)
	        {
		        if (arImage.name == name)
		        {

				}
	        }
#endif
		}

		public void ClearAllImages()
        {
#if ARFoundation
	        if (_arTrackedImageManager == null)
		        return;

#endif
		}
		#endregion

		#region 未开启等待列表
#if ARFoundation
		private List<ARFoundationImageData> _waitList;

        private void AddToWaitList(ARFoundationImageData data)
        {
	        if (_waitList == null)
		        _waitList = new List<ARFoundationImageData>();

	        _waitList.Add(data);
        }

        private void ExecuteWaitList()
        {
	        if (_waitList == null)
		        return;

	        foreach (var imageData in _waitList)
	        {
		        AddImageDataAux(imageData);
	        }

	        ClearWaitList();
        }

        private void ClearWaitList()
        {
	        if (_waitList != null)
	        {
		        _waitList.Clear();
		        _waitList = null;
	        }
        }
#endif
		#endregion
	}
}