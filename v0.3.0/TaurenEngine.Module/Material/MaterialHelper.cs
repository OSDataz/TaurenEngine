/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/10/8 20:13:17
 *└────────────────────────┘*/

using UnityEngine;

namespace TaurenEngine.MaterialEx
{
    public static class MaterialHelper
    {
        public static Material GetMaterial(string shaderName = "Standard")
        {
            return new Material(Shader.Find(shaderName));
        }
    }
}