namespace GameMaker.Core.Runtime
{
    public class PropertyGateway
    {
        private static PlayerPropertyRuntimeDataManager _manager;
        internal static PlayerPropertyRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerPropertyRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}