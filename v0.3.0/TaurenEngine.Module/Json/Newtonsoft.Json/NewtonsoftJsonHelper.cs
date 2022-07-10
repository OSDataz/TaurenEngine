/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v1.0
 *│　Time    ：2021/8/29 8:32:33
 *└────────────────────────┘*/

//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;

namespace TaurenEngine.Json
{
	/// <summary>
	/// Unity 2020版本自带 Newtonsoft.Json ？
	/// 
	/// 依赖库：https://github.com/jilleJr/Newtonsoft.Json-for-Unity.git
	/// 通过Package Manager - add package from disk 加载导入的文件 package.json
	/// 通过Package Manager - add package from git url 方式导入(下载不了）
	/// 
	/// 高级用法介绍：https://www.cnblogs.com/yanweidie/p/4605212.html
	/// 
	/// 默认值,类中所有公有成员会被序列化,如果不想被序列化,可以用特性JsonIgnore
	/// 类标签:：[JsonObject(MemberSerialization.OptOut)]
	/// 属性：[JsonIgnore]
	/// 
	/// 默认情况下,所有的成员不会被序列化,类中的成员只有标有特性JsonProperty的才会被序列化
	/// 类标签:：[JsonObject(MemberSerialization.OptIn)]
	/// 属性：[JsonProperty] PS:序列化时默认都是处理公共成员,如果需要处理非公共成员，就要在该成员上加特性"JsonProperty"
	/// 
	/// 自定义序列化的字段名称
	/// 属性：[JsonProperty(PropertyName = "CName")]
	/// 
	/// 枚举值存储为指定字符串
	/// 属性：[JsonConverter(typeof(StringEnumConverter))]
	/// </summary>
	public static class NewtonsoftJsonHelper
	{
		//public static JsonSerializerSettings Settings { get; }

		//static JsonHelper()
		//{
		//	Settings = new JsonSerializerSettings();

		//	// 序列化时想忽略默认值属性
		//	// Settings.DefaultValueHandling;

		//	// 日期格式处理
		//	Settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
		//	Settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";

		//	Settings.TypeNameHandling = TypeNameHandling.All;
		//	Settings.TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Simple;

		//	// 序列化时需要忽略值为NULL的属性
		//	Settings.NullValueHandling = NullValueHandling.Ignore;

		//	// 高级用法九
		//	/*
		//	var types = typeof(JsonUtil).Assembly.DefinedTypes
		//	    .Where(x => x.BaseType?.IsGenericType == true && x.BaseType?.GetGenericTypeDefinition() == typeof(CustomCreationConverter<>));
		//	foreach (var type in types)
		//	{
		//	    Settings.Converters.Add((JsonConverter)Activator.CreateInstance(type));
		//	}
		//	*/
		//}

		//public static T ToObject<T>(string value)
		//{
		//	return JsonConvert.DeserializeObject<T>(value, Settings);
		//}

		//public static JObject ToObject(string value)
		//{
		//	return ToObject<JObject>(value);
		//}

		//public static string ToJson(object value)
		//{
		//	return JsonConvert.SerializeObject(value, Formatting.Indented, Settings);
		//}
	}
}