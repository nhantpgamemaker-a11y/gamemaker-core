namespace GameMaker.Core.Runtime
{
    public class CurrencyGateway
    {
        private static PlayerCurrencyRuntimeDataManager _manager;
        internal static PlayerCurrencyRuntimeDataManager Manager => _manager;
        internal static void Initialize(PlayerCurrencyRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}