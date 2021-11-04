using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDropBehaviour 
{
    // Start is called before the first frame update
    void Pop(float tme);
    void Bounce(); 
}
