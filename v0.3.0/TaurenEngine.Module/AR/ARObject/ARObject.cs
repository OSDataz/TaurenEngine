/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:23:23
 *└────────────────────────┘*/

using TaurenEngine.LocationEx;
using UnityEngine;

namespace TaurenEngine.AR
{
	public class ARObject
	{
		internal Transform transform;

		public void Destroy()
		{
			if (transform != null)
			{
				GameObject.Destroy(transform.gameObject);
				transform = null;
			}
		}

		#region 显示对象
		public GameObject GameObject => transform?.gameObject;

		public Vector3 Position
		{
			get => transform?.localPosition ?? Vector3.zero;
			set
			{
				if (transform == null)
					return;

				transform.localPosition = value;
			}
		}

		public bool IsShow
		{
			get => transform?.gameObject.activeSelf ?? false;
			set { transform?.gameObject.SetActive(value); }
		}
		#endregion

		#region GPS定位
		private Location _location;
		internal bool useGps;

		public Location Location
		{
			get => _location;
			set
			{
				_location = value;
				useGps = true;
			}
		}
		#endregion
	}
}