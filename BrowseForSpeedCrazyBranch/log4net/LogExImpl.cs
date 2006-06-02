#region Copyright & License
//
// Copyright 2001-2006 The Apache Software Foundation
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

//***********************************************************************
// CHANGES:
//
// Revision History:
// BY:             DATE:            MODIFICATION:
// =====           ==========       =====================
// Chris Schletter 04/2006          Added support for Trace and Verbose logging.  Logging methods
//                                  now take a method name as an additional parameter for better logging
//                                  as inclusion of class name by log4net decreases performance.
//
// Chris Schletter 04/2006          Configuration methods added.

using System;
using System.Reflection;

using log4net;
using log4net.Core;
using log4net.Repository;

namespace LFS.BrowseForSpeed
{
    /// <summary>
    /// The LogExImpl implementation that exposes the Trace and Verbose messages.
    /// </summary>
    public class LogExImpl : LogImpl, ILogEx
    {
        public LogExImpl(ILogger logger)
            : base(logger)
        {
        }

        #region Public Methods
        public void Config()
        {
            Config(".config");
        }

        public void Config(string extension)
        {
            string sCodeBase = System.IO.Path.GetFileName(Assembly.GetEntryAssembly().CodeBase);
            System.IO.FileInfo fiCodeBase = new System.IO.FileInfo(string.Concat(sCodeBase, extension));
            log4net.Config.XmlConfigurator.ConfigureAndWatch(fiCodeBase);
        }

        /// <summary>
        /// Log a debug message with an message/value.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string methodName, object message, object value)
        {
            Debug(string.Concat(Format(methodName, message), "=", value));
        }

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string methodName, object message)
        {
            Debug(Format(methodName, message));
        }

