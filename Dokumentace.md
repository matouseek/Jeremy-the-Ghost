# Jeremy the Ghost - Dokumentace

V tomto dokumentu jsou popsány a přiblíženy skripty, prefaby, ScriptableObjects ze hry Jeremy the Ghost.

## Jeremy
Samotná postava Jeremyho sestává z prefabu, který na sobě má JeremyController skript. Je zde popsáno i fungování MoveManageru nebo EnergyBaru, které jsou s Jeremym spojeny.

Child GameObjecty Jeremyho jsou jeho sprite, z důvodu animace, která probíhá v lokálních souřadnicích parent GameObjetu, a Canvas, obsahující hlášku, která je zobrazena při stisknutí klávesy Space mimo collider objektu se skriptem Children.cs (viz kapitola TODO-Children).

### <a name="ScareChildren"></a>Strašení dětí (JeremyController)
V každém volání Update se zkouší vystrašit děti. To se podaří, pokud jsou splněny následující podmínky. Je stisknutý Space, Jeremy koliduje s colliderem dětí (GameObject ve scéně, opět viz TODO-Children) a momentálně děti nestraší. Při splnění podmíněk se spustí korutina, která postupně přehraje audio klipy, změní Jeremyho sprite a zavolá příslušné funkce pro dokončení levelu, jelikož vystrašením dětí končí level.

### <a name="JeremyReset"></a>Resetování Jeremyho (JeremyController)
Metoda Reset slouží k přemístění Jeremyho na daný (v průběhu hry klidně proměnlivý) respawn point, nastavení jeho počtu úskoků na maximum a nastavení jeho rychlosti 0. Jde o takový ekvivalent zabití Jeremyho. Volá se např. při kolizi Jeremyho s nějakým objektem, který ho zabije, nebo při spotřebování všech úskoků dostupných pro platformovací sekci.

### <a name="JeremyMovement"></a> Vstup a pohyb (JeremyController)
V každém volání Update se zpracovává vstup hráče. 

V metodě ResolveMoveInput se zpracovává vstup ovlivňující úskoky Jeremyho (klávesy W, A, D). Pokud hráč nestiskl ani jednu z kláves nebo nemá dostatek energie, nic se neprovede. V ostatních případech se sníží počet dostupných úskoků o 1 a pokud tím hráč nepřekročil maximální počet úskoků (v takovém případě se Jeremy resetuje), bude se vstup dále zpracovávat ve volání FixedUpdate.

V metodě ResolveDownDashInput se zpracovává zrychlení směrem dolů (držení klávesy S), viz [EnergyBar](#EnergyBar).

Ve volání FixedUpdate se oba vstupy zpracují a přidá se příslušná síla v příslušných směrech Jeremyho Rigidbody2D.

### <a name="EnergyBar"></a> EnergyBar 
Je child objektem Canvas prefabu použitého jako UI ve všech levelech. Jeho skript zajišťuje neustálou obnovu energie, která je omezena nějakou maximální hodnotou. Pokud hráč drží klávesu S, je obnova energie snížena. To tak, že se zmenší konstanta, která je každé volání Update (po vynásobení Time.deltaTime) přičtena k momentální energii. 

### MoveManager
Prefab se stejnojmeným skriptem, který obstarává Jeremyho počet úskoků. Při každém úskoku volá [JeremyController](#JeremyMovement) metodu tohoto skriptu (DecreaseAvailableMoves). Při [resetování](#JeremyReset) se volá ResetMoves z JeremyControlleru. Po každé úpravě počtu úskoků se aktualizuje příslušný text z Canvasu. Před/mezi platformovacími sekcemi jde počítání a omezování úskoků vypnout pomocí DisableMoveCounter a poté zapnout pomocí EnableMoveCounter.

## ScriptableObjecty

Ve hře slouží ScriptableObjecty pro ukládání dat za runtimu. Jsou TODO-serializovány(odkaz na data persistance) při ukončení aplikace a deserializovány při jejím spuštění. Krom zde zmíněných ScriptableObjectů jsou ve hře využity ještě následující: [LevelDescription](#LevelDescription), [LevelSectionDescription](#LevelSectionDescription) a TODO-achievementy.

### JeremyDescription
Slouží pro ukládání dat o Jeremym. Obsahuje customizaci Jeremyho. To zahrnuje Jeremyho barvu a skin očí.

### Inventory
Hráč může během hraní získávat předměty, které se ukládají právě sem. Momentálně se zde nachází pouze získané skiny očí za splnění TODO-achievementů.

## Levely

Každý level ve hře má v Unity svou vlastní scénu. Levely sestávají z tzv. platformovacích sekcí. To jsou na sebe navazující části levelu ve kterých má hráč omezený počet pohybů a při neúspěchu procházení jedné ze sekcí se [vrátí](#JeremyReset) na její začátek. Každá z těchto sekcí má svůj TODO-leaderboard.

### <a name="LevelDescription"></a> ScriptableObjecty levelů
Každý level obsahuje nějaký svůj popis - LevelDescription. Nachází se zde např. list level sekcí tohoto levelu, bool, zda byl level dohrán, nebo TODO-achievement za dohrání tohoto levelu.

### <a name="LevelSectionDescription"></a>ScriptableObjecty level sekcí
Zde má každá sekce uložený public key příslušného TODO-leaderboardu.

### LevelManager
Udržuje seznam všech levelů a momentálně hraný level. V případě [dohrání levelu](#ScareChildren) se zavolá funkce CompleteLevel. Dále obsahuje OnClick funkce pro výběr levelu z TODO-PlayMenu.