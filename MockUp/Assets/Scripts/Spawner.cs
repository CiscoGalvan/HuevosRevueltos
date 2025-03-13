using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    List<GameObject> _prefabsobjects;
    [SerializeField]
    float amountoftime = 1.0f;
    float currenttime;
  
    // Update is called once per frame
    void Update()
    {
        currenttime += Time.deltaTime;
        if(currenttime>= amountoftime)
        {
            currenttime -= amountoftime;
            //random de la lista 
            if (_prefabsobjects.Count > 0)
            {
                int randomIndex = Random.Range(0, _prefabsobjects.Count);
                Instantiate(_prefabsobjects[randomIndex], transform.position, Quaternion.identity);
            }
        }
    }
}
