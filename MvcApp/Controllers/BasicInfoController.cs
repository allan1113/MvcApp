﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services;
using MvcApp.Models.Service;
using MvcApp.Models;
using MvcApp.Models.Interfaces;
using MvcApp.Models.MyModels;

namespace MvcApp.Controllers
{
    public class BasicInfoController : Controller
    {
        //
        // GET: /BasicInfo/
        IBasicInfoService basicInfoService;
        ILocationService locationService;
        IEmergencyInfoService emergencyInfoService;
        IHealthPlanService healthPlanService;

        public BasicInfoController()
        {
            basicInfoService = new BasicInfoService();
            locationService = new LocationService();
            emergencyInfoService = new EmergencyInfoService();
            healthPlanService = new HealthPlanService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login(string userName, string password) //用户登录
        {
            bool sta = basicInfoService.login(userName, password);
            string state = "true";

            if (sta == false)
            {
                state = "false";

            }
            return Json(state);
            //return RedirectToAction("BasicInfo", "Home");
        }

        public string addUser() //添加用户  
        {
            BasicInfo basicInfo = BasicInfo.CreateBasicInfo(3, "caocan", 22, true, false, "12333333333", "qy", "none", "play", "test", "test");
            basicInfoService.addUser(basicInfo);
            return "addUser";
        }

        public string deleteUser(int id) //删除用户
        {
            basicInfoService.deleteUser(id);
            return "deleteUser";
        }

        public string changeUser(int id) //更改用户信息
        {
            BasicInfo userToChange = BasicInfo.CreateBasicInfo(3, "hehe", 22, true, true, "13666666666", "chengdu", "none", "play", "test", "123456");
            basicInfoService.changeUser(id, userToChange);
            return "changeUser";
        }

        public ActionResult getUserByUserName(string userName) //获取用户信息
        {
            MyBasicInfo userSel = basicInfoService.getUserByUserName(userName);

            return Json(userSel, JsonRequestBehavior.AllowGet);
        }

        public string getAllUsers() //获取所有用户
        {
            List<BasicInfo> allUser = basicInfoService.getAllUsers();
            return "getAllUsers";
        }

        public ActionResult getLocationByUserId(int id) //获取用户活动信息
        {
            BasicInfo test = basicInfoService.getUserById(id);
            List<Location> locations = locationService.getByUser(test);
            return Json(locations, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getEmergencyInfoByUserId(int id) //获取用户急救信息
        {
            BasicInfo test = basicInfoService.getUserById(id);
            List<EmergencyInfo> emergencyInfos = emergencyInfoService.getByUser(test);
            return Json(emergencyInfos, JsonRequestBehavior.AllowGet);
        }

        public ActionResult getHealthPlanByUserId(int id) //获取用户健康计划
        {
            BasicInfo test = basicInfoService.getUserById(id);
            List<HealthPlan> healthPlans = healthPlanService.getByUser(test);
            return Json(healthPlans, JsonRequestBehavior.AllowGet);
        }

        public JsonResult searchForEmergencyInfo(int userId, string bTime, string eTime) //根据日期搜索来获取用户急救信息
        {
            //DateTime beginTime = new DateTime(2011, 11, 10);
            //DateTime endTime = new DateTime(2012, 12, 11);

            DateTime beginTime = DateTime.Parse(bTime);
            DateTime endTime = DateTime.Parse(eTime);
            List<MyEmergencyInfo> emergencyInfoList = new List<MyEmergencyInfo>(); //所有数据存到list中
            emergencyInfoList = emergencyInfoService.searchForEmergencyInfo(userId, beginTime, endTime);
            return Json(emergencyInfoList, JsonRequestBehavior.AllowGet);
        }
    }
}