using Godot;
using System;

public partial class HealthBar : TextureProgressBar {

	[Export] private Damageable target;

	public override void _Process(double delta) {
		base._Process(delta);

		this.MaxValue = target.MaxHealth;
		this.Value = this.MaxValue - target.Health;
	}

}
