using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.DTOs
{
    public class GetDataOptionsRequest
    {
        public int Page {  get; set; }
        public int PageSize { get; set; }
        public string? Sort { get; set; }
        public string? Search {  get; set; }
        public string? Element { get; set; }
    }
}
