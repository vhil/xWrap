namespace Xwrap.CodeGeneration
{
	using System;
	using System.Collections;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Xml;
	using Sitecore.Abstractions;
	using Sitecore.Collections;
	using Sitecore.Configuration;
	using Sitecore.Data;
	using Sitecore.Data.DataProviders;
	using Sitecore.Data.IDTables;
	using Sitecore.Data.Items;
	using Sitecore.Links;
	using Sitecore.Sites;
	using Sitecore.Tasks;
	using Sitecore.Text;
	using Sitecore.Web;
	using Sitecore.Web.UI.WebControls;
	using Sitecore.Xml.XPath;

	public class MockedConfigurationFactory : BaseFactory
	{
		public override ErrorControl CreateErrorControl(string message, string details)
		{
			throw new NotImplementedException();
		}

		public override ItemNavigator CreateItemNavigator(Item item)
		{
			throw new NotImplementedException();
		}

		public override T CreateObject<T>(XmlNode configNode)
		{
			throw new NotImplementedException();
		}

		public override ConfigStore GetConfigStore(string configStoreName)
		{
			throw new NotImplementedException();
		}

		public override List<CustomHandler> GetCustomHandlers()
		{
			throw new NotImplementedException();
		}

		public override Database GetDatabase(string name)
		{
			throw new NotImplementedException();
		}

		public override Database GetDatabase(string name, bool assert)
		{
			throw new NotImplementedException();
		}

		public override string[] GetDatabaseNames()
		{
			throw new NotImplementedException();
		}

		public override List<Database> GetDatabases()
		{
			throw new NotImplementedException();
		}

		public override StringDictionary GetDomainMap(string path)
		{
			throw new NotImplementedException();
		}

		public override Hashtable GetHashtable(string path, Factory.HashKeyType keyType, Factory.HashValueType valueType, Factory.HashValueFormat format,
			Type dataType)
		{
			throw new NotImplementedException();
		}

		public override IDTableProvider GetIDTable()
		{
			throw new NotImplementedException();
		}

		public override IComparer<Item> GetItemComparer(Item item)
		{
			throw new NotImplementedException();
		}

		public override LinkDatabase GetLinkDatabase()
		{
			throw new NotImplementedException();
		}

		public override MasterVariablesReplacer GetMasterVariablesReplacer()
		{
			throw new NotImplementedException();
		}

		public override PerformanceCounterCollection GetPerformanceCounters()
		{
			throw new NotImplementedException();
		}

		public override TCollection GetProviders<TProvider, TCollection>(string rootPath, out TProvider defaultProvider)
		{
			throw new NotImplementedException();
		}

		public override IRetryable GetRetryer()
		{
			throw new NotImplementedException();
		}

		public override Replacer GetReplacer(string name)
		{
			throw new NotImplementedException();
		}

		public override SiteContext GetSite(string siteName)
		{
			throw new NotImplementedException();
		}

		public override SiteInfo GetSiteInfo(string siteName)
		{
			throw new NotImplementedException();
		}

		public override List<SiteInfo> GetSiteInfoList()
		{
			throw new NotImplementedException();
		}

		public override string[] GetSiteNames()
		{
			throw new NotImplementedException();
		}

		public override string GetString(string configPath, bool assert)
		{
			throw new NotImplementedException();
		}

		public override Set<string> GetStringSet(string configPath)
		{
			throw new NotImplementedException();
		}

		public override TaskDatabase GetArchiveTaskDatabase()
		{
			throw new NotImplementedException();
		}

		public override TaskDatabase GetTaskDatabase()
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(string configPath, bool assert)
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(string configPath, string[] parameters, bool assert)
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(XmlNode configNode, bool assert)
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(XmlNode configNode, string[] parameters, bool assert)
		{
			throw new NotImplementedException();
		}

		public override object CreateObject(XmlNode configNode, string[] parameters, bool assert, IFactoryHelper helper)
		{
			throw new NotImplementedException();
		}

		public override Type CreateType(XmlNode configNode, bool assert)
		{
			throw new NotImplementedException();
		}

		public override Type CreateType(XmlNode configNode, string[] parameters, bool assert)
		{
			throw new NotImplementedException();
		}

		public override Type FindType(string className, Assembly assembly)
		{
			throw new NotImplementedException();
		}

		public override string GetAttribute(string name, XmlNode node, string[] parameters)
		{
			throw new NotImplementedException();
		}

		public override XmlNode GetConfigNode(string xpath)
		{
			throw new NotImplementedException();
		}

		public override XmlNode GetConfigNode(string xpath, bool assert)
		{
			throw new NotImplementedException();
		}

		public override XmlNodeList GetConfigNodes(string xpath)
		{
			throw new NotImplementedException();
		}

		public override void Reset()
		{
			throw new NotImplementedException();
		}

		public override XmlDocument GetConfiguration()
		{
			throw new NotImplementedException();
		}
	}
}
