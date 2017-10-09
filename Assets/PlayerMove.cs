using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	public string mName;
	
	private Transform mTransform;

	public bool isLocal;

	private const float moveSpeed = 5;
	
	Vector2 moveVector = Vector2.zero;

	private HashSet<ItemObject> mFocusingObject;

	public Dictionary<string, ItemObject> mCollectionItemObjects;

	public bool mIsCanControll;

	private static PlayerMove mLocalPlayer;
	
	public static PlayerMove LocalPlayer
	{
		get { return mLocalPlayer; }
	}

	private void Awake()
	{
		mTransform = transform;
		mFocusingObject = new HashSet<ItemObject>();
		mCollectionItemObjects = new Dictionary<string, ItemObject>();
		mIsCanControll = true;
	}

	// Use this for initialization
	void Start () {
		if (isLocal)
		{
			mLocalPlayer = this;
		}
		
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		moveVector = Vector2.zero;
		if (isLocal && mIsCanControll)
		{
			if (Input.GetKey(KeyCode.W))
			{
				moveVector.y += moveSpeed * Time.fixedDeltaTime;
			}
			if (Input.GetKey(KeyCode.S))
			{
				moveVector.y -= moveSpeed * Time.fixedDeltaTime;
			}
			if (Input.GetKey(KeyCode.A))
			{
				moveVector.x -= moveSpeed * Time.fixedDeltaTime;
			}
			if (Input.GetKey(KeyCode.D))
			{
				moveVector.x += moveSpeed * Time.fixedDeltaTime;
			}
			Move(moveVector);
		}
		else
		{
			
		}
	}

	private void Update()
	{
		if (isLocal && mIsCanControll)
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (mFocusingObject.Count > 0)
				{
					ItemObject item = mFocusingObject.First();
					HintUI.Instance.Show(item.mText,item);
				}
				else
				{
					HintUI.Instance.Show("没有什么异常");
				}
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (isLocal)
		{
			if (other.tag == "Item")
			{
				ItemObject item = other.GetComponentInParent<ItemObject>();
				if (item != null && !mFocusingObject.Contains(item))
				{
					mFocusingObject.Add(item);
				}
			}
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (isLocal)
		{
			if (other.tag == "Item")
			{
				ItemObject item = other.GetComponentInParent<ItemObject>();
				if (item != null && mFocusingObject.Contains(item))
				{
					mFocusingObject.Remove(item);
				}
			}
		}
	}

	public void Move(Vector2 velocity)
	{
		mTransform.position += new Vector3(velocity.x,velocity.y,0);
	}

	public void SetPos(Vector2 pos)
	{
		mTransform.position = new Vector3(pos.x,pos.y,0);
	}

	public void SmoothTo(Vector2 pos,float speed)
	{
		StartCoroutine(_smoothTo(pos, speed));
	}

	IEnumerator _smoothTo(Vector2 pos,float speed)
	{
		Vector3 target = new Vector3(pos.x,pos.y,0);
		yield return new WaitUntil(() =>
		{
			mTransform.position = Vector3.MoveTowards(mTransform.position,target,speed * Time.deltaTime);
			return mTransform.position == target;
		});
	}
}
