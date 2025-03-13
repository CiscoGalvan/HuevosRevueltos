using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboar : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		transform.rotation = Quaternion.Euler(Camera.main.transform.rotation.eulerAngles.x, 0f, 0f);
	}

    // Update is called once per frame
    void Update()
    {
		
	}
}
