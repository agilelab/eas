using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Explorer.Entities;

namespace EAS.Explorer.BLL
{
    /// <summary>
    /// 登录结果信息。
    /// </summary>
    [Serializable]
    public class LoginResult
    {
        Account account = null;
        bool passed = false;

        public LoginResult()
        {
        }

        public LoginResult(Account account, bool passed)
        {
            this.account = account;
            this.passed = passed;
        }

        /// <summary>
        /// 帐号信息。
        /// </summary>
        public Account Account
        {
            get { return this.account; }
            set { this.account = value; }
        }

        /// <summary>
        /// 是否通过验证。
        /// </summary>
        public bool Passed
        {
            get { return this.passed; }
            set { this.passed = value; }
        }
    }
}
