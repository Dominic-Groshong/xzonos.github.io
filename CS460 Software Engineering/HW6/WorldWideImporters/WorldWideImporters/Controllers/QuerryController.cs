using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorldWideImporters.Models;
using WorldWideImporters.Models.ViewModels;

namespace WorldWideImporters.Controllers
{
  public class QuerryController : Controller
  {
    // Initialize the database.
    private BDDContext db = new BDDContext();

    // GET: Querry
    public ActionResult Index()
    {
      return View();
    }

    [HttpGet]
    public ActionResult Search(string search)
    {
      // If the search string is empty return an empty view.
      if (search == "" || search == null)
      {
        return View();
      }

      // Otherwise run the search 
      else
      {
        var List = NameList(search);
        ViewBag.Bit = 1;
        return View(List);
      }
    }

    public ActionResult IndividualDetails(string Name)
    {
      // Redirect to search page if someone gets cheeky and type in the address directly
      if (Name == null || Name == "")
      {
        return RedirectToAction("Search");
      }

      // Otherwise run the search
      else
      {
        //var searchResult = PersonDetails(Name);

        // Initalize the view model
        var Details = new FullDetailsVM();

        // Establish Inital contact information.
        Details.Person = PersonDetails(Name);

        Debug.WriteLine(Details.Person.FirstOrDefault().Customer);

        // This is how I prevent a null exception on the view. 
        if(Details.Person.FirstOrDefault().Customer.Count() > 0)
        {
          // If primary contact add company contact infomation.
          Details.Company = CompanyDetails(Details.Person.First().PersonID);

          // Now that we have a customer we can use the CustomerID to get everything else
          // The total number of orders
          Details.TotalOrders = CountOrders(Details.Company.First().CustomerID);

          // Total Gross Sales
          Details.GrossSales = CalcGross(Details.Company.First().CustomerID);

          // Total Profit
          Details.TotalProfit = CalcProfit(Details.Company.First().CustomerID);

          // Top 10 most profitable line items
          Details.Invoice = MostProfitable(Details.Company.First().CustomerID);

          // Get the company address
          Details.Address = GetAddress(Details.Company.First().CustomerID);

          // Get the Latitude address
          Details.Latitude = GetLat(Details.Company.First().CustomerID);

          // Get the Latitude address
          Details.Longitude = GetLong(Details.Company.First().CustomerID);
        }
        // Get the total number of orders for the customer.



        return View(Details);
      }

    }

    /// <summary>
    /// Preform a search on the databaase for anything containing the input string.
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    private List<PersonVM> NameList(string search)
    {
      List<PersonVM> ListNames = db.People
                                   .Where(n => n.FullName.Contains(search))
                                   .Select(n => new PersonVM
                                   {
                                     FullName = n.FullName
                                   }).ToList();
      return ListNames;
    }

    /// <summary>
    /// Get the latitude of the customer
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    private double? GetLat(int ID)
    {
      double? Latitude = db.Customers
                            .Where(o => o.CustomerID.Equals(ID))
                            .Select(x => x.City)
                            .Include("City")
                            .Select(x => x.Location.Latitude).First();

      return Latitude;
    }

    /// <summary>
    /// Get the Longitude of the customer
    /// </summary>
    /// <param name="search"></param>
    /// <returns></returns>
    private double? GetLong(int ID)
    {
      double? Longitude = db.Customers
                            .Where(o => o.CustomerID.Equals(ID))
                            .Select(x => x.City)
                            .Include("City")
                            .Select(x => x.Location.Longitude).First();

      return Longitude;
    }


    /// <summary>
    /// Preform a search on the databaase for the speicific person and return their full details.
    /// </summary>
    /// <param name="Name">The string to search form</param>
    /// <returns>List of any resulting people in the database</returns>
    private List<PersonVM> PersonDetails(string Search)
    {

     var Details = db.People
                      .Where(p => p.FullName.Equals(Search))
                      .Select(p => new PersonVM
                      {
                        PersonID = p.PersonID,
                        FullName = p.FullName,
                        PreferredName = p.PreferredName,
                        PhoneNumber = p.PhoneNumber,
                        FaxNumber = p.FaxNumber,
                        EmailAddress = p.EmailAddress,
                        MemberSince = p.ValidFrom,
                        Customer = p.Customers2
                      }).ToList();

      return Details;
    }

