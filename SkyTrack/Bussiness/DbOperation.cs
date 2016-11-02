using Dapper;
using SkyTrack.Common;
using SkyTrack.ru.dpd.ws2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTrack.Bussiness
{
    public class DbOperation
    {
        public static readonly DbOperation Instance = new DbOperation();
        string connectStr = string.Empty;
        private DbOperation()
        {
            connectStr = ConfigHelper.Instance.GetConnectionStringsConfigValue(Constants.SkyTrackConfigFile, Constants.DB);
        }

        public bool UpdateStatesPracels(stateParcels paracels)
        {
            IDbConnection conn = new SqlConnection(connectStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            IDbTransaction tran = conn.BeginTransaction();

            try
            {
                //conn.Execute(Constants.UpdateStatePracels,
                //   new
                //   {
                //       docId = paracels.docId,
                //       docDate = paracels.docDate,
                //       clientNumber = paracels.clientNumber,
                //       resultComplete = paracels.resultComplete
                //   }, tran);
                conn.Execute(Constants.UpdateStatePracels, paracels, tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }

        public bool UpdateStatesPracel(stateParcels paracels)
        {
            IDbConnection conn = new SqlConnection(connectStr);
            if (conn.State == ConnectionState.Closed)
                conn.Open();

            IDbTransaction tran = conn.BeginTransaction();

            try
            {
                conn.Execute(Constants.UpdateStatePracels,
                   new
                   {
                       docId = paracels.docId,
                       docDate = paracels.docDate,
                       clientNumber = paracels.clientNumber,
                       resultComplete = paracels.resultComplete
                   }, tran);
                tran.Commit();
                return true;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                Console.WriteLine(ex.Message);
                return false;
            }
            finally
            {
                if (conn.State == ConnectionState.Open)
                    conn.Close();
            }
        }
    }
}
