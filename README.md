# DirectoryEntryDemo

首先介绍一下命名空间System.DirectoryServices

System.DirectoryServices 命名空间用以从托管代码简便地访问 Active Directory。该命名空间包含两个组件类,即 DirectoryEntry 和 DirectorySearcher，它们使用 Active Directory 服务接口 (ADSI) 技术。ADSI 是 Microsoft 提供的一组接口，作为使用各种网络提供程序的灵活的工具。无论网络有多大，ADSI 都可以使管理员能够相对容易地定位和管理网络上的资源。

System.DirectoryServices 命名空间中的类可以与任何 Active Directory 服务提供程序一起使用。当前的一些提供程序包括 Internet 信息服务 (IIS)、轻量目录访问协议 (LDAP)、Novell NetWare 目录服务 (NDS) 和 WinNT。

操作IIS时，DirectoryEntry的Path的格式为：IIS://ComputerName/Service/Website/Directory
  ComputerName：即操作的服务器的名字，可以是名字也可以是IP，经常用的就是localhost； 
  Service：即操作的服务器，IIS中有Web，也有FTP，还有SMTP这些服务，我们主要是操作IIS的Web功能，因此此处就是"W3SVC",如果是FTP则应是"MSFTPSVC" ；
  WebSite:一个IIS服务中可以包括很多的站点，这个就用于设置操作的站点。他的值是一个数字，默认是1，表示缺省站点，如果有其它，则从1开始依次类推；
  Directory：操作的目录名称，一个站点一般顶层目录为"ROOT"，其它目录则是他的孩子（Child）。 
