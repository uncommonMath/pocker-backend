using System;
using System.Linq;
using System.Runtime.CompilerServices;
using pocker_backend_core.messages;
using pocker_backend_core.messages.@event;

namespace pocker_backend_core.helpers
{
    public static class InitializationHelper
    {
        private static bool _initialized;

        public static void Initialize()
        {
            lock (typeof(InitializationHelper))
            {
                if (_initialized) throw new ApplicationException("initialized already");

                InitializeActors();
                InitializeEvents();
                AdditionalHelper.InitializeAdditional();
                _initialized = true;
            }
        }

        private static void InitializeActors()
        {
            ReflectionHelper.GetSubclasses(typeof(Actor))
                .ForEach(x => RuntimeHelpers.RunClassConstructor(x.TypeHandle));
        }

        private static void InitializeEvents()
        {
            ReflectionHelper.GetSubclasses(typeof(AbstractEvent<,>)).Select(x =>
                x.BaseType!.GetMethod("Static")!.MakeGenericMethod(x)
            ).ToList().ForEach(x => x.Invoke(null, new object[0]));
        }
    }
}