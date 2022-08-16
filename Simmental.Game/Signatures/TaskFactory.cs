using Simmental.Game.Characters.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simmental.Game.Signatures
{
    /// <summary>
    /// Manages ITasks in creation via the signature structure
    /// </summary>
    public class TaskFactory
    {

        private static Dictionary<string, Type> _stampToType;
        private static Dictionary<Type, string> _typeToStamp;

        static TaskFactory()
        {
            _stampToType = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);

            _stampToType["AttackPlayer"] = typeof(AttackPlayer);
            _stampToType["Wait"] = typeof(Wait);
            _stampToType["Wander"] = typeof(Wander);

            _typeToStamp = new();
            foreach (string stamp in _stampToType.Keys)
                _typeToStamp[_stampToType[stamp]] = stamp;
        }
        public static Type TypeFromStamp(string stamp)
        {
            //(rw)
            return _stampToType[stamp];
        }

        public static bool IsValidStamp(string stamp)
        {
            return _stampToType.ContainsKey(stamp);
        }

        public static string StampFromType(Type type)
        {
            return _typeToStamp[type];
        }
        public static bool IsValidType(Type type)
        {
            return _typeToStamp.ContainsKey(type);
        }

        public IEnumerable<ITask> CreateTasks(string taskSignatures)
        {
            List<ITask> tasks = new();
            foreach(string taskSignature in taskSignatures.Split(Environment.NewLine))
            {
                if (!string.IsNullOrEmpty(taskSignature))
                {
                    tasks.Add(CreateTaskFromSignature(taskSignature));
                }
            }
            return tasks;
        }

        public string GetTaskSignatures(IEnumerable<ITask> tasks)
        {
            string result = "";
            foreach(ITask task in tasks)
            {
                result += task.GetSignature() + Environment.NewLine;
            }
            return result;
        }

        public string ValidateTaskSignatures(string taskSignatures)
        {
            return "";
        }

        public ITask CreateTaskFromSignature(string taskSignature)
        {
            var tp = new TaskParts(taskSignature);
            switch (tp.SignatureStamp)
            {
                case "AttackPlayer": return new AttackPlayer(tp);
                case "Wander": return new Wander(tp);
                case "Wait": return new Wait(tp);

                default: return null;
            }
        }

    }
}
