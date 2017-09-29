using UnityEngine;
using System;
using System.Collections.Generic;

namespace DuloGames.UI
{
	[Serializable]
	public class UIItemInfo
	{
		public int ID;
		public string Name;
		public Sprite Icon;
		public string Description;
        public UIItemQuality Quality;
        public UIEquipmentType EquipType;
		public int ItemType;
		public string Type;
		public string Subtype;
		public int Damage;
		public float AttackSpeed;
		public int Block;
		public int Armor;
        //public int Stamina;
        //public int Strength;
        public Dictionary<string, float> ItemAttributes = new Dictionary<string, float>();
        public int Durability;
        public int RequiredLevel;

        public void TestVoid ()
        {
            foreach (KeyValuePair<string, float> attachStat in ItemAttributes)
            {
                //Now you can access the key and value both separately from this attachStat as:
                Debug.Log(attachStat.Key);
                Debug.Log(attachStat.Value);
            }
        }
	}
}
