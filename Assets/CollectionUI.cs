using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;

public class CollectionUI : MonoBehaviour
{

	private List<GameObject> uis;

	private bool isToggle;

	private GameObject mGroup;

	private int nowCount;

	private void Awake()
	{
		uis = new List<GameObject>();
		for (int i = 0; i < 12; i++)
		{
			GameObject ga = transform.Find("group/item" + i).gameObject;
			uis.Add(ga);
		}
		mGroup = transform.Find("group").gameObject;
		EventManager.Instance.AddListener(EventType.RecordItem, OnAddRecord);
	}

	// Use this for initialization
	void Start () {
		
	}

	private void OnAddRecord(string[] args)
	{
		string itemName = args[0];
		string playerName = args[1];
		if (playerName == PlayerMove.LocalPlayer.mName)
		{
			ItemObject item = ItemObjectManager.Instance.GetItemObject(itemName);
			uis[nowCount].SetActive(true);
			uis[nowCount].GetComponent<RawImage>().texture = item.mTexture;
			uis[nowCount].transform.Find("Text").GetComponent<Text>().text = item.mText;
			nowCount++;
		}
	}

	public void Refresh()
	{
		Dictionary<string, ItemObject> items = PlayerMove.LocalPlayer.mCollectionItemObjects;
		int index = 0;
		foreach (var item in items)
		{
			uis[index].SetActive(true);
			uis[index].GetComponent<RawImage>().texture = item.Value.mTexture;
			uis[index].transform.Find("Text").GetComponent<Text>().text = item.Value.mText;
			index++;
		}
		nowCount = index;
		for (int i = index; i < uis.Count; i++)
		{
			uis[i].SetActive(false);
		}
	}
	
	public void Toggle()
	{
		isToggle = !isToggle;
		if (!isToggle)
		{
			mGroup.SetActive(false);
			PlayerMove.LocalPlayer.mIsCanControll = true;
		}
		else
		{
			mGroup.SetActive(true);
			PlayerMove.LocalPlayer.mIsCanControll = false;
			Refresh();
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
