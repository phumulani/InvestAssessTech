using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace AssessInvestTests
{
    [TestFixture]
    public static class API_Test
    {
        [TestCase("https://swapi.dev/api/people/", "R2-D2", "white", "blue")]
        public static void Verify_R2D2_skin_colour_is_white_and_blue(string url, string name, string color1, string color2)
        {
            string responseStr = WebClientRequest(url);

            var peopleList = new JavaScriptSerializer().Deserialize<PeopleList>(responseStr);

            foreach (var itemResults in peopleList.results)
            {
                Result result = itemResults;

                if (result.name == name)
                {
                    string []skinColorResult = result.skin_color.Split(',');
         
                    Assert.AreEqual(skinColorResult[0].Trim(), color1, "1st skin color match.");
                    Assert.AreEqual(skinColorResult[1].Trim(), color2, "2st skin color match.");
                    goto tc;
                }
            }
        tc:
            Console.WriteLine("Test Completed Successfuly.");
        }

        public static string WebClientRequest(string url)
        {
            var client = new WebClient();
            var response = client.DownloadString(url);
            return response;
        }
    }
}
