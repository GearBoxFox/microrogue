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

const TILE_SIZE: int = 32
const FLOOR_TILE_INDEX: Vector2i = Vector2i(1, 1)
const LEFT_WALL_TILE_INDEX: Vector2i = Vector2(3, 1)
const RIGHT_WALL_TILE_INDEX: Vector2i = Vector2(0, 1)

@export var num_levels: int = 5

@onready var player: CharacterBody2D = get_parent().get_node("Player")

func _ready() -> void:
	_spawn_rooms()
	
func _spawn_rooms() -> void:
	var previous_room: Node2D
	
	for i in num_levels:
		var room: Node2D
		
		if i == 0:
			room = SPAWN_ROOMS[randi() % SPAWN_ROOMS.size()].instantiate()
			player.global_position = room.get_node("PlayerSpawn").global_position
		else:
			if i == num_levels - 1:
				room = END_ROOMS[randi() % END_ROOMS.size()].instantiate()
			else:
				room = INTERMEDIATE_ROOMS[randi() % INTERMEDIATE_ROOMS.size()].instantiate()
				
			var previous_room_tilemap: TileMap = previous_room.get_node("TileMap")
			var previous_room_door: StaticBody2D = previous_room.get_node("Doors/Door")
			var exit_tile_pos: Vector2 = (previous_room_door.position / TILE_SIZE) + (Vector2.UP * 2) 
			
			var corridor_height: int = (randi() % 5) + 2
			for y in corridor_height:
				print(str(exit_tile_pos + Vector2(0, -y)))
				# build the exit
				previous_room_tilemap.set_cell(0, 
				(exit_tile_pos + Vector2(-1, -y)), 
				3, LEFT_WALL_TILE_INDEX)
				previous_room_tilemap.set_cell(0, 
				(exit_tile_pos + Vector2(0, -y)), 
				3, FLOOR_TILE_INDEX)
				previous_room_tilemap.set_cell(0, 
				(exit_tile_pos + Vector2(1, -y)), 
				3, RIGHT_WALL_TILE_INDEX)
				
			# move the next room into position
			var room_tilemap: TileMap = room.get_node("TileMap")
			room.position = (previous_room_door.global_position 
			+ Vector2.UP * room_tilemap.get_used_rect().size.y * TILE_SIZE
			+ Vector2.UP * ((1 + corridor_height) * TILE_SIZE)
			+ (Vector2.DOWN * 0.5) * TILE_SIZE
			+ Vector2.LEFT * room.get_node("Entrance/Marker2D").position.x)
			
		add_child(room)
		previous_room = room
