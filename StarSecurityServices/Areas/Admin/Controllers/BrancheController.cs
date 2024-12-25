using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data_DAL.Entities;
using Business_BLL.EmployeeSrv;
using Business_BLL.ClientSrv;
using Business_BLL.ServiceSrv;
using Business_BLL.BrancheSrv;
using Business_BLL.DepartmentSrv;

namespace StarSecurityServices.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BrancheController : Controller
    {
        private readonly IBranche _brancheService;

        public BrancheController(IBranche brancheService)
        {
            _brancheService = brancheService;
        }


        // GET: Admin/Branche
        public async Task<IActionResult> Index(int page = 1)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("Staff"))
            {
                var br = await _brancheService.GetAll(page);
                return View(br);
            }

            return View("View404");
        }


        // GET: Admin/Branche/Create
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
        public async Task<IActionResult> Create( Branche branche)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                if (ModelState.IsValid)
                {
                  
                    await _brancheService.Add(branche);
                    return RedirectToAction(nameof(Index));
                }
                return View(branche);
            }
            return View("View404");
            
        }

        // GET: Admin/Branche/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                if (id == null)
                {
                    return NotFound();
                }

                var branche = await _brancheService.GetById(id);
                if (branche == null)
                {
                    return NotFound();
                }
                return View(branche);
            }
            return View("View404");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Branche branche)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                if (id != branche.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                   
                    await _brancheService.Update(branche);
                    return RedirectToAction(nameof(Index));
                }
                return View(branche);
            }
            return View("View404");
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                await _brancheService.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");

        }
    }
}
