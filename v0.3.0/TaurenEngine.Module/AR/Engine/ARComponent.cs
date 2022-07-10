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
    public class ARComponent
    {
        /// <summary>
        /// 创建/适配AR摄像机
        /// </summary>
        protected Camera CreateARCamera()
        {
            GameObject oARCamera;
            Camera camera = Camera.main;
            if (camera == null)
            {
                oARCamera = GameObjectHelper.GetOrCreateGameObject("Main Camera");
                camera = oARCamera.AddComponent<Camera>();
                camera.tag = "MainCamera";
            }
            else
            {
                oARCamera = camera.gameObject;
            }

            camera.clearFlags = CameraClearFlags.SolidColor;
            camera.backgroundColor = Color.black;
            camera.nearClipPlane = 0.01f;
            camera.farClipPlane = 1000;
            camera.renderingPath = RenderingPath.Forward;
            //Camera.allowHDR = false;// 为此摄像机启用高动态范围渲染。
            //Camera.allowMSAA = false;// 为此摄像机启用多重采样抗锯齿。

            // 耀斑层 光晕层：光晕层组件可以贴在相机（Cameras）上让镜头光晕（Lens Flares）出现在图像中。默认情况下，相机已经贴上光晕层（Flare Layer）
            oARCamera.GetOrAddComponent<FlareLayer>();

            return camera;
        }
    }
}