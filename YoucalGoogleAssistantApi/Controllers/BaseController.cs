using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YoucalGoogleAssistantApi.DAL;

namespace YoucalGoogleAssistantApi.Controllers
{
    public class BaseController : Controller //controller för anslutning till DB
    {
        // GET: Base
        protected YoucalContext db = new YoucalContext();

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();

            base.Dispose(disposing);
        }
    }
}