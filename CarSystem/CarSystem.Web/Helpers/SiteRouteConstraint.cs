using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSystem.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace CarSystem.Web.Helpers
{
    public class SiteRouteConstraint : IRouteConstraint
    {
        private readonly IUnitOfWorkCarSystem _unitOfWork;

        public SiteRouteConstraint(IUnitOfWorkCarSystem unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var site = values[routeKey]?.ToString();

            if (string.IsNullOrWhiteSpace(site) || site.ToLower() == "admin")
                return false;

            var siteExists = _unitOfWork.Vehicle.Find(x =>
                    x.LicensePlate.Trim().ToLower() == site.Trim().ToLower())
                .Any();

            return siteExists;
        }
    }
}
