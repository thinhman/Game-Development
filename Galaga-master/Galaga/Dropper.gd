extends KinematicBody2D

const sheildItem_scn = preload("res://SheildItem.tscn")
const laserItem_scn = preload("res://LaserItem.tscn")
const ammoItem_scn = preload("res://AmmoItem.tscn")

var randomNum
onready var ship = weakref(get_node("/root/Root/Ship"))

func _process(delta):
	# Called every frame. Delta is time since last frame.
	# Update game logic here.
	if ship.get_ref():
		randomize()
		randomNum = rand_range(1, 1001)
		if randomNum <= 3.0 and ship.get_ref().ammo == false:
			var ammo = ammoItem_scn.instance()
			print("Spawned Ammo")
			get_parent().add_child(ammo)

		randomize()
		randomNum = rand_range(1, 1001)
		if randomNum <= 3.0 and ship.get_ref().missile != true:
			var laser = laserItem_scn.instance()
			print("Spawned Laser")
			get_parent().add_child(laser)

		randomize()
		randomNum = rand_range(1, 1001)
		if randomNum <= 3.0 and ship.get_ref().shield != true:
			var sheild = sheildItem_scn.instance()
			print("Spawned Shield")
			get_parent().add_child(sheild)
	pass
