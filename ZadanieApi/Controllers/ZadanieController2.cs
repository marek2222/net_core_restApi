//#define NEVER
#if NEVER
// This controller is used only for documentation purposes.
#region snippet_todo1
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZadanieApi.Models;

#region ZadanieController
namespace ZadanieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZadanieController : ControllerBase
    {
        private readonly ZadanieContext _context;
        #endregion

        public ZadanieController(ZadanieContext context)
        {
            _context = context;

            if (_context.Zadanies.Count() == 0)
            {
                // Create a new Zadanie if collection is empty,
                // which means you can't delete all Zadanies.
                _context.Zadanies.Add(new Zadanie { Name = "Item1" });
                _context.SaveChanges();
            }
        }
    }
}
#endregion
#endif
