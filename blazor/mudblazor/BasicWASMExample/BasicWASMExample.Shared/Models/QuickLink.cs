using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicWASMExample.Shared.Models;

    public class QuickLink
    {
        public string name { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public int order { get; set; }
        public string Selector { get; set; }
    }