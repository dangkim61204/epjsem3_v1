using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Data_DAL.Entities;
using Business_BLL.VacancieSrv;
using Business_BLL.EmployeeSrv;

namespace StarSecurityServices.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VacancieController : Controller
    {
        private readonly IVacancie _vacancieService;

        public VacancieController(IVacancie vacancieService)
        {
            _vacancieService = vacancieService;
        }

        // GET: Admin/Vacancie
        public async Task<IActionResult> Index(int page = 1)
        {
            if(User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("Staff"))
            {
                return View(await _vacancieService.GetAll(page));
            }
           return View("View404");
        }

        // GET: Admin/Vacancie/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") || User.IsInRole("Staff"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vacancie = await _vacancieService.GetById(id);

                if (vacancie == null)
                {
                    return NotFound();
                }

                return View(vacancie);
            }
            return View("View404");
           
        }

        // GET: Admin/Vacancie/Create
        public IActionResult Create()
        {

            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {

                return View();
            }
            return View("View404");
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( Vacancie vacancie)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (ModelState.IsValid)
                {
                    if (vacancie.StartDate > vacancie.EndDate)
                    {
                        ModelState.AddModelError("DateError", "StartDate cannot be after EndDate.");
                        return View(vacancie);
                    }

                    await _vacancieService.Add(vacancie);
                        return RedirectToAction(nameof(Index));
                    
                   
                }
                return View(vacancie);
            }
            return View("View404");
           
        }

        // GET: Admin/Vacancie/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                if (id == null)
                {
                    return NotFound();
                }

                var vacancie = await _vacancieService.GetById(id);
                if (vacancie == null)
                {
                    return NotFound();
                }
                return View(vacancie);
            }
            return View("View404");
           
        }

  
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,  Vacancie vacancie)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id != vacancie.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    if (vacancie.StartDate > vacancie.EndDate)
                    {
                        ModelState.AddModelError("DateError", "StartDate cannot be after EndDate.");
                        return View(vacancie);
                    }

                    await _vacancieService.Update(vacancie);
                    return RedirectToAction(nameof(Index));
                }
                return View(vacancie);
            }
            return View("View404");
       
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager") )
            {
                await _vacancieService.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");

        }
    }
}
