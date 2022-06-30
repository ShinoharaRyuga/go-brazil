using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    [SerializeField] float _rotateValue = 0.2f;
    void Start()
    {
        
    }

    void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
   
        if (h == 1)
        {
           transform.Rotate(0, 0, _rotateValue);
        }
        else if(h == -1)
        {
            transform.Rotate(0, 0, -_rotateValue);
        }

    }
}
