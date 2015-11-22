namespace ClassLibrary
{
	using System.Configuration;
	using System.Data;
	using System.Data.SqlClient;

	public class Error
	{
		//**********************************************************************************************************************
		//FUNCTION:	takes an error number returned by a SQL stored procedure and returns the error message associated with it
		//**********************************************************************************************************************
		public static string GetSqlErrorMessage(int errorNo) {

			//check for custom errors
			switch (ErrorNo) {
				case 1:
					return "A duplicate record already exists in the database.";
				default:

					//check for an error message defined in the sysmessages table
					using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings[0].ConnectionString)) {
						using (var command = new SqlCommand("SELECT [Description] FROM master.dbo.sysmessages WHERE msglangid = 1033 AND Error = @ErrorNo", connection))
						{
							command.Parameters.Add(new SqlParameter("@ErrorNo", errorNo));
							command.Open();
							object errorMessage = command.ExecuteScalar();
							if (errorMessage != null)
							{
								return errorMessage.ToString();
							}
							else {
								return "The error number has not been recognised.";
							}
						}
					}
			}
		}
	}
}