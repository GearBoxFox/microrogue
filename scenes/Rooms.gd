extends Node

const SPAWN_ROOMS: Array = [
	preload("res://scenes/rooms/spawn_room.tscn"),
]
const INTERMEDIATE_ROOMS: Array = [
	preload("res://scenes/rooms/room_0.tscn"),
	preload("res://scenes/rooms/room_1.tscn"),
	preload("res://scenes/rooms/room_2.tscn"),
	preload("res://scenes/rooms/room_3.tscn")
]
const END_ROOMS: Array = [
	preload("res://scenes/rooms/exit_room.tscn"),
]

const TILE_SIZE: int = 8 * 4
const FLOOR_TILE_INDEX: Vector2i = Vector2i(1, 1)
const LEFT_WALL_TILE_INDEX: Vector2i = Vector2(0, 1)
const RIGHT_WALL_TILE_INDEX: Vector2i = Vector2(3, 1)

@export var num_levels: int = 5

@onready var player: CharacterBody2D = get_parent().get_node("Player")

func _ready() -> void:
	_spawn_rooms()
	
func _spawn_rooms() -> void:
	var previous_room: Node2D
	
	for i in num_levels:
		var
