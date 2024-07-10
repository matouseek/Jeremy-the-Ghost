# Jeremy the Ghost - Dokumentace

V tomto dokumentu jsou popsány a přiblíženy skripty, prefaby, ScriptableObjects ze hry Jeremy the Ghost.

## Jeremy
Samotná postava Jeremyho sestává z prefabu, který na sobě má JeremyController skript. Je zde popsáno i fungování MoveManageru nebo EnergyBaru, které jsou s Jeremym spojeny.

Child GameObjecty Jeremyho jsou jeho sprite, z důvodu animace, která probíhá v lokálních souřadnicích parent GameObjetu, a Canvas, obsahující hlášku, která je zobrazena při stisknutí klávesy Space mimo collider objektu se skriptem Children.cs (viz kapitola TODO-Children).

### Strašení dětí (JeremyController)
V každém volání Update se zkouší vystrašit děti. To se podaří, pokud jsou splněny následující podmínky. Je stisknutý Space, Jeremy koliduje s colliderem dětí (GameObject ve scéně, opět viz TODO-Children) a momentálně děti nestraší. Při splnění podmíněk se spustí korutina, která postupně přehraje audio klipy, změní Jeremyho sprite a zavolá příslušné funkce pro dokončení levelu, jelikož vystrašením dětí končí level.

### Resetování Jeremyho (JeremyController)
Metoda Reset slouží k přemístění Jeremyho na daný (v průběhu hry klidně proměnlivý) respawn point, nastavení jeho počtu úskoků na maximum a nastavení jeho rychlosti 0. Jde o takový ekvivalent zabití Jeremyho. Volá se např. při kolizi Jeremyho s nějakým objektem, který ho zabije, nebo při spotřebování všech úskoků dostupných pro platformovací sekci.

### Vstup a pohyb (JeremyController)
V každém volání Update se zpracovává vstup hráče. 

V metodě ResolveMoveInput se zpracovává vstup ovlivňující úskoky Jeremyho (klávesy W, A, D). Pokud hráč nestiskl ani jednu z kláves nebo nemá dostatek energie, nic se neprovede. V ostatních případech se sníží počet dostupných úskoků o 1 a pokud tím hráč nepřekročil maximální počet úskoků (v takovém případě se Jeremy resetuje), bude se vstup dále zpracovávat ve volání FixedUpdate.

V metodě ResolveDownDashInput se zpracovává zrychlení směrem dolů (držení klávesy S), viz TODO-EnergyBar.

Ve volání FixedUpdate se oba vstupy zpracují a přidá se příslušná síla v příslušných směrech Jeremyho Rigidbody2D.

### EnergyBar