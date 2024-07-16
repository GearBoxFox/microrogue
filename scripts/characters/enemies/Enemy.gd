extends Character

@onready var player: Character = get_tree().current_scene.get_node("Player")
@onready var path_timer: Timer = get_node("PathTimer")
@onready var nav_agent: NavigationAgent2D = get_node("NavigationAgent")

func get_input() -> void:
	if not nav_agent.is_target_reached():
		var vector_to_next_target: Vector2 = nav_agent.get_next_path_position() - global_position
		move_direction = vector_to_next_target
		
		if vector_to_next_target.x > 0 and animated_sprite.flip_h:
			animated_sprite.flip_h = false
		elif vector_to_next_target.x < 0 and not animated_sprite.flip_h:
			animated_sprite.flip_h = true
			
func _on_path_timer_timeout() -> void:
	if is_instance_valid(player):
		_get_path_to_player()
	else:
		path_timer.stop()
		move_direction = Vector2.ZERO

func _get_path_to_player() -> void:
	nav_agent.target_position = player.global_position
