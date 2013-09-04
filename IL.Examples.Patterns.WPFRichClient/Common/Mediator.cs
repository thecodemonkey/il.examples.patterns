using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IL.Examples.Patterns.WPFRichClient.Common
{
    public sealed class Mediator
    {
        static readonly Mediator instance = new Mediator();
        private volatile object locker = new object();

        MultiDictionary<string, Action<Object>> internalList = new MultiDictionary<string, Action<Object>>();

        static Mediator() { }
        private Mediator() { }

        public static Mediator Instance
        {
            get
            {
                return instance;
            }
        }

        /// <summary>Registers a Colleague to a specific message</summary>
        /// <param name="callback">The callback to use when the message it seen</param>
        /// <param name="message">The message to register to</param>
        public void Register(Action<Object> callback, string message)
        {
            internalList.AddValue(message, callback);
        }

        /// <summary>Notify all colleagues that are registed to the specific message</summary>
        /// <param name="message">The message for the notify by</param>
        /// <param name="args">The arguments for the message</param>
        public void ForceAction(string message, object args)
        {
            if (internalList.ContainsKey(message))
            {
                //forward the message to all listeners
                foreach (Action<object> callback in internalList[message])
                    callback(args);
            }
        }
    }
}
