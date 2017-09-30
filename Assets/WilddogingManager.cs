using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using GameFramework;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Networking;
using UnityGameFramework.Runtime;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public enum WilddogEventType
{
	PUT,
	PATCH,
	KEEP_ALIVE,
	AUTH_REVOKED
}

public delegate void OnWilddogingMes(JObject data);
public class WilddogingManager : MonoBehaviour
{
	private class JsonHelper
	{
		public string path;
		public JObject data;
	}
	private static WilddogingManager mInstance;

	public static WilddogingManager Instance
	{
		get
		{
			if (mInstance == null)
			{
				mInstance = FindObjectOfType<WilddogingManager>();
				if (mInstance == null)
				{
					GameObject ga = new GameObject("WilddogingManager");
					mInstance = ga.AddComponent<WilddogingManager>();
				}
			}
			return mInstance;
		}
	}
	
	private const string url = "https://wd5351207252cliwhx.wilddogio.com/";
	Dictionary<string, string> putHeaders = new Dictionary<string, string>() {{"X-HTTP-Method-Override","PUT"}};

	
	
	public void GetStream(string path,OnWilddogingMes onRes)
	{
		StartCoroutine(_getStream(path,onRes));
	}

	public void PutStream(string path,Dictionary<string,object> data)
	{
		StartCoroutine(_putStream(path, data));
	}
	
	private IEnumerator _getStream(string path,OnWilddogingMes onRes)
	{
		string mUrl = url + path;
		WWW www = new WWW(mUrl);
		yield return www;
		onRes.Invoke(JsonConvert.DeserializeObject<JObject>(www.text));
	}

	private IEnumerator _putStream(string path, Dictionary<string,object> data)
	{
		WWW www = new WWW(url + path,Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(data)),putHeaders);
		yield return www;
	}
	
	/// <summary>
	/// 字符串转Vector3
	/// </summary>
	/// <param name="p_sVec3">需要转换的字符串</param>
	/// <returns></returns>
	public static Vector3 GetVec3ByString(string p_sVec3)
	{
		if (p_sVec3.Length <= 0)
			return Vector3.zero;

		string[] tmp_sValues = p_sVec3.Trim('(').Trim(')').Trim(' ').Split(',');
		if (tmp_sValues != null && tmp_sValues.Length == 3)
		{
			float tmp_fX = float.Parse(tmp_sValues[0]);
			float tmp_fY = float.Parse(tmp_sValues[1]);
			float tmp_fZ = float.Parse(tmp_sValues[2]);

			return new Vector3(tmp_fX, tmp_fY, tmp_fZ);
		}
		return Vector3.zero;
	}
	/// <summary>
	/// 字符串转换Quaternion
	/// </summary>
	/// <param name="p_sVec3">需要转换的字符串</param>
	/// <returns></returns>
	public static Quaternion GetQuaByString(string p_sVec3)
	{
		if (p_sVec3.Length <= 0)
			return Quaternion.identity;

		string[] tmp_sValues = p_sVec3.Trim('(').Trim(')').Trim(' ').Split(',');
		if (tmp_sValues != null && tmp_sValues.Length == 4)
		{
			float tmp_fX = float.Parse(tmp_sValues[0]);
			float tmp_fY = float.Parse(tmp_sValues[1]);
			float tmp_fZ = float.Parse(tmp_sValues[2]);
			float tmp_fH = float.Parse(tmp_sValues[3]);

			return new Quaternion(tmp_fX, tmp_fY, tmp_fZ, tmp_fH);
		}
		return Quaternion.identity;
	}
}
