/*┌────────────────────────┐
 *│　Engine  ：TaurenEngine
 *│　Author  ：Osdataz
 *│　Version ：v0.11.0
 *│　Time    ：2023/10/3 20:40:26
 *└────────────────────────┘*/

namespace Tools.JobAnalysis
{
	public class JobPlatform
	{
		public const string Boss = "BOSS直聘";
		public const string LaGou = "拉勾";
		public const string LiePin = "猎聘";
		public const string ZhiLian = "智联招聘";

		public static string[] GetArray()
		{
			return new string[] 
			{
				Boss, LaGou, LiePin, ZhiLian
			};
		}
	}

	public class JobCity
	{
		public const string ShenZhen = "深圳";
		public const string ChengDu = "成都";

		public static string[] GetArray()
		{
			return new string[] 
			{
				ShenZhen, ChengDu
			};
		}
	}

	public class RecruitType
	{
		public static string Direct = "直招";
		public static string Proxy = "代招";
		public static string Headhunting = "猎头";
	}

	/// <summary>
	/// 意向状态
	/// </summary>
	public class WishStatus
	{
		public const string None = "待定";
		public const string Refuse = "无意向";
		public const string Low = "低意向";
		public const string Middle = "中意向";
		public const string High = "高意向";

		public static string[] GetArray()
		{
			return new string[] 
			{
				None, Refuse, Low, Middle, High
			};
		}
	}

	/// <summary>
	/// 面试状态
	/// </summary>
	public class InterviewStatus
	{
		public const string None = "未沟通";
		public const string Chat = "沟通中";
		public const string Interview = "面试中";
		public const string Success = "已通过";
		public const string IRefuse = "我拒绝";
		public const string Rejected = "被拒绝";

		public static string[] GetArray()
		{
			return new string[] 
			{
				None, Chat, Interview, Success, IRefuse, Rejected
			};
		}
	}
}