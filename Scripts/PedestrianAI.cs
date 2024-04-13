using Godot;
using System;

public partial class PedestrianAI : Damageable {

	[Export] private Texture2D[] buildingSprites;
	[Export] private Sprite2D displaySprite;

	[Export] private float movementSpeed;
	[Export] private float decisionRate;
	[Export] private float startledMultiplier;

	private float timeUntilDecision;
	private int direction;

	private float panicTime = 0;

	public enum PedestrianStates {
		NORMAL = 0,
		COOKED = 1,
		EATEN = 2
	}

	private PedestrianStates currentState = PedestrianStates.NORMAL;

	public override void _Ready() {
		base._Ready();

		this.OnDamage += ReactionToDamage;

		timeUntilDecision = decisionRate;
	}

	private void ReactionToDamage() {
		panicTime += 10f;
		if (direction == 0) {
			if (this.GlobalPosition.X % 2 == 0) direction = 1;
			else direction = -1;
		}

		if (Health > 0 && Health > -100) {
			Health = 0;
			if (currentState == PedestrianStates.NORMAL) {
				currentState = PedestrianStates.COOKED;
				displaySprite.Texture = buildingSprites[(int) currentState];
			}
		} else if (Health <= -100) {
			Health = -100;
			QueueFree();
		}
	}

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		float modDelta = (float) delta * (panicTime > 0 ? startledMultiplier : 1);
		timeUntilDecision -= modDelta;

		if (timeUntilDecision <= 0) {
			timeUntilDecision += decisionRate;

			int decision = (int) (this.GlobalPosition.X + this.GlobalPosition.Y + (modDelta * Mathf.Pi)) % 3;

			if (panicTime > 0 && decision == 0) {
				if (this.GlobalPosition.X % 2 == 0) decision = 1;
				else decision = 2;
			}

			switch (decision) {
				case 0:
					if (direction != 0)
						direction = 0;
					else direction = 1;
					break;
				case 1:
					if (direction != 1)
						direction = 1;
					else direction = -1;
					break;
				case 2:
					if (direction != -1)
						direction = -1;
					else direction = 1;
					break;
			}
        }

		if (direction != 0) {
			if (MoveAndCollide(Vector2.Right * direction * movementSpeed) != null) {
				direction *= -1;
			}
		}
	}

}
