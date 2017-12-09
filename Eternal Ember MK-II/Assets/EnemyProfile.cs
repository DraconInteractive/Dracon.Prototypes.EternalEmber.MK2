using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Enemy", menuName = "Generate/Profile/Enemy", order = 0)]
public class EnemyProfile : ScriptableObject {
	public ItemContainerAdvanced items;
	public StatisticsProfile stats;
}
