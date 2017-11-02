using UnityEngine;
using System.Collections;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;    //一次FixedUpdate移动的限定范围
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;                 // 玩家速度
	public float tilt;                  // 左右旋转偏转系数
	public Done_Boundary boundary;      

	public GameObject shot;             // 子弹
	public Transform shotSpawn;         // 子弹产生位置
	public float fireRate;              // 子弹间隔事件
	 
	private float nextFire;

    private void Awake()
    {
        this.nextFire = 0;
    }

    void Update ()
	{
		if (Input.GetButton("Fire1") && Time.time > nextFire)           // 按下"Fire1"按键，可以在project setting中自定义"Fire1"
		{
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);  // 产生一个子弹
			GetComponent<AudioSource>().Play ();                        // 播放玩家攻击音效
		}
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");    //一次FixedUpdate水平偏移总值
		float moveVertical = Input.GetAxis ("Vertical");        //一次FixedUpdate竖直偏移总值

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		GetComponent<Rigidbody>().velocity = movement * speed;
		
		GetComponent<Rigidbody>().position = new Vector3    //移动到新的坐标
		(
            // Mathf.Clamp(a, min, max)将a限定在min和max之间 a < min => a = min; a > max => a = max.
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
        // 根据x方向移动的距离和系数tilt确定相对于z轴旋转的角度
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
	}
}
