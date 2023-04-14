# Mini projet réseaux - ROUX Elisa

## Préambule

Ce repository en divisé en quatre projets. Ils possèdent la même structure mais peuvent être lancés séparement pour observer le comportement de chaque question. 

## Utilisation

- Par manque de temps je n'ai pas pu ajouter de scripts afin de lancer les serveurs, ils doivent donc être démarrés depuis Visual Studio
- Pour avoir une vision globale vous pouvez lancer le projet Question4 qui regroupe les fonctionnalités de tous les autres
- Les **urls** sont récupérées par le programme dans un fichier JSON dans chaque projet. Elles peuvent donc être modifiées à cet endroit

## Conception

Ainsi que précédemment, la structure de chaque projet est la même. 3 fichiers sont présents :

` PageServer `

Il permet de lancer le serveur, de créer la liste des sites à appeler depuis le urls.json de référence et regroupe tous les endpoints de l'application. Puisque je n'utilise que .net quand une url correspond à un de mes endpoints je lui renvoie une page HTML mise à jour avec les données sous la forme d'une chaîne de caractères.

` WebRequests `

Il permet d'envoyer les requêtes aux différents sites. J'initialise une fois cet objet dans PageServer et dans son constructeur il va aller faire une requête HEAD sur tous les sites et stocker toute les informations dans un dictionnaire afin de ne pas avoir à faire d'autres requêtes après. Ce fichier traite également les données récupérées mais ne leur applique pas de logique par contre.

` Stats `

C'est lui qui s'occupe de réaliser des opérations de traitement mathématique ou de regrouper les données pour qu'elles soient utilisables par WebRequests.

## Détail de la question 3

Voici des scénarios envisageables que je n'ai pas pu implémenter en entier :

- Je pourrais récupérer le header X-XSS-Protection car cela permet de savoir si le site à activé la protection anti cross site scritping des navigateurs ce qui nous donne une idée de la sécurité de celui-ci (même s'il peut avoir utilisé des mécanismes internes contre ce type d'attaques)
- Je pourrais récupérer le header Cache-Control qui indique comment le serveur gère son cache, par exemple la page a-t-elle une durée de vie maximale en cache ? Celui-ci sera-t-il vidé ? Cela nous fournit également des informations sur ses performances et donc nous donner une idée sur sa consommation de ressources si jamais je devais beaucoup le solliciter puisque si les données ne sont pas cachées mon navigateur va devoir envoyer de nombreuses requêtes au serveur.
- Enfin je pourrais contrôler le header Content-Type pour savoir les médias les plus supportés par les sites webs. Dans une utilisation personnelle cela pourrait me permettre de savoir si mon support sera adapté à l'utilisation ou du site ou dans un contexte plus large, de savoir si je suis face à un choix quel support choisir pour avoir un maximum de compatibilité.

## Améliorations

Par soucis de temps je n'ai pas pu ajouter l'interprétation et la pertinence de chacune des mesures sur les pages 
