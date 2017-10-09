using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

	public string mName;

	public bool mIsOnlyOnce;
	
	public string mText;

	private GameObject mModel;

	public string attachPlayer;

	public Texture2D mTexture;

	private void Awake()
	{
		mName = gameObject.name;
		ItemObjectManager.Instance.AddItemObject(mName,this);
		mModel = transform.Find("model").gameObject;
	}

	public void AttachTo(string playerName)
	{
		PlayerManager.Instance.GetPlayer(playerName).mCollectionItemObjects[mName] = this;
		if (mIsOnlyOnce)
		{
			attachPlayer = playerName;
			mModel.SetActive(false);
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
