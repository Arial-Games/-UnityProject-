# Dev Log - Projet Unity - GeometryDash

Ce document trace l'historique des développements pour le projet, avec des descriptions détaillées des versions, des fonctionnalités ajoutées, des améliorations et des corrections de bugs.

# Dev Log - Projet Unity - GeometryDash

Ce document trace l'historique des développements pour le projet, avec des descriptions détaillées des versions, des fonctionnalités ajoutées, des améliorations et des corrections de bugs.

---

## Release - v0.4.7 - 15-05-2025

### Added

- Ajout de nouvelles fonctionnalités dans le système d’inventaire et amélioration des interfaces.
- Implémentation de l’IA ennemie avec pathfinding basique.
- Ajout des scènes de tutoriel et d’atelier (Workshop).
- Ajout des portails et téléporteurs dans certains niveaux.

### Fixed

- Correction de bugs liés à la persistance de l’inventaire entre les scènes.
- Résolution de problèmes dans le système de sauvegarde des skins.
- Correction de la navigation dans les menus d’options.

---

## Release - v0.4.6 - 10-05-2025

### Added

- Ajout d’un système de réglages graphiques dans le menu options.
- Début d’implémentation du mode coopératif.
- Amélioration de l’animation des portails dans les niveaux.

### Fixed

- Correction de crash lors du chargement de certaines scènes.
- Correction du problème d’activation des scripts IA sur certains ennemis.

---

## Release - v0.4.5 - 05-05-2025

### Added

- Ajout des skins « v1.0 » et de leur système d’achat.
- Amélioration des effets visuels pour les transitions de scènes.
- Ajout de sons dans les menus et pendant la partie.

### Fixed

- Correction de bug dans la gestion des checkpoints.
- Correction de la taille des portails dans certains niveaux.

---

## Release - v0.4.4 - 01-05-2025

### Added

- Ajout du support du clavier et de la manette.
- Ajout d’un système de notifications dans l’interface.
- Ajout d’un nouveau niveau « Montagne ».

### Fixed

- Correction des animations dans les menus.
- Amélioration des performances sur les niveaux lourds.

---

## Release - v0.4.3 - 25-04-2025

### Added

- Ajout du système de musique dynamique selon le niveau.
- Ajout des mini-jeux dans l’atelier (Workshop).
- Ajout de nouvelles interactions dans les niveaux.

### Fixed

- Correction des bugs de collision dans le niveau 3.
- Amélioration du système de sélection des niveaux.

---

## Release - v0.4.2 - 20-04-2025

### Added

- Ajout du système de trophées et achievements.
- Ajout de la fonctionnalité de partage de scores en ligne.
- Ajout du mode challenge dans certains niveaux.

### Fixed

- Correction des erreurs de sauvegarde dans le cloud.
- Correction du bug du portail téléporteur.

---

## Release - v0.4.1 - 15-04-2025

### Added

- Ajout d’un nouveau système d’IA pour les ennemis.
- Ajout du mode entraînement dans le menu principal.
- Ajout d’un menu debug accessible par un raccourci clavier.

### Fixed

- Correction de bugs graphiques sur certains écrans.
- Correction du bug de l’inventaire qui se vidait aléatoirement.

---

## Release - v0.4.0 - 10-04-2025

### Added

- Ajout du système complet d’inventaire joueur.
- Ajout du shop de skins fonctionnel.
- Ajout de la gestion des niveaux avec transition et sauvegarde.
- Ajout des menus principaux et secondaires complets.

### Fixed

- Correction de bugs de scène et des erreurs de compilation.
- Correction du système de spawn des ennemis.

---

## Release - v0.3.2 - 10-04-2025

### Added

- Ajout de la gestion avancée des collisions entre joueurs et ennemis.
- Implémentation des premiers effets visuels pour les actions (ex : éclairs, explosions).
- Ajout du système de checkpoints dans les niveaux.
- Amélioration de l’animation du personnage principal (run, jump).

### Fixed

- Correction de bugs liés aux animations qui ne se lançaient pas.
- Correction de problèmes de clipping graphique.

---

## Release - v0.3.1 - 05-04-2025

### Added

- Mise en place du système de gestion des vies et de la mort du personnage.
- Ajout des premiers ennemis basiques avec mouvements simples.
- Ajout du système de collecte d’objets (coins, power-ups).
- Ajout des scripts pour la gestion des portails et téléporteurs.

### Fixed

- Correction d’un bug qui empêchait la détection des collisions avec les ennemis.
- Correction de problèmes de réinitialisation des niveaux après échec.

