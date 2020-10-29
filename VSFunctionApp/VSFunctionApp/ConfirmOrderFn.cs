using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VSFunctionApp.Models;

namespace VSFunctionApp
{
    public static class ConfirmOrderFn
    {
        [FunctionName("ConfirmOrder")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            [Blob("myfiles/{sys.randguid}.json", FileAccess.Write, Connection ="OrderBlobStorageConnection")]Stream orderBlob,
            ILogger log)
        {

            string reqBody= await new StreamReader(req.Body).ReadToEndAsync();
            Order order = JsonConvert.DeserializeObject<Order>(reqBody);
            
            order.OrderId = Guid.NewGuid().ToString();
            order.Status = "Confirmed";

            StreamWriter sw = new StreamWriter(orderBlob);
            var orderData = JsonConvert.SerializeObject(order, Formatting.Indented);
            await sw.WriteLineAsync(orderData);
            sw.Flush();
            sw.Close();

            return new OkObjectResult(order);
        }
    }
}
