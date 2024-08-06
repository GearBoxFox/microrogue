extends Character
class_name Enemy

@onready var player: Character = get_tree().current_scene.get_node("Player")
@onready var path_timer: Timer = get_node("PathTimer")
@onready var nav_agent: NavigationAgent2D = get_node("NavigationAgent")

func _ready() -> void:
	nav_agent.velocity_computed.connect(Callable(_on_navigation_agent_velocity_computed))

func get_input() -> void:
	if not nav_agent.is_target_reached():
		var next_path_position: Vector2 = nav_agent.get_next_path_position()
		var new_velocity: Vector2 = global_position.direction_to(next_path_position) * max_speed
		if nav_agent.avoidance_enabled:
			nav_agent.velocity = new_velocity
		else:
			_on_navigation_agent_velocity_computed(new_velocity)
		
		if velocity.x > 0 and animated_sprite.flip_h:
			animated_sprite.flip_h = false
		elif velocity.x < 0 and not animated_sprite.flip_h:
			animated_sprite.flip_h = true

func _on_path_timer_timeout() -> void:
	if is_instance_valid(player):
		_get_path_to_player()
	else:
		path_timer.stop()
		move_direction = Vector2.ZERO

func _get_path_to_player() -> void:
	nav_agent.target_position = player.global_position


func _on_navigation_agent_velocity_computed(safe_velocity: Vector2) -> void:
	move_direction = safe_velocity
