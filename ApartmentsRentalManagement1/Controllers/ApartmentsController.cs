using System.Net;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using ApartmentsRentalManagement1.Models;

namespace ApartmentsRentalManagement1.Controllers
{
    public class ApartmentsController : Controller
    {
        BusinessObjects objects = new BusinessObjects();

        // GET: Apartments
        public ActionResult Index()
        {
            List<Apartment> apartments = objects.GetApartments();

            return View(apartments);
        }

        // GET: Apartments/Details/5
        public ActionResult Details(int id)
        {
            Apartment residence = null;

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdApartments = new SqlCommand("SELECT ApartmentID, UnitNumber, Bedrooms, " +
                                                          "       Bathrooms, MonthlyRate, " +
                                                          "       SecurityDeposit, OccupancyStatus " +
                                                          "FROM Management.Apartments " +
                                                          "WHERE ApartmentID = " + id + ";",
                                                          scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdApartments.ExecuteNonQuery();

                SqlDataAdapter sdaApartments = new SqlDataAdapter(cmdApartments);
                DataSet dsApartments = new DataSet("apartment");

                sdaApartments.Fill(dsApartments);

                if (dsApartments.Tables[0].Rows.Count > 0)
                {
                    residence = new Apartment()
                    {
                        ApartmentID = int.Parse(dsApartments.Tables[0].Rows[0][0].ToString()),
                        UnitNumber = dsApartments.Tables[0].Rows[0][1].ToString(),
                        Bedrooms = int.Parse(dsApartments.Tables[0].Rows[0][2].ToString()),
                        Bathrooms = int.Parse(dsApartments.Tables[0].Rows[0][3].ToString()),
                        MonthlyRate = int.Parse(dsApartments.Tables[0].Rows[0][4].ToString()),
                        SecurityDeposit = int.Parse(dsApartments.Tables[0].Rows[0][5].ToString()),
                        OccupancyStatus = dsApartments.Tables[0].Rows[0][6].ToString()
                    };
                }
            }

            return View(residence);
        }

        // GET: Apartments/Create
        public ActionResult Create()
        {
            List<SelectListItem> conditions = new List<SelectListItem>();

            conditions.Add(new SelectListItem() { Text = "Unknown", Value = "Unknown" });
            conditions.Add(new SelectListItem() { Text = "Occupied", Value = "Occupied" });
            conditions.Add(new SelectListItem() { Text = "Available", Value = "Available" });
            conditions.Add(new SelectListItem() { Text = "Not Ready", Value = "Not Ready" });
            conditions.Add(new SelectListItem() { Text = "Needs Maintenance", Value = "Needs Maintenance" });

            ViewBag.OccupancyStatus = conditions;

            return View();
        }

