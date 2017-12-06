using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainerAdvanced : ItemContainer {
	public Dictionary <Item_Type, ItemCount> itemDictionary;
	public List<ItemCount> quantities = new List<ItemCount>();
	// Use this for initialization
	void Start () {
		itemDictionary = new Dictionary<Item_Type, ItemCount> ();
		for (int i = 0; i < itemsInContainer.Count; i++) {
			itemDictionary.Add (itemsInContainer [i], quantities [i]);
		}
	}
}
[Serializable]
public class ItemCount {
	public enum CountType {SingleNumber, RandomNumber, NumberRange, RandomChoice};
	public CountType count;

	public int single;
	public int rangeMin;
	public int rangeMax;
	public List<int> choices = new List<int>();

	public int GetResult () {
		int res = 0;
		switch (count)
		{
		case CountType.SingleNumber:
			res = single;
			break;
		case CountType.RandomNumber:
			res = UnityEngine.Random.Range (0, 100);
			break;
		case CountType.NumberRange:
			res = UnityEngine.Random.Range (rangeMin, rangeMax);
			break;
		case CountType.RandomChoice:
			int r = UnityEngine.Random.Range (0, choices.Count);
			res = choices [r];
			break;
		}

		return res;
	}
}
