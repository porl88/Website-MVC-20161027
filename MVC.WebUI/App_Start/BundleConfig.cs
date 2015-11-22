namespace MVC.WebUI
{
	using System.Web.Optimization;

	public class BundleConfig
	{
		// For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
		public static void RegisterBundles(BundleCollection bundles)
		{
			// BUNDLE NAMES MUST NOT BE NAMED THE SAME AS AN EXISTING DIRECTORY - WILL GET 403 ERROR

			/*
			 * Need to add:
			 * <add namespace="System.Web.Optimization" /> to pages/namespaces in web.config
			 * BundleConfig.RegisterBundles(BundleTable.Bundles); - to global.asax
			 */

			AddStyleSheets(bundles);
			AddJavaScript(bundles);

			BundleTable.EnableOptimizations = false; // enables minification and bundling - switched off by default in debug environment
		}

		private static void AddStyleSheets(BundleCollection bundles)
		{
            bundles.Add(new ScriptBundle("~/css/website")
                .Include("~/content/css/styles.css")
            );
			/*
			 * Style sheets should follow the natural cascading pattern of more general to more specific
			 * They should be split into 3 sections: generic selectors; site-wide, reusable classes; page-specific styling
			 * For example here is an example for Amazon:

			 bundles.Add(new StyleBundle("~/css").Include(
			 * SECTION 1: GENERIC SELECTOR STYLING - reset style sheets, default styling, fonts
				"~/content/css/1-reset.css",
				"~/content/css/amazon/1-amazon-fonts.css",
			 * SECTION 2: SITE-WIDE RE-USABLE CLASSES: site 'bootstrap' stylesheet, 3rd party style sheets
				"~/content/css/amazon/2-amazon-bootstrap.css",
			 * SECTION 3: PAGE-SPECIFIC STYLING - must be grouped by the actual file path - can optionally be split into files grouped by directory, as below:
				"~/content/css/amazon/3-amazon-article.css",
				"~/content/css/amazon/3-amazon-account.css",
				"~/content/css/amazon/3-amazon-home.css",
				"~/content/css/amazon/3-amazon-product.css",
				"~/content/css/amazon/3-amazon-shared.css"));

			 */

			//// print style sheets
			//bundles.Add(new StyleBundle("~/css/print")
			//	.Include("~/content/css/print.css")
			//);
		}

		private static void AddJavaScript(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/js/website")
				.Include("~/content/js/website/website.js")
			);
		}
	}
}