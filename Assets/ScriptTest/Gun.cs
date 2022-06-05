using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {
    private GameObject prefab;
    private void Start()
    {
        prefab = ResourceManager.Load<GameObject>("Bullet");
    }
    public void Fire()
    {
        //Instantiate(prefab, transform.position, transform.rotation);
        GameObjectPool.Instance.CreateObject("bullet", prefab, transform.position, transform.rotation);
    }
    private void OnGUI()
    {
        if (GUILayout.Button("开火"))
        {
            Fire();
        }
    }
}
