using BusinessLayer.Concrete;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBlogSitesi.Controllers
{
    public class BlogController : Controller
    {
        BlogManager bm = new BlogManager(new EFBlogDal());
        CommentManager cm = new CommentManager(new EFCommentDal());
        // GET: Blog
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult BlogList(int page=1)
        {

            var bloglist = bm.GetList().ToPagedList(
                page,6);
            return PartialView(bloglist);
        }
        [AllowAnonymous]
        public PartialViewResult FeaturedPosts()
        {
            //1.Post
            var post27 = bm.GetList().FirstOrDefault(z => z.BlogID == 27 && z.CategoryID == 2);
            if (post27 != null)
            {
                ViewBag.blogpostid1 = post27.BlogID;
                ViewBag.posttitle1 = post27.BlogTitle;
                ViewBag.postimage1 = post27.BlogImage;
                ViewBag.blogdate1 = post27.BlogDate;
            }

            //2.Post
            var post28 = bm.GetList().FirstOrDefault(z => z.BlogID == 28 && z.CategoryID == 1);
            if (post28 != null)
            {
                ViewBag.blogpostid2 = post28.BlogID;
                ViewBag.posttitle2 = post28.BlogTitle;
                ViewBag.postimage2 = post28.BlogImage;
                ViewBag.blogdate2 = post28.BlogDate;
            }


            //3.Post
            var post18 = bm.GetList().FirstOrDefault(z => z.BlogID == 18 && z.CategoryID == 2);
            if (post18 != null)
            {
                ViewBag.blogpostid3 = post18.BlogID;
                ViewBag.posttitle3 = post18.BlogTitle;
                ViewBag.postimage3 = post18.BlogImage;
                ViewBag.blogdate3 = post18.BlogDate;
            }


            //4.Post
            var post22 = bm.GetList().FirstOrDefault(z => z.BlogID == 22 && z.CategoryID == 1);
            if (post22 != null)
            {
                ViewBag.blogpostid4 = post22.BlogID;
                ViewBag.posttitle4 = post22.BlogTitle;
                ViewBag.postimage4 = post22.BlogImage;
                ViewBag.blogdate4 = post22.BlogDate;
            }



            //5.Post
            var post23 = bm.GetList().FirstOrDefault(z => z.BlogID == 23 && z.CategoryID == 1);
            if (post23 != null)
            {
                ViewBag.blogpostid5 = post23.BlogID;
                ViewBag.posttitle5 = post23.BlogTitle;
                ViewBag.postimage5 = post23.BlogImage;
                ViewBag.blogdate5 = post23.BlogDate;
            }

            return PartialView();
        }
        public PartialViewResult OtherFeaturedPosts()
        {
            return PartialView();
        }
        [AllowAnonymous]
        public ActionResult BlogDetails()
        {
            return View();
        }
        [AllowAnonymous]
        public PartialViewResult BlogCover(int id)
        {
            var blogdetailslist = bm.GetBlogByID(id);
            return PartialView(blogdetailslist);
        }
        [AllowAnonymous]
        public PartialViewResult BlogReadAll(int id)
        {
            var blogdetailslist = bm.GetBlogByID(id);

            return PartialView(blogdetailslist);
        }
        [AllowAnonymous]
        public ActionResult BlogByCategory(int id)
        {
            var bloglistbycategory = bm.GetBlogByCategory(id);
            var categoryname = bm.GetBlogByCategory(id).Select(y => 
            y.Category.CategoryName).FirstOrDefault();
            ViewBag.CategoryName = categoryname;

            var categorydesc = bm.GetBlogByCategory(id).Select(y =>
            y.Category.CategoryDescription).FirstOrDefault();
            ViewBag.categorydesc = categorydesc;

            return View(bloglistbycategory);
        }
      
        public ActionResult AdminBlogList()
        {
            var bloglist = bm.GetList();
            return View(bloglist);
        }
        public ActionResult AdminBlogList2()
        {
            var bloglist = bm.GetList();
            return View(bloglist);
        }
       [Authorize(Roles ="A")]
        [HttpGet]
        public ActionResult AddNewBlog()
        {
            Context c = new Context();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.values = values;
            
            List<SelectListItem> values2 = (from x in c.Authors.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.AuthorName,
                                               Value = x.AuthorID.ToString()
                                           }).ToList();
            ViewBag.values2 = values2;
            return View();
        }
        [HttpPost]
        public ActionResult AddNewBlog(Blog p) 
        {
            bm.TAdd(p);
            return RedirectToAction("AdminBlogList");
        }
        public ActionResult DeleteBlog(int id)
        {
            Blog blog = bm.GetByID(id);
            bm.TDelete(blog);
            return RedirectToAction("AdminBlogList");
        }
        [HttpGet]
        public ActionResult UpdateBlog(int id)
        {
            Context c = new Context();
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.values = values;

            List<SelectListItem> values2 = (from x in c.Authors.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.AuthorName,
                                                Value = x.AuthorID.ToString()
                                            }).ToList();
            ViewBag.values2 = values2;
            Blog blog = bm.GetByID(id);
            return View(blog);
        }
        [HttpPost]
        public ActionResult UpdateBlog(Blog p)
        {
            bm.TUpdate(p);
            return RedirectToAction("AdminBlogList");
        }
        public ActionResult GetCommentByBlog(int id)
        {
           
            var commentlist = cm.CommentByBlog(id);
            return PartialView(commentlist);
        }
        public ActionResult AuthorBlogList(int id)
        {
           
            var blogs = bm.GetBlogByAuthor(id);
            return View(blogs);
        }

    }
}