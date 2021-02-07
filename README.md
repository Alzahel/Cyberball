# Cyberball
 
Cyberball est un prototype que j'ai réalisé afin d'essayer l'API [Mirror](https://mirror-networking.com).  
Le projet est actuellement en pause mais j'aimerai le développer plus en profondeur dans le futur, notamment en ajoutant des options de déplacement suplémentaires (dodge, wall jump...) et en réalisant de plus belles maps.

## Concept

Cyberball est un jeu compétitif 5V5 à mi chemin entre un jeu de tir à la 3ème personne et un jeu de football américain. 
Deux équipes s'affrontent, chacune dispose d'une base dans laquelle se trouve un point d'apparition et un but.  L'objectif pour les joueurs est de récupérer la balle situé en milieu de terrain et de la placer dans le but adverse. La balle s'attache au 1er joueur qui entre en contact avec et tombe au sol lorsque celui-ci meurt. Une partie dure 5 minutes à la fin desquelles l'équipe avec le maximum de points sort victorieuse.

## Univers / Lore

Le jeu se déroule dans un univers futuriste dans lequel le Cyberball est un sport populaire prenant son essor dans la popularité grandissante de la réalité virtuelle.

## Contrôles 

ZQSD : Déplacement  
2 Appuis rapide sur Z : Sprint   
Espace : Saut  
Clic gauche : Tir  
R : Recharger  

## Démos

<details>
  <summary>Host un Lobby</summary>
  <img src="https://media.giphy.com/media/VTWXFDAsvFxPnNGcMX/giphy.gif">
</details>

<details>
  <summary>Rejoindre un Lobby</summary>
  <img src=https://media.giphy.com/media/nwwUzJ9k8uYQc9Anyv/giphy.gif>
</details>

<details>
  <summary>Changer d'équipe</summary>
  <img src=https://media.giphy.com/media/N6MKMzLCXrpVolIgvF/giphy.gif>
</details>

<details>
  <summary>Statut "prêt" des joueurs pour lancer la game</summary>
  <img src=https://media.giphy.com/media/mNC6Zxd9WPGpdZvd96/giphy.gif>
</details>

<details>
  <summary>Débuter la partie</summary>
  <img src=https://media.giphy.com/media/nouVdrADJ0MgmOZpZm/giphy.gif>
</details>

<details>
  <summary>Mouvements  sprint  saut  accroupissement</summary>
  <img src=https://media.giphy.com/media/gYBaVh1nKzkymJ77Su/giphy.gif>
</details>

<details>
  <summary>Tirs, tirs , tirs à la tête, kill, respawn</summary>
  <img src=https://media.giphy.com/media/UVfUAhWpsmkDTSYGZa/giphy.gif>
</details>

<details>
  <summary>Rammasser la balle</summary>
  <img src=https://media.giphy.com/media/bjcmnEGnPmRBmrcdzv/giphy.gif>
</details>

<details>
  <summary>Voler la balle aux ennemis</summary>
  <img src=https://media.giphy.com/media/k1VnJ1VFXR2pXGHH0E/giphy.gif>
</details>

<details>
  <summary>Marque un Touchdown</summary>
  <img src="https://media.giphy.com/media/5enKlXrwBdp3ie9PPH/giphy.gif">
</details>


## Instructions d'installation

Pour essayer le jeu, téléchargez simplement le build sur https://alzahel.itch.io/cyberball et lancez Builds/mainBuild/Cyberball.exe  

Pour se connecter à plusieurs en simultané, le 1er joueur doit sélectionner "Host".  
Une fois une instance Host lancée, les autres devront sélection "Join Lobby" > Join, en laissant localhost (uniquement si vous êtes sur le même réseau).  

Pour se connecter de réseaux différents, il faudra en revanche mettre en place une redirection de port et entrez l'adresse IP de l'Host.  

Pour tester les fonctionnalités réseaux seul, lancez simplement plusieurs instances en LocalHost.
