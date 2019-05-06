using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EAS.Services;

namespace EAS
{
    class ExceptionHandler
    {
        /// <summary>
        /// 处理错误异常。
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static Exception Handle(System.Exception exc)
        {
            if (!(exc is LicenseException))
                EAS.Loggers.Logger.Error(exc);

            return HandleException(exc);
        }

        /// <summary>
        /// 处理错误异常。
        /// </summary>
        /// <param name="exc"></param>
        /// <returns></returns>
        public static Exception Handle(System.Exception exc,string Text)
        {
            if (!(exc is LicenseException))
                EAS.Loggers.Logger.Write(Text, exc);
            
            return HandleException(exc);
        }

        private static Exception HandleException(System.Exception exc)
        {
            if (exc is System.Reflection.TargetInvocationException)
            {
                return exc.InnerException;
            }
            else if (exc is System.Reflection.TargetParameterCountException)
            {
                return exc.InnerException;
            }
            else if (exc is System.Reflection.TargetException)
            {
                return exc.InnerException;
            }
            else
            {
                return exc;
            }
        }
    }
}
