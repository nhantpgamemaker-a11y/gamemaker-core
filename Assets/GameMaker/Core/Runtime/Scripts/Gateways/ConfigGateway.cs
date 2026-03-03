namespace GameMaker.Core.Runtime
{
    public class ConfigGateway
    {
        private static PlayerConfigRuntimeDataManager _manager;
        public static PlayerConfigRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerConfigRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}