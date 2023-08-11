using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Dapper.Services.Custom.CustomerCommand;

public class SqlCommandConst
{
    private const string _tablename = "Customer";
    public const string sqlGetCountCustomersNew = "SELECT COUNT(*) FROM " + _tablename + " where cast( Created as date)= cast(Getdate() as date)";
    public const string sqlGetFirstCustomers = " SELECT id,firstname + ' ' + lastName as FullName  ,Email +' | ' + PhoneNumber Email_PhoneNumber ,BankAccountNumber " +
        "FROM " + _tablename + " where cast( Created as date)= cast(Getdate() as date)";



    public const string sqlGetAllCustomers =
        @" SELECT id,firstname + ' ' + lastName as FullName ,Email +' | ' + PhoneNumber Email_PhoneNumber ,BankAccountNumber " +
        "FROM " + _tablename + " where cast( Created as date)= cast(Getdate() as date)";

    public const string sqlGetFirstAndLastCountCustomers =
        @" SELECT top 1 id,firstname + ' ' + lastName as FullName ,Email +' | ' + PhoneNumber Email_PhoneNumber ,BankAccountNumber " +
        "FROM " + _tablename + "  order by Id  " +
        "SELECT top 1 id,firstname + ' ' + lastName as FullName ,Email +' | ' + PhoneNumber Email_PhoneNumber ,BankAccountNumber " +
        "FROM " + _tablename + "  order by Id desc ";

    public const string sqlUpdateEmailNullCustomers = @"  update " + _tablename + " set Email='Empty' where Email is null ";

    public const string sqlGetValidCsutomer = "SELECT COUNT(*) as count,max(Created) as maxCreated  FROM " + _tablename;
    public const string sqlGetCsutomerByEmail = "SELECT id,firstname + ' ' + lastName as FullName ,Email +' | ' + PhoneNumber Email_PhoneNumber ,BankAccountNumber  FROM " + _tablename + " where Email=@Email";


}
