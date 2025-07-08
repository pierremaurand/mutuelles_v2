using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;
using System.Drawing.Imaging;
using mutuelleApi.dtos;

namespace mutuelleApi.controllers
{
    public class FileUploadController(IWebHostEnvironment webHost) : BaseController
    {
        private readonly IWebHostEnvironment _env = webHost;

        [HttpPost("upload")]
        [AllowAnonymous]
        public Task<IActionResult> UploadFile(UploadImage fichier)
        {
            if (fichier == null || fichier.Image == null || fichier.Image == "")
            {
                return Task.FromResult<IActionResult>(BadRequest("Aucun fichier n'a été envoyé."));
            }

            var uploadsFolder = Path.Combine(_env.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string fileName = Guid.NewGuid().ToString() + '.' + fichier.Extension;
            string filePath = Path.Combine(uploadsFolder, fileName);

            byte[] bytes = Convert.FromBase64String(fichier.Image.Split(',')[1]);

            Image image;
            using (MemoryStream ms = new MemoryStream(bytes))
            {
                image = Image.FromStream(ms);
            }

            image.Save(filePath, ImageFormat.Png);

            return Task.FromResult<IActionResult>(Ok(new { FileName = fileName }));
        }
    }
}