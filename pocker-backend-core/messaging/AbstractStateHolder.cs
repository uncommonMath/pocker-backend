using System;
using System.Collections;
using System.Reflection;
using Newtonsoft.Json;
using NUnit.Framework;
using pocker_backend_core.helper;
using PostSharp.Aspects;
using PostSharp.Extensibility;

namespace pocker_backend_core.messaging
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class StateChanger : MethodInterceptionAspect
    {
        public Type ResponseType { get; set; }

        public override bool CompileTimeValidate(MethodBase method)
        {
            if (ReflectionHelper.IsAssignableToGeneric(method.DeclaringType, typeof(AbstractStateHolder)))
                return true;
            Message.Write(
                MessageLocation.Unknown,
                SeverityType.Error,
                "CUSTOM01",
                "Can not apply [StateChanger] to method {0} because its class {1} is not assignable to AbstractStateHolder.",
                method,
                method.DeclaringType);
            return false;
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            args.Proceed();
            var holder = args.Instance as AbstractStateHolder;
            Assert.IsNotNull(holder);
            foreach (var o in holder.Subscribers)
                typeof(AbstractMessage<Actor>).GetMethod("Send")!.MakeGenericMethod(ResponseType)
                    .Invoke(null, new object[] {new[] {o, args.Method.Name, holder}});
        }
    }

    public abstract class AbstractStateHolder
    {
        [JsonIgnore] public abstract ICollection Subscribers { get; }
    }
}