using System;
using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure
{
    public class ActionResultException : Exception
    {
        public ActionResultException(ActionResult result)
        {
            Result = result;
        }

        public ActionResult Result { get; }
    }
}