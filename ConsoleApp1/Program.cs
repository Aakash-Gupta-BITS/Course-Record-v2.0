using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using IronPython.Hosting;
using IronPython.Runtime.Types;
using IronPython.Runtime.Operations;
using Microsoft.Scripting.Hosting;
using IPEngine;
using System.Reflection;

namespace IPEngine
{
    public class EngineManager
    {
        private ScriptEngine engine;
        private ScriptScope scope;
        dynamic ClassInstance;

        public EngineManager(string filepath)
        {
            engine = Python.CreateEngine();
            scope = engine.CreateScope();

            engine.ExecuteFile(filepath, scope);
        }

        public void CreateObject(string classname)
        {
            ClassInstance = engine.Operations.CreateInstance(scope.GetVariable(classname));
        }

        public T ExecuteClassOperation<T>(string method, params object[] para)
        {
            return (T)engine.Operations.InvokeMember(ClassInstance, method, para);
        }
    }
}

namespace DriveQuickstart
{
    class Program
    {
        static void Main(string[] args)
        {
            var engine = Python.CreateEngine();
            dynamic py = engine.ExecuteFile("pyt.py");
            dynamic calc = py.add(4.5, 6.7);
            Console.WriteLine(calc);
        }
    }
}