using EGWebApplication3.Models;
using EGWebApplication3.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace EGWebApplication3.Controllers
{

   
    public class HomeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IHostingEnvironment _hostingEnvironment;

        public HomeController(IEmployeeRepository employeeRepository,
                                IHostingEnvironment hostingEnvironment )
        {
            _employeeRepository = employeeRepository;
            _hostingEnvironment = hostingEnvironment;
        }
      
        public ViewResult Index()
        {

            var model = _employeeRepository.GetAllEmployees();
            return View("~/Views/Home/index.cshtml",model);
            //return _employeeRepository.GetEmployee(101).Name;

        }

        //public JsonResult Details()
        //{
        //    Employee model = _employeeRepository.GetEmployee(101);
        //    return Json(model);
        //}

        [Authorize]
        public ViewResult Details(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);

            if (employee == null)
            {
                Response.StatusCode = 404;
                return View("EmployeeNotFound", id);
            }


            HomeDetailsViewModel homeDetailsViewModel = new HomeDetailsViewModel()
            {
                Employee = employee,
                PageTitle = "Employee Details"
            };




            //   Employee model = _employeeRepository.GetEmployee(101);
            //// ViewBag.Employee = model;
            //   ViewBag.PageTitle = "Employee Details";

            return View( homeDetailsViewModel);
        }

        [HttpGet]
        [Authorize]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeCreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = ProcessUploadedFile(model);

                Employee newEmployee = new Employee
                {
                    Name = model.Name,
                    Email = model.Email,
                    Department = model.Department,
                    // Store the file name in PhotoPath property of the employee object
                    // which gets saved to the Employees database table
                    PhotoPath = uniqueFileName
                };

                _employeeRepository.Add(newEmployee);
                return RedirectToAction("details", new { id = newEmployee.Id });
            }

            return View();
        }

        [HttpGet]
        [Authorize]
        public ViewResult Edit(int id)
        {
            Employee employee = _employeeRepository.GetEmployee(id);
            EmployeeEditViewModel employeeEditViewModel = new EmployeeEditViewModel
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Department = employee.Department,
                ExistingPhotoPath = employee.PhotoPath
            };
            return View(employeeEditViewModel);
        }
        [HttpPost]
        public IActionResult Edit(EmployeeEditViewModel model)
        {
            if (ModelState.IsValid)
            {


              Employee employee = _employeeRepository.GetEmployee(model.Id);
              employee.Name = model.Name;
              employee.Email = model.Email;
              employee.Department = model.Department;
              if (model.Photo != null)
              {
                  if (model.ExistingPhotoPath != null)
                  {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath,
                                                       "images", model.ExistingPhotoPath);
                        System.IO.File.Delete(filePath);
                  } 
                  
                  employee.PhotoPath = ProcessUploadedFile(model);
              }

              //string uniqueFileName = ProcessUploadedFile(model);

           

                _employeeRepository.Update(employee);
                return RedirectToAction("Index");
            }

            return View();
        }

        private string ProcessUploadedFile(EmployeeCreateViewModel model)
        {
             string uniqueFileName= null;

             //If the Photo property on the incoming model object is not null, then the user
             //has selected an image to upload.
            if (model.Photo != null)

            {
                
                // The image must be uploaded to the images folder in wwwroot
                // To get the path of the wwwroot folder we are using the inject
                // HostingEnvironment service provided by ASP.NET Core
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");

                // To make sure the file name is unique we are appending a new
                // GUID value and and an underscore to the file name
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.Photo.FileName;

                // Use CopyTo() method provided by IFormFile interface to
                // copy the file to wwwroot/images folder
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.Photo.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
