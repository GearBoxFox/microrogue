extends Control

@onready var parent: Enemy = get_parent()
@onready var hp_bar: ColorRect = get_node("HpBar")


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	hp_bar.scale.x = parent.hp as float / parent.max_hp as float
