using PumaCoinCatalog.Services.UsCoinBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CbCollectionController : Controller
    {
        private readonly CbCollectionService _collectionService;

        public CbCollectionController()
        {
            _collectionService = new CbCollectionService();
        }

        // GET: CbCollection
        public ActionResult Index()
        {

            return View();
        }
    }
}