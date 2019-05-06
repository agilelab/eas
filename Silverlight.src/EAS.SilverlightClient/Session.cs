using System;
using EAS.Sessions;
using EAS.Explorer;

namespace EAS.SilverlightClient
{
    class Session : EAS.Sessions.ISession
	{
        IAccount account = null;
        Guid m_ID = Guid.Empty;
        string m_DataSet = string.Empty;
        string m_Organization = string.Empty;

		internal Session()
		{
            this.m_ID = Guid.NewGuid();
		}

		public IAccount Account
		{
			get
			{
				return this.account;
			}
            internal set
            {
                this.account = value;
            }
		}

        #region ISession 成员

        public Guid ID
        {
            get
            {
                return m_ID;
            }
        }

        public string DataSet
        {
            get
            {
                return m_DataSet;
            }
            internal set
            {
                this.m_DataSet = value;
            }
        }

        public string Organization
        {
            get
            {
                return m_Organization;
            }
            internal set
            {
                this.m_Organization = value;
            }
        }

        public EAS.Sessions.IClient Client
        {
            get
            {
                return this.account;
            }
        }

        #endregion

        #region ISession 成员

        public void Start(params object[] parameters)
        {

        }

        public void Abandon()
        {

        }

        #endregion
	}
}
