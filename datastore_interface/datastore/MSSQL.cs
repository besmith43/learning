using System.Data;
using Microsoft.Data.SqlClient;

namespace datastore;

public class MSSQL : IDataStore
{
	public List<SqlParameter> parameters = new List<SqlParameter>();

	public string ConnectionString { get; set; }
	public void AddParameter(string parameterName, dynamic value, string type = "VarChar")
	{
		Console.WriteLine($"Parameter Name: {parameterName}");
		Console.WriteLine($"Value: {value}");
		Console.WriteLine($"Type: {type}");

		SqlParameter param = new SqlParameter(parameterName, getSQLParameterType(type));
		param.Value = value;
		parameters.Add(param);
	}

	private SqlDbType getSQLParameterType(string strType)
	{
		switch (strType)
		{
			case "BigInt":
				return SqlDbType.BigInt;
			case "Bit":
				return SqlDbType.Bit;
			case "Char":
				return SqlDbType.Char;
			case "Date":
				return SqlDbType.Date;
			case "DateTime":
				return SqlDbType.DateTime;
			case "DateTime2":
				return SqlDbType.DateTime2;
			case "DateTimeOffset":
				return SqlDbType.DateTimeOffset;
			case "Decimal":
				return SqlDbType.Decimal;
			case "Float":
				return SqlDbType.Float;
			case "Image":
				return SqlDbType.Image;
			case "Int":
				return SqlDbType.Int;
			case "Money":
				return SqlDbType.Money;
			case "NChar":
				return SqlDbType.NChar;
			case "NVarChar":
				return SqlDbType.NVarChar;
			case "NText":
				return SqlDbType.NText;
			case "Real":
				return SqlDbType.Real;
			case "SmallDateTime":
				return SqlDbType.SmallDateTime;
			case "SmallInt":
				return SqlDbType.SmallInt;
			case "SmallMoney":
				return SqlDbType.SmallMoney;
			case "Structured":
				return SqlDbType.Structured;
			case "Text":
				return SqlDbType.Text;
			case "Time":
				return SqlDbType.Time;
			case "Timestamp":
				return SqlDbType.Timestamp;
			case "TinyInt":
				return SqlDbType.TinyInt;
			case "Udt":
				return SqlDbType.Udt;
			case "UniqueIdentifier":
				return SqlDbType.UniqueIdentifier;
			case "VarBinary":
				return SqlDbType.VarBinary;
			case "VarChar":
				return SqlDbType.VarChar;
			case "Variant":
				return SqlDbType.Variant;
			case "XML":
				return SqlDbType.Xml;
			default:
				return SqlDbType.VarChar;
		}
		return SqlDbType.VarChar;
	}
}