using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace FileUploadSampleAspnetCor.Models
{
    public class BoardModel
    {
        public string title { get; set; }
        public string content { get; set; }
        public IList<IFormFile> files { get; set; }
    }
}