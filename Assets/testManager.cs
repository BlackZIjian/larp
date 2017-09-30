﻿using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class MVector3
{
	public float x;
	public float y;
	public float z;

	public MVector3(float tx, float ty, float tz)
	{
		x = tx;
		y = ty;
		z = tz;
	}

	public MVector3()
	{
		
	}

	public new string ToString()
	{
		return "("+x+","+y+","+z+")";
	}

	public static MVector3 Parse(Vector3 unityVector3)
	{
		MVector3 mv3 = new MVector3();
		mv3.x = unityVector3.x;
		mv3.y = unityVector3.y;
		mv3.z = unityVector3.z;
		return mv3;
	}

	public Vector3 ToVector3()
	{
		return new Vector3(x,y,z);
	}
}

public class testManager : MonoBehaviour
{

	Dictionary<string,object> localUserInfo = new Dictionary<string, object>();
	Dictionary<string,Transform> userTransforms = new Dictionary<string, Transform>();
	private Transform localTransform;

	// Use this for initialization
	private void Start()
	{
		localUserInfo["name"] = Random.Range(0, 100000).ToString();
		localUserInfo["positon"] = new MVector3(Random.Range(-5f, 5f), 5, Random.Range(-5f, 5f));
		
	}

	// Update is called once per frame
	void Update()
	{
		WilddogingManager.Instance.GetStream("users.json", data =>
		{
			if (data != null)
			{
				foreach (var player in data)
				{
					if (player.Key != (string)(localUserInfo["name"]))
					{
						JToken postion = player.Value["positon"];
						if (postion == null)
						{
							continue;
						}
						MVector3 pos = postion.ToObject<MVector3>();
						Transform trans = null;
						if(!userTransforms.ContainsKey(player.Key))
						{
							GameObject ga = new GameObject(player.Key);
							trans = ga.transform;
							userTransforms.Add(player.Key,trans);
						}
						else
						{
							trans = userTransforms[player.Key];
						}
						trans.position = pos.ToVector3();
					}
				}
			}
			WilddogingManager.Instance.PutStream("users/" + localUserInfo["name"] + ".json", localUserInfo);
		});
	}
}
