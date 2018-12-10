using System;
using System.Net;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Collections.Generic;
using ApartmentsRentalManagement1.Models;

namespace ApartmentsRentalManagement1.Controllers
{
    public class RentContractsController : Controller
    {
        BusinessObjects objects = new BusinessObjects();

        // GET: RentContracts
        public ActionResult Index()
        {
            return View(objects.GetRentContracts());
        }

        // GET: RentContracts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RentContract contract = objects.FindRentContract(id);

            if (contract == null)
            {
                return HttpNotFound();
            }

            return View(contract);
        }

        // GET: RentContracts/Create
        public ActionResult Create()
        {
            List<SelectListItem> maritals = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Unknown",   Value = "Unknown"   },
                new SelectListItem() { Text = "Single",    Value = "Single"    },
                new SelectListItem() { Text = "Widdow",    Value = "Widdow"    },
                new SelectListItem() { Text = "Married",   Value = "Married"   },
                new SelectListItem() { Text = "Divorced",  Value = "Divorced"  },
                new SelectListItem() { Text = "Separated", Value = "Separated" }
            };

            ViewBag.MaritalStatus = maritals;

            ViewBag.ApartmentID = new SelectList(objects.GetApartments(), "ApartmentID", "Residence");
            ViewBag.EmployeeID = new SelectList(objects.GetEmployees(), "EmployeeID", "Identification");

