using Godot;
using System;

public partial class BuildingManager : Node {

	[Export] SceneTransition sceneManager;
	[Export] public Node2D Target { get; set; }

	private int currentBuildingCount;

	public void RegisterBuilding(Building building) {
		this.currentBuildingCount++;

		building.Manager = this;
	}

	public void ConsumeBuilding() {
		this.currentBuildingCount--;

		if (this.currentBuildingCount <= 0) {
			// WIN
			sceneManager.ReloadScene();
		}
	}


}
