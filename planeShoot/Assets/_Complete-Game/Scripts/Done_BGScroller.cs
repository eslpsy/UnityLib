using UnityEngine;
using System.Collections;

public class Done_BGScroller : MonoBehaviour
{
	public float scrollSpeed;       //背景移动速度
	public float tileSizeZ;         //背景y值大小

	private Vector3 startPosition;  //背景的初始位置

	void Start ()
	{
		startPosition = transform.position;
	}

	void Update ()
	{
        // 上下放了两张同样的图片，按当前帧计算出当前背景移动的距离，repeat得到从0增长到tileSizeZ之后从0重新开始
        float newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileSizeZ);   
		transform.position = startPosition + Vector3.forward * newPosition;
	}
}