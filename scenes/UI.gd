extends CanvasLayer

const MIN_HEALTH: int = 0

var max_hp: int = 4

@onready var player: CharacterBody2D = get_parent().get_node("Player")
@onready var health_bar: TextureProgressBar = get_node("HealthBar/HealthBarProgress")

func _ready() -> void:
	max_hp = player.hp
	_update_health_bar(max_hp)
	
func _update_health_bar(new_value: int) -> void:
	# turn hp into a range of 0-100
	health_bar.set_value((float(new_value) / float(max_hp)) * 100.0)
