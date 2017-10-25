using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GOL.Models;
using Newtonsoft.Json;

namespace GOL.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Index the specified model.
        /// </summary>
        /// <returns>The index.</returns>
        /// <param name="model">Model.</param>
        public IActionResult Index(HomeModel model)
        {
            if(model == null)
                model = new HomeModel();

            return View(model);
        }

        /// <summary>
        /// Indexs the post.
        /// </summary>
        /// <returns>The post.</returns>
        /// <param name="model">Model.</param>
        [HttpPost]
        public IActionResult IndexPost(HomeModel model)
        {
            if (!ModelState.IsValid)
                return Index(model);

            return RedirectToAction("Grid", "Home", new {columns = model.Columns, rows = model.Rows});
        }

        /// <summary>
        /// Grid the specified columns and rows.
        /// </summary>
        /// <returns>The grid.</returns>
        /// <param name="columns">Columns.</param>
        /// <param name="rows">Rows.</param>
        public IActionResult Grid(int columns, int rows)
        {
            var model = new GridModel(columns, rows);

            return View(model);
        }

        /// <summary>
        /// Update to move the grids state 
        /// </summary>
        /// <returns>The update.</returns>
        /// <param name="cells">Cells.</param>
		[HttpPost]
		public string GridUpdate([FromBody] int[,] cells)
		{
            var gridModel = new GridModel(cells);

            gridModel.NextGeneration();

            return JsonConvert.SerializeObject(gridModel.Cells);
		}

        /// <summary>
        /// Error this instance.
        /// </summary>
        /// <returns>The error.</returns>
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
