# -UnityProject- GEOMETRY DASH
Projet pour le projet Unity -UIMM CNAM-

Ce projet est conçu dans le cadre de l'initiative UIMM CNAM pour développer un jeu Unity. Ce document contient des informations sur l'organisation, les outils de gestion de projet, et les tâches à accomplir pour assurer un bon suivi de l'évolution du projet.

## Sommaire
- [Description du projet](#description-du-projet)
- [Configuration et installation](#configuration-et-installation)
- [Gestion de version et collaboration](#gestion-de-version-et-collaboration)
- [Suivi de projet](#suivi-de-projet)
- [Tâches à accomplir](#tâches-à-accomplir)
- [Rôles et responsabilités](#rôles-et-responsabilités)
- [Liste des tâches](#liste-des-tâches)
- [Outils recommandés](#outils-recommandés)

## Description du projet
Ce projet Unity a pour objectif de //// et s'inspire de GEOMETRY DASH. Le développement se fera en utilisant Unity et d'autres outils de support pour garantir une production organisée et efficace.

## Configuration et installation
1. **Unity** : Assurez-vous d'avoir installé Unity (La dernière -a definir-).
2. **Git** : Utilisé pour le contrôle de version. Voir [Gestion de version et collaboration](#gestion-de-version-et-collaboration) pour plus de détails.
3. **Autres dépendances** : /////.

## Gestion de version et collaboration
Le contrôle de version est géré avec Git. Voici les étapes de base pour configurer et collaborer sur ce projet :
- Cloner le projet : `git clone <URL-du-répository>`
- Créer une nouvelle branche pour chaque fonctionnalité ou correction de bug : `git checkout -b nom_de_branche`
- Soumettre les modifications avec des messages de commit clairs : `git commit -m "Description des modifications"`
- Pousser les modifications sur la branche : `git push origin nom_de_branche`
- Faire une pull request pour examiner et intégrer les changements.

**Conseil** : Effectuer des commits réguliers et éviter de travailler directement sur la branche principale.

## Suivi de projet
La gestion du projet est assurée par l'outil Trello, où chaque étape et tâche est définie, assignée et suivie. Vous pouvez accéder au tableau Trello du projet [ici](https://trello.com/fr). 

Le tableau contient :
- **Liste des tâches à accomplir**
- **Priorité des tâches**
- **Suivi des étapes clés**

## Tâches à accomplir
### 1. Mise en place du contrôle de version avec Git
   - Créer un dépôt Git pour le projet.
   - Définir une structure de branches pour le développement, par exemple : `main`, `dev`, et des branches pour chaque fonctionnalité.
   - S'assurer que chaque membre de l'équipe sait cloner, créer des branches, committer et pousser ses modifications.
   - Faire les carte sur un éditeurs de niveaux.

### 2. Création d’un diagramme de Gantt
   - Utiliser un outil gratuit pour créer un diagramme de Gantt (par exemple, [GanttProject](http://www.ganttproject.biz/) ou [Toggl Plan](https://toggl.com/plan/)).
   - Planifier les étapes principales du projet et y associer des échéances.
   - Partager ce diagramme avec l’équipe pour permettre un suivi visuel de l’avancement.

### 3. Gestion du projet avec Trello
   - Créer un tableau Trello avec les colonnes : "À faire", "En cours", "En révision" et "Terminé".
   - Ajouter chaque tâche du projet sous forme de cartes et y inclure les détails, la priorité et les échéances.
   - Assigner les membres de l’équipe aux tâches et mettre à jour les cartes au fur et à mesure de leur progression.
   - [Lien vers le tableau Trello du projet](https://trello.com/fr)

Les détails et échéances des tâches sont visibles dans le diagramme de Gantt. Plusieurs applications gratuites permettent de créer des diagrammes de Gantt comme [GanttProject](http://www.ganttproject.biz/) et [Toggl Plan](https://toggl.com/plan/).

## Rôles et responsabilités
- **DEV** : TOUS
- **TEST** : TOUS
- **2D** : S.S
- **Audio** : R.A
- **Level Manager** : M.F
- **Gantt** : S.S
- **Trello** : M.F
- **GitHub** : M.F
- **GamesManager** : MF
- **UML** : S.S / R.A

## Liste des tâches
### Audio
- [x] Musique des niveaux
- [x] Son des boutons
- [x] Effets sonores (mort, contact avec les obstacles, etc.)

### Animations
- [ ] Animation des personnages
- [ ] Animation des obstacles
- [ ] Effets visuels

### UI
- [x] Création du personnage
- [ ] Création des cartes (sheets)
- [ ] Boutons et éléments interactifs

### Développement
- [x] Player Controller
- [ ] Camera Controller
- [ ] ScoreManager
- [ ] GameManager
- [ ] SaveSys
- [ ] LevelSpawner
- [x] Scriptable Objects (SO) pour les skins
- [x] OptionSet

### Fichiers de gestion
- [ ] Fichiers de sauvegarde
- [ ] Fichiers de niveaux (XML ou texte)
- [ ] Shémas images

### Gestion de projet
- [x] Trello
- [x] Diagramme de Gantt (GanttProject)
- [x] GitHub
- [x] Diagrammes UML (Lucid Chart)

## Outils recommandés
Pour faciliter le suivi et la collaboration, voici quelques outils recommandés :
- **Git** : Pour le contrôle de version.
- **Trello** : Pour la gestion de projet et l'organisation des tâches.
- **GanttProject** ou **Toggl Plan** : Pour le suivi des deadlines avec des diagrammes de Gantt.
  
  
N'hésitez pas à consulter la documentation Unity pour vous familiariser avec les fonctionnalités avancées si nécessaire. :)
