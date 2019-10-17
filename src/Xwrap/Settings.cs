namespace Xwrap
{
	/// <summary>
	/// Sitecore settings for xWrap
	/// </summary>
	public static class Settings
	{
		/// <summary>
		/// reads "xWrap.DisableSecurityOnLinkGeneration" Sitecore setting
		/// </summary>
		public static bool DisableSecurityOnLinkGeneration => Sitecore.Configuration.Settings.GetBoolSetting("xWrap.DisableSecurityOnLinkGeneration", false);
	}
}
