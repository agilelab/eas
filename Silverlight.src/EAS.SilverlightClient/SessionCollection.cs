using System;
using System.Collections;
using System.Collections.Generic;
using EAS.Sessions;

namespace EAS.SilverlightClient
{
    class SessionCollection : DictionaryEx<string, ISession>
    {
        public SessionCollection()
        {
            this.Add(this.Count.ToString(), new Session());
        }

        public void Add(Session session)
        {
            this.Add(session.Client.ID.ToString(), session);
        }

        public void Remove(Session session)
        {
            this.Remove(session.Client.ID.ToString());
        }

        public Session this[IClient client]
        {
            get
            {
                if (client == null)
                    throw new ArgumentNullException("client", "无效的会话客户对象，不能是空引用。");

                foreach (KeyValuePair<string, ISession> kv in this)
                {
                    if (kv.Value.Client.ID == client.ID)
                    {
                        return (Session)kv.Value;
                    }
                }

                return null;
            }
        }

        public Session this[int index]
        {
            get
            {
                return (Session)base[index];
            }
        }

        public Session this[string key]
        {
            get
            {
                return (Session)base[key];
            }
        }
    }
}
