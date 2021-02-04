# Cyberball
 
Cyberball est un prototype que j'ai réalisé afin d'essayer l'API [Mirror](https://mirror-networking.com)
Le projet est actuellement en pause mais j'aimerai le développer plus en profondeur dans le futur.

## Concept

Cyberball est un jeu compétitif 5V5 à mi chemin entre un jeu de tir à la 3ème personne et un jeu de football américain. 
Deux équipes s'affrontent, chacune dispose d'une base dans laquelle se trouve un point d'apparition et un but.  L'objectif pour les joueurs est de récupérer la balle situé en milieu de terrain et de la placer dans le but adverse. La balle s'attache au 1er joueur qui entre en contact avec et tombe au sol lorsque celui-ci meurt. Une partie dure 5 minutes à la fin desquelles l'équipe avec le maximum de points sort victorieuse.

## Univers

Le jeu se déroule dans un univers futuriste dans lequel le Cyberball est un sport populaire qui se déroule en réalité virtuelle.

## Contrôles 

ZQSD : Déplacement
2 Appuis rapide sur Z : Sprint 
Espace : Saut
Clic gauche : Tir
R : Recharger

## Vidéo démo

## Instructions d'installation

Pour essayer le jeu, téléchargez simplement le projet et lancez Builds/mainBuild/Cyberball.exe

Pour se connecter à plusieurs en simultané, le 1er joueur doit sélectionner "Host".
Une fois une instance Host lancée, les autres devront sélection "Join Lobby" > Join, en laissant localhost (uniquement si vous êtes sur le même réseau).

Pour se connecter de réseaux différents, il faudra en revanche mettre en place une redirection de port et entrez l'adresse IP de l'Host.

Pour tester les fonctionnalités réseaux seul, lancez simplement plusieurs instances en LocalHost.
