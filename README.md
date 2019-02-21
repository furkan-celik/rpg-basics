# rpg-basics

In this project, I want to develop basic mechanics of rpg games. I made a simple scene and added some items and npcs to interact with. I used unity engine with c# and used navigation system of unity for the movements. There is two camera angle (rpg and fps) and each camera has different control set of player. Players can interact with npcs with even sometimes selection can be added to conversations. Trade system has a barter system which involves a little bit AI for trade npc to be feasable.

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p1.png" width="250" title="rpg camera">
  <a>This is how rpg camera looks to player. In UI side of play, there is only a health bar which keeps track of player's health and shows it in gradual scale. Rpg camera's control layout is click and go by unity navigation system.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p2.png" width="250" title="fps camera">
  <a>This is fps camera. Player can zoom in and out to the world and if player zooms enough, than fps camera became active. This camera's control set is like general fps games' control layout.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p4.png" width="250" title="inventory">
  <a>You can see inventory in this picture. It has 20 slots for items. Every item slots contains a sprite of picked item and slots have a little bar underneath them to show remained durability if item can lose durability over time of usage. And when player hovered on to an object, little box appears to show details of item. Naming of item has rarity scale of uncommon, common, legendary, epic in wellknown color schema of popular games.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p6.png" width="250" title="equipments">
  <a>In the left side of the picture, you can see equipments menu. It almost the same system with inventory but slots needs to have the same type of item (like head slots neads an item wearable to head).</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p7.png" width="250" title="dialog and npc">
  <a>Game has a simple dialog system. In this picture you can see a part of conversation. White block is replacement for character avatars since I did not have proper images and character bodies I used white blocks. Character image being on right means player is talking and character image being on left means npc is talking. For example, in this picture player is talking. This dialog system has sound feature too, it plays while dialog box is active. I managed to achive that dialog system buy creating a dialog manager and queeing up all dialogs in dialog manager and showing them one after one. And if you bored with talking, you can always walk away but that may upset npc you were talking. There is 3 type of npc's, friendly mild and agressive. Each corresponds dialogs, selection of player and actions of player in a unique way and another important factor in this respond system is npc's relationship with player beside that npc type. For example, npc's can broke up to player and refuse to talk.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p8.png" width="250" title="dialog selection">
  <a>You can see dialog selection system in this picture. Player has various selections depending to created dialog conversation (2 to any amount of choice). Responds of each selection is different and some of them may effect players relationship with npc or disappear/appear depending on a event. In this one merchant is dedicated to his work so he only wants to sell items.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p9.png" width="250" title="trade menu">
  <a>You can see trade menu in this picture. Player has couple of options in this area. Player can sell an item to merchant, player can buy an item from merchant or repair an item for cost. Buying system has some logic behing like an very simple intellegance.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p12.png" width="250" title="trade item start">
  <a>You can see merchant wants 4 coin for this item from me. But if I found this price too low I can higher price for this item. (Initially this idea may sound bad because player definately lose money but player will get friendship point at the npc's end and I am using the same system for selling items too so highering price will have negative effect on selling side.)</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p13.png" width="250" title="higher price">
  <a>Let's see what will be respond of npc if I will offer 6 coin</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p14.png" width="250" title="lowering price start">
  <a>As you can see on right, I got the item without trouble and now I want to buy some protection for my legs and lets see that. Initial price for this item is 3.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p15.png" width="250" title="lowering price part 2">
  <a>I am offering 1 coin for this leggings because I found them in very poor condition. Let's see what happens</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p16.png" width="250" title="lowering price part 3">
  <a>As you can see I could not got the item from seller and addition to that merchant became unhappy (this one is probably an agressive type of npc) and highered price to 4. I will offer 1 coin again to insist.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p17.png" width="250" title="lowering price part 4">
  <a>Well, seller declined my offer and finished our trade with this could be good business. I can clearly sense same anger and sadness from loss of possible money. But this decline has several costs to player. Firstly, that npc will not solve this item to player ever again (unless npc starts to really like player). Player needs to find and buy that item from another merchant. Secondly, that merchant has negative thoughts about player now and that npc may be harsh on player or refuses to sell some items.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p10.png" width="250" title="main menu">
  <a>Enemy system is pretty simple, when player gets into enemies sight, enemy starts attacking to player.</a>
</p>

<p align="center">
  <img src="https://raw.githubusercontent.com/furkan-celik/rpg-basics/master/Screenshots/p11.png" width="250" title="main menu">
  <a>If enemy gets close enough enemy will hit to player and player will lose some health (in presence of armor, armor reduces attacks instead of zeroing them and loses some durability). If player hits, player can kill enemy too but player will face with a cooldown between attacks to prevent spamming attacks. </a>
</p>

Beside these basics demo had a loot system with chests too but after an update of unity, I am unable to show system because of some changes at the unity engine side affected loot system I had to remove them since I shifted my focus to another project of my own.
