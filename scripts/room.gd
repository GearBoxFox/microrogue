extends Node2D

const SPAWN_EXPLOSION_SCENE: PackedScene = preload("res://scenes/characters/enemies/spawnexplosion.tscn")

const ENEMY_SCENES: Dictionary = {
	"FLYING_CREATURE": preload("res://scenes/characters/enemies/FlyingEnemy/flyingenemy.tscn")
}

var num_enemies: int

@onready var tilemap: TileMap = get_node("TileMap")
@onready var entrance: Node2D = get_node("Entrance")
@onready var door_container: Node2D = get_node("Doors")
@onready var enemy_position_container: Node2D = get_node("EnemyPositions")
@onready var player_detector: Area2D = get_node("PlayerDetector")

func _ready() -> void:
	num_enemies = enemy_position_container.get_child_count()
	
func _on_enemy_killed() -> void:
	num_enemies -= 1
	if num_enemies == 0:
		_open_doors()
	
func _open_doors() -> void:
	for door in door_container.get_children():
		door.open()
		
func _close_entrance() -> void:
	for entry_position in entrance.get_children():
		# divide by 32 to convert scaling
		tilemap.set_cell(0, entry_position.global_position / (4.0 * 8.0), 3, Vector2i(1, 0))
		tilemap.set_cell(0, (entry_position.global_position / (4.0 * 8.0)) + Vector2.LEFT, 3, Vector2i(1, 0))
		tilemap.set_cell(0, (entry_position.global_position / (4.0 * 8.0)) + Vector2.RIGHT, 3, Vector2i(1, 0))
		
func _spawn_enemies() -> void:
	for enemy_position in enemy_position_container.get_children():
		var enemy: CharacterBody2D = ENEMY_SCENES.FLYING_CREATURE.instantiate()
		
		# connect death signal
		var __ = enemy.connect("tree_exited", Callable(_on_enemy_killed))
		enemy.global_position = enemy_position.position
		call_deferred("add_child", enemy)
		
		# create spawn explosion
		var spawn_explosion: Sprite2D = SPAWN_EXPLOSION_SCENE.instantiate()
		spawn_explosion.global_position = enemy_position.position
		call_deferred("add_child", spawn_explosion)



func _on_player_detector_body_entered(body: Node2D) -> void:
	player_detector.queue_free()
	if num_enemies > 0:
		_close_entrance()
		_spawn_enemies()
	else:
		_open_doors()
