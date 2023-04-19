using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NumbersController : ControllerBase
    {
        [HttpGet(Name = "Numbers/{min}/{max}")]
        public int Get(int min, int max)
        {
            return GenerateRandom(min, max);
        }

        private int GenerateRandom(int min, int max)
        {
            List<int> oldNumbers = new List<int>();

            int seed = 0;
            bool toRun = true;
            while (toRun)
            {
                seed = DateTime.Now.Millisecond;
                if (seed >= min && seed <= max)
                {
                    toRun = false;
                }   
            }

            return seed;
        }
    }
}
