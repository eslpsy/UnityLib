using UnityEngine;
using System.Collections;

public class Done_DestroyByBoundary : MonoBehaviour
{
    //添加了一个box collider，当colider内部带有rigidbody的物体离开的时候，销毁它
    void OnTriggerExit (Collider other)     
	{
		Destroy(other.gameObject);
	}
}