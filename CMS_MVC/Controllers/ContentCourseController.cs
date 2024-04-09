using CMS_MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMS_MVC.Controllers
{
    public class ContentCourseController : Controller
    {
        public readonly string url = "https://localhost:7175/api/ContentCourse";
        public readonly string url1 = "https://localhost:7175/api/course";
        public readonly string url2 = "https://localhost:7175/api/contenttype";
        public readonly string url3 = "https://localhost:7175/api/ContentDetail";

        /// <summary>
        /// Content detail by courseId
        /// </summary>
        /// <param name="courseId"></param>
        /// <returns></returns>
        public async Task<IActionResult> IndexAsync(int courseId)
        {
            HttpContext.Session.SetInt32("courseId", courseId);
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(url + "/?courseId=" + courseId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<ContentCourse> list = JsonConvert.DeserializeObject<List<ContentCourse>>(data);

                                ViewBag.ListContentCourse = list;
                               
                            }
                        }
                    }

                    using (var response = await client.GetAsync(url1 + "/GetCourseById/" + courseId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                                if (list != null && list.Count() != 0)
                                {
                                    ViewBag.ListCourse = list;
                                }
                            }
                        }
                    }

                    using (var response = await client.GetAsync(url3))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<ContentDetail> list = JsonConvert.DeserializeObject<List<ContentDetail>>(data);

                                if (list != null && list.Count() != 0)
                                {
                                    ViewBag.ContentDetails = list;
                                }
                            }
                        }
                    }


                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        public async Task<IActionResult> CreateViewAsync()
        {
            using (var client = new HttpClient())
            {
                try
                {
                    using (var response = await client.GetAsync(url2))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<ContentType> list = JsonConvert.DeserializeObject<List<ContentType>>(data);

                                if (list != null && list.Count() != 0)
                                {
                                    ViewBag.Categories = list;
                                }
                                return View();
                            }
                        }
                        return View();
                    }
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(string title, int contentTypeId, string? content, string? contentPath)
        {
            var c = new ContentCourseInput();
            c.CourseId = (int)HttpContext.Session.GetInt32("courseId");
            c.ContentTitle = title;
            c.ContentTypeId = contentTypeId;
            c.Content = contentTypeId == 1 ? "~/file/" + contentPath : content;

            using (var client = new HttpClient())
            {
                try
                {
                    if (HttpContext.Session.GetInt32("UserId") != null)
                    {
                        using (var response = await client.PostAsJsonAsync(url, c))
                        {
                            if (response.IsSuccessStatusCode)
                            {
                                HttpContext.Session.SetString("ResultCreate", "A New ccontent has been created");
                                return Redirect("index?courseId=" + c.CourseId);
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

        public async Task<IActionResult> ContentDetailAsync(int courseId)
        {
            using (var client = new HttpClient())
            {
                try
                {                 
                    using (var response = await client.GetAsync(url1 + "/GetCourseById/" + courseId))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent context = response.Content)
                            {
                                string data = await context.ReadAsStringAsync();

                                List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                                if (list != null && list.Count() != 0)
                                {
                                    ViewBag.Course = list;
                                }
                            }
                        }
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }


        [HttpPost]
        public async Task<IActionResult> Update(string status)
        {
            using (var client = new HttpClient())
            {
                try 
                {
                    using (var response = await client.GetAsync("https://localhost:7175/api/Course/update/" + status + "/" + HttpContext.Session.GetInt32("courseId")))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            return Redirect("../contentcourse?courseId=" + HttpContext.Session.GetInt32("courseId"));
                            //using (HttpContent context = response.Content)
                            //{
                            //    string data = await context.ReadAsStringAsync();

                            //    List<Course> list = JsonConvert.DeserializeObject<List<Course>>(data);

                            //    if (list != null && list.Count() != 0)
                            //    {
                            //        ViewBag.Course = list;
                            //    }
                            //}
                        }
                    }
                    return View();
                }
                catch (Exception ex)
                {
                    return View(ex);
                }
            }
        }
    }
}
