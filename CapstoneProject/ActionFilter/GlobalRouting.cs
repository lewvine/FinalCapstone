using CapstoneProject.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace CapstoneProject.ActionFilter
{
    public class GlobalRouting : IActionFilter
    {
        private readonly ClaimsPrincipal _claimsPrincipal;
        private readonly ApplicationDbContext _context;
        public GlobalRouting(ClaimsPrincipal claimsPrincipal, ApplicationDbContext context)
        {
            _claimsPrincipal = claimsPrincipal;
            _context = context;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controller = context.RouteData.Values["controller"];
            if(controller.Equals("Home"))
            {
                //if (_claimsPrincipal.IsInRole("Customer"))
                //{
                //    context.Result = new RedirectToActionResult("Index", "Home", null);
                //}
                if (_claimsPrincipal.IsInRole("Salesperson"))
                {
                    context.Result = new RedirectToActionResult("Index", "Salesperson", null);
                }
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
