using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace QRServerWeb.Models
{
    public class FilesModel
    {
        public List<IFormFile> Files { get; set; }
    }
}
