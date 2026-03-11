using GameMaker.Core.Runtime;

namespace GameMaker.Feature.Shop.Runtime
{
    public class ShopGateway
    {
        private static PlayerShopRuntimeDataManager _manager;
        internal static PlayerShopRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerShopRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}