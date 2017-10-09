using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EventType
{
	RecordItem
}

public delegate void OnEvent(params string[] args);
public class EventManager
{
	private static EventManager mInstance;

	public static EventManager Instance
	{
		get 
		{
			if(mInstance == null)
				mInstance = new EventManager();
			return mInstance;
		}
	}

	private Dictionary<EventType, OnEvent> mEventListeners;

	private Dictionary<string, object> mEventInfo;

	EventManager()
	{
		mEventListeners = new Dictionary<EventType, OnEvent>();
		mEventInfo = new Dictionary<string, object>();
	}
	
	public void AddListener(EventType type, OnEvent callback)
	{
		if (mEventListeners.ContainsKey(type))
		{
			mEventListeners[type] += callback;
		}
		else
		{
			mEventListeners[type] = callback;
		}
	}

	public void RemoveListener(EventType type, OnEvent listener)
	{
		if (mEventListeners.ContainsKey(type))
		{
			mEventListeners[type] -= listener;
		}
	}

	public void Invoke(EventType type,bool isNet = false,params string[] args)
	{
		if (mEventListeners.ContainsKey(type) && mEventListeners[type] != null)
		{
			mEventListeners[type].Invoke(args);
		}
		return;
		if (isNet)
		{
			mEventInfo.Clear();
			string timeStramp = System.DateTime.Now.ToShortTimeString();
			mEventInfo["time"] = timeStramp;
			mEventInfo["type"] = type;
			mEventInfo["args"] = args;
			WilddogingManager.Instance.PutStream("events/" + timeStramp + ".json", mEventInfo);
			WilddogingManager.Instance.PutStream("events/lastTimeStramp.json",timeStramp);
		}
	}
}
