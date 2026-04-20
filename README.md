# The-PGG--P2I
Voici le repository contenant les données nécessaires au lancement de mon projet p2i : The Portfolio Generator Game

##*Guide d'installation*

###1.1. Installation

- Cloner le repository disponible sur github ou télécharger depuis github
- Ouvrir le projet avec Unity Hub (version 6000.3.9f1)
    - Cliquer sur “Add project”
    - Sélectionner le projet
    - Vérifier la version d’Unity
- Ouvrir la scène MainScene
- Cliquer sur play

###1.2. Dépendances

/!\ Ce projet utilise les packages Unity suivants :
- TextMeshPro
- Input System
- 
Ils devraient automatiquement être installés via le Package Manager lors de l’ouverture du projet. Si jamais, ils ne sont pas activés ou nécessitent une configuration :

*Activer Input System*

- Aller dans Edit > Project Settings > Player
- Dans “Active Input Handling”, sélectionner : “Input System Package (New)” ou “Both”
- Redémarrer Unity si besoin

  
*Initialiser TextMeshPro*

- Aller dans Window > TextMeshPro > Import TMP Essential Ressources

###1.3. Dépannage

En cas d’erreur indiquant un package manquant : 
Aller dans Window > Package Manager > Reset Packages to defaults


*Fonctionnement du jeu :*

Le joueur se déplace grâce aux flèches du clavier. Il se déplace horizontalement et interragit avec les PNJ (Personnage Non Jouable). Pour interagir avec il faut presser la touche "E", et dialoguer avec le PNJ afin d'entrer les différentes données du portfolio.

La touche "P" permet d'ouvrir les données enregistrées selon les PNJ avec qui le joueur a eu une interraction sous forme de Panel de Portfolio. Ce panel permet de prévisualiser les données, ouvrir le fichier de sauvegarde, exporter en HTML et changer la police du fichier exporté.

*Limite du jeu*

Pour le moment le jeu permet de parler avec deux PNJ afin de faire une présentation de soi et d'un projet.




