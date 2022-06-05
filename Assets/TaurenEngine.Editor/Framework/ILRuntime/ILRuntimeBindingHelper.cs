/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.5.0
 *│　Time    ：2022/6/5 18:06:53
 *└────────────────────────┘*/

using System.IO;
using UnityEditor;
using UnityEngine;

namespace TaurenEngine.Editor.Framework
{
	public static class ILRuntimeBindingHelper
	{
		[MenuItem("TaurenEngine/ILRuntime/打开ILRuntime中文文档")]
		private static void OpenDocumentation()
		{
			Application.OpenURL("https://ourpalm.github.io/ILRuntime/");
		}

		[MenuItem("TaurenEngine/ILRuntime/打开ILRuntime视频教程")]
		private static void OpenTutorial()
		{
			Application.OpenURL("https://learn.u3d.cn/tutorial/ilruntime");
		}

        [MenuItem("TaurenEngine/ILRuntime/通过自动分析热更DLL生成CLR绑定")]
        private static void GenerateCLRBindingByAnalysis()
        {
            //用新的分析热更dll调用引用来生成绑定代码
            ILRuntime.Runtime.Enviorment.AppDomain domain = new ILRuntime.Runtime.Enviorment.AppDomain();
            using (FileStream fs = new FileStream("Assets/StreamingAssets/HotFix_Project.dll", FileMode.Open, FileAccess.Read))
            {
                domain.LoadAssembly(fs);

                //Crossbind Adapter is needed to generate the correct binding code
                InitILRuntime(domain);

                ILRuntime.Runtime.CLRBinding.BindingCodeGenerator.GenerateBindingCode(domain, "Assets/Samples/ILRuntime/Generated");
            }

            AssetDatabase.Refresh();
        }

        private static void InitILRuntime(ILRuntime.Runtime.Enviorment.AppDomain domain)
        {
            //这里需要注册所有热更DLL中用到的跨域继承Adapter，否则无法正确抓取引用
            //domain.RegisterCrossBindingAdaptor(new MonoBehaviourAdapter());
            //domain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
            //domain.RegisterCrossBindingAdaptor(new TestClassBaseAdapter());
            //domain.RegisterValueTypeBinder(typeof(Vector3), new Vector3Binder());
            //domain.RegisterValueTypeBinder(typeof(Vector2), new Vector2Binder());
            //domain.RegisterValueTypeBinder(typeof(Quaternion), new QuaternionBinder());
        }

        [MenuItem("TaurenEngine/ILRuntime/生成跨域继承适配器")]
        static void GenerateCrossbindAdapter()
        {
            //由于跨域继承特殊性太多，自动生成无法实现完全无副作用生成，所以这里提供的代码自动生成主要是给大家生成个初始模版，简化大家的工作
            //大多数情况直接使用自动生成的模版即可，如果遇到问题可以手动去修改生成后的文件，因此这里需要大家自行处理是否覆盖的问题

            using (StreamWriter sw = new StreamWriter("Assets/Samples/ILRuntime/2.0.2/Demo/Scripts/Examples/04_Inheritance/InheritanceAdapter.cs"))
            {
                //sw.WriteLine(ILRuntime.Runtime.Enviorment.CrossBindingCodeGenerator.GenerateCrossBindingAdapterCode(typeof(TestClassBase), "ILRuntimeDemo"));
            }

            AssetDatabase.Refresh();
        }
    }
}