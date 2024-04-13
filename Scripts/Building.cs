using Godot;
using System;

public partial class Building : Damageable {

	[Export] private Texture2D[] buildingSprites;
	[Export] private Sprite2D displaySprite;
	[Export] private EnemyMicrowaveEmitter microwave;

	public BuildingManager Manager { get; set; }

	public enum BuildingStates {
		NORMAL = 0,
		COOKED = 1,
		EATEN = 2
	}

	private BuildingStates currentState = BuildingStates.NORMAL;

	public override void _Ready() {
		base._Ready();

		GetTree().Root.GetChild(0).GetNode<BuildingManager>("BuildingManager").RegisterBuilding(this);

		this.OnDamage += ReactionToDamage;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		microwave?.UpdateTargeting(this.Manager.Target);
	}

	private void ReactionToDamage() {
		if (Health > 0 && Health > -100) {
			Health = 0;
			if (currentState == BuildingStates.NORMAL) {
				currentState = BuildingStates.COOKED;
				displaySprite.Texture = buildingSprites[(int) currentState];
			}
		} else if (Health <= -100) {
			Health = -100;
			if (currentState == BuildingStates.NORMAL || currentState == BuildingStates.COOKED) {
				currentState = BuildingStates.EATEN;
				displaySprite.Texture = buildingSprites[(int) currentState];
			}
		}
	}

}
