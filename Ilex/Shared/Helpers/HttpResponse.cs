﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ilex.Shared.Helpers
{
    public class HttpResponse<T>
    {
        public T Content { get; set; }
        public string Message { get; set; }
        public string Error { get; set; }
        public int HttpCode { get; set; }
    }

    public class HttpResponse
    {
        public string Message { get; set; }
        public string Error { get; set; }
        public int HttpCode { get; set; }
    }
}
