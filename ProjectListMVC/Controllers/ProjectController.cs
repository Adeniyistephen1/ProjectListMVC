using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectListMVC.Models;

namespace ProjectListMVC.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ApplicationDbContext _db;
        [BindProperty]
        public Project Project { get; set; }
        public ProjectController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            //Project as an instance of project, for a new project... 
            Project = new Project();
            if(id == null)
            {
                //create
                return View(Project);
            }
            //Update
            Project = _db.Projects.FirstOrDefault(u => u.Id == id);
            if(Project == null)
            {
                return NotFound();
            }
            return View(Project);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if(Project.Id == 0)
                {
                    //Create
                    _db.Projects.Add(Project);
                }
                else
                {
                    _db.Projects.Update(Project);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Project);
        }
              
        #region API Calls

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Projects.ToListAsync()});
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var projectFromDb = await _db.Projects.FirstOrDefaultAsync(u => u.Id == id);
            if (projectFromDb == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }
            _db.Projects.Remove(projectFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successful" });
        }
        #endregion
    }
}