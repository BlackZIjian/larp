using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {

	private static PlayerManager mInstance;

	public static PlayerManager Instance
	{
		get
		{
			return mInstance;
		}
	}

	public GameObject playerPrefab;
	
	private void Awake()
	{
		mInstance = this;
		GameObject ga = Instantiate(playerPrefab);
		ga.name = "God";
		PlayerMove player = ga.GetComponent<PlayerMove>();
		player.mName = "God";
		AddPlayer("God",player);
	}

	private Dictionary<string, PlayerMove> mPlayers;

	PlayerManager()
	{
		mPlayers = new Dictionary<string, PlayerMove>();
	}

	public void AddPlayer(string name, PlayerMove item)
	{
		mPlayers[name] = item;
	}

	public PlayerMove GetPlayer(string name)
	{
		if (mPlayers.ContainsKey(name))
		{
			return mPlayers[name];
		}
		return null;
	}
}