---

## Release - v0.3.0 - 01-04-2025

### Added

- Implémentation initiale de la logique de jeu principale.
- Ajout des premières IA ennemies basiques.
- Ajout des premiers niveaux prototypes avec portails.
- Ajout des contrôles clavier pour le personnage principal.
- Ajout des premiers scripts de gestion des portails et téléporteurs.
- Ajout des systèmes basiques de scoring.

### Fixed

- Correction des bugs d’affichage UI sur la sélection des niveaux.
- Correction des erreurs de collision dans certains environnements.

---

## Release - v0.2.0 - 20-03-2025

### Added

- Ajout du système de gestion des scènes.
- Ajout du menu pause.
- Ajout des premières animations du personnage principal.

### Fixed

- Correction de bugs liés à la caméra.
- Correction des plantages au lancement du jeu.

---

## Release - v0.1.12 - 10-03-2025

### Added

- Ajout du système de base pour la gestion des personnages.
- Ajout des scripts de contrôle du joueur.
- Ajout d’un premier niveau de test.

### Fixed

- Correction d’erreurs dans la gestion des inputs.
- Correction des erreurs graphiques mineures.

---

## Early build - v0.1.1 - 10-12-2024

### Added

- Ajout complet du menu communautaire dans le jeu.
- Ajout de la gestion et du nettoyage des polices dans l'interface utilisateur.

### Fixed

- Correction d'un bug dans le menu où les clics ne fonctionnaient pas correctement avec les nouveaux assets UI.

## Early build - v0.1.0 - 05-12-2024

### Added

- Rajout de la gestion des objets interactifs via ScriptableObjects (Gate, Jumper, Piège, Collectibles).
- Création des éléments de base pour le jeu avec l'intégration des objets collectables et pièges dans les scènes.
- Mise à jour du système de sauvegarde des skins et inventaire via ScriptableObjects.
- Ajout des affichages textes dans le jeu (messages, scores, etc.).
- Ajout d'un fichier `CHANGELOG.md` pour la documentation continue du projet.
- Mise à jour du fichier `README.md` pour décrire les nouvelles fonctionnalités.

### Fixed

- Correction de bug lié au choix de skins dans le shop.
- Résolution des erreurs de chargement des scènes dans le jeu.
- ***

## Early build - 0.0.13 - 25-10-2024

### Added

- Mise en place des premiers éléments de la scène de menu principal.
- Ajout des animations de transition entre les menus.

---

## Early build - 0.0.12 - 22-10-2024

### Added

- Préparation des modèles pour les skins de personnage.
- Système d'interactions de base pour le joueur avec l'environnement.

---

## Early build - 0.0.11 - 18-10-2024

### Added

- Système d'affichage de score basique.
- Interface initiale pour les options de jeu.

---

## Early build - 0.0.10 - 15-10-2024

### Added

- Intégration de la logique de progression du joueur.
- Test de fonctionnalités de l'interface utilisateur.

---

## Early build - 0.0.9 - 10-10-2024

### Added

- Début de la gestion des objets dynamiques dans le jeu.
- Intégration des premiers objets interactifs (plateformes, obstacles).

---

## Early build - 0.0.8 - 05-10-2024

### Added

- Amélioration des contrôles du joueur.
- Préparation des assets pour le gameplay.

---

## Early build - 0.0.7 - 01-10-2024

### Added

- Interface de base pour le démarrage du jeu.
- Système de gestion des entrées utilisateur pour les commandes.

---

## Early build - 0.0.6 - 28-09-2024

### Added

- Préparation pour l'intégration des premières scènes de jeu.
- Ajout de contrôles de caméra.

---

## Early build - 0.0.5 - 25-09-2024

### Added

- Configuration initiale du projet dans Unity.
- Mise en place de la structure de dossiers du projet.

---

## Early build - 0.0.4 - 23-09-2024

### Added

- Structure de base des scènes du projet.
- Ajout des premiers objets de test dans le jeu.

---

## Early build - 0.0.3 - 20-09-2024

### Added

- Intégration de l'environnement de développement Unity.
- Système de détection des entrées de la souris et du clavier.

---

## Early build - 0.0.2 - 18-09-2024

### Added

- Importation de bases de données pour les assets du projet.
- Mise en place du système de tests des entrées du joueur.

---

## Early build - 0.0.1 - 09-15-2024

### Added

- Initialisation du projet Unity.
- Première version avec un environnement de base pour les tests.
