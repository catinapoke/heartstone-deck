# Heartstone Deck

This project was made as a test task. Heartstone card appearance has been taken as a reference. Whole gameplay was made using UI canvas. It also can be made in 3D with changing raycast logic in CardDragger. 

It uses Extenject for Dependency Injection and DoTween for animations. UI layout designed for 16:9 display.
OnValidate method is being used for Inspector fields validation.

Scene structure consists of two scenes: Preloading and CoreGame. The first one is used for resources preparation and the second contains main gameplay.

SOLID and YAGNI principles were used.

## Task details

### Card view
Create UI for an "in-hand card" object for CCG-like game. Card consist of:
- Art + UI overlay
- Title
- Description
- Attack icon + text value
- HP icon + text value
- Mana icon + text value

### Icon loading
Load card art randomly from https://picsum.photos/ each time app starts. Fill player’s hand with 4-6 cards in a visually pleasing way and use the arc pattern for displaying the cards (look at the pic below). The number of cards should be determined randomly at the start of the game.

### Card change
Create an UI button at the center of the screen to randomly change one randomly selected value -2→9 (the range is from -2 to 9) of each one card sequentially, starting from the most left card in the player's hand moving right and repeating the sequence after reaching the most right card. Bind Attack, Health and mana properties to UI.

### Counter animation
Changing those values from code must be reflected on the card's UI with counter animation. (counting from the initial to the new value). If some card’s HP drop below 1 - remove this card from player’s hand. (dont forget to reposition other cards, use tweens to make it smooth)

### Card drag
Player can drag a card and drop it on middle section of the table (use drop panel of any size) Card moves back to player’s hand if it’s hasn't been dropped over the drop panel. Cards shines when its being dragged.