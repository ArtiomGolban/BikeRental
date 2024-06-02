using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BikeRental.Domain.Entities.User;

namespace BikeRental.Web.Extension
{
    public static class HttpContextExtensions
    {
        public static UserDBTable GetMySessionObject(this HttpContext current)
        {
            return (UserDBTable)current?.Session["__SessionObject"];
        }

        public static void SetMySessionObject(this HttpContext current, UserDBTable profile)
        {
            current.Session.Add("__SessionObject", profile);
        }
    }
}