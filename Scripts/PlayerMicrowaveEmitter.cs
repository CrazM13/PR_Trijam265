using Godot;
using System;

public partial class PlayerMicrowaveEmitter : Node2D {

	[Export] private MicrowaveManager microwaveManager;
	[Export] private AudioManager audioManager;

	private bool isMousePressed = false;

	public override void _Process(double delta) {
		base._Process(delta);

		Vector2 mousePosition = GetViewport().GetMousePosition();

		LookAt(mousePosition);

		if (Input.IsMouseButtonPressed(MouseButton.Left)) {
			if (!isMousePressed) {
				microwaveManager.Fire(this.GlobalPosition, this.Transform.X);
				isMousePressed = true;
				audioManager.PlayLocal(2, GlobalPosition);
			}
		} else {
			isMousePressed = false;
		}
	}

}
