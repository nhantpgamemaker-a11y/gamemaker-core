namespace GameMaker.Core.Runtime
{
    public class ItemGateway
    {
        private static PlayerItemDetailRuntimeDataManager _manager;
        internal static PlayerItemDetailRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerItemDetailRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}