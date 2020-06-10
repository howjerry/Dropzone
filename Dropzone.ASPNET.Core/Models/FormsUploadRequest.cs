using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dropzone.ASPNET.Core.Models
{
    public class FormsUploadRequest
    {

        public string[] UUIDs { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
