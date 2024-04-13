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

	public BuildingStates CurrentState { get; private set; } = BuildingStates.NORMAL;

	public override void _Ready() {
		base._Ready();

		GetTree().Root.GetChild(0).GetNode<BuildingManager>("BuildingManager").RegisterBuilding(this);

		this.Health = this.MaxHealth;
		this.OnDamage += ReactionToDamage;
	}

	public override void _Process(double delta) {
		base._Process(delta);

		microwave?.UpdateTargeting(this.Manager.Target);
	}

	private void ReactionToDamage() {
		if (Health > 0 && Health > -100) {
			Health = 0;
			if (CurrentState == BuildingStates.NORMAL) {
				CurrentState = BuildingStates.COOKED;
				displaySprite.Texture = buildingSprites[(int) CurrentState];
			}
		} else if (Health <= -100) {
			Health = -100;
			if (CurrentState == BuildingStates.NORMAL || CurrentState == BuildingStates.COOKED) {
				CurrentState = BuildingStates.EATEN;
				displaySprite.Texture = buildingSprites[(int) CurrentState];
			}
		}
	}

}
