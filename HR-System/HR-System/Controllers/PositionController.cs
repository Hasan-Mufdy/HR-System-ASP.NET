using HR_System.Models;
using HR_System.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HR_System.Controllers
{
    public class PositionController : Controller
    {
        private readonly IPosition _position;

        public PositionController(IPosition position)
        {
            _position = position;
        }
        public async Task<IActionResult> Index()
        {
            var positions = await _position.GetAllPositions();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Position position)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(position);
            //}

            await _position.PostPosition(position);
            return RedirectToAction("Index", "Employee");
        }
    }
}
