using System.Drawing;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.ML;
namespace POSMLModel_WebApis
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Configuration
            WebHost.CreateDefaultBuilder()
          .ConfigureServices(services =>
          {
                    // Register Prediction Engine Pool
                    services.AddPredictionEnginePool<POSMLModel.ModelInput, POSMLModel.ModelOutput>().FromFile("POSMLModel.zip");
          })
          .Configure(options =>
          {
              options.UseRouting();
              options.UseEndpoints(routes =>
              {
                        // Define prediction endpoint
                        routes.MapPost("/predict", PredictHandler);
              });
          })
          .Build()
          .Run();
        }

        static async Task PredictHandler(HttpContext http)
        {
            // Get PredictionEnginePool service
            var predictionEnginePool = http.RequestServices.GetRequiredService<PredictionEnginePool<POSMLModel.ModelInput, POSMLModel.ModelOutput>>();
            // Deserialize HTTP request JSON body
            var body = http.Request.Body as Stream;
            var input = await JsonSerializer.DeserializeAsync<POSMLModel.ModelInput>(body);
            // Predict
            POSMLModel.ModelOutput prediction = predictionEnginePool.Predict(input);

            // Return prediction as response
            await http.Response.WriteAsJsonAsync(prediction);
        }
    }
}

