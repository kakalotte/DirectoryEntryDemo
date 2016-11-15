using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace DirectoryEntryDemo.IIS
{

    public class IISTool
    {
        private static string HostName = "localhost";
        public static List<SiteInfo> SiteInfoList;
        /// <summary>
        /// 获取本地IIS版本
        /// </summary>
        /// <returns></returns>
        public static string GetIIsVersion()
        {
            try
            {
                DirectoryEntry entry = new DirectoryEntry("IIS://" + HostName + "/W3SVC/INFO");
                string version = entry.Properties["MajorIISVersionNumber"].Value.ToString();
                return version;
            }
            catch (Exception se)
            {
                //说明一点:IIS5.0中没有(int)entry.Properties["MajorIISVersionNumber"].Value;属性，将抛出异常 证明版本为 5.0
                return string.Empty;
            }
        }
        private static List<IIsWebServer> GetAllWebServers()
        {
            string entPath = String.Format("IIS://" + HostName + "/w3svc");
            DirectoryEntry ent = new DirectoryEntry(entPath);
            List<IIsWebServer> list = new List<IIsWebServer>();
            foreach (DirectoryEntry child in ent.Children)
            {
                IIsWebServer webserver = new IIsWebServer();
                if (child.SchemaClassName.Equals("IIsWebServer", StringComparison.OrdinalIgnoreCase))
                {
                    webserver.ServerComment = GetValue(child, "ServerComment");// child.Properties["ServerComment"][0].ToString();
                    webserver.ServerBindings = GetValue(child, "ServerBindings"); //child.Properties["ServerBindings"][0].ToString();
                    webserver.ServerState = GetValue(child, "ServerState"); //child.Properties["ServerState"][0].ToString();
                    foreach (DirectoryEntry subchild in child.Children)
                    {
                        if (subchild.Properties["KeyType"][0].ToString().Equals("IIsWebVirtualDir", StringComparison.OrdinalIgnoreCase))
                        {
                            webserver.AppPoolId = GetValue(subchild, "AppPoolId"); //subchild.Properties["AppPoolId"][0].ToString();
                            webserver.AppRoot = GetValue(subchild, "AppRoot"); //subchild.Properties["AppRoot"][0].ToString();
                            webserver.Path = GetValue(subchild, "Path"); //subchild.Properties["Path"][0].ToString();
                            break;
                        }
                    }
                }
                if (!String.IsNullOrEmpty(webserver.ServerBindings) || !String.IsNullOrEmpty(webserver.AppPoolId))
                {
                    list.Add(webserver);
                }
            }
            return list;
        }
        private static List<IIsApplicationPools> GetAllIIsApplicationPools()
        {
            string entPath = String.Format("IIS://" + HostName + "/w3svc");
            DirectoryEntry ent = new DirectoryEntry(entPath);
            List<IIsApplicationPools> list = new List<IIsApplicationPools>();
            foreach (DirectoryEntry child in ent.Children)
            {
                if (child.SchemaClassName.Equals("IIsApplicationPools", StringComparison.OrdinalIgnoreCase))
                {
                    foreach (DirectoryEntry subchild in child.Children)
                    {
                        IIsApplicationPools pool = new IIsApplicationPools();
                        pool.PoolName = subchild.Name;
                        pool.PoolPath = subchild.Path;
                        pool.AppPoolIdentityType = GetValue(subchild, "AppPoolIdentityType");
                        pool.AppPoolState = GetValue(subchild, "AppPoolState");
                        pool.Enable32BitAppOnWin64 = GetValue(subchild, "Enable32BitAppOnWin64");
                        pool.ManagedPipelineMode = GetValue(subchild, "ManagedPipelineMode");
                        if (!String.IsNullOrEmpty(pool.PoolName))
                        {
                            list.Add(pool);
                        }
                    }
                }
            }
            return list;
        }
        private static String GetValue(DirectoryEntry entry, String key)
        {
            String str = "";
            try
            {
                str = entry.Properties[key][0].ToString();
            }
            catch (Exception e)
            {

            }
            return str;
        }

        public static void GetSiteInfo()
        {
            List<IIsWebServer> listwebserver = GetAllWebServers();
            List<IIsApplicationPools> listpools = GetAllIIsApplicationPools();
            SiteInfoList = new List<SiteInfo>();
            foreach (IIsWebServer webserver in listwebserver)
            {
                SiteInfo siteinfo = new SiteInfo();
                siteinfo.WebServer = webserver;
                foreach (IIsApplicationPools pool in listpools)
                {
                    if (pool.PoolName.Equals(webserver.AppPoolId, StringComparison.OrdinalIgnoreCase))
                    {
                        siteinfo.Pool = pool;
                        SiteInfoList.Add(siteinfo);
                        break;
                    }
                }
            }
        }
    }
}
