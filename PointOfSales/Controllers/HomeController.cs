using System;
using System.Collections.Generic;
using System.Dynamic;
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
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            List<ItemModel> items = new List<ItemModel>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT Name, Price, Stock FROM Items";
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
                                Name = sdr["Name"].ToString(),
                                Price = Convert.ToInt32(sdr["Price"]),
                                Stock = Convert.ToInt32(sdr["Stock"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            List<CartModel> my_carts = new List<CartModel>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT Items.Name, Items.Price, Carts.Amount FROM Items INNER JOIN Carts ON Carts.ItemId = Items.ItemId WHERE Carts.TransactionId IS NULL";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            ItemModel my_item = new ItemModel
                            {
                                Name = sdr["Name"].ToString(),
                                Price = Convert.ToInt32(sdr["Price"])
                            };

                            my_carts.Add(new CartModel
                            {
                                Item = my_item,
                                Amount = Convert.ToInt32(sdr["Amount"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            dynamic models = new ExpandoObject(); 
            models.Items = items;
            models.Carts = my_carts;

            return View(models);
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
            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "INSERT INTO Items (ItemId, Name, Price, Stock, Image) VALUES (null, '" + 
                    brnama + "', " + brharga + ", " +  brstok + "," + "'kosong'" + ")";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {

                        }
                    }
                    con.Close();
                }
            }
            return RedirectToAction("StuffList");
        }

        public ActionResult Report()
        {
            ViewBag.Message = "Report page.";

            string constr = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;

            List<TransactionModel> transactions = new List<TransactionModel>();
            using (MySqlConnection con = new MySqlConnection(constr))
            {
                string query = "SELECT TransactionId, TotalPrice, CreatedAt FROM Transactions";
                using (MySqlCommand cmd = new MySqlCommand(query))
                {
                    cmd.Connection = con;
                    con.Open();
                    using (MySqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            transactions.Add(new TransactionModel
                            {
                                TransactionId = Convert.ToInt32(sdr["TransactionId"]),
                                TotalPrice = Convert.ToInt32(sdr["TotalPrice"]),
                                CreatedAt = Convert.ToDateTime(sdr["CreatedAt"])
                            });
                        }
                    }
                    con.Close();
                }
            }

            return View(transactions);
        }
    }
}