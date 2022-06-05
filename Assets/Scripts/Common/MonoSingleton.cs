using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 单例：场景中只有一个对象就可以使用单例模式   提供何时何地都可以访问对象的功能
/// </summary>
/// <typeparam name="T"></typeparam>
public class MonoSingleton <T>: MonoBehaviour where T:MonoSingleton<T> //约束T必须是MonoSingleton的子类
{
    public bool isDontDestroyOnLoad = false;
    protected static T instance;
    public static T Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<T>();//注意这里这句话只能在主线程调用，所以如果是主线程的话，我们就可以不用new ThreadCrossHelper对象
                if (instance == null)
                {
                    new GameObject().AddComponent<T>();//注意这里这句话只能在主线程调用
                }
                else
                {
                    instance.Init();
                }
            }

            return instance;
        }
    }
    protected void Awake()
    {
        if(instance == null)
        {
            print("MonoSingleton--Awake");
            instance = this as T;
            Init();
        }
        
    }

    protected virtual void Init()
    {
        if (isDontDestroyOnLoad) DontDestroyOnLoad(gameObject); //DontDestroyOnLoad可以保证Gameobject以及上面绑定的组件不会销毁，在处理全局控制的时候有用
        print("MonoSingleTon--Init");
    }
}
