using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LightestNight.System.Utilities
{
    public class LightestNightException
    {
        public class Frame
        {
            /// <summary>
            /// The Filename this Frame is relating to
            /// </summary>
            public string Filename { get; set; }
            
            /// <summary>
            /// The Line Number this frame is relating to
            /// </summary>
            public int LineNumber { get; set; }
            
            /// <summary>
            /// The Column Number this frame is relating to
            /// </summary>
            public int ColumnNumber { get; set; }
            
            /// <summary>
            /// The Method this frame is relating to
            /// </summary>
            public string Method { get; set; }
            
            /// <summary>
            /// The Class this frame is relating to
            /// </summary>
            public string ClassName { get; set; }
            
            /// <summary>
            /// Any arguments that were within this Frame
            /// </summary>
            public string[] Arguments { get; set; }
        }
        
        /// <summary>
        /// The <see cref="Exception" /> that was thrown
        /// </summary>
        public Exception Exception { get; set; }
        
        /// <summary>
        /// The Frames within the <see cref="Exception" />
        /// </summary>
        public Frame[] Frames { get; set; }

        public LightestNightException(Exception ex)
        {
            if (ex == null)
                return;
            
            Exception = ex;
            Frames = new StackTrace(ex, true).GetFrames()?.Select(frame => new Frame
            {
                Filename = frame.GetFileName(),
                LineNumber = frame.GetFileLineNumber(),
                ColumnNumber = frame.GetFileColumnNumber(),
                Method = frame.GetMethod().Name,
                ClassName = frame.GetMethod().DeclaringType?.AssemblyQualifiedName,
                Arguments = frame.GetMethod().GetParameters().Select(arg => arg.Name).ToArray()
            }).ToArray();
        }
    }
}