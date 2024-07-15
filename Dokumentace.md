# Jeremy the Ghost - Dokumentace

V tomto dokumentu jsou popsány a přiblíženy skripty, prefaby, ScriptableObjects ze hry Jeremy the Ghost.

## <a name="Jeremy"></a>Jeremy
Samotná postava Jeremyho sestává z prefabu, který na sobě má JeremyController skript. Je zde popsáno i fungování MoveManageru nebo EnergyBaru, které jsou s Jeremym spojeny.

Child GameObjecty Jeremyho jsou jeho sprite, z důvodu animace, která probíhá v lokálních souřadnicích parent GameObjetu, a Canvas, obsahující hlášku, která je zobrazena při stisknutí klávesy Space mimo collider [objektu se skriptem Children.cs](#Children).

### <a name="ScareChildren"></a>Strašení dětí (JeremyController)
V každém volání Update se zkouší vystrašit děti. To se podaří, pokud jsou splněny následující podmínky. Je stisknutý Space, Jeremy koliduje s colliderem [dětí](#Children) a momentálně děti nestraší. Při splnění podmíněk se spustí korutina, která postupně přehraje audio klipy, změní Jeremyho sprite a zavolá příslušné funkce pro dokončení levelu, jelikož vystrašením dětí končí level.

### <a name="JeremyReset"></a>Resetování Jeremyho (JeremyController)
Metoda Reset slouží k přemístění Jeremyho na daný (v průběhu hry klidně proměnlivý) respawn point, nastavení jeho počtu úskoků na maximum a nastavení jeho rychlosti 0. Jde o takový ekvivalent zabití Jeremyho. Volá se např. při kolizi Jeremyho s nějakým objektem, který ho zabije, nebo při spotřebování všech úskoků dostupných pro platformovací sekci.

### <a name="JeremyMovement"></a> Vstup a pohyb (JeremyController)
V každém volání Update se zpracovává vstup hráče. 

V metodě ResolveMoveInput se zpracovává vstup ovlivňující úskoky Jeremyho (klávesy W, A, D). Pokud hráč nestiskl ani jednu z kláves nebo nemá dostatek energie, nic se neprovede. V ostatních případech se sníží počet dostupných úskoků o 1 a pokud tím hráč nepřekročil maximální počet úskoků (v takovém případě se Jeremy resetuje), bude se vstup dále zpracovávat ve volání FixedUpdate.

V metodě ResolveDownDashInput se zpracovává zrychlení směrem dolů (držení klávesy S), viz [EnergyBar](#EnergyBar).

Ve volání FixedUpdate se oba vstupy zpracují a přidá se příslušná síla v příslušných směrech Jeremyho Rigidbody2D.

### <a name="EnergyBar"></a> EnergyBar 
Je child objektem Canvas prefabu použitého jako UI ve všech levelech. Jeho skript zajišťuje neustálou obnovu energie, která je omezena nějakou maximální hodnotou. Pokud hráč drží klávesu S, je obnova energie snížena. To tak, že se zmenší konstanta, která je každé volání Update (po vynásobení Time.deltaTime) přičtena k momentální energii. 

### <a name="MoveManager"></a> MoveManager
Prefab se stejnojmeným skriptem, který obstarává Jeremyho počet úskoků. Při každém úskoku volá [JeremyController](#JeremyMovement) metodu tohoto skriptu (DecreaseAvailableMoves). Při [resetování](#JeremyReset) se volá ResetMoves z JeremyControlleru. Po každé úpravě počtu úskoků se aktualizuje příslušný text z Canvasu. Před/mezi platformovacími sekcemi jde počítání a omezování úskoků vypnout pomocí DisableMoveCounter a poté zapnout pomocí EnableMoveCounter.

## ScriptableObjecty

Ve hře slouží ScriptableObjecty pro ukládání dat za runtimu. Jsou [serializovány](#DataPersistence) při ukončení aplikace a deserializovány při jejím spuštění. Krom zde zmíněných ScriptableObjectů jsou ve hře využity ještě následující: [LevelDescription](#LevelDescription), [LevelSectionDescription](#LevelSectionDescription) a [achievementy](#Achievements).

### <a name="JeremyDescription"></a> JeremyDescription
Slouží pro ukládání dat o Jeremym. Obsahuje customizaci Jeremyho. To zahrnuje Jeremyho barvu a skin očí.

### <a name="Inventory"></a> Inventory
Hráč může během hraní získávat předměty, které se ukládají právě sem. Momentálně se zde nachází pouze získané skiny očí za splnění [achievementů](#Achievements).

## Levely

Každý level ve hře má v Unity svou vlastní scénu. Levely sestávají z tzv. platformovacích sekcí. To jsou na sebe navazující části levelu ve kterých má hráč omezený počet pohybů a při neúspěchu procházení jedné ze sekcí se [vrátí](#JeremyReset) na její začátek. Každá z těchto sekcí má svůj [leaderboard](#LeaderboardMenu).

### <a name="LevelDescription"></a> ScriptableObjecty levelů
Každý level obsahuje nějaký svůj popis - LevelDescription. Nachází se zde např. list level sekcí tohoto levelu, bool, zda byl level dohrán, nebo [achievement](#Achievements) za dohrání tohoto levelu.

### <a name="LevelSectionDescription"></a>ScriptableObjecty level sekcí
Zde má každá sekce uložený public key příslušného [leaderboardu](#LeaderboardMenu).

### LevelManager
Udržuje seznam všech levelů a momentálně hraný level. V případě [dohrání levelu](#ScareChildren) se zavolá funkce CompleteLevel. Dále obsahuje OnClick funkce pro výběr levelu z [PlayMenu](#PlayMenu).

## Environment

Prostředí ve hře je tvořeno zejména 2D sprity s (non-trigger) colliderem. Jsou ovšem nějaké GameObjecty, které se vyskytují častěji nebo opakovaně a existují tedy jejich prefaby.

### Resetující GameObjecty

Skupina GameObjectů nebo skriptů, které nějakým způsobem [resetují](#JeremyReset) Jeremyho nebo úzce interagují s GameObjecty, které Jeremyho resetují.

#### DamagingObjectController
Pokud GameObject s tímto skriptem koliduje s Jeremym, Jeremy se [resetuje](#JeremyReset).

#### FallReset
[Resetuje](#JeremyReset) Jeremyho při kolizi s tímto objektem. Používá se pod platformovacími sekcemi, které nemají pevnou zem.

#### <a name="Hammer"></a>Hammer
Má na sobě skript HammerController a animátor. V animaci jsou v určitých momentech volány funkce HammerControlleru, které aktivují/deaktivují DamagingCollider. Také je možné animaci kladiva začít s nějakým zpožděním za pomoci [animation helpera](#AnimationHelper).

#### <a name="ThornCannon"></a>ThornCannon
Konfigurovatelný kanon/turret, kterému lze nastavit maximální dosah a čas po kterém na Jeremyho začně střílet. ThornCannonController si nejprve ve Startu zjistí, jak vysoký je jeho sprite (střílí totiž z vrcholu a ne ze středu a tedy místo ze kterého povede raycast bude upraveno pomocí této hodnoty). Poté se každé volání Update kouká, zda je hráč v dosahu (pomocí raycastu směrem k hráči) a pokud ano, sníží countdown. Když countdown dosáhne 0, spustí se animace výstřelu na jejímž konci je pomocí eventu zavolána funkce, které vytvoří instanci střely.

#### ThornBullet
Střela letí daným směrem a ignoruje kolize se vším, co má tag s hodnotou _ignoredByBulletsTag proměnné (což jsou např. ostatní střely). Pokud zkoliduje s Jeremym, [resetuje](#JeremyReset) ho. Pokud zkoliduje s něčím jiným, spustí se animace hniloby střely (trnu) na jejímž konci střela zanikne.

### Neresetující GameObjecty

Skupina GameObjectů, které se častěji, či opakovaně vyskytují v levelech, ale nijak ne[resetují](#JeremyReset) Jeremyho.
Mimo jiné také GameObjecty pro správu tzv. platformovacích sekcí ze kterých se levely skládají. Ty na sebe můžou, ale nemusí, navazovat.

#### <a name="PlatformingSection"></a>PlatformingSection
Označuje platformovací sekci, které může, ale nemusí, předcházet jiná platformovací sekce. Obsahuje 4 child GameObjecty, které platformovací sekci tvoří. 

NoGoingBackCollider je deaktivovaný (non-trigger) collider, který se vstupem do platformovací sekce aktivuje, aby z ní hráč nemohl jít zpět. 

<a name="PS_Respawn"></a>PS_Respawn je nic neobsahující game object, jehož transform.position se využívá jako [respawn](#JeremyReset) dané platformovací sekce. 

PS_Start s jeho colliderem označuje začátek platformovací sekce. Na něm je skript PSEnter, ten obsahuje proměnné pro NoGoingBackCollider, předchozí a novou kameru (každý vstup do platformovací sekce je spjat se změnou kamery), maximální počet úskoků na tuto sekci, [respawn point](#PS_Respawn) a nepovinný [MoveLogger](#MoveLogger) předchozí sekce (pokud nějaká byla). Při kolizi skript aktivuje NoGoingBackCollider, [změní priority kamer](#CameraHelper), nastaví [MoveManageru](#MoveManager) maximální počet úskoků, nastaví [Jeremymu](#Jeremy) nový respawn point a pokud je přítomný [MoveLogger předchozí sekce](#MoveLogger), zaloguje počet použitých úskoků.

<a name="MoveLogger"></a>MoveLogger má pouze jednu metodu, která zjistí použitý počet úskoků na platformovací sekci a zapíše ji do [leaderboardu](#LeaderboardMenu).

#### Konec navazujících platformovacích sekcí
Pro ukončení řetězce několika na sebe navazujících platformovacích sekcí se používá prefab PlatformingSectionsEnd. Ten je, dalo by se říct, podmnožinou PlatformingSection. Při kolizi pouze aktivuje svůj NoGoingBackCollider, [změní prioritu kamer](#CameraHelper) a [zaloguje počet použitých úskoků](#MoveLogger) (to musí vždy, jelikož ukončuje nějaký řetězec platformovacích sekcí, takže mu nějaká z nich musí předcházet).

#### <a name="Children"></a> Children
OnTriggerEnter2D zobrazí text indikující, že Jeremy je nyní dostatečně blízko na strašení dětí a nastaví CanScare v [JeremyControlleru](#ScareChildren) na true, čímž značí, že je Jeremy dostatečně blízko na vystrašení dětí.

OnTriggerExit2D vrátí věci do původního stavu (text zmizí, CanScare = false).

## Menu

Popis funkcionality a přechodů v menu.

### <a name="MainMenu"></a> MainMenu
Hlavní menu je spravováno objektem MenuManager se stejnojmeným skriptem. Všechny funkce zde slouží jako OnClick funkce nějakého tlačítka v menu, nebo ze dále volají z OnClick funkcí.

<a name="PlayMenu"></a>Play funkce (pokud si hráč ještě nezvolil přezdívku) zobrazí input field do kterého hráč zadá svou přezdívku. Pokud si hráč již přezdívku zvolil, zobrazí výběr levelů podle toho, které levely již hráč dokončil. Krom prvního levelu platí, že tlačítko pro spuštění levelu se zobrazí, pokud je předchozí level dokončen.

ReturnToMainMenu slouží pro navrácení do hlavního menu pomocí tlačítka Return v nějakém submenu.

ShowLeaderboard zobrazí [leaderboard menu](#LeaderboardMenu).

ShowCustomization zobrazí [customization menu](#CustomizationMenu).

SubmitName při stisku Submit tlačítka u volby přezdívky uloží do PlayerPreferences pod názvem "Name" přezdívku hráče.

ShowAchievement zobrazí [achievements menu](#AchievementMenu).

### <a name="CustomizationMenu"></a>CustomizationMenu
Při zobrazení menu se načtou data z [inventáře](#Inventory). Pokud má hráč na výběr z různých kosmetických doplňků (tedy má v inventáří více než jeden), zobrazí se i tlačítka pro výběr těchto předmětů.

Skript obsahuje OnClick funcke pro změnu barvy, nebo změnu kosmetických doplňků Jeremyho. Změny Jeremyho customizace se ukládají do [příslušného ScriptableObjectu](#JeremyDescription).

### <a name="LeaderboardMenu"></a>LeaderboardMenu
Příslušný manager tohoto menu na sobě má skript, který ovládá leaderboard menu. Ten obsahuje listy jednotlivých sloupců v leaderboardu, dropdown menu pro výběr levelu a level sekce a text field pro zobrazování zpráv při načítání leaderboardu.

GetLeaderboard načte data z leaderboardu a a po dobu načítání dat zobrazí text "Loading". Při neúspěšném načtení dat zobrazí text "Error loading leaderboard".

ClearPreviousEntries zobrazí na všech místech leaderboardu prázdné řetězce.

SetEntry nastaví hráči novou hodnotu v leaderboardu a to v jednom ze dvou případů. Pokud žádnout hodnotu pro daný level a level sekci v leaderboardu ještě nemá, nebo pokud danou level sekci zvládl dohrát s nižším počtem úskoků.

ShowLeaderboard je funkce volaná z [MenuManageru](#MainMenu) při přechodu do LeaderboardMenu.

SelectLevel upraví dropdown LevelSection tak, aby odpovídal sekcím zvoleného levelu. Dále zavolá SelectLevelSection.

SelectLevelSection zavolá GetLeaderboard pro zvolenou sekci.

### PauseMenu
Je menu, které obstarává PauseMenuManager (se stejnojmeným skriptem) a zobrazí se, když hráč při hraní levelu stiskne klávesu Esc. Menu se dá zavřít stisknutím klávesy podruhé, nebo kliknutím na tlačítko Close. Pro tuto funkčnost obsahuje skript metody Pause a Unpause. Skript dále obsahuje OnClick funkce pro jednotlivá tlačítka menu.

## <a name="Achievements"></a>Achievementy

### Achievementy jako ScriptableObjects
Každý achievement je reprezentován jako ScriptableObject, který má jméno, popis, odměnu (zatím kosmetický doplněk - sprite očí) a bool, zda je achievement splněný, dále je zde také odkaz na inventář, kam se případná odměna přidá, když je achievement splněný.

### CountingAchievementy
Potomek Achievementu, který představuje achievement, který je udělen za nějaký počet něčeho (např. achievement za 10 objevených secretů). Tento potomek svého rodiče tedy rozšiřuje o počítadlo a nějakou hraniční hodnotu při jejímž překonání je achievement splněn. Počítadlo pak ostatní skripty mohou zvětšovat pomocí IncreaseCount metody, která se sama stará o případné splnění achievementu.

### <a name="AchievementMenu"></a>AchievementManager
Spravuje AchievementMenu, konkrétně správné zobrazení achievementu v tomto menu. Pro každý achievement metoda vyplní příslušný box jménem, popisem a případně zobrazí odměnu za daný achievement. Pokud je achievement splněný, je označen nápisem "Completed".

## <a name="DataPersistence"></a>DataPersistence

Za běhu jsou data uložená ve ScriptableObjectech, po ukončení aplikace ale musí být serializovány a uloženy. Data se serializují do JSON souborů a nijak se nekódují.

### DataPersistentScriptableObject
Předek pro každý ScriptableObject, který chce být serializován. Obsahuje virtuální metody pro serializaci a deserializaci ScriptableObjectu. Ty mají implementaci, která zaserializuje veřejné vlastnosti objektu a zapíše do souborů v adresáři Application.persistentDataPath, soubory jsou pojmenovány podle jména příslušného ScriptableObjectu. Různé SciptableObjecty si tuto funkcionalitu mohou overridenout.

### DataPersistenceManager
DontDestroyOnLoad objekt, jehož skript obsahuje všechny DataPersistentScriptableObjecty. Při Startu zavolá metodu pro deserializaci každého takového objektu. Při OnApplicationQuit zaserializuje každý takový objekt.

## Efekty

Zde jsou popsány skripty, které vytvářejí nějaký efekt.

### MovingObjectSpriteFade
Tento skript dostane nějaký list SpriteRendererů, nějaký pohybující se objekt a počátek a konec fade efektu. Když se pohybující se objekt nachází před počátkem efektu, nic se neděje. Když se pohybující se objekt nachází mezi počátkem a koncem efektu, nastaví se alfa kanál SpriteRendererů podle toho, jak moc je pohybující se objekt blízko konci. Pokud je pohybující se objekt za koncem efektu, alfa kanál všech SpriteRendererů je nastaven na 0.

Tento efekt se využívá v Intro levelu při průchodu dveřmi v domě.

### FadeInThornCannons
Efekt slouží pouze jednomu účelu. Nachází se na objektu s trigger colliderem a jakmile hráč zkoliduje s tímto objektem, má odahlit [kanóny](#ThornCannon). Při Startu tedy nastaví animátorům kanónů trigger, který kanóny zahalí. Při kolizi se kanóny začnou odhalovat.

## Helper skripty

Zde jsou popsány skripty, které obsahují nějaké pomocné funkce k různým aspektům hry a nejsou specializovány na nějaké konkrétní objekt.

### <a name="AnimationHelper"></a>AnimationHelper
Obsahuje metody, které pomáhají s animacemi.

SetAnimatorBoolWithDelay dostane argumenty animátor, jméno bool hodnoty, její hodnotu a zpoždění. Po tomto zpoždění nastaví danou bool hodnotu animátoru. Toto je využívané pro zpoždění animace [kladiv](#Hammer).

### <a name="CameraHelper"></a>CameraHelper
Obsahuje metody, které pomáhají s kamerami.

SwapCameraPriority prohodí priority dvou kamer. Používá se pro změnu kamer při přechodu mezi [platformovacími sekcemi](#PlatformingSection).