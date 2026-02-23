using GameMaker.IAP.Runtime;

namespace GameMaker.Core.Runtime
{
    public class IAPGateway
    {
        private static PlayerIAPRuntimeDataManager _manager;
        internal static PlayerIAPRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerIAPRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}