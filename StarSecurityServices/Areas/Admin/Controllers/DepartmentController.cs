using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_BLL.DepartmentSrv;
using Business_BLL.ServiceSrv;
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

    public class DepartmentController : Controller
    {
        private readonly IDepartment _departmentService;
        private readonly IService _serviceEmployee;


        public DepartmentController(IDepartment departmentService , IService serviceEmployee)
        {
            _departmentService = departmentService;
            _serviceEmployee = serviceEmployee;
        }


        public async Task<IActionResult> Index(int page = 1)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                var dep = await _departmentService.GetAllpage(page);
                return View(dep);
            }
        
            return View("View404");

        }

        public IActionResult Create()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                return View();
            }
     
            return View("View404");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Department department)
        {

            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    await _departmentService.Add(department);
                    return RedirectToAction("Index");
                }
                return View(department);
            }
            return View("View404");
        }

        // GET: Department/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
               
                var department = await _departmentService.GetById(id);
                if (department == null)
                {
                    return NotFound();
                }

                return View(department);
            }
            return View("View404");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Department department)
        {

            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {

                if (id != department.Id)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    await _departmentService.Update(department);
                    return RedirectToAction(nameof(Index));
                }

                return View(department);
            }
            return View("View404");

        }
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                await _departmentService.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");
        }

  
    }
}
