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

using log4net;

namespace LFS.BrowseForSpeed
{
    /// <summary>
    /// The ILogEx interface that exposes the Trace and Verbose messages.
    /// </summary>
    public interface ILogEx : ILog
    {
        void Config();
        void Config(string extension);
        
        void Debug(string methodName, object message, object value);

        /// <summary>
        /// Log a debug message.
        /// </summary>
        /// <param name="message"></param>
        void Debug(string methodName, object message);
		
        /// <summary>
        /// Log a debug message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Debug(string methodName, object message, Exception exception);

        /// <summary>
        /// Log an error message.
        /// </summary>
        /// <param name="message"></param>
        void Error(string methodName, object message);
		
        /// <summary>
        /// Log an error message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Error(string methodName, object message, Exception exception);

        /// <summary>
        /// Log a fatal message.
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string methodName, object message);
		
        /// <summary>
        /// Log a fatal message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Fatal(string methodName, object message, Exception exception);

        /// <summary>
        /// Log an info message.
        /// </summary>
        /// <param name="message"></param>
        void Info(string methodName, object message);
		
        /// <summary>
        /// Log an info message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Info(string methodName, object message, Exception exception);

        /// <summary>
        /// Log a trace message.
        /// </summary>
        /// <param name="message"></param>
        void Trace(object message);
		
        /// <summary>
        /// Log a trace message.
        /// </summary>
        /// <param name="message"></param>
        void Trace(string methodName, object message);
		
        /// <summary>
        /// Log a trace message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Trace(string methodName, object message, Exception exception);
		
        /// <summary>
        /// A message used to trace the flow of an application.  Available during TRACE.
        /// </summary>
        void TraceFinish(string methodName);

        /// <summary>
        /// A message used to trace the flow of an application.  Available during TRACE.
        /// </summary>
        void TraceStart(string methodName);

        /// <summary>
        /// Log a verbose message.
        /// </summary>
        /// <param name="message"></param>
        void Verbose(object message);
		
        /// <summary>
        /// Log a verbose message.
        /// </summary>
        /// <param name="message"></param>
        void Verbose(string methodName, object message);
		
        /// <summary>
        /// Log a verbose message with an exception.
        /// </summary>
        /// <param name="message"></param>
        void Verbose(string methodName, object message, Exception exception);

        /// <summary>
        /// Is trace logging enabled via the log4net configuration.
        /// </summary>
        bool IsTraceEnabled { get; }

        /// <summary>
        /// Is verbose logging enabled via the log4net configuration.
        /// </summary>
        bool IsVerboseEnabled { get; }

		/// <summary>
		/// Log a warning message.
		/// </summary>
		/// <param name="message"></param>
		void Warn(string methodName, object message);
		
		/// <summary>
		/// Log a warning message with an exception.
		/// </summary>
		/// <param name="message"></param>
        void Warn(string methodName, object message, Exception exception);
    }
}