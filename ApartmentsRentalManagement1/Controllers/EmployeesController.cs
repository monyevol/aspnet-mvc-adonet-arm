using System.Net;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Data.SqlClient;
using ApartmentsRentalManagement1.Models;

namespace ApartmentsRentalManagement1.Controllers
{
    public class EmployeesController : Controller
    {
        BusinessObjects objects = new BusinessObjects();

        // GET: Employees
        public ActionResult Index()
        {
            return View(objects.GetEmployees());
        }

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = objects.FindEmployee(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
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
                    SqlCommand cmdEmployees = new SqlCommand("INSERT INTO HumanResources.Employees(EmployeeNumber, FirstName, LastName, EmploymentTitle) " +
                                                             "VALUES(N'" + collection["EmployeeNumber"] + "', " +
                                                             "       N'" + collection["FirstName"] + "', " +
                                                             "       N'" + collection["LastName"] + "', " +
                                                             "       N'" + collection["EmploymentTitle"] + "');",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdEmployees.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int ? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = objects.FindEmployee(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View(employee);
        }

        // POST: Employees/Edit/5
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
                    SqlCommand cmdEmployees = new SqlCommand("UPDATE HumanResources.Employees           " +
                                                             "SET    EmployeeNumber  = N'" + collection["EmployeeNumber"] + "', " +
                                                             "       FirstName       = N'" + collection["FirstName"] + "', " +
                                                             "       LastName        = N'" + collection["LastName"] + "', " +
                                                             "       EmploymentTitle = N'" + collection["EmploymentTitle"] + "'  " +
                                                             "WHERE  EmployeeID      =   " + id + ";",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdEmployees.ExecuteNonQuery();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int ?id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Employee employee = objects.FindEmployee(id);

            if (employee == null)
            {
                return HttpNotFound();
            }

            return View();
        }

        // POST: Employees/Delete/5
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
                    SqlCommand cmdEmployees = new SqlCommand("DELETE FROM HumanResources.Employees " +
                                                             "WHERE EmployeeID = " + id + ";",
                                                             scRentManagement);

                    scRentManagement.Open();
                    cmdEmployees.ExecuteNonQuery();
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
