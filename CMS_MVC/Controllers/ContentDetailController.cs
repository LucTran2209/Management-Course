using CMS_MVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CMS_MVC.Controllers
{
    public class ContentDetailController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateDetail(int contentId)
        {
            ViewBag.ContentId = contentId;
            return View();
        }

        public async Task<IActionResult> CreateAsync(int courseContentId, string title, string detail, string? contentPath)
        {
            var de = new ContentDetail();
            de.Title = title;
            de.Detail = detail == null ? contentPath : detail;
            de.CourseContentId= courseContentId;

            using (var client = new HttpClient())
            {
                try
                {
                    if (HttpContext.Session.GetInt32("UserId") != null)
                    {
                        using (var response = await client.PostAsJsonAsync("https://localhost:7175/api/ContentDetail", de))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                return Redirect("../ContentCourse?courseId="+ HttpContext.Session.GetInt32("courseId"));
                            }
                            else
                            {
                                return View();
                            }
                        }
                    }
                    else
                    {
                        return Redirect("/Login");
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }          
        }

    }
}
