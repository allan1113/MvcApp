﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApp.Models.Interfaces;
using MvcApp.Models.Service;
using MvcApp.Models;
using MvcApp.Models.MyModels;

namespace MvcApp.Controllers
{
    public class ManagerController : Controller
    {
        //
        // GET: /Manager/
        IManagerService managerService;

        public ManagerController()
        {
            managerService = new ManagerService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public JsonResult login(string userName, string password)
        {
            bool sta = managerService.login(userName, password);
            string state = "true";

            if (sta == false)
            {
                state = "false";
                
            }
            return Json(state);
        }

        public string addManager()
        {
            return "addManager";
        }

        public string deleteManager(int id)
        {
            managerService.deleteManager(id);
            return "deleteManager";
        }

        public string changeManager(int id)
        {
            return "changeManager";
        }

        public ActionResult getManagerByManName(string manName)
        {
            MyManager managerSel = managerService.getManagerByManName(manName);
            return Json(managerSel, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getAllManagers()
        {
            List<Manager> allManager = managerService.getAllManagers();
            return Json(allManager,JsonRequestBehavior.AllowGet);
        }
    }
}
