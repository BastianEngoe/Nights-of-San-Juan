using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Euler(0,
        Camera.main.transform.rotation.eulerAngles.y,
        0);
        transform.LookAt(Camera.main.transform);
    }
}
