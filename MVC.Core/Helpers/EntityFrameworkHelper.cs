namespace MVC.Core.Helpers
{
	using System.Data.Entity.Core.Objects;

	public static class EntityFrameworkHelper
	{
		public static string OutputSql(object dbResult)
		{
			var sql = dbResult as ObjectQuery;
			if (sql != null)
			{
				return sql.ToTraceString();
			}

			return null;
		}
	}
}
