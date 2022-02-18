## IIM_DesignPattern_Exam


### Réparation du code du professeur :
+ (Oui c'est assez important pour que je le marque)
  + \+ Plus d'erreur dans la scène.
  + \+ On prend bien des dégâts.
  + \+ Remplacement du système de temps sur les Bullet par un système de connaissance du lanceur.


### Mort du personnage :
+ Une classe SceneLoader, s'occupant du chargement de scene.
+ Une classe refenrence vers celle-ci pour pouvoir y accéeder depuis le prefab du joueur.
+ Appeler la fonction pour reload la scene dans l'event OnDeath du player.


### Se défendre :
+ Une classe EntityBlock.
+ Ajout dans l'input du joueur le bouton "block".
+ Ajout d'une variable dans Health "CanTakeDamage"
+ Quand on utilise le bouclier, on passe cette variable à false pour ne plus prendre de dégâts.
+ Dans l'input, on désactive la possibilté de tirer tant qu'on utilise le bouclier.
  + \+ Création d'une classe ActiveSwitcher pour changer le sprite à afficher.
  + \+ Création d'un script "ShieldOrientation" pour changer l'orientation du bouclier en fonction d'où regarde le joueur.
  + \+ Création fait main d'un pixel art de bouclier.


### Récuperer des objets :
+ Création des interfaces IGrabbable pour grab des objets, et IUsable pour les objets utilisables.
+ Création de la classe EntityGrab pour pouvoir grab/use les objets.
+ Ajout d'un Input pour Grab et Use (e). Et d'un Input pour Drop (a).
+ Création du prefab Key comprenant les interfaces IGrabbable et IUsable.
+ Création du prefab Potion comprenant les interfaces IGrabbable et IUsable.
+ Changement de l'interface IHealth et des classes l'incluant pour y inclure la fonction Heal et l'event OnHeal.
  + \+ Gestion d'affichage du sprite de l'objet lorsqu'on n'utilise pas le bouclier, où qu'on a pas tirer depuis un certain temps (EntityHoldingRenderer).
  + \+ Création fait main d'un pixel art de potion et de clé.


### Barre de vie :
+ Création du script LifebarUpdater.
+ Ajout de la fonction UpdateHealth dans les Event Health.OnDamage et Health.OnHeal (Pas d'utilisation du paramètre car plus safe de cette manière (résultat toujours correct)).


### ObjectPool :
+ //
  + \+ Création de l'interface IRecyclable.
  + \+ Création de la classe abstraite ObjectPool<T> where T : IRecyclable.
  + \+ Implémentation de l'interface IRecyclable dans Bullet.
+ Création de la classe BulletPool : ObjectPool<Bullet>.
+ Gestion de la création de Bullet (Ne pas en instancier si déjà présent).
+ Désactiver les Bullet au lieu de les détruire.
+ Créer une coroutine dans la pool pour clean les Bullet si il y a beaucoup de Bullet non utilisées.

  
### FX/SFX :
+ Création de la classe SoundPlayer pour jouer un son.
+ Création de sa référence (Reference<SoundPlayer>) et de son setter pour pouvoir y accéder plus facilement.
+ Création de l'UnityEvent OnTouch dans les Bullet et ajout d'un PlaySound().

  
### Interrupteur :
+ Création de l'interface IToggle.
+ Création de la classe EntityToggle implémentant IToggle.
+ Création de la classe DoorToggle qui demande une liste de Toggle et quand ils sont tous true, le gameObject disparait.
+ Changement dans la classe Bullet pour qu'il active les IToggle.
  + \+ Ajout d'un visuel sur les Toggle quand ils sont actifs.

  
### Box :
+ Réalisation qu'il existait déjà une interface ITouchable.
+ Modification de la classe Box pour qu'elle implémante l'inteface IToggle et non ITouchable.
+ Quand Toggled, l'objet est détruit.
+ Création d'un système de drop à poids, appelé quand Toggled.

  
### Screenshake :
+ Création de la référence vers la classe ControlShake (Reference<ControlShake>) et son setter.
+ Création de l'UnityEvent OnDamage dans la classe Health
+ Ajout du Screenshake dans l'event OnDamage;
