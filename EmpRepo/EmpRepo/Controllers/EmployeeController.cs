using EmpRepo.Models;
using EmpRepo.Repository.Context;
using EmpRepo.Repository.Contract;
using EmpRepo.Repository.Contract.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmpRepo.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            IUnitOfWork iu = new UnitOfWork(new EmpContext());
            List<Employee> lc = iu.Employees.GetAll().ToList();
            return View(lc);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee e)
        {
            if (ModelState.IsValid)
            {
                IUnitOfWork iu = new UnitOfWork(new EmpContext());
                iu.Employees.Add(e);
                iu.Complete();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Edit(string id)
        {
            IUnitOfWork iu = new UnitOfWork(new EmpContext());
            Employee e = iu.Employees.Find(c => c.EmployeeID == id).FirstOrDefault();
            return View(e);
        }

        [HttpPost]
        public ActionResult Edit(Employee e)
        {
            if (ModelState.IsValid)
            {
                IUnitOfWork iu = new UnitOfWork(new EmpContext());
                iu.Employees.Modify(e);
                iu.Complete();
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }

        public ActionResult Details(string id)
        {
            IUnitOfWork iu = new UnitOfWork(new EmpContext());
            Employee e = iu.Employees.Find(c => c.EmployeeID == id).FirstOrDefault();
            return View(e);
        }

        public ActionResult Delete(string id)
        {
            IUnitOfWork iu = new UnitOfWork(new EmpContext());
            Employee e = iu.Employees.Find(c => c.EmployeeID == id).FirstOrDefault();
            iu.Employees.Remove(e);
            iu.Complete();
            return RedirectToAction("Index");
        }
    }
}