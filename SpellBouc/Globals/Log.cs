using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;

namespace SpellBouc.Globals
{
    /* Classe de génération de logs */
    class Log
    {
        private const string GENERIC_LOG_MESSAGE = "Erreur dans ";

        /* Génère un log msgBox + message dans le debugger */
        public static void GenerateLog(ErrorCode errorCode, string message)
        {
            var outputMessage = GENERIC_LOG_MESSAGE + GetMethodNameInStack() + " :" + message;
            Console.WriteLine(outputMessage);
            MessageBox.Show(outputMessage);
        }

        /* Récupère le nom de la dernière methode dans la stack */
        private static object GetMethodNameInStack()
        {
            var s = new StackTrace();
            var thisasm = Assembly.GetExecutingAssembly();
            return s.GetFrames().Select(f => f.GetMethod()).First(m => m.Module.Assembly == thisasm).Name;
        }
    }
}
