/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 11:15:14
 *└────────────────────────┘*/

using System.Collections.Generic;
using TaurenEngine.MaterialEx;
using UnityEngine;

namespace TaurenEngine.LBS
{
    /// <summary>
    /// 导航路径线
    /// </summary>
    public class RouteLine : MonoBehaviour
    {
        private LineRenderer _renderer;

        void Awake()
        {
            _renderer = gameObject.AddComponent<LineRenderer>();
            _renderer.material = MaterialHelper.GetMaterial("Sprites/Default");
            _renderer.startWidth = 1;
            _renderer.endWidth = 1;
            _renderer.startColor = Color.green;
            _renderer.endColor = Color.green;
        }

        public void SetData(List<Vector3> list)
        {
            _renderer.positionCount = list.Count;

            for (int i = 0; i < list.Count; ++i)
            {
                _renderer.SetPosition(i, list[i]);
            }
        }
    }
}