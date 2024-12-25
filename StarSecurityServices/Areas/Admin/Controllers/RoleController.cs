using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_BLL.DepartmentSrv;
using Business_BLL.RoleSrv;
using Data_DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StarSecurityServices.Models;

namespace StarSecurityServices.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    
    public class RoleController : Controller
    {
        private readonly IRole _roleService;

        public RoleController(IRole roleService)
        {
            _roleService = roleService;
        }

        public async Task<IActionResult> Index(int page =1)
        {
            if (User.IsInRole("Admin")) 
            {
                var role = await _roleService.GetAllpage(page);
                return View(role);
            }
            return View("View404");
            
        }

        public IActionResult Create()
        {
            if (User.IsInRole("Admin"))
            {
                return View();
            }
            return View("View404");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role role)
        {

            if (User.IsInRole("Admin"))
            {
                if (ModelState.IsValid)
                {           
                    await _roleService.Add(role);
                    return RedirectToAction("Index");
                }
                return View(role);
            }
            return View("View404");
            
        }

        public async Task<IActionResult> Delete(int id)
        {

            if (User.IsInRole("Admin"))
            {
                await _roleService.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");
            
        }

       
    }
}
