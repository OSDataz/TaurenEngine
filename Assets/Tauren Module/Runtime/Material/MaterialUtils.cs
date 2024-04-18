/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.1.0
 *│　Time    ：2021/10/8 20:13:17
 *└────────────────────────┘*/

using UnityEngine;

namespace Tauren.Module.Runtime
{
    public static class MaterialUtils
    {
        public static Material GetMaterial(string shaderName = "Standard")
        {
            return new Material(Shader.Find(shaderName));
        }
    }
}