using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_BLL.RoleSrv;
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

    public class ServiceController : Controller
    {
        private readonly IService _serviceSrv;

        public ServiceController(IService serviceSrv)
        {
            _serviceSrv = serviceSrv;
        }   

        // GET: Admin/Service
        public async Task<IActionResult> Index(int page =1)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                return View(await _serviceSrv.GetAllpage(page));
            }
            return View("View404");
        }

        // GET: Admin/Service/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var service = await _serviceSrv.GetById(id);

                if (service == null)
                {
                    return NotFound();
                }

                return View(service);
            }
            return View("View404");
          
        }

        // GET: Admin/Service/Create
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
        public async Task<IActionResult> Create( Service service)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    await _serviceSrv.Add(service);

                    return RedirectToAction(nameof(Index));
                }
                return View(service);
            }
            return View("View404");
            
        }

        // GET: Admin/Service/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var service = await _serviceSrv.GetById(id);
                if (service == null)
                {
                    return NotFound();
                }
                return View(service);
            }
            return View("View404");
           
        }

 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Service service)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id != service.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {

                    await _serviceSrv.Update(service);

                    return RedirectToAction(nameof(Index));
                }
                return View(service);
            }
            return View("View404");
            
        }

        // GET: Admin/Service/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                await _serviceSrv.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");

            
        }
    }
}
