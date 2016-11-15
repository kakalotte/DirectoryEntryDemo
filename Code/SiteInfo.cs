using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryEntryDemo.IIS
{
    public class SiteInfo
    {
        public IIsWebServer WebServer { get; set; }
        public IIsApplicationPools Pool { get; set; }
    }
}