        /// <summary>
        /// Log a debug message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Debug(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelDebug, Format(methodName, message), exception);
        }

        public override void Debug(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelDebug, message, null);
        }

        public override void Debug(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelDebug, message, exception);
        }

        public override void DebugFormat(string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        public override void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message"></param>
        public void Error(string methodName, object message)
        {
            Error(Format(methodName, message));
        }

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message"></param>
        public void Error(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelError, Format(methodName, message), exception);
        }

        public override void Error(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelError, message, null);
        }

        public override void Error(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelError, message, exception);
        }

        public override void ErrorFormat(string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        public override void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        /// <summary>
        /// Log a fatal message.
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string methodName, object message)
        {
            Fatal(Format(methodName, message));
        }

        /// <summary>
        /// Log a fatal message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Fatal(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelFatal, Format(methodName, message), exception);
        }

        public override void Fatal(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelFatal, message, null);
        }

        public override void Fatal(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelFatal, message, exception);
        }

        public override void FatalFormat(string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        public override void FatalFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message"></param>
        public void Info(string methodName, object message)
        {
            Info(Format(methodName, message));
        }

        /// <summary>
        /// Log an info message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Info(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelInfo, Format(methodName, message), exception);
        }

        public override void Info(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelInfo, message, null);
        }

        public override void Info(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelInfo, message, exception);
        }

        public override void InfoFormat(string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        public override void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        /// <summary>
        /// Log a trace message.
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string methodName, object message)
        {
            Trace(Format(methodName, message));
        }

        /// <summary>
        /// Log an trace message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Trace(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelTrace, Format(methodName, message), exception);
        }

        public void Trace(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelTrace, message, null);
        }

        public void Trace(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelTrace, message, exception);
        }

        /// <summary>
        /// A message used to trace the flow of an application.  Available during TRACE.
        /// </summary>
        public void TraceFinish(string methodName)
        {
            Trace(methodName, "...Finished");
        }

        /// <summary>
        /// A message used to trace the flow of an application.  Available during TRACE.
        /// </summary>
        public void TraceStart(string methodName)
        {
            Trace(methodName, "Started...");
        }

        /// <summary>
        /// Log a verbose message.
        /// </summary>
        /// <param name="message"></param>
        public void Verbose(string methodName, object message)
        {
            Verbose(Format(methodName, message));
        }

        /// <summary>
        /// Log an verbose message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Verbose(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelVerbose, Format(methodName, message), exception);
        }

        public void Verbose(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelTrace, message, null);
        }

        public void Verbose(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelTrace, message, exception);
        }

        /// <summary>
        /// Log a warn message.
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string methodName, object message)
        {
            Warn(Format(methodName, message));
        }

        /// <summary>
        /// Log a warn message with an exception.
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string methodName, object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelWarn, Format(methodName, message), exception);
        }

        public override void Warn(object message)
        {
            Logger.Log(LogImplDeclaringType, LevelWarn, message, null);
        }

        public override void Warn(object message, Exception exception)
        {
            Logger.Log(LogImplDeclaringType, LevelWarn, message, exception);
        }

        public override void WarnFormat(string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }

        public override void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            throw new NotImplementedException("Not available.");
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Is debug logging enabled via the log4net configuration.
        /// </summary>
        public override bool IsDebugEnabled
        {
            get { return Logger.IsEnabledFor(LevelDebug); }
        }

        /// <summary>
        /// Is error logging enabled via the log4net configuration.
        /// </summary>
        public override bool IsErrorEnabled
        {
            get { return Logger.IsEnabledFor(LevelError); }
        }

        /// <summary>
        /// Is fatal logging enabled via the log4net configuration.
        /// </summary>
        public override bool IsFatalEnabled
        {
            get { return Logger.IsEnabledFor(LevelFatal); }
        }

        /// <summary>
        /// Is info logging enabled via the log4net configuration.
        /// </summary>
        public override bool IsInfoEnabled
        {
            get { return Logger.IsEnabledFor(LevelInfo); }
        }

        /// <summary>
        /// Is trace logging enabled via the log4net configuration.
        /// </summary>
        public bool IsTraceEnabled
        {
            get { return Logger.IsEnabledFor(LevelTrace); }
        }

        /// <summary>
        /// Is verbose logging enabled via the log4net configuration.
        /// </summary>
        public bool IsVerboseEnabled
        {
            get { return Logger.IsEnabledFor(LevelVerbose); }
        }

        /// <summary>
        /// Is warn logging enabled via the log4net configuration.
        /// </summary>
        public override bool IsWarnEnabled
        {
            get { return Logger.IsEnabledFor(LevelWarn); }
        }
        #endregion

        #region Protected Methods
        protected string Format(string methodName, object message)
        {
            bool isMethodEmpty = (methodName == null);
            bool isMessageEmpty = (message == null);

            return string.Concat((isMethodEmpty ? string.Empty : methodName),
                                 ", ",
                                 System.Threading.Thread.CurrentThread.ManagedThreadId,
                                 (isMessageEmpty ? string.Empty : ": "),
                                 message);
        }

        /// <summary>
        /// Virtual method called when the configuration of the repository changes
        /// </summary>
        /// <param name="repository">the repository holding the levels</param>
        /// <remarks>
        /// <para>
        /// Virtual method called when the configuration of the repository changes
        /// </para>
        /// </remarks>
        protected override void ReloadLevels(ILoggerRepository repository)
        {
            LevelMap levelMap = repository.LevelMap;

            levelMap.Clear();
            levelMap.Add(LevelAll);
            levelMap.Add(LevelVerbose);
            levelMap.Add(LevelDebug);
            levelMap.Add(LevelTrace);
            levelMap.Add(LevelWarn);
            levelMap.Add(LevelError);
            levelMap.Add(LevelFatal);
            levelMap.Add(LevelInfo);
            levelMap.Add(LevelOff);
        }
        #endregion

        #region Fields
        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private readonly static Type LogImplDeclaringType = typeof(LogExImpl);
        #endregion

        #region Constants
        public static Level LevelAll = new Level(0, "ALL");
        public static Level LevelVerbose = new Level(1, "VERBOSE");
        public static Level LevelDebug = new Level(2, "DEBUG");
        public static Level LevelTrace = new Level(3, "TRACE");
        public static Level LevelWarn = new Level(4, "WARN");
        public static Level LevelError = new Level(5, "ERROR");
        public static Level LevelFatal = new Level(6, "FATAL");
        public static Level LevelInfo = new Level(7, "INFO");
        public static Level LevelOff = new Level(8, "OFF");
        #endregion
    }
}