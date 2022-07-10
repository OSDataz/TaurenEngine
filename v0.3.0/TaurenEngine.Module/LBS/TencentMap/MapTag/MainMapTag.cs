/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using TaurenEngine.AR;
using TaurenEngine.LocationEx;
using UnityEngine;

namespace TaurenEngine.LBS
{
	public class MainMapTag : MonoBehaviour, IMapTag
	{
		public Location Location => ARLocation.Instance.GpsLocation.LocationAuto;

		public bool IsShow
		{
			get => gameObject.activeSelf;
			set { gameObject.SetActive(value); }
		}
	}
}