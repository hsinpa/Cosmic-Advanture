using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General {
	public const int unitLayer = 9;
	public const int unitLayerMask = 1 << 9;

	public const int vehicleLayer = 10;
	public const int vehicleLayerMask = 1 << 10;
}

public class PoolingID {
	public const int TerrainPlain = 10001;
	public const int TerrainTrain = 10003;
	public const int TerrainRoad = 10002;

	public const int ObstacleTree = 20001;
	public const int ObstacleItems = 20001;
}

public class EventFlag {
	public class Game {
		public const string SetUp = "game.setup@event";
		public const string EnterGame = "game.enter@event";

		public const string Restart = "game.start@event";

		public const string EnemyMove = "enemy.move@event";
		public const string PlayerMove = "player.move@event";

		public const string GameEnd = "game.end@event";
	}

	public class AIAgent {
		public const string MeetInline = "meet_emeny_inline";
		public const string HitBlock = "hit_block";
		public const string UnderAttack = "under_attack";
	}

	public class AIEvent {
		public const string LINK = "LINK";
	}
	
}
