using Microsoft.AspNetCore.Mvc;
using Lab5Lib;
using Microsoft.AspNetCore.Authorization;

namespace Lab5.Controllers
{
    public class LabsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Lab1()
        {
            return View();
        }
        public IActionResult Lab1Run(string inputValue) 
        {
            try
            {
                string res = Lab1Lib.Lab1Res(inputValue);
                ViewBag.Result = res;
                return View("Lab1");
            }
            catch(Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Lab1");
            }
        }
        public IActionResult Lab2()
        {
            return View();
        }
        public IActionResult Lab2Run(string inputValue)
        {
            try
            {
                string res = Lab2Lib.Lab2Res(inputValue);
                ViewBag.Result = res;
                return View("Lab2");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Lab2");
            }
        }
        public IActionResult Lab3()
        {
            return View();
        }
        public IActionResult Lab3Run(string inputValue)
        {
            try
            {
                string res = Lab3Lib.Lab3Res(inputValue);
                ViewBag.Result = res;
                return View("Lab3");
            }
            catch (Exception ex)
            {
                ViewBag.Exception = ex;
                return View("Lab3");
            }
        }
    }
}
