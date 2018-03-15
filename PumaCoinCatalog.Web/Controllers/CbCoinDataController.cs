﻿using PumaCoinCatalog.Services;
using PumaCoinCatalog.Web.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PumaCoinCatalog.Web.Controllers
{
    public class CbCoinDataController : Controller
    {
        private readonly CbCoinDataService _cbCoinDataService;

        public CbCoinDataController()
        {
            _cbCoinDataService = new CbCoinDataService();
        }

        // GET: CbCoinData
        public ActionResult Index(string countryTitle = "United States of America")
        {
            var data = _cbCoinDataService.GetCountry(countryTitle);
            var model = data.Map();
            return View(model);
        }        

        public ActionResult Denomination(int denominationId)
        {
            var data = _cbCoinDataService.GetDenomination(denominationId);
            var model = data.Map();
            return View(model);
        }

        public ActionResult Variety(int varietyId)
        {
            var data = _cbCoinDataService.GetVariety(varietyId);
            var model = data.Map();
            return View(model);
        }

        #region Type

        public ActionResult Type(int typeId)
        {
            var data = _cbCoinDataService.GetType(typeId);
            var model = data.Map();
            return View(model);
        }

        public ActionResult ChangeTypeObverseImage(int typeId, string uri)
        {
            _cbCoinDataService.ChangeTypeObverseImageUri(typeId, uri);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangeTypeReverseImage(int typeId, string uri)
        {
            _cbCoinDataService.ChangeTypeReverseImageUri(typeId, uri);
            return Json("success", JsonRequestBehavior.AllowGet);
        }

        #endregion Type
    }
}