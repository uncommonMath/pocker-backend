using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace pocker_backend_core.messaging
{
    public class Directory
    {
        private static readonly Directory Instance = new Directory();

        private readonly HashSet<Actor> _actors = new HashSet<Actor>();

        private readonly BlockingCollection<dynamic> _messages =
            new BlockingCollection<dynamic>();

        private Directory()
        {
        }

        public static void StartDirectory()
        {
            lock (Instance)
            {
                Instance.Start();
            }
        }

        public static void Send<T>(AbstractMessage<T> msg) where T : Actor
        {
            lock (Instance._messages)
            {
                Instance._messages.Add(msg);
            }
        }

        public static void Send(dynamic msg)
        {
            lock (Instance._messages)
            {
                Instance._messages.Add(msg);
            }
        }

        public static void RegisterActor(Actor actor)
        {
            lock (Instance._actors)
            {
                if (Instance._actors.Any(x => x.GetType() == actor.GetType())) throw new ApplicationException("exists");
                Instance._actors.Add(actor);
            }
        }

        public static void RegisterEvent<TM, TA, TE>() where TM : AbstractEvent<TA, TE>
        {
            lock (Instance._actors)
            {
                Instance._actors.Select(x => new
                {
                    x,
                    y = x.GetType().GetEvents()
                        .FirstOrDefault(y => y.EventHandlerType!.GenericTypeArguments[0] == typeof(TE))
                }).Where(x => x.y != null).ToList().ForEach(x => x.y.AddEventHandler(x.x,
                    new Action<object>(e => { Send((TM) Activator.CreateInstance(typeof(TM), e)); })));
            }
        }

        [SuppressMessage("ReSharper", "FunctionNeverReturns")]
        private void Start()
        {
            while (!_messages.IsCompleted) RunMsg(_messages.Take());
        }

        private void RunMsg<T>(AbstractMessage<T> msg) where T : Actor
        {
            Task.Run(() =>
            {
                var actor = _actors.First(x => x.GetType() == msg.GetMessageType());
                msg.Run(actor as T);
            }).ContinueWith(x =>
            {
                if (x.Exception != null) Console.Error.WriteLine(x.Exception.ToString());
            });
        }
    }
}