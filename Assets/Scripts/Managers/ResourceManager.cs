using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            // name.LastIndex(char value)는 값이 없을 시 -1을 반환
            if (index > -1)
            {
                // /이후가 아이템 이름일테니 index의 1부터 string.Substring으로 자름
                name = name.Substring(index + 1);
            }
            // 풀매니저 들어갈 곳
            
        }

        return Resources.Load<T>(path);
    }

    public T[] LoadAll<T>(string path) where T : Object
    {
        if (typeof(T) == typeof(GameObject))
        {
            string name = path;
            int index = name.LastIndexOf('/');
            if (index > -1)
            {
                name = name.Substring(index + 1);
            }
        }
        // 풀 안에 이름 있는지 확인 후 있으면 return ~
        
        // 아니면 배열 생성 후 반환
        T[] array = Resources.LoadAll<T>(path);
        return array;
    }

    public GameObject Instantiate(string path, Transform parent = null)
    {
        GameObject original = Load<GameObject>(path);
        // 경로에 오브젝트가 없으면 리턴
        if (!original)
        {
            Debug.Log($"Failed to load prefab : {path}");
            return null;
        }
        
        // 풀링된 게 있으면 꺼내주기
        // return ~
        // if (Manager.Pool.)
        // 아니면 새로 생성해서 주기
        GameObject go = Object.Instantiate(original, parent);
        go.name = original.name;
        return go;
    }

    public void Destroy(GameObject go)
    {
        // 풀에 있으면 풀에 다시 집어넣기 없으면 그냥파괴
        
        Object.Destroy(go);
    }
}
