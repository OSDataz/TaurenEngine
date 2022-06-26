using System;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;

namespace TaurenEngine.Framework
{   
    public class UIPanelAdapter : CrossBindingAdaptor
    {
        public override Type BaseCLRType
        {
            get
            {
                return typeof(TaurenEngine.Framework.UIPanel);
            }
        }

        public override Type AdaptorType
        {
            get
            {
                return typeof(Adapter);
            }
        }

        public override object CreateCLRInstance(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
        {
            return new Adapter(appdomain, instance);
        }

        public class Adapter : TaurenEngine.Framework.UIPanel, CrossBindingAdaptorType
        {
            CrossBindingMethodInfo<UnityEngine.Transform> mInit_0 = new CrossBindingMethodInfo<UnityEngine.Transform>("Init");
            CrossBindingMethodInfo mClose_1 = new CrossBindingMethodInfo("Close");
            CrossBindingMethodInfo mOnOpen_2 = new CrossBindingMethodInfo("OnOpen");
            CrossBindingMethodInfo mOnClose_3 = new CrossBindingMethodInfo("OnClose");

            bool isInvokingToString;
            ILTypeInstance instance;
            ILRuntime.Runtime.Enviorment.AppDomain appdomain;

            public Adapter()
            {

            }

            public Adapter(ILRuntime.Runtime.Enviorment.AppDomain appdomain, ILTypeInstance instance)
            {
                this.appdomain = appdomain;
                this.instance = instance;
            }

            public ILTypeInstance ILInstance { get { return instance; } }

            public override void Init(UnityEngine.Transform root)
            {
                if (mInit_0.CheckShouldInvokeBase(this.instance))
                    base.Init(root);
                else
                    mInit_0.Invoke(this.instance, root);
            }

            public override void Close()
            {
                if (mClose_1.CheckShouldInvokeBase(this.instance))
                    base.Close();
                else
                    mClose_1.Invoke(this.instance);
            }

            public override void OnOpen()
            {
                if (mOnOpen_2.CheckShouldInvokeBase(this.instance))
                    base.OnOpen();
                else
                    mOnOpen_2.Invoke(this.instance);
            }

            public override void OnClose()
            {
                if (mOnClose_3.CheckShouldInvokeBase(this.instance))
                    base.OnClose();
                else
                    mOnClose_3.Invoke(this.instance);
            }

            public override string ToString()
            {
                IMethod m = appdomain.ObjectType.GetMethod("ToString", 0);
                m = instance.Type.GetVirtualMethod(m);
                if (m == null || m is ILMethod)
                {
                    if (!isInvokingToString)
                    {
                        isInvokingToString = true;
                        string res = instance.ToString();
                        isInvokingToString = false;
                        return res;
                    }
                    else
                        return instance.Type.FullName;
                }
                else
                    return instance.Type.FullName;
            }
        }
    }
}

