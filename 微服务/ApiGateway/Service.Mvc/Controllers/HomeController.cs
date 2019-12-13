using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Mvc.Models;

namespace Service.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbBeginStudyContext myContext;
        public HomeController(DbBeginStudyContext dbBeginStudy)
        {
            myContext = dbBeginStudy;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        #region 列表页

        public async Task<IActionResult> ListShow()
        {
            return View(await myContext.ZouNi.ToListAsync());
        }
        #endregion

        #region 创建页

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("B")] ZouNi zouNi)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    myContext.Add(zouNi);
                    await myContext.SaveChangesAsync();
                    return RedirectToAction(nameof(ListShow));
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " +
            "Try again, and if the problem persists " +
            "see your system administrator.");
            }
            return View(zouNi);
        }
        #endregion

        #region 删除页

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <param name="saveChangesError"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id,bool? saveChangesError = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zouNi = await myContext.ZouNi
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.A == id);
            if (zouNi == null)
            {
                return NotFound();
            }

            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                    "Delete failed. Try again, and if the problem persists " +
                    "see your system administrator.";
            }

            return View(zouNi);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await myContext.ZouNi.FindAsync(id);
            if (student == null)
            {
                return RedirectToAction(nameof(Index));
            }

            try
            {
                myContext.ZouNi.Remove(student);
                await myContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(Delete), new { id = id, saveChangesError = true });
            }
        }
        #endregion

        #region 详情页

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var zouNi = await myContext.ZouNi
                .FirstOrDefaultAsync(s => s.A == id);

            if (zouNi == null)
                return NotFound();

            return View(zouNi);
        }
        #endregion

        #region 编辑页

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var zouNi = await myContext.ZouNi.FindAsync(id);
            if (zouNi == null)
                return NotFound();

            return View(zouNi);
        }

        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var zouNiToUpdate = await myContext.ZouNi.FirstOrDefaultAsync(s => s.A == id);
            if (await TryUpdateModelAsync<ZouNi>(
                zouNiToUpdate,
                "",
                s => s.B))
            {
                try
                {
                    await myContext.SaveChangesAsync();
                    return RedirectToAction(nameof(ListShow));
                }
                catch (DbUpdateException /* ex */)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(zouNiToUpdate);
        }

        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
