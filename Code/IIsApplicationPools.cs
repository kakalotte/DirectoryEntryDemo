using System;
using System.Collections.Generic;
using System.Text;

namespace DirectoryEntryDemo.IIS
{
    public class IIsApplicationPools
    {
        /// <summary>
        /// 应用程序池名
        /// </summary>
        public String PoolName { get; set; }
        /// <summary>
        /// 应用程序池在IIS中的虚拟路径
        /// eg.IIS://localhost/w3svc/APPPOOLS/sdcms
        /// </summary>
        public String PoolPath { get; set; }
        public String AppPoolIdentityType { get; set; }
        public String AppPoolState { get; set; }
        public String Enable32BitAppOnWin64 { get; set; }
        /// <summary>
        /// 应用程序池的管道模式 0:集成 1：经典
		///IIS7及以上版本有效，以下版本均为经典模式
        /// </summary>
        public String ManagedPipelineMode { get; set; }

    }
}
