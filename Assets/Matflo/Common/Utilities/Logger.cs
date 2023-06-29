using UnityEngine;

namespace Utilities
{
    public class Logger : MonoBehaviour
    {
        public static void Log(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log(message);
#endif
        }

        public static void Log(object message, Object context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.Log(message, context);
#endif
        }

        public static void LogWarning(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarning(message);
#endif
        }

        public static void LogWarning(object message, Object context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarning(message, context);
#endif
        }

        public static void LogError(object message)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogError(message);
#endif
        }

        public static void LogError(object message, Object context)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogError(message, context);
#endif
        }

        public static void LogFormat(string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogFormat(format, args);
#endif
        }

        public static void LogFormat(Object context, string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogFormat(context, format, args);
#endif
        }

        public static void LogFormat(LogType logType, LogOption logOptions, Object context, string format,
            params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogFormat(logType, logOptions, context, format, args);
#endif
        }

        public static void LogWarningFormat(string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarningFormat(format, args);
#endif
        }

        public static void LogWarningFormat(Object context, string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogWarningFormat(context, format, args);
#endif
        }

        public static void LogErrorFormat(string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogErrorFormat(format, args);
#endif
        }

        public static void LogErrorFormat(Object context, string format, params object[] args)
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            UnityEngine.Debug.LogErrorFormat(context, format, args);
#endif
        }
    }
}