using System;
using System.Collections.Generic;
using System.Text;
using EAS.Sessions;
using EAS.Context;
using EAS.Objects;
using EAS.Modularization;
using EAS.Workflow;

namespace EAS.SilverlightClient
{
    class ApplicationBridge : EAS.Application
    {
        internal void SetInstance2(Application instance)
        {
            IPlatform platform = (IPlatform)instance;
            this.SetInstance(platform);
        }

        protected override void SetInstance(IPlatform instance)
        {
            base.SetInstance(instance);
        }

        public override string Name
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IContext Context
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override IContainer Container
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override ISession Session
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        public override void StartModule(object addIn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void StartModule(Type addIn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void StartModule(Guid addIn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void CloseModule(object addIn)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void CloseModule()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void AppStart()
        {
            throw new NotImplementedException();
        }

        public override void Login(string organization, string loginID, string password)
        {
            throw new NotImplementedException();
        }

        public override void ChangePassword()
        {
            throw new NotImplementedException();
        }

        public override void Logout()
        {
            throw new NotImplementedException();
        }

        public override void AppEnd()
        {
            throw new NotImplementedException();
        }

        public override void CallScript(string script,IDictionary<string,object> args)
        {
            throw new NotImplementedException();
        }

        public override DateTime Time
        {
            get
            {
                return DateTimeClient.CurrentTime;
            }
        }

        public override IWorkflowRuntime WorkflowRuntime
        {
            get { throw new NotImplementedException(); }
        }
    }
}
