extends Character

@export var indicator_distance: float = 75.0

@onready var sword: Node2D = get_node("Sword")
@onready var sword_hitbox: Area2D = get_node("Sword/Node/Sprite2D/Hitbox")
@onready var sword_animation_player: AnimationPlayer = sword.get_node("SwordAnimationPlayer")
@onready var attack_indicator: Sprite2D = get_node("AttackIndicator")

var use_keyboard: bool = true
var prev_aim_position = Vector2.LEFT

func _process(_delta: float) -> void:
	var mouse_direction: Vector2 = Vector2.ZERO
	
	if use_keyboard:
		mouse_direction = (get_global_mouse_position() - global_position)
	else:
		mouse_direction = Input.get_vector("aim_left", "aim_right", "aim_up", "aim_down") 

	# if no input, use last aim direction
	if mouse_direction == Vector2.ZERO:
		mouse_direction = prev_aim_position
		
	attack_indicator.position = mouse_direction.normalized() * indicator_distance
		
	sword.rotation = mouse_direction.angle()
	sword_hitbox.knockback_direction = mouse_direction
	
	prev_aim_position = mouse_direction
	
	# handle attacking
	if Input.is_action_pressed("attack") and not sword_animation_player.is_playing():
		sword.show()
		sword_animation_player.play("attack")

func _input(event):
	if event is InputEventKey or InputEventMouse:
		attack_indicator.hide()
		use_keyboard = true
		Input.mouse_mode = Input.MOUSE_MODE_VISIBLE
	else:
		attack_indicator.show()
		use_keyboard = false
		Input.mouse_mode = Input.MOUSE_MODE_HIDDEN

func get_input() -> void:
	move_direction = Input.get_vector("move_left", "move_right", "move_up", "move_down")
	



func _on_sword_animation_player_animation_finished(anim_name):
	if anim_name == "attack":
		sword.hide()
		sword.get_node("Node").rotation = 0
		
