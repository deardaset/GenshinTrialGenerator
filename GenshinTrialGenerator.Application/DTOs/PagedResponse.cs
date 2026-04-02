
using System;
using System.Collections.Generic;
using System.Text;

namespace GenshinTrialGenerator.Application.DTOs
{
    public class PagedResponse<T>
    {
        public List<T> Items { get; set; } = null!;
        public int TotalCount { get; set; }
    }
}
