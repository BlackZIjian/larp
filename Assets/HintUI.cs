using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintUI : MonoBehaviour
{

	private static HintUI mInstance;

	public static HintUI Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = FindObjectOfType<HintUI>();
				if (mInstance == null)
				{
					Debug.LogError("No Hint");
				}
			}
			return mInstance;
		}
	}

	public Text mText;

	private void Awake()
	{
		Hide();
		mInstance = this;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private ItemObject mShowItem;
	public void Show(string text = null,ItemObject item = null)
	{
		gameObject.SetActive(true);
		if (text != null)
			mText.text = text;
		PlayerMove.LocalPlayer.mIsCanControll = false;
		if (item != null)
		{
			mShowItem = item;
		}
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		if (PlayerMove.LocalPlayer != null)
			PlayerMove.LocalPlayer.mIsCanControll = true;
		mShowItem = null;
	}

	public void SetText(string text)
	{
		mText.text = text;
	}

	public void Record()
	{
		if (mShowItem != null)
		{
			//发送记录事件
			EventManager.Instance.Invoke(EventType.RecordItem,true,mShowItem.mName,PlayerMove.LocalPlayer.mName);
		}
		Hide();
	}
}
