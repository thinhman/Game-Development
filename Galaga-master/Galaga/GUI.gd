extends Control

var score_text = "Current Score: "
var high_score_text = "High Score: "

var life1 = TextureRect
var life2 = TextureRect

onready var game_manager = get_node("/root/Root/GameManager")
onready var scoreContainer = get_node("ScoreContainer")

func _ready():
	# Gets the score container which holds the score and high score
	# and updates the values
	$ScoreContainer/Score.set_text(score_text + str(game_manager.score))
	$ScoreContainer/High_Score.set_text(high_score_text + str(game_manager.high_score))
	
	#get references to the life ui texture boxes
	life1 = get_node("BottomUI/Lives/Life")
	life2 = get_node("BottomUI/Lives/Life2")
	pass

func _process(delta):
	# Gets the score container which holds the score and high score
	# and updates the values
	$ScoreContainer/Score.set_text(score_text + str(game_manager.score))
	$ScoreContainer/High_Score.set_text(high_score_text + str(game_manager.high_score))
	pass

# Based on the amount of lives passed into the function it
# displays the ships to show how many lives you have
func lost_life(life):
	if life == 3:
		life1.show()
		life2.show()
		return
	elif life == 2:
		life2.hide()
	elif life == 1:
		life1.hide()
		life2.hide()