        // POST: Apartments/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                 ConfigurationManager.
                                                                                 ConnectionStrings["csApartmentsRentalManagement"].
                                                                                 ConnectionString))
                {
                    SqlCommand cmdApartments = new SqlCommand("INSERT INTO Management.Apartments(UnitNumber, Bedrooms, Bathrooms, " +
                                                              "                                  MonthlyRate, SecurityDeposit, " +
                                                              "                                  OccupancyStatus) " +
                                                             "VALUES(N'" + collection["UnitNumber"] + "', " + collection["Bedrooms"] +
                                                             ", " + collection["Bathrooms"] + ", " + collection["MonthlyRate"] + ", " +
                                                             collection["SecurityDeposit"] + ", N'" + collection["OccupancyStatus"] + "');",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdApartments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Apartments/Edit/5
        public ActionResult Edit(int id)
        {
            Apartment residence = null;

            using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                            Configuration.
                                                                            ConfigurationManager.
                                                                            ConnectionStrings["csApartmentsRentalManagement"].
                                                                            ConnectionString))
            {
                SqlCommand cmdApartments = new SqlCommand("SELECT ApartmentID, UnitNumber, Bedrooms, " +
                                                          "       Bathrooms, MonthlyRate, " +
                                                          "       SecurityDeposit, OccupancyStatus " +
                                                          "FROM Management.Apartments " +
                                                          "WHERE ApartmentID = " + id + ";",
                                                          scApartmentsManagement);

                scApartmentsManagement.Open();
                cmdApartments.ExecuteNonQuery();

                SqlDataAdapter sdaApartments = new SqlDataAdapter(cmdApartments);
                DataSet dsApartments = new DataSet("apartment");

                sdaApartments.Fill(dsApartments);

                if (dsApartments.Tables[0].Rows.Count > 0)
                {
                    residence = new Apartment()
                    {
                        ApartmentID = int.Parse(dsApartments.Tables[0].Rows[0][0].ToString()),
                        UnitNumber = dsApartments.Tables[0].Rows[0][1].ToString(),
                        Bedrooms = int.Parse(dsApartments.Tables[0].Rows[0][2].ToString()),
                        Bathrooms = int.Parse(dsApartments.Tables[0].Rows[0][3].ToString()),
                        MonthlyRate = int.Parse(dsApartments.Tables[0].Rows[0][4].ToString()),
                        SecurityDeposit = int.Parse(dsApartments.Tables[0].Rows[0][5].ToString()),
                        OccupancyStatus = dsApartments.Tables[0].Rows[0][6].ToString()
                    };
                }
            }

            List<SelectListItem> conditions = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Unknown",      Value = "Unknown",      Selected = (residence.OccupancyStatus == "Unknown")      },
                new SelectListItem() { Text = "Occupied",     Value = "Occupied",     Selected = (residence.OccupancyStatus == "Occupied")     },
                new SelectListItem() { Text = "Available",    Value = "Available",    Selected = (residence.OccupancyStatus == "Available")    },
                new SelectListItem() { Text = "Not Ready",    Value = "Not Ready",    Selected = (residence.OccupancyStatus == "Not Ready")    },
                new SelectListItem() { Text = "Needs Maintenance", Value = "Needs Maintenance", Selected = (residence.OccupancyStatus == "Needs Maintenance") }
            };

            ViewBag.OccupancyStatus = conditions;

            return View(residence);
        }

        // POST: Apartments/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                 ConfigurationManager.
                                                                                 ConnectionStrings["csApartmentsRentalManagement"].
                                                                                 ConnectionString))
                {
                    SqlCommand cmdApartments = new SqlCommand("UPDATE Management.Apartments " +
                                                              "SET   UnitNumber      = N'" + collection["UnitNumber"] + "', " +
                                                              "      Bedrooms        =   " + collection["Bedrooms"] + ",  " +
                                                              "      Bathrooms       =   " + collection["Bathrooms"] + ",  " +
                                                              "      MonthlyRate     =   " + collection["MonthlyRate"] + ",  " +
                                                              "      SecurityDeposit =   " + collection["SecurityDeposit"] + ",  " +
                                                              "      OccupancyStatus = N'" + collection["OccupancyStatus"] + "'  " +
                                                              "WHERE ApartmentID     =   " + id + ";",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdApartments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Apartments/Delete/5
        public ActionResult Delete(int id)
        {
            Apartment residence = null;

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                             ConfigurationManager.
                                                                             ConnectionStrings["csApartmentsRentalManagement"].
                                                                             ConnectionString))
            {
                SqlCommand cmdApartments = new SqlCommand("SELECT ApartmentID, UnitNumber, Bedrooms, " +
                                                          "       Bathrooms, MonthlyRate, " +
                                                          "       SecurityDeposit, OccupancyStatus " +
                                                          "FROM Management.Apartments " +
                                                          "WHERE ApartmentID = " + id + ";",
                                                          scRentManagement);
                scRentManagement.Open();

                SqlDataAdapter sdaApartments = new SqlDataAdapter(cmdApartments);
                DataSet dsApartments = new DataSet("apartments");

                sdaApartments.Fill(dsApartments);

                if (dsApartments.Tables[0].Rows.Count > 0)
                {
                    residence = new Apartment()
                    {
                        ApartmentID = int.Parse(dsApartments.Tables[0].Rows[0][0].ToString()),
                        UnitNumber = dsApartments.Tables[0].Rows[0][1].ToString(),
                        Bedrooms = int.Parse(dsApartments.Tables[0].Rows[0][2].ToString()),
                        Bathrooms = int.Parse(dsApartments.Tables[0].Rows[0][3].ToString()),
                        MonthlyRate = int.Parse(dsApartments.Tables[0].Rows[0][4].ToString()),
                        SecurityDeposit = int.Parse(dsApartments.Tables[0].Rows[0][5].ToString()),
                        OccupancyStatus = dsApartments.Tables[0].Rows[0][6].ToString()
                    };
                }
            }

            return residence == null ? HttpNotFound() : (ActionResult)View(residence);
        }

        // POST: Apartments/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                 ConfigurationManager.
                                                                                 ConnectionStrings["csApartmentsRentalManagement"].
                                                                                 ConnectionString))
                {
                    SqlCommand cmdApartments = new SqlCommand("DELETE Management.Apartments " +
                                                              "WHERE ApartmentID = " + id + ";",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdApartments.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