    /// <summary>
    /// Preform a search on the database for a persons id, if they are a customer return company details.
    /// </summary>
    /// <param name="ID">The ID of the person we are searching for</param>
    /// <returns>List of any resulting people in the database</returns>
    private List<CustomerVM> CompanyDetails(int ID)
    {
      var Details = db.Customers
                      .Where(c => c.PrimaryContactPersonID.Equals(ID))
                      .Select(c => new CustomerVM
                      {
                        CustomerID = c.CustomerID,
                        CustomerName = c.CustomerName,
                        PhoneNumber = c.PhoneNumber,
                        FaxNumber = c.FaxNumber,
                        WebsiteURL = c.WebsiteURL,
                        MemberSince = c.ValidFrom,
                      }).ToList();

      return Details;
    }

    /// <summary>
    /// Preform a search on the database for orders and count the total for a customer.
    /// </summary>
    /// <param name="ID">The ID of the Customer we are searching for</param>
    /// <returns>Total number of orders for a customer</returns>
    private int CountOrders(int ID)
    {
      int totalOrders = db.Orders
                          .Where(o => o.CustomerID.Equals(ID))
                          .Count();

      return totalOrders;
    }

    /// <summary>
    /// Preform a search on the database for the address of a given customer.
    /// </summary>
    /// <param name="ID">The ID of the Customer we are searching for</param>
    /// <returns>Address of the customer</returns>
    private List<AddressVM> GetAddress(int ID)
    {
      var address = db.Customers
                          .Where(c => c.CustomerID.Equals(ID))
                          .Select(ad => new AddressVM
                          {
                            City = ad.City.CityName,
                            State = ad.City.StateProvince.StateProvinceCode,
                            Address = ad.PostalAddressLine1,
                            Zip = ad.PostalPostalCode
                          }).ToList();

      return address;
    }

    /// <summary>
    /// Preform a search on the database for orders and get the sum of the extended price.
    /// </summary>
    /// <param name="ID">The ID of the Customer we are searching for</param>
    /// <returns>Total number of orders for a customer</returns>
    private decimal CalcGross(int ID)
    {

      decimal GrossSalesTotal = db.Orders
                                  .Where(o => o.CustomerID.Equals(ID))
                                  .SelectMany(i => i.Invoices)
                                  .SelectMany(il => il.InvoiceLines)
                                  .Sum(x => x.ExtendedPrice);

      return GrossSalesTotal;
    }

    /// <summary>
    /// Preform a search on the database for orders and get the sum of the profit column.
    /// </summary>
    /// <param name="ID">The ID of the person we are searching for</param>
    /// <returns>Total number of orders for a customer</returns>
    private decimal CalcProfit(int ID)
    {

      decimal TotalProfit = db.Orders
                                  .Where(o => o.CustomerID.Equals(ID))
                                  .SelectMany(i => i.Invoices)
                                  .SelectMany(il => il.InvoiceLines)
                                  .Sum(x => x.LineProfit);

      return TotalProfit;
    }

    /// <summary>
    /// Select many invoices, and then many invoice lines, then order by decending, take the top 10, create the new invoice view model.
    /// </summary>
    /// <param name="ID">The ID of the Customer we are searching</param>
    /// <returns>List of top 10 Line Items</returns>
    private List<InvoiceLineVM> MostProfitable(int ID)
    {

      var profitable = db.Orders
                      .Where(o => o.CustomerID.Equals(ID))
                      .SelectMany(i => i.Invoices)
                      .SelectMany(il => il.InvoiceLines)
                      .OrderByDescending(x => x.LineProfit)
                      .Take(10)
                      .Select(il => new InvoiceLineVM
                      {
                        StockID = il.StockItemID,
                        Description = il.Description,
                        Profit = il.LineProfit,
                      }).ToList();
 
      var contact = db.Orders
                      .Where(o => o.CustomerID.Equals(ID))
                      .SelectMany(i => i.Invoices)
                      .SelectMany(il => il.InvoiceLines)
                      .OrderByDescending(x => x.LineProfit)
                      .Take(10)
                      .Include("InvoiceID")
                      .Select(x => x.Invoice)
                      .Include("SalespersonID")
                      .Select(x => x.Person4.FullName).ToList();

      for (int i = 0; i < 10; i++)
      {
        profitable.ElementAt(i).SalesPerson = contact.ElementAt(i);
      }


      return profitable;
    }
  }
}

