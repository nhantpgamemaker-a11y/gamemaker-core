namespace GameMaker.Sound.Runtime
{
    public class SoundGateway
    {
        private static SoundRuntimeDataManager _manager;
        internal static SoundRuntimeDataManager Manager => _manager;
        internal static void Initialize(SoundRuntimeDataManager manager)
        {
            _manager = manager;
        }
    }
}