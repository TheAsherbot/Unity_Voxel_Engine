using System;

using UnityEngine;

namespace TheAshBot
{
    public static class Logger
    {
        /// <summary>
        /// This will add the color atrabute to a string. (This will only color things that use unity's rich text).
        /// </summary>
        /// <param name="_string">This is the string that is going to be colored.</param>
        /// <param name="color">This is the color that it is going to be colored to.</param>
        /// <returns>The string with the color atrabute added to it.</returns>
        public static string Color(this string _string, string color)
        {
            return "<color=" + color + ">" + _string + "</color>";
        }
        /// <summary>
        /// This changes the size of the string using unity rich text.
        /// </summary>
        /// <param name="_string">is the string that is going to be sized.</param>
        /// <param name="size">is the size of the font.</param>
        /// <returns>teh sting with the size atabute before and after the string.</returns>
        public static string Size(this string _string, int size)
        {
            return "<size=" + size + ">" + _string + "</size>";
        }
        /// <summary>
        /// This will make the font bold using unity rich text.
        /// </summary>
        /// <param name="_string">This is the string that is bolded</param>
        /// <returns>this is the string that is going to be bold</returns>
        public static string Bold(this string _string)
        {
            return "<b>" + _string + "</b>";
        }



        /// <summary>
        /// will log a massage, and when you click on it it will bring you to the game object that loged it.
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void Log(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.Log, "<size=14>", _Object, "</size>", message);
        }

        /// <summary>
        /// will log a message that have atrabutes to single that there was a secsess.
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void LogSuccess(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.Log, "<size=14><color=green>", _Object, "</color></size>", message);
        }


        /// <summary>
        /// will log an error massage
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void LogError(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.LogError, "<color=red><b><size=16>!!!", _Object, "</size></b></color>", message);
        }

        /// <summary>
        /// will log a message and make it look like an error.
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void LogFakeError(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.Log, "<color=red><b><size=16>!!!", _Object, "</size></b></color>", message);
        }


        /// <summary>
        /// will log a warning message
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void LogWarning(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.LogWarning, "<color=yellow><size=16>", _Object, "</size></color>", message);
        }

        /// <summary>
        /// will log a message and make it look like a warning
        /// </summary>
        /// <param name="_Object"> is the object that is logging the message</param>
        /// <param name="message">are the messages that will be loged.</param>
        public static void LogFakeWarning(this UnityEngine.Object _Object, params object[] message)
        {
            LogBase(Debug.Log, "<color=yellow><size=16>", _Object, "</size></color>", message);
        }



        /// <summary>
        /// This logs the massage to the console
        /// </summary>
        /// <param name="logFunction">This is the function used to log the message</param>
        /// <param name="prefix">This is the prefix of the messgae</param>
        /// <param name="_Object">This is the object that loged the message</param>
        /// <param name="sufix">this comes after the base message</param>
        /// <param name="message">this is the messgaes loged</param>
        private static void LogBase(Action<string, UnityEngine.Object> logFunction, string prefix, UnityEngine.Object _Object, string sufix = "", params object[] message)
        {
#if UNITY_EDITOR
            logFunction(prefix + _Object.name.Color("lightblue") + ": " + String.Join("; ", message) + sufix, _Object);
#endif
        }


    }
}