            return View();
        }

        // POST: RentContracts/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                                Configuration.
                                                                                ConfigurationManager.
                                                                                ConnectionStrings["csApartmentsRentalManagement"].
                                                                                ConnectionString))
                {
                    // This command is used to create a rental contract.
                    SqlCommand cmdRentContracts = new SqlCommand("INSERT INTO Management.RentContracts(ContractNumber, EmployeeID, " +
                                                                 "                                     ContractDate, FirstName, " +
                                                                 "                                     LastName, MaritalStatus, " +
                                                                 "                                     NumberOfChildren, ApartmentID, " +
                                                                 "                                     RentStartDate) " +
                                                                 "VALUES(" + collection["ContractNumber"] + ", " +
                                                                 collection["EmployeeID"] + ", N'" + collection["ContractDate"] +
                                                                 "', N'" + collection["FirstName"] + "', N'" + collection["LastName"] +
                                                                 "', N'" + collection["MaritalStatus"] + "', " +
                                                                 collection["NumberOfChildren"] + ", " + collection["ApartmentID"] +
                                                                 ", N'" + collection["RentStartDate"] + "');",
                                                                 scApartmentsManagement);

                    scApartmentsManagement.Open();
                    cmdRentContracts.ExecuteNonQuery();
                }

                /* When an apartment has been selected for a rental contract, 
                 * we must change the status of that apartment from Available to Occupied. */
                using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                ConfigurationManager.
                                                                                ConnectionStrings["csApartmentsRentalManagement"].
                                                                                ConnectionString))
                {
                    SqlCommand cmdApartments = new SqlCommand("UPDATE Management.Apartments " +
                                                              "SET   OccupancyStatus = N'Occupied'  " +
                                                              "WHERE ApartmentID     =   " + collection["ApartmentID"] + ";",
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

        // GET: RentContracts/Edit/5
        public ActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            RentContract contract = objects.FindRentContract(id);

            if (contract == null)
            {
                return HttpNotFound();
            }

            List<SelectListItem> maritals = new List<SelectListItem>
            {
                new SelectListItem() { Text = "Single",    Value = "Single",    Selected = (contract.MaritalStatus == "Single")    },
                new SelectListItem() { Text = "Widdow",    Value = "Widdow",    Selected = (contract.MaritalStatus == "Widdow")    },
                new SelectListItem() { Text = "Married",   Value = "Married",   Selected = (contract.MaritalStatus == "Married")   },
                new SelectListItem() { Text = "Unknown",   Value = "Unknown",   Selected = (contract.MaritalStatus == "Unknown")   },
                new SelectListItem() { Text = "Divorced",  Value = "Divorced",  Selected = (contract.MaritalStatus == "Divorced")  },
                new SelectListItem() { Text = "Separated", Value = "Separated", Selected = (contract.MaritalStatus == "Separated") }
            };

            ViewBag.MaritalStatus = maritals;

            ViewBag.EmployeeID = new SelectList(objects.GetEmployees(), "EmployeeID", "Identification", contract.EmployeeID);
            ViewBag.ApartmentID = new SelectList(objects.GetApartments(), "ApartmentID", "Residence", contract.ApartmentID);

            return View();
        }

        // POST: RentContracts/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                                    Configuration.
                                                                                    ConfigurationManager.
                                                                                    ConnectionStrings["csApartmentsRentalManagement"].
                                                                                    ConnectionString))
                    {
                        string strUpdate = "UPDATE Management.RentContracts " +
                                           "SET   ContractNumber     =   " + collection["ContractNumber"] + ", " +
                                           "      EmployeeID         =   " + collection["EmployeeID"] + ", " +
                                           "      FirstName          = N'" + collection["FirstName"] + "', " +
                                           "      LastName           = N'" + collection["LastName"] + "', " +
                                           "      MaritalStatus      = N'" + collection["MaritalStatus"] + "', " +
                                           "      NumberOfChildren   =   " + collection["NumberOfChildren"] + ", " +
                                           "      ApartmentID        =   " + collection["ApartmentID"] + " " +
                                           "WHERE RentContractID = " + id + ";";
                        if (DateTime.Parse(collection["ContractDate"]) != new DateTime(1900, 1, 1))
                            strUpdate += "UPDATE Management.RentContracts " +
                                         "SET   ContractDate       = N'" + collection["ContractDate"] + "' " +
                                         "WHERE RentContractID = " + id + ";";
                        if (DateTime.Parse(collection["RentStartDate"]) != new DateTime(1900, 1, 1))
                            strUpdate += "UPDATE Management.RentContracts " +
                                         "SET   RentStartDate       = N'" + collection["RentStartDate"] + "' " +
                                         "WHERE RentContractID = " + id + ";";

                        SqlCommand cmdRentContracts = new SqlCommand(strUpdate,
                                                                     scApartmentsManagement);

                        scApartmentsManagement.Open();
                        cmdRentContracts.ExecuteNonQuery();
                    }

                    /* Change the status of the newly selected apartment (the apartment that has just been applied to the contract), 
                     * to Occupied (from whatever was its status). */
                    using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                                ConfigurationManager.
                                                                                ConnectionStrings["csApartmentsRentalManagement"].
                                                                                ConnectionString))
                    {
                        SqlCommand cmdApartments = new SqlCommand("UPDATE Management.Apartments " +
                                                                  "SET   OccupancyStatus = N'Occupied'  " +
                                                                  "WHERE ApartmentID     =   " + collection["ApartmentID"] + ";",
                                                                  scRentManagement);

                        scRentManagement.Open();
                        cmdApartments.ExecuteNonQuery();
                    }

                    return RedirectToAction("Index");
                }

                RentContract contract = objects.FindRentContract(id);

                List<SelectListItem> maritals = new List<SelectListItem>
                {
                    new SelectListItem() { Text = "Single", Value = "Single" },
                    new SelectListItem() { Text = "Widdow", Value = "Widdow" },
                    new SelectListItem() { Text = "Married", Value = "Married" },
                    new SelectListItem() { Text = "Unknown", Value = "Unknown" },
                    new SelectListItem() { Text = "Divorced", Value = "Divorced" },
                    new SelectListItem() { Text = "Separated", Value = "Separated" }
                };

                ViewBag.MaritalStatus = maritals;

                ViewBag.EmployeeID  = new SelectList(objects.GetEmployees(),  "EmployeeID",  "Identification", contract.EmployeeID);
                ViewBag.ApartmentID = new SelectList(objects.GetApartments(), "ApartmentID", "Residence",      contract.ApartmentID);

                return View(contract);
            }
            catch
            {
                return View();
            }
        }

        // GET: RentContracts/Delete/5
        public ActionResult Delete(int ?id)
        {
            RentContract contract = null;

            if (id == 0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (SqlConnection scRentManagement = new SqlConnection(System.Configuration.
                                                                             ConfigurationManager.
                                                                             ConnectionStrings["csApartmentsRentalManagement"].
                                                                             ConnectionString))
            {
                SqlCommand cmdRentContracts = new SqlCommand("SELECT RentContractID, ContractNumber, " +
                                                             "       EmployeeID, ContractDate, " +
                                                             "       FirstName, LastName, " +
                                                             "       MaritalStatus, NumberOfChildren, " +
                                                             "       ApartmentID, RentStartDate " +
                                                             "FROM Management.RentContracts " +
                                                             "WHERE RentContractID = " + id + ";",
                                                             scRentManagement);
                scRentManagement.Open();

                SqlDataAdapter sdaRentContracts = new SqlDataAdapter(cmdRentContracts);
                DataSet dsRentContracts = new DataSet("rent-contracts");

                sdaRentContracts.Fill(dsRentContracts);

                if (dsRentContracts.Tables[0].Rows.Count > 0)
                {
                    contract = new RentContract()
                    {
                        RentContractID = int.Parse(dsRentContracts.Tables[0].Rows[0][0].ToString()),
                        ContractNumber = int.Parse(dsRentContracts.Tables[0].Rows[0][1].ToString()),
                        EmployeeID = int.Parse(dsRentContracts.Tables[0].Rows[0][2].ToString()),
                        ContractDate = DateTime.Parse(dsRentContracts.Tables[0].Rows[0][3].ToString()),
                        FirstName = dsRentContracts.Tables[0].Rows[0][4].ToString(),
                        LastName = dsRentContracts.Tables[0].Rows[0][5].ToString(),
                        MaritalStatus = dsRentContracts.Tables[0].Rows[0][6].ToString(),
                        NumberOfChildren = int.Parse(dsRentContracts.Tables[0].Rows[0][7].ToString()),
                        ApartmentID = int.Parse(dsRentContracts.Tables[0].Rows[0][8].ToString()),
                        RentStartDate = DateTime.Parse(dsRentContracts.Tables[0].Rows[0][9].ToString())
                    };
                }
            }

            return contract == null ? HttpNotFound() : (ActionResult)View(contract);
        }

        // POST: RentContracts/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                using (SqlConnection scApartmentsManagement = new SqlConnection(System.
                                                                                Configuration.
                                                                                ConfigurationManager.
                                                                                ConnectionStrings["csApartmentsRentalManagement"].
                                                                                ConnectionString))
                {
                    SqlCommand cmdRentContracts = new SqlCommand("DELETE Management.RentContracts " +
                                                                 "WHERE RentContractID = " + id + ";",
                                                                 scApartmentsManagement);

                    scApartmentsManagement.Open();
                    cmdRentContracts.ExecuteNonQuery();
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
