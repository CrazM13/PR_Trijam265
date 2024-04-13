using Godot;
using System;

public partial class EatScript : RayCast2D {

	[Export] private AudioManager audioManager;

	public override void _PhysicsProcess(double delta) {
		base._PhysicsProcess(delta);

		GodotObject collider = this.GetCollider();
		if (collider is Building building) {
			if (building.CurrentState == Building.BuildingStates.COOKED) {
				building.OnDamageReceived(1000);

				audioManager.PlayLocal(0, GlobalPosition);
			}
		}

		if (collider is PedestrianAI pedestrian) {
			if (pedestrian.CurrentState == PedestrianAI.PedestrianStates.COOKED) {
				pedestrian.OnDamageReceived(1000);

				audioManager.PlayLocal(0, GlobalPosition);
			}
		}
	}

}
