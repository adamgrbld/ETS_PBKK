using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using PointOfSales.Models;

namespace PointOfSales.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult StuffList()
        {
            ViewBag.Message = "Your application description page.";

            List<ItemModel> items = new List<ItemModel>();
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT ItemId, Name FROM Items";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            items.Add(new ItemModel
                            {
                                ItemId = Convert.ToInt32(sdr["ItemId"]),
                                Name = sdr["Name"].ToString(),
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(items);
        }

        [HttpPost]
        public ActionResult createBarang(string brnama, int brharga, int brstok, HttpPostedFileBase brimage)
        {
            System.Diagnostics.Debug.WriteLine(brnama);
            return View("StuffList");
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}