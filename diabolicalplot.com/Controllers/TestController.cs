using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DiabolicalPlot.Business;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace diabolicalplot.com.Controllers
{
    [Route("")]
    public class TestController : Controller
    {
        // GET: api/values
        [HttpGet]
        [Route("{word}")]
        public async Task<IEnumerable<string>> Get(string word) =>
            await new DataMuseService().GetSynonymsAsync(word);
    }
}
