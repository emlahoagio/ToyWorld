using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToyWorldSystem.Controller
{
    [Route("api/happy_new_year_toy_world_team")]
    [ApiController]
    public class HappyNewYearController : ControllerBase
    {
        private readonly Random random = new Random();

        [AllowAnonymous]
        [HttpGet]
        [Route("bai_ba_la")]
        public IActionResult getLuckyNumber()
        {
            string threeCard = "";

            for(int i=0; i < 3; i++)
            {
                var num = random.Next(1, 13);
                var co_ro_chuon_bich = random.Next(1, 4);

                if(num == 11)
                {
                    threeCard += "J ";
                }
                else if (num == 12)
                {
                    threeCard += "Q ";
                }
                else if(num == 13)
                {
                    threeCard += "K ";
                }
                else if(num == 1)
                {
                    threeCard += "A ";
                }else
                {
                    threeCard += num+" ";
                }

                if(co_ro_chuon_bich == 1)
                {
                    threeCard += "Cơ; ";
                }else if(co_ro_chuon_bich == 2)
                {
                    threeCard += "Rô; ";
                }else if(co_ro_chuon_bich == 3)
                {
                    threeCard += "Chuồn; ";
                }else
                {
                    threeCard += "Bích; ";
                }
            }

            return Ok(threeCard);
        }
    }
}
