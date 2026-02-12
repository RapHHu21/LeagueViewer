using LVrefitLQ;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner
{
    public class testClass
    {
        public testClass() { }

        public async Task testWiki2()
        {
            var api = RestService.For<ItestWiki>("https://en.wikipedia.org");
            try
            {
                Console.WriteLine("try");
                var res = await api.WikiQueryAsync(
                    action: "query",
                    list: "search",
                    srSearch: "Craig Noone",
                    format: "json"
                );

                if (res.IsSuccessStatusCode)
                {
                    Console.WriteLine(res.Content);
                    Console.WriteLine("wypadek1");
                }
                else
                {
                    Console.WriteLine(res.StatusCode);
                    Console.WriteLine("wypadek2");
                }



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("wypadek3");
            }
        }

    }

    public interface ItestWiki3
    {
        [Get("/w/api.php")]
        Task<ApiResponse<string>> WikiQueryAsync2(
            [AliasAs("action")] string action,
            [AliasAs("list")] string list,
            [AliasAs("srsearch")] string srSearch,
            [AliasAs("format")] string format
            );
    }
}
