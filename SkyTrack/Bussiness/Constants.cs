using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyTrack.Bussiness
{
    public class Constants
    {
        public static readonly string DB = "db";
        public static readonly string SkyTrackConfigFile = "SkyTrack.exe.config";

        public static readonly string UpdateStatePracels = "UPDATE  dbo.stateParcels SET docDate = @docDate, clientNumber = @clientNumber, resultComplete = @resultComplete WHERE docId = @docId; IF @@ROWCOUNT = 0 BEGIN INSERT  dbo.stateParcels( docId ,docDate ,clientNumber ,resultComplete) VALUES(@docId ,@docDate, @clientNumber,@resultComplete);END;";

        public static readonly string UpdateStatePracel = "DELETE FROM dbo.stateParcel WHERE docId = @docId; INSERT dbo.stateParcel(docId,clientOrderNr ,clientParcelNr ,dpdOrderNr ,dpdParcelNr ,pickupDate ,dpdOrderReNr ,dpdParcelReNr ,isReturn ,isReturnSpecified ,planDeliveryDate ,planDeliveryDateSpecified ,newState ,transitionTime ,terminalCode ,terminalCity ,incidentCode ,incidentName ,consignee)VALUES(@docId,@clientOrderNr ,@clientParcelNr ,@dpdOrderNr ,@dpdParcelNr,@pickupDate ,@dpdOrderReNr ,@dpdParcelReNr ,@isReturn ,@isReturnSpecified ,@planDeliveryDate ,@planDeliveryDateSpecified ,@newState ,@transitionTime ,@terminalCode ,@terminalCity ,@incidentCode ,@incidentName ,@consignee);";

        public static readonly string DeleteStateParcel = "DELETE FROM dbo.stateParcel WHERE clientOrderNr = @clientOrderNr;";
    }

}
