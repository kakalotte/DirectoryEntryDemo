using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryEntryDemo.IIS
{
    public class IIsWebServer
    {
        /// <summary>
        /// 站点名
        /// eg.ceshi01
        /// </summary>
        public string ServerComment { set; get; }
        /// <summary>
        /// 站点绑定端口属性
        /// </summary>
        public string ServerBindings { set; get; }
        public string AppPoolId { set; get; }
        /// <summary>
        /// 站点物理路径
        /// eg.E:\wwwroot\web
        /// </summary>
        public string Path { set; get; }
        /// <summary>
        /// 站点IIS中的路径
        /// eg./LM/W3SVC/1/ROOT
        /// </summary>
        public string AppRoot { set; get; }
        public string ServerState { set; get; }
    }
}
