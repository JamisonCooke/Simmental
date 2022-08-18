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
            _stampToType["Patrol"] = typeof(Patrol);

            _typeToStamp = new();
            foreach (string stamp in _stampToType.Keys)
                _typeToStamp[_stampToType[stamp]] = stamp;
        }

        public static Type TypeFromStamp(string stamp) => _stampToType[stamp];
        public static bool IsValidStamp(string stamp) => _stampToType.ContainsKey(stamp);
        public static string StampFromType(Type type) => _typeToStamp[type];
        public static bool IsValidType(Type type) => _typeToStamp.ContainsKey(type);
        public static string AllTasksNames => string.Join(", ", _stampToType.Keys);


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
                case "Patrol": return new Patrol(tp);

                default: return null;
            }
        }
        public string ValidateMultipleSignatures(string signatureText)
        {
            int lineNumber = 0;
            foreach (string signature in signatureText.Split(Environment.NewLine))
            {
                lineNumber++;
                string errors = ValidateSignature(signature.Trim());
                if (!string.IsNullOrEmpty(errors))
                    return $"{lineNumber}: {errors}";

            }
            return "";
        }

        public string ValidateSignature(string signatureText)
        {
            if (string.IsNullOrEmpty(signatureText.Trim()))
            {
                // We have no line at all! There are no errors in an empty line. A blank signature means nothing is there.
                return "";
            }
            var tp = new TaskParts(signatureText);
            if (string.IsNullOrEmpty(tp.SignatureStamp) || !IsValidStamp(tp.SignatureStamp))
            {
                // We are missing a signature stamp! This is the only error we can report
                return $"The first word must be one of these: {string.Join(", ", _stampToType.Keys)}.";
            }


            string signatureFormat = GetSignatureFormat(tp.SignatureStamp);
            tp.SetSignatureFormat(signatureFormat);

            return tp.GetErrorText();

        }

        public string GetSignatureFormat(string signatureStamp)
        {
            switch (signatureStamp)
            {
                case "AttackPlayer": return AttackPlayer.GetSignatureFormat();
                case "Wait": return Wait.GetSignatureFormat();
                case "Wander": return Wander.GetSignatureFormat();
                case "Patrol": return Patrol.GetSignatureFormat();

                default: return null;
            }
        }

        public string GetPrettySignatureFormat(string signatureStamp)
        {
            string uglySignatureFormat = GetSignatureFormat(signatureStamp);
            if (uglySignatureFormat == null)
                return string.Empty;
            else if (uglySignatureFormat == "")
                return signatureStamp;

            StringBuilder sb = new();
            sb.Append(signatureStamp + " ");
            bool firstItem = true;
            foreach (string p in uglySignatureFormat.Split(","))
            {
                if (firstItem)
                    firstItem = false;
                else
                    sb.Append(", ");

                sb.Append($"[{p.Split(':')[0]}]");
            }

            return sb.ToString();
        }
    }
}
