/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.Core;
using UnityEngine;

namespace TaurenEngine.AR
{
    /// <summary>
    /// 图像识别子系统
    /// </summary>
    public class ARImage : SystemBase<ISystemImage>
    {
        internal override ISystemImage System
        {
            get
            {
                if (system == null)
                    system = EngineBase?.SystemImage;

                return system;
            }
        }

        private List<ARImageData> _imageDatas = new List<ARImageData>();

        public void AddTrackImages(List<ARImageData> list)
        {
	        list?.ForEach(item => AddTrackImage(item));
        }

        public void AddTrackImage(ARImageData data)
        {
	        if (data == null)
		        return;

	        if (_imageDatas.FindIndex(item => item.Name == data.Name || item.Url == data.Url) != -1)
		        return;

            _imageDatas.Add(data);

            System?.AddTrackImage(data);
        }

        public void RemoveTrackImages(List<string> list)
        {
            list?.ForEach(item => RemoveTrackImage(item));
        }

        public void RemoveTrackImage(string name)
        {
	        var index = _imageDatas.FindIndex(item => item.Name == name);
	        if (index == -1)
		        return;

            _imageDatas.RemoveAt(index);

            System?.RemoveTrackImage(name);
        }

        public void RemoveAllImages()
        {
            System?.ClearAllImages();

	        _imageDatas.Clear();
        }

        internal void TrackedImage(string name, bool isTracked, Transform transform)
        {
	        var data = _imageDatas.Find(item => item.Name == name);
	        if (data == null)
		        return;

	        data.IsTracked = isTracked;

            // 图像识别改变后派发事件。参数：图像名，图像识别状态，图像位置
            EventCenter.Dispatcher.Send(ARImageEvent.Tracked, new ARImageEvent()
            {
                Name = name,
                IsTracked = isTracked,
                Transform = transform
            });
        }

        internal override void Clear()
        {
	        _imageDatas.Clear();

            base.Clear();
        }
    }

    public class ARImageData
    {
        public string Url { get; set; }

        public string Name { get; set; }

        internal bool IsTracked { get; set; }

        public virtual void Copy(ARImageData data)
        {
	        Url = data.Url;
	        Name = data.Name;
	        IsTracked = data.IsTracked;
        }
    }

    public class ARImageEvent : IEventData
    {
	    public const string Tracked = "Tracked"; 

	    public string Name { get; internal set; }
	    public bool IsTracked { get; internal set; }
	    public Transform Transform { get; internal set; }
    }

    public interface ISystemImage : ISystemBase
    {
        /// <summary>
        /// 添加图片到识别库
        /// </summary>
        /// <param name="data"></param>
	    void AddTrackImage(ARImageData data);
        /// <summary>
        /// 从识别库删除图片
        /// </summary>
        /// <param name="name"></param>
	    void RemoveTrackImage(string name);
        /// <summary>
        /// 清楚识别库所有图片
        /// </summary>
	    void ClearAllImages();
    }
}