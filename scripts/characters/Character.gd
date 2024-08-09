extends CharacterBody2D
class_name Character

const FRICTION: float = 0.15

@export var max_hp: int = 2
@export var hp: int = 2: set = set_hp
signal hp_changed(new_hp)

@export var acceleration: int = 100
@export var max_speed: int = 150

@onready var state_machine: Node = get_node("FiniteStateMachine")
@onready var animated_sprite: AnimatedSprite2D = get_node("AnimatedSprite2D")
@onready var hurt_sound: AudioStreamPlayer = get_node("HurtSound")

var move_direction: Vector2 = Vector2.ZERO

func _physics_process(_delta: float):
	move_and_slide()
	velocity = lerp(velocity, Vector2.ZERO, FRICTION)

func move() -> void:
	move_direction = move_direction.normalized()
	velocity += move_direction * acceleration
	velocity = velocity.limit_length(max_speed)
	
func take_damage(dam: int, dir: Vector2, force: int) -> void:
	self.hp -= dam
	if hp <= 0:
		state_machine.set_state(state_machine.states.dead)
		velocity = dir * (force * 2.0)
	else:
		state_machine.set_state(state_machine.states.hurt)
		velocity = dir * force
	
	if not hurt_sound.playing:
		hurt_sound.play()
		
func set_hp(new_hp: int) -> void:
	hp = new_hp
	emit_signal("hp_changed", new_hp)
	
	
