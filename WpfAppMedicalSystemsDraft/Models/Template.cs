using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppMedicalSystemsDraft.Models
{
    class Template
    {
        public string Header { get; set; }
        public string Content { get; set; }
        public JObject Params { get; set; }
    }
}
