/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/10/8 20:04:54
 *└────────────────────────┘*/

using TaurenEngine.Core;
using TaurenEngine.MaterialEx;
using TaurenEngine.MeshEx;
using UnityEngine;
using UnityEngine.Video;

namespace TaurenEngine.VideoEx
{
	/// <summary>
	/// 3D视频播放器
	///
	/// https://docs.unity3d.com/cn/2019.2/ScriptReference/Video.VideoPlayer.html
	/// </summary>
	[ExecuteInEditMode]
	public class Video3D : MonoComponent
    {
		private MeshFilter _meshFilter;
		private MeshRenderer _meshRenderer;

		public VideoPlayer VideoPlayer { get; private set; }

		void Awake()
		{
			_meshFilter = gameObject.GetOrAddComponent<MeshFilter>();
			_meshFilter.mesh = MeshHelper.CreateMeshQuad();

			_meshRenderer = gameObject.GetOrAddComponent<MeshRenderer>();
			_meshRenderer.material = MaterialHelper.GetMaterial();

			VideoPlayer = gameObject.GetOrAddComponent<VideoPlayer>();
			VideoPlayer.targetMaterialRenderer = _meshRenderer;
		}

		public void PlayUrl(string url)
		{
			VideoPlayer.source = VideoSource.Url;
			VideoPlayer.url = url;

			VideoPlayer.Play();
		}

		public void PlayVideoClip(VideoClip clip)
		{
			VideoPlayer.source = VideoSource.VideoClip;
			VideoPlayer.clip = clip;

			VideoPlayer.Play();
		}
	}
}