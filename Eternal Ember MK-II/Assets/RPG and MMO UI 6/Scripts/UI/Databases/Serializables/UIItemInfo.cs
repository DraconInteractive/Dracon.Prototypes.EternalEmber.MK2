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
		public int Stamina;
		public int Strength;
        public int Durability;
        public int RequiredLevel;
		public Dictionary<string, float> ItemAttributes = new Dictionary<string, float>();
		public Vector3 cost = new Vector3();
	}
}
