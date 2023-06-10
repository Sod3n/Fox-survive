using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class playercontroller : MonoBehaviour
{

	public float speed = 1.5f; // макс. скорость
	public float acceleration = 100f; // ускорение

	public Transform rotate; // объект вращения (локальный)

	public KeyCode jumpButton = KeyCode.Space; // клавиша для прыжка
	public float jumpForce = 10; // сила прыжка
	public float jumpDistance = 2;
	public bool doubleJump = false;
	public bool speedBoost = false;
	public float plusHarvestSpeedOnSpeedBoost = 2f;

	
    public List<Transform> foots;
    

	public Animator animator;

	private foot footScript;
	private needs needs;
	private bool onGround;
	private float doubleJumpState = 0;
	private Vector3 direction;
	private float h, v;
	private int layerMask;
	private Rigidbody body;
	private float rotationY;
	private float rotationX;
	private float speedOfHarvest;

	private Quaternion startRot;

	void Awake()
	{
		body = GetComponent<Rigidbody>();
		//body.freezeRotation = true;
		gameObject.tag = "Player";

		needs = GetComponent<needs>();

		// объекту должен быть присвоен отдельный слой, для работы прыжка
		layerMask = 1 << gameObject.layer | 1 << 2;
		layerMask = ~layerMask;
	}
	void Start()
	{
		//mainMenu = GameObject.FindGameObjectWithTag("MainMenu");
		//plInf = GameObject.Find("playerInfo");
		startRot = transform.rotation;
		Debug.Log(startRot);
	}

	void FixedUpdate()
	{
		if (!pause.Pause)
		{
			body.isKinematic = false;
			
			if (Input.GetButton("speedBoost") && speedBoost == true && GetJump())
            {
				//body.velocity = direction * speed * 0.01f * 2;
				//transform.Translate(direction * speed * Time.deltaTime * 2);
				//body.AddForce(direction * speed * 0.01f * 2, ForceMode.VelocityChange);
				transform.position += direction * speed * 0.001f * 2;
                if (!needs.sprint)
                {
					needs.sprint = true;
					needs.speedOfHarvest += plusHarvestSpeedOnSpeedBoost;
				}
			}
            else
            {
				//body.velocity = direction * speed * 0.01f;
				//body.AddForce(direction * speed * 0.01f, ForceMode.VelocityChange);
				//transform.Translate(direction * speed * 0.001f);
				transform.position += direction * speed * 0.001f;

			}
				
		}
        else
        {
			body.isKinematic = true;
        }
		
	}

	bool GetJump() // проверяем, есть ли коллайдер под ногами
	{
		bool result = false;

		onGround = false;

		int layerMask = 1 << 8;
        layerMask = ~layerMask;
        RaycastHit hit;
		foreach(Transform foot in foots)
		{
            if (Physics.Raycast(foot.transform.position, Vector3.down, out hit, jumpDistance, layerMask))
            {
                Debug.DrawRay(foot.transform.position, Vector3.down * hit.distance, Color.yellow);
                onGround = true;
            }
        }

        if (onGround)
		{
			result = true;
			doubleJumpState = 0;
		}
		else if(doubleJump == true && doubleJumpState == 0)
		{
			result = true;
		}

		return result;
	}

	void Update()
	{
		Debug.DrawRay(foots[0].transform.position, Vector3.down * jumpDistance, Color.yellow);
        if (!pause.Pause)
		{
			h = Input.GetAxis("Horizontal");
			v = Input.GetAxis("Vertical");

			// вектор направления движения
			direction = new Vector3(h, 0, v);
			/*if (direction == Vector3.zero || !footScript.onGround)
			{
				animator.SetBool("walk", false);
				animator.SetBool("Idle", true);
			}
			else
            {
				animator.SetBool("walk", true);
				animator.SetBool("Idle", false);
			}*/
			direction = Camera.main.transform.TransformDirection(direction);
			direction = new Vector3(direction.x, 0, direction.z);

            /*RaycastHit RayHit;

			Debug.DrawRay(transform.position + Vector3.up * 0.01f - transform.forward * 0.2f, Vector3.down * 0.1f, Color.red);

			if (Physics.Raycast(transform.position + Vector3.up*0.01f - transform.forward * 0.2f, Vector3.down, out RayHit, 0.1f) || Physics.Raycast(transform.position + Vector3.up * 0.01f, Vector3.down, out RayHit, 0.1f) || Physics.Raycast(transform.position + Vector3.up * 0.01f + transform.forward * 0.2f, Vector3.down, out RayHit, 0.1f))
			{
				footScript.onGround = true;
				if (RayHit.collider.gameObject.tag == "Ground" || RayHit.collider.gameObject.tag == "Ground_two")
                {
					body.freezeRotation = false;
					float y = transform.eulerAngles.y;
					float z = transform.eulerAngles.z;
					Quaternion endPoint = Quaternion.FromToRotation(transform.up, RayHit.normal);
					endPoint = Quaternion.Lerp(transform.rotation, endPoint, 1f * Time.deltaTime);
					transform.rotation = Quaternion.Euler(new Vector3(endPoint.eulerAngles.x, y, z));
					Debug.Log(endPoint.eulerAngles + " " + transform.rotation.eulerAngles);
					if ((Mathf.Abs(endPoint.eulerAngles.x - transform.eulerAngles.x) > 3))
					{
						
						transform.rotation = Quaternion.Euler(new Vector3(endPoint.eulerAngles.x, y, z));
					}
				}
                else
                {
					body.freezeRotation = true;
				}
			}
			else
			{
				body.freezeRotation = false;
				footScript.onGround = false;
				float y = transform.eulerAngles.y;
				float z = transform.eulerAngles.z;
				Quaternion endPoint = startRot;
				Debug.Log(endPoint + " " + transform.rotation);
				if ((Mathf.Abs(transform.rotation.x - endPoint.x) > 0.01) || (Mathf.Abs(transform.rotation.y - endPoint.y) > 0.01) || (Mathf.Abs(transform.rotation.z - endPoint.z) > 0.01) || (Mathf.Abs(transform.rotation.w - endPoint.w) > 0.01))
				{
					Debug.Log("1");
					transform.rotation = Quaternion.Lerp(transform.rotation, endPoint, 10 * Time.deltaTime);
					transform.rotation = Quaternion.Euler(new Vector3(transform.eulerAngles.x, y, transform.eulerAngles.z));
				}
			}*/

			if (Mathf.Abs(v) > 0 || Mathf.Abs(h) > 0) // разворот тела по вектору движения
			{
				float x = transform.localEulerAngles.x;
				rotate.localRotation = Quaternion.Lerp(rotate.localRotation, Quaternion.LookRotation(-direction), 10 * Time.deltaTime);
				transform.localRotation = Quaternion.Euler(new Vector3(x, transform.localEulerAngles.y, transform.localEulerAngles.z));
			}

			//Debug.DrawRay(transform.position, Vector3.down * jumpDistance, Color.red); // подсветка, для визуальной настройки jumpDistance

			if (Input.GetKeyDown(jumpButton) && GetJump())
			{
				if(!onGround)
					doubleJumpState = 1;
                body.velocity = new Vector2(0, jumpForce);
            }

			if (Input.GetButtonUp("speedBoost") && speedBoost == true)
			{
				if (needs.sprint)
				{
					needs.sprint = false;
					needs.speedOfHarvest -= plusHarvestSpeedOnSpeedBoost;
				}

			}

		}
	}
	/*void OnCollisionStay(Collision col)
	{
		if(col.gameObject.tag == "Ground" || col.gameObject.tag == "Ground_two")
			onGround = true;
	}
	void OnCollisionExit(Collision col)
	{
		if (col.gameObject.tag == "Ground" || col.gameObject.tag == "Ground_two")
			onGround = false;
	}
	*/
}
