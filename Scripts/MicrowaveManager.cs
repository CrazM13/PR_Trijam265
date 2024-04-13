using Godot;
using System;

public partial class MicrowaveManager : ProjectilePool {


	public void Fire(Vector2 position, Vector2 direction) {

		ProjectileMicrowave newMicrowave = this.Spawn() as ProjectileMicrowave;

		if (newMicrowave != null) {
			AddChild(newMicrowave);
			newMicrowave.GlobalPosition = position;
			newMicrowave.LookAt(position + direction);

			newMicrowave.Pool = this;
			newMicrowave.Direction = direction;
		}

	}

}
