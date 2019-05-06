using System;

namespace EAS.SilverlightClient.AddIn
{
	/// <summary>
	/// 定义权限所有者选择器的类型。
	/// </summary>
	enum PrivilegerSelectorType
	{
		/// <summary>
		/// 只选择帐户。
		/// </summary>
		AccountSelector = 0x0001,

		/// <summary>
		/// 只选择角色。
		/// </summary>
		RoleSelector = 0x0002,
	}
}
