using WebApplication1.Entities;
using System.Text.Json;
using WebApplication1.Controllers;

namespace WebApplication1.Middleware
{
    public class DataSyncMiddleware
    {
        private readonly RequestDelegate _next;
        private static bool _isDataLoaded = false;
        private const string FilePath1 = "Data/pets.json";
        private const string FilePath2 = "Data/petOwners.json";
        private const string FilePath3 = "Data/petCareTakers.json";
        private const string FilePath4 = "Data/petAttractions.json";

        public DataSyncMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Load data only once before first request
            if (!_isDataLoaded)
            {
                var dir1 = Path.GetDirectoryName(FilePath1);
                if (!Directory.Exists(dir1))
                    Directory.CreateDirectory(dir1);

                var dir2 = Path.GetDirectoryName(FilePath2);
                if (!Directory.Exists(dir2))
                    Directory.CreateDirectory(dir2);

                var dir3 = Path.GetDirectoryName(FilePath3);
                if (!Directory.Exists(dir3))
                    Directory.CreateDirectory(dir3);

                var dir4 = Path.GetDirectoryName(FilePath4);
                if (!Directory.Exists(dir4))
                    Directory.CreateDirectory(dir4);


                if (File.Exists(FilePath1) || File.Exists(FilePath2) || File.Exists(FilePath3) || File.Exists(FilePath4))
                {
                    var json1 = await File.ReadAllTextAsync(FilePath1);
                    var json2 = await File.ReadAllTextAsync(FilePath2);
                    var json3 = await File.ReadAllTextAsync(FilePath3);
                    var json4 = await File.ReadAllTextAsync(FilePath4);


                    var list = JsonSerializer.Deserialize<List<Pet>>(json1);
                    if (list != null)
                        PetCareController.pets = list;

                    var list2 = JsonSerializer.Deserialize<List<PetOwner>>(json2);
                    if (list2 != null)
                        PetCareController.petOwners = list2;

                    var list3 = JsonSerializer.Deserialize<List<PetCareTaker>>(json3);
                    if (list3 != null)
                        PetCareController.petCareTakers = list3;

                    var list4 = JsonSerializer.Deserialize<List<PetAttraction>>(json4);
                    if (list4 != null)
                        PetCareController.petAttractions = list4;
                }


                else
                {
                    // File does not exist — initialize with empty list and save
                    PetCareController.pets = new List<Pet>();
                    var initialJson1 = JsonSerializer.Serialize(PetCareController.pets, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(FilePath1, initialJson1);

                    PetCareController.petOwners = new List<PetOwner>();
                    var initialJson2 = JsonSerializer.Serialize(PetCareController.petOwners, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(FilePath2, initialJson2);

                    PetCareController.petCareTakers = new List<PetCareTaker>();
                    var initialJson3 = JsonSerializer.Serialize(PetCareController.petCareTakers, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(FilePath3, initialJson3);

                    PetCareController.petAttractions = new List<PetAttraction>();
                    var initialJson4 = JsonSerializer.Serialize(PetCareController.petAttractions, new JsonSerializerOptions { WriteIndented = true });
                    await File.WriteAllTextAsync(FilePath4, initialJson4);
                }

                _isDataLoaded = true;
            }


            // Capture the response
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);


            // After request, persist changes if it was a modifying request
            if (context.Request.Method is "POST" or "PUT" or "DELETE")
            {
                var updatedJson = JsonSerializer.Serialize(PetCareController.pets, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath1, updatedJson);

                var updatedJson2 = JsonSerializer.Serialize(PetCareController.petOwners, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath2, updatedJson);

                var updatedJson3 = JsonSerializer.Serialize(PetCareController.petCareTakers, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath3, updatedJson);

                var updatedJson4 = JsonSerializer.Serialize(PetCareController.petAttractions, new JsonSerializerOptions { WriteIndented = true });
                await File.WriteAllTextAsync(FilePath4, updatedJson);
            }

            // Copy back the response
            responseBody.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }

    }
}