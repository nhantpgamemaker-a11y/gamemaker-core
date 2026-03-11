using UnityEngine;

namespace GameMaker.Core.Runtime
{
    public class TestSingleton : Singleton<TestSingleton>
    {
        private string name = "Nhân";
    }
    public abstract class Test1
    {
        
    }
    public class Test2:Test1
    {

    }
    public class TestSingletonForTest: Singleton<Test2>
    {
        
    }
}
