using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObjectManager
{


    private static ItemObjectManager mInstance;

    public static ItemObjectManager Instance
    {
        get
        {
            if(mInstance == null)
                mInstance = new ItemObjectManager();
            return mInstance;
        }
    }

    private Dictionary<string, ItemObject> mItemObjects;

    ItemObjectManager()
    {
        mItemObjects = new Dictionary<string, ItemObject>();
        EventManager.Instance.AddListener(EventType.RecordItem, RecordItem);
    }

    public void AddItemObject(string name, ItemObject item)
    {
        mItemObjects[name] = item;
    }

    public ItemObject GetItemObject(string name)
    {
        if (mItemObjects.ContainsKey(name))
        {
            return mItemObjects[name];
        }
        return null;
    }

    private void RecordItem(string[] args)
    {
        string itemName = args[0];
        string playerName = args[1];
        mItemObjects[itemName].AttachTo(playerName);
    }
}
