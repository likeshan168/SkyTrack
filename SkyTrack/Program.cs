
using SkyTrack.Bussiness;
using SkyTrack.ru.dpd.ws2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTrack
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                long clientNo = 1203000022;
                string clientKey = "6847DC925C3BFB79FFF4F654B0C294C002F9CF38";


                #region geography service
                //DPDGeography2Service service = new DPDGeography2Service();
                //auth au = new auth() { clientKey = clientKey, clientNumber = clientNo };
                //terminalSelf[] self = service.getTerminalsSelfDelivery2(au); 
                #endregion

                #region tracking service

                ParcelTracingService trackingService = new ParcelTracingService();
                //使用mockservice的地址可以获测试数据
                trackingService.Url = "http://desktop-avssgiq:8080/MockService";
                auth au = new auth() { clientKey = clientKey, clientNumber = clientNo };
                stateParcels states = null;
                if (GetStateParcels(trackingService, au, out states) && states != null)
                {
                    Console.WriteLine("更新数据库成功");
                    Confirm(trackingService, au, states.docId);
                }

                #endregion


                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static bool GetStateParcels(ParcelTracingService service, auth au, out stateParcels states)
        {
            try
            {
                Console.WriteLine("获取所有的订单信息");
                requestClient client = new requestClient() { auth = au };
                states = service.getStatesByClient(client);
                Console.WriteLine("获取成功");
                //save the parcles states into db
                return DbOperation.Instance.UpdateStatesPracels(states);
            }
            catch (Exception ex)
            {
                //write the logs into db
                Console.WriteLine(ex.Message);
                states = null;
                return false;
            }
        }

        private static bool Confirm(ParcelTracingService service, auth au, long docId)
        {
            try
            {
                requestConfirm rc = new requestConfirm() { auth = au, docId = docId };
                service.confirm(rc);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
