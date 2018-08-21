using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General {
	public const int interactableLayer = 9;
	public const int interactableLayerMask = 1 << 9;

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

		public const string GameEnd = "game.end@event";
	}
}
