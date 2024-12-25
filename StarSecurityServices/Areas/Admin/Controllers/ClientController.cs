using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business_BLL.ClientSrv;
using Business_BLL.DepartmentSrv;
using Business_BLL.EmployeeSrv;
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

    public class ClientController : Controller
    {
        private readonly IClient _clientService;
        private readonly IService _serviceSrv;
        private readonly IEmployee _employeeService;



        public ClientController(IClient clientService, IService serviceSrv, IEmployee employeeService)
        {
            _clientService = clientService;
            _serviceSrv = serviceSrv;
            _employeeService = employeeService;
        }

        // GET: Admin/Client
        public async Task<IActionResult> Index(int page =1)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                var list = await _clientService.GetAll(page);
                return View(list);
            }
            return View("View404");
         
        }

        // GET: Admin/Client/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _serviceSrv.GetById(id);

                if (client == null)
                {
                    return NotFound();
                }

                return View(client);
            }
            return View("View404");
          
        }

        // GET: Admin/Client/Create
        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                ViewBag.serviceId = new SelectList(await _serviceSrv.GetAll(), "Id", "ServiceName");
                ViewBag.EmployeeSelectList = new SelectList(await _employeeService.GetAll(), "Code", "Name");
                return View();
            }
            return View("View404");
           
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Client client, int[] emplyeeIds)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                ViewBag.serviceId = new SelectList(await _serviceSrv.GetAll(), "Id", "ServiceName");
                ViewBag.EmployeeSelectList = new SelectList(await _employeeService.GetAll(), "Code", "Name");

                if (ModelState.IsValid)
                {
                    try
                    {

                        await _clientService.Add(client, emplyeeIds);
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        // Thêm thông báo lỗi khi thêm thất bại
                        ModelState.AddModelError(string.Empty, $"An error occurred while adding a new customer.: {ex.Message}");
                    }
                }

                ViewBag.serviceId = new SelectList(await _serviceSrv.GetAll(), "Id", "ServiceName");
                ViewBag.EmployeeSelectList = new SelectList(await _employeeService.GetAll(), "Code", "Name");
                return View(client);
            }
            return View("View404");
           
        }




        // GET: Admin/Client/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id == null)
                {
                    return NotFound();
                }

                var client = await _clientService.GetById(id);
                if (client == null)
                {
                    return NotFound();
                }
    

                ViewBag.serviceId = new SelectList(await _serviceSrv.GetAll(), "Id", "ServiceName");
                ViewBag.EmployeeSelectList = new SelectList(await _employeeService.GetAll(), "Code", "Name");

                return View(client);
            }
            return View("View404");
         
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Client client, int[] employeeIds)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                if (id != client.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    await _clientService.Update(client, employeeIds);

                    return RedirectToAction(nameof(Index));
                }
          

                ViewBag.serviceId = new SelectList(await _serviceSrv.GetAll(), "Id", "ServiceName");
                ViewBag.EmployeeSelectList = new SelectList(await _employeeService.GetAll(), "Code", "Name");

                return View(client);
            }
            return View("View404");
        
        }

        // GET: Admin/Client/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (User.IsInRole("Admin") || User.IsInRole("Manager"))
            {
                await _clientService.Delete(id);
                return RedirectToAction("Index");
            }
            return View("View404");
          
        }


    
    }
}
