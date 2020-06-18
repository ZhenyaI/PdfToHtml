﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PdfRepresantation.Server.Controllers
{
    [Route("")]
    [ApiController]
    public class PdfConverterController : ControllerBase
    {
        private static readonly PdfHtmlWriter htmlWriter = new PdfHtmlWriter();
        public  class FileRequest
        {
            public string ContentBase64 { get; set; }
        }
        [HttpPost("ConvertToHtml")]
        public ActionResult<string> ConvertToHtml([FromBody] FileRequest pdf)
        {
            var buffer = Convert.FromBase64String(pdf.ContentBase64);
            var details = PdfDetailsFactory.Create(buffer);
            var result = htmlWriter.ConvertPdf(details);
            return result;
        }

        

        [HttpPost("ConvertToText")]
        public ActionResult<string> ConvertToText([FromBody] FileRequest pdf)
        {
            var buffer = Convert.FromBase64String(pdf.ContentBase64);
            var details = PdfDetailsFactory.Create(buffer);
            return details.ToString();
        }
        [HttpPost("ConvertToHtml/Base64")]
        public ActionResult<string>  ConvertToHtml([FromBody] string base64pdf)
        {
            return ConvertToHtml(new FileRequest{ContentBase64 = base64pdf});
        }
        [HttpPost("ConvertToText/Base64")]
        public ActionResult<string> ConvertToText([FromBody] string base64pdf)
        {
            return ConvertToText(new FileRequest{ContentBase64 = base64pdf});
        }
    }
}
