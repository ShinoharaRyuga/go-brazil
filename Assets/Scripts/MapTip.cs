using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTip : MonoBehaviour
{
   
    Status _status = Status.Road;
    SpriteRenderer _spriteRenderer => GetComponent<SpriteRenderer>();
    public Status Status 
    {
        get { return _status; }
        set
        {
            _status = value;
            ChangeColor();
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    
    void ChangeColor()
    {
        switch (_status)
        {
            case Status.Road:
                _spriteRenderer.color = Color.black;
                break;
            case Status.Wall:
                _spriteRenderer.color = Color.red;
                break;
        }
    }
}

public enum Status
{
    Wall,
    Road,
}
