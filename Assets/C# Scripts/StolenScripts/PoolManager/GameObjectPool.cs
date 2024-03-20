using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameObjectPool
{
    public GameObject parentGameObject;

    public Queue<GameObject> gameObjectQueue=new Queue<GameObject>();
    
    public GameObjectPool(GameObject gameObject, GameObject poolRoot)
    {
        parentGameObject = new GameObject(gameObject.name);
        parentGameObject.transform.SetParent(poolRoot.transform);

        gameObjectQueue = new Queue<GameObject>();
        PushGameObject(gameObject);
    }
    
    public void PushGameObject(GameObject gameObject)
    {
        gameObjectQueue.Enqueue(gameObject);
        gameObject.transform.SetParent(parentGameObject.transform);
        gameObject.SetActive(false);
    }
    
    public GameObject GetGameObject(Transform parent=null)
    {
        GameObject gameObject = gameObjectQueue.Dequeue();
        gameObject.transform.SetParent(null);
        SceneManager.MoveGameObjectToScene(gameObject,SceneManager.GetActiveScene());
        gameObject.transform.SetParent(parent);
        gameObject.SetActive(true);
        return gameObject;
    }

}