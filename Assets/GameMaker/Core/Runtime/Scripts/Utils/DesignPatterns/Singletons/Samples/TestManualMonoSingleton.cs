using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class TestManualMonoSingleton : ManualMonoSingleton<TestManualMonoSingleton>
    {
        [SerializeField] private string _myName = "Nhân";
    }
}
