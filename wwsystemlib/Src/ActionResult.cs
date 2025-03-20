using System;
using System.Collections.Generic;
using DesktopApp.Exceptions;

namespace WWsystemLib
{
    public class ActionResult<T> where T : class
    {
        public T Content;

        public string Message;

        public bool IsSuccess;
        public bool IsFail => !IsSuccess;

        public static ActionResult<T> Success(T content)
        {
            return new ActionResult<T>()
            {
                IsSuccess = true,
                Content = content,
                Message = ""
            };
        }

        public T GetOrThrow(string message = "")
        {
            if (IsFail || Content == null)
            {
                throw new ActionResultException(message == string.Empty ? Message : message);
            }

            return Content;
        }
        
        public T OrThrow(string message = "")
        {
            return GetOrThrow(message);
        }

        public T Get()
        {
            return Content;
        }

        public static ActionResult<T> Fail(string message)
        {
            return new ActionResult<T>()
            {
                IsSuccess = false,
                Content = null,
                Message = message
            };
        }

        public static ActionResult<T> Fail(string message, T content)
        {
            return new ActionResult<T>()
            {
                IsSuccess = false,
                Content = content,
                Message = message
            };
        }
    }
}