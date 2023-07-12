//#########################################################
//        Déclaration des mots clefs et commandes   
//#########################################################   
//              !!!LIBREMENT MODIFIABLE!!!   
//          Si vous ajoutez des objets moddés,   
//      il faudra également mettre à jour la méthode    
//                       init()   
//##########################################################  

// Constante pour autoriser l'accès à l'inventaire des Structures connectées
const bool MultiGrid = true;

// Constante pour activer le lancement automatique sans timer block
const bool autoRun = true;
// Temps minimum en secondes entre chaque lancement (test effectué toutes les 1.4 secondes environ)
const int autoRunDelay = 1;

// Constantes pour la gestion de la balise   
const string BeginTAG = "(",
    EndTAG = ")",
    Separator = ";",
    set_subtype = ":",
    negate_subtype = "-",
    sub_separator = ",";

// Constantes pour la validation et refut d'une commande   
const string TAGValid = "[V]",
    TAGInvalid = "[X]";

// Constantes pour l'exemption d'un block entier ou de l'inventaire de ressource   
const string exempted = "exempté",
    exemptProd = "exemptprod";

// Constantes des commandes principales   
const string ore = "minerai",
    ingot = "lingot",
    compo = "compo",
    ammo = "munition",
    tools = "outils",
    oxygen = "oxygene",
    hydrogen = "hydrogene",
    conso = "consomable";

// Constantes pour les sous-types "lingot" et "minerai"   
const string stone = "pierre",
    gravel = "gravier",
    iron = "fer",
    silicon = "silicium",
    nickel = "nickel",
    cobalt = "cobalt",
    silver = "argent",
    gold = "or",
    uranium = "uranium",
    magnesium = "magnesium",
    platinum = "platine",
    ice = "glace",
    scrap = "ferraille";

// Constantes pour le sous-type "compo"   
const string construction = "construction",
    grid = "grille",
    interior = "interieur",
    steelplate = "plaque",
    girder = "poutre",
    smalltube = "petit tube",
    largetube = "grand tube",
    motor = "moteur",
    display = "affichage",
    bulletproof = "vitre",
    computer = "ordinateur",
    reactor = "réacteur",
    thrust = "propulseur",
    gravity = "gravité",
    medical = "médical",
    radio = "radio",
    detector = "détecteur",
    explosives = "explosifs",
    solarcell = "solaire",
    powercell = "énergie",
    superconductor = "supercond";

// Constantes pour le sous-type "munition"   
const string NATO_5p56x45mm = "5.56x45mm",
    LargeCalibreAmmo = "Obus d'artillerie",
    MediumCalibreAmmo = "Obus de canon d'assaut",
    AutocannonClip = "Cargeur de canon automatique",
    NATO_25x184mm = "Conteneur de munition OTAN 25x184mm",
    LargeRailgunAmmo = "Grand sabot de railgun",
    Missile200mm = "Conteneur de missile 200 mm",
    AutomaticRifleGun_Mag_20rd = "Chargeur MR-20",
    UltimateAutomaticRifleGun_Mag_30rd = "Chargeur MR-30E",
    RapidFireAutomaticRifleGun_Mag_50rd = "Chargeur MR-50A",
    PreciseAutomaticRifleGun_Mag_5rd = "Chargeur MR-8P",
    SemiAutoPistolMagazine = "Chargeur S-10",
    ElitePistolMagazine = "Chargeur S-10E",
    FullAutoPistolMagazine = "Chargeur S-20A",
    SmallRailgunAmmo = "Petit sabot de railgun";

// Constantes pour le sous-type "outils"   
const string AngleGrinder4Item = "Meuleuse élite",
    HandDrill4Item = "Foreuse élite",
    Welder4Item = "Chalumeau élite",
    AngleGrinder2Item = "Meuleuse améliorée",
    HandDrill2Item = "Foreuse améliorée",
    Welder2Item = "Chalumeau amélioré",
    AngleGrinderItem = "Meuleuse",
    HandDrillItem = "Foreuse",
    HydrogenBottle = "Bouteille d'hydrogène",
    AutomaticRifleItem = "MR-20",
    UltimateAutomaticRifleItem ="MR-30E",
    RapidFireAutomaticRifleItem = "MR-50A",
    PreciseAutomaticRifleItem = "MR-8P",
    OxygenBottle = "Bouteille d'oxygène",
    AdvancedHandHeldLauncherItem = "PRO-1",
    AngleGrinder3Item = "Meuleuse professionnelle",
    HandDrill3Item = "Foreuse professionnelle",
    Welder3Item = "Chalumeau professionnel",
    BasicHandHeldLauncherItem = "RO-1",
    SemiAutoPistolItem = "S-10",
    ElitePistolItem = "S-10E",
    FullAutoPistolItem = "S-20A",
    WelderItem = "Chalumeau";

// Constantes pour le sous-type "consomable"
const string powerkit = "Powerkit";

// Constantes pour les objets moddés   
const string advanced_thrust = "advanced_thrust";

//########################################################   
// Déclaration des variables globales purement technique   
//########################################################   
//             !!!! NE PAS MODIFIER !!!!   
///#######################################################   

// Variables pour l'empillage des inventaires   
List<string> ItemsNotStackable = new List<string>();

// Déclaration des différentes List   
List<string> MainTypes = new List<string>();
List<string> SubTypes = new List<string>();
List<string> ExactMainTypes = new List<string>();
List<string> ExactTypes = new List<string>();
Dictionary<string, List<string>> combinaisons = new Dictionary<string, List<string>>();

List<IMyCargoContainer> TaggedContainers = new List<IMyCargoContainer>();
List<Sandbox.ModAPI.Ingame.IMyTerminalBlock> SourceBlocks = new List<Sandbox.ModAPI.Ingame.IMyTerminalBlock>();

List<Sandbox.ModAPI.Ingame.IMyTerminalBlock> blocks = new List<Sandbox.ModAPI.Ingame.IMyTerminalBlock>();

// Constante du sous-type des Conteneurs   
const string CargoContainer = "CargoContainer";

// Constantes pour le parsing des commandes   
const int ValidTAG = 0, InvalidTAG = 1, ExemptTAG = 2, NoTAG = 3;

// Variable pour signalé la présence d'objets inconnus   
bool Unknown = false;

// Date et heure du dernier lancement
DateTime lastRun = DateTime.Now;

//#################################################################   
// Méthode d'initialisation de la liste des objets non empillables   
//#################################################################   
void InitItemsNotStackable()
{
    ItemsNotStackable.Add(AngleGrinder4Item);
    ItemsNotStackable.Add(HandDrill4Item);
    ItemsNotStackable.Add(Welder4Item);
    ItemsNotStackable.Add(AngleGrinder2Item);
    ItemsNotStackable.Add(HandDrill2Item);
    ItemsNotStackable.Add(Welder2Item);
    ItemsNotStackable.Add(AngleGrinderItem);
    ItemsNotStackable.Add(HandDrillItem);
    ItemsNotStackable.Add(HydrogenBottle);
    ItemsNotStackable.Add(AutomaticRifleItem);
    ItemsNotStackable.Add(UltimateAutomaticRifleItem);
    ItemsNotStackable.Add(RapidFireAutomaticRifleItem);
    ItemsNotStackable.Add(PreciseAutomaticRifleItem);
    ItemsNotStackable.Add(OxygenBottle);
    ItemsNotStackable.Add(AdvancedHandHeldLauncherItem);
    ItemsNotStackable.Add(AngleGrinder3Item);
    ItemsNotStackable.Add(HandDrill3Item);
    ItemsNotStackable.Add(Welder3Item);
    ItemsNotStackable.Add(BasicHandHeldLauncherItem);
    ItemsNotStackable.Add(SemiAutoPistolItem);
    ItemsNotStackable.Add(ElitePistolItem);
    ItemsNotStackable.Add(FullAutoPistolItem);
    ItemsNotStackable.Add(WelderItem);
}

//###################################################   
// Méthode d'initialisation des listes :   
// - Détermine les Commandes principales   
// - Détermine les sous-commandes autorisées   
// - Initialise la liste des objets non empillables   
//###################################################   
private void Init()
{
    // Déclaration des commandes principales (commande , TypeId)   
    this.AddMain(ore, "MyObjectBuilder_Ore");
    this.AddMain(ingot, "MyObjectBuilder_Ingot");
    this.AddMain(compo, "MyObjectBuilder_Component");
    this.AddMain(ammo, "MyObjectBuilder_AmmoMagazine");
    this.AddMain(tools, "MyObjectBuilder_PhysicalGunObject");
    this.AddMain(oxygen, "MyObjectBuilder_OxygenContainerObject");
    this.AddMain(hydrogen, "MyObjectBuilder_GasContainerObject");
    this.AddMain(conso, "MyObjectBuilder_ConsumableItem");

    // Déclaration des sous-commandes (commande principal, sous-commande, SubtypeName, volume)   

    // Déclaration des sous-commande "minerai"   
    this.Add(ore, stone, "Stone");
    this.Add(ore, iron, "Iron");
    this.Add(ore, silicon, "Silicon");
    this.Add(ore, nickel, "Nickel");
    this.Add(ore, cobalt, "Cobalt");
    this.Add(ore, silver, "Silver");
    this.Add(ore, gold, "Gold");
    this.Add(ore, uranium, "Uranium");
    this.Add(ore, magnesium, "Magnesium");
    this.Add(ore, platinum, "Platinum");
    this.Add(ore, ice, "Ice");
    this.Add(ore, scrap, "Scrap");

    // Déclaration des sous-commande "lingot"   
    this.Add(ingot, gravel, "Stone");
    this.Add(ingot, iron, "Iron");
    this.Add(ingot, silicon, "Silicon");
    this.Add(ingot, nickel, "Nickel");
    this.Add(ingot, cobalt, "Cobalt");
    this.Add(ingot, silver, "Silver");
    this.Add(ingot, gold, "Gold");
    this.Add(ingot, uranium, "Uranium");
    this.Add(ingot, magnesium, "Magnesium");
    this.Add(ingot, platinum, "Platinum");

    // Déclaration des sous-commande "compo"   
    this.Add(compo, construction, "Construction");
    this.Add(compo, grid, "MetalGrid");
    this.Add(compo, interior, "InteriorPlate");
    this.Add(compo, steelplate, "SteelPlate");
    this.Add(compo, girder, "Girder");
    this.Add(compo, smalltube, "SmallTube");
    this.Add(compo, largetube, "LargeTube");
    this.Add(compo, motor, "Motor");
    this.Add(compo, display, "Display");
    this.Add(compo, bulletproof, "BulletproofGlass");
    this.Add(compo, computer, "Computer");
    this.Add(compo, reactor, "Reactor");
    this.Add(compo, thrust, "Thrust");
    this.Add(compo, gravity, "GravityGenerator");
    this.Add(compo, medical, "Medical");
    this.Add(compo, radio, "RadioCommunication");
    this.Add(compo, detector, "Detector");
    this.Add(compo, explosives, "Explosives");
    this.Add(compo, solarcell, "SolarCell");
    this.Add(compo, powercell, "PowerCell");
    this.Add(compo, superconductor, "Superconductor");

    // Déclaration des sous-commande "munition"   
    this.Add(ammo, NATO_5p56x45mm, "NATO_5p56x45mm");
    this.Add(ammo, LargeCalibreAmmo, "LargeCalibreAmmo");
    this.Add(ammo, MediumCalibreAmmo, "MediumCalibreAmmo");
    this.Add(ammo, AutocannonClip, "AutocannonClip");
    this.Add(ammo, NATO_25x184mm, "NATO_25x184mm");
    this.Add(ammo, LargeRailgunAmmo, "LargeRailgunAmmo");
    this.Add(ammo, Missile200mm, "Missile200mm");
    this.Add(ammo, AutomaticRifleGun_Mag_20rd, "AutomaticRifleGun_Mag_20rd");
    this.Add(ammo, UltimateAutomaticRifleGun_Mag_30rd, "UltimateAutomaticRifleGun_Mag_30rd");
    this.Add(ammo, RapidFireAutomaticRifleGun_Mag_50rd, "RapidFireAutomaticRifleGun_Mag_50rd");
    this.Add(ammo, PreciseAutomaticRifleGun_Mag_5rd, "PreciseAutomaticRifleGun_Mag_5rd");
    this.Add(ammo, SemiAutoPistolMagazine, "SemiAutoPistolMagazine");
    this.Add(ammo, ElitePistolMagazine, "ElitePistolMagazine");
    this.Add(ammo, FullAutoPistolMagazine, "FullAutoPistolMagazine");
    this.Add(ammo, SmallRailgunAmmo, "SmallRailgunAmmo");

    // Déclaration des sous-commande "outils"   
    this.Add(tools, AngleGrinder4Item, "AngleGrinder4Item");
    this.Add(tools, HandDrill4Item, "HandDrill4Item");
    this.Add(tools, Welder4Item, "Welder4Item");
    this.Add(tools, AngleGrinder2Item, "AngleGrinder2Item");
    this.Add(tools, HandDrill2Item, "HandDrill2Item");
    this.Add(tools, Welder2Item, "Welder2Item");
    this.Add(tools, AngleGrinderItem, "AngleGrinderItem");
    this.Add(tools, HandDrillItem, "HandDrillItem");
    this.Add(tools, hydrogen, "HydrogenBottle");
    this.Add(tools, AutomaticRifleItem, "AutomaticRifleItem");
    this.Add(tools, UltimateAutomaticRifleItem, "UltimateAutomaticRifleItem");
    this.Add(tools, RapidFireAutomaticRifleItem, "RapidFireAutomaticRifleItem");
    this.Add(tools, PreciseAutomaticRifleItem, "PreciseAutomaticRifleItem");
    this.Add(tools, oxygen, "OxygenBottle");
    this.Add(tools, AdvancedHandHeldLauncherItem, "AdvancedHandHeldLauncherItem");
    this.Add(tools, AngleGrinder3Item, "AngleGrinder3Item");
    this.Add(tools, HandDrill3Item, "HandDrill3Item");
    this.Add(tools, Welder3Item, "Welder3Item");
    this.Add(tools, BasicHandHeldLauncherItem, "BasicHandHeldLauncherItem");
    this.Add(tools, SemiAutoPistolItem, "SemiAutoPistolItem");
    this.Add(tools, ElitePistolItem, "ElitePistolItem");
    this.Add(tools, FullAutoPistolItem, "FullAutoPistolItem");
    this.Add(tools, WelderItem, "WelderItem");

    // Déclaration des sous-commandes "oxygen" et "hydrogen"   
    this.Add(oxygen, oxygen, "OxygenBottle");
    this.Add(hydrogen, hydrogen, "HydrogenBottle");

    // Déclaration des sous-commandes "conso"
    this.Add(conso, powerkit, "Powerkit");

    // Déclaration des sous-commandes des objets moddés   
    this.Add(compo, advanced_thrust, "AdvancedThrustModule");

    // Initialisation de la liste des objets non empillables   
    this.InitItemsNotStackable();
}

//#############################
// Méthode CONSTRUCTEUR (lancé 1 seule fois par session pour initialiser)
//#############################
public Program()
{
    // Initialisation
    this.Init();

    Runtime.UpdateFrequency = autoRun ? UpdateFrequency.Update100 : UpdateFrequency.None;
}

//##############################
// Méthode qui détermine si le programme peut être lancé
//##############################
bool CanRun(UpdateType updateSource)
{
    // Si le programme est lancé manuellement, on autorise le lancement du programme
    if (updateSource != UpdateType.Update100) return true;

    // Lancement automatique
    // Si le temps depuis le dernier lancement est inférieur au temps minimum, on refuse le lancement
    if ((DateTime.Now - lastRun).TotalSeconds < autoRunDelay) return false;            
    else  // Sinon, on autorise le lancement
    {
        Echo(string.Format("Lancement auto : {0}s", autoRunDelay));
        Echo("-----------------------------------------");
        return true;
    }
}

//#############################   
// Méthode PRINCIPAL   
//#############################   
public void Main(string argument, UpdateType updateSource)
{
    // Si le programme ne remplit pas les conditions pour être lancé, on quitte
    if (!CanRun(updateSource)) return;

    // Remise à zéro du booléen qui signale la présence d'objets inconnus   
    Unknown = false;
    Me.CustomData = "";

    // On récupère tous les blocks possédant un inventaire   
    GridTerminalSystem.GetBlocksOfType<Sandbox.ModAPI.Ingame.IMyTerminalBlock>(blocks, GetBlockWithInventory);

    // On récupère les blocks possédant une balise   
    this.GetTaggedContainers();

    // On récupère les blocks source   
    GridTerminalSystem.GetBlocksOfType<Sandbox.ModAPI.Ingame.IMyTerminalBlock>(SourceBlocks, GetSourceBlock);

    // On empile les inventaire des blocks   
    this.Stack_inventories();

    // On trie les inventaires   
    this.SortInventories();

    // Affichage du message de fin d'exécution   
    if (TaggedContainers.Count > 0)
    {
        Echo("-- Tri OK --");
        if (Unknown) Echo("\n-- Des objets inconnus sont présents --\n-- Voir Données Personnalisées --");
    }
    else Echo("-- Aucun Conteneur Cible --");
    Echo("");
    Echo("Dernier lancement : ");
    Echo(DateTime.Now.ToString("Le dd/MM/yyyy à HH:mm:ss"));

    // Mise à jour de la date de dernier lancement
    lastRun = DateTime.Now;
}

//###############################################   
// Méthode pour ajouter une commande principale   
//###############################################   
void AddMain(string type, string exact_type)
{
    // Si le type n'existe pas on l'ajoute    
    if (!MainTypes.Contains(type))
    {
        MainTypes.Add(type);
        ExactMainTypes.Add(exact_type);
        combinaisons.Add(type, new List<string>());
    }
}

//#################################################   
// Méthode pour ajouter une sous-commande   
//#################################################   
void Add(string type, string subtype, string exact_subtype)
{
    int index_type;

    // On récupère l'index du type    
    index_type = MainTypes.IndexOf(type);

    // Ajout du sous-type               
    SubTypes.Add(subtype);
    ExactTypes.Add(ExactMainTypes[index_type] + exact_subtype);

    // Ajout du sous-type dans les combinaisons autorisées du type   
    combinaisons[type].Add(subtype);
}

//#############################################################   
// Méhode pour parser et récupérer les blocks avec une balise   
//#############################################################   
private void GetTaggedContainers()
{
    Sandbox.ModAPI.Ingame.IMyTerminalBlock block;

    // Remise à zéro de la liste des blocks avec une balise   
    TaggedContainers.Clear();

    // Parcours de tous les blocks    
    for (int i = 0; i < blocks.Count; i++)
    {                
        block = blocks[i];

        // Si le block est un container
        if (block is IMyCargoContainer)
        {
            // Parse du nom du block   
            switch (this.ParseBlockName(block))
            {
                // Si la balise est valide   
                case ValidTAG:
                    // Mise à jour du nom du block   
                    this.UpdateBlockName(block, ValidTAG);

                    // Si il s'agit d'un conteneur on l'ajoute à la liste des conteneurs balisé   
                    if (block.BlockDefinition.TypeIdString.EndsWith(CargoContainer))
                    {
                        TaggedContainers.Add(block as IMyCargoContainer);
                    }
                    break;

                // Si la balise n'est pas valide, on met à jour le nom bu block   
                case InvalidTAG:
                    this.UpdateBlockName(block, InvalidTAG);
                    break;

                // Si la balise est une balise d'exemption, on met à jour le nom du block   
                case ExemptTAG:
                    this.UpdateBlockName(block, ValidTAG);
                    break;

                // Sinon, par défaut on enlève le TAG de validation/refut ([V] / [X])   
                default:
                    this.UpdateBlockName(block, NoTAG);
                    break;
            }
        }
    }
}

//########################################   
// Méthode qui parse le nom d'un block   
//########################################   
private int ParseBlockName(Sandbox.ModAPI.Ingame.IMyTerminalBlock block)
{
    string TAG;
    string ContainerName;

    // Suppression des espaces inutiles en début et fin de chaîne    
    ContainerName = block.CustomName.Trim();

    // Si le nom du conteneur ne contient pas de TAG on renvoi Aucun TAG   
    if (!this.ContainerIsTagged(ContainerName)) return NoTAG;

    // Récupération du TAG    
    TAG = this.GetContainerTAG(ContainerName);

    // Si la longueur du TAG est vide on renvoi Aucun TAG   
    if (TAG.Length == 0) return NoTAG;

    // Si le TAG contient le mot clef d'exemption ET d'exemption d'inventaire on renvoi TAG invalide   
    if (TAG.Contains(exempted) && TAG.Contains(exemptProd)) return InvalidTAG;

    // Si le TAG contient le mot clef d'exempltion d'inventaire mais que le block ne possède   
    // qu'un seul inventaire (donc pas un bloc de production) on renvoi TAG invalide   
    if (TAG.Contains(exemptProd) && block.InventoryCount < 2) return InvalidTAG;

    // Si le TAG ne contient qu'un seul des mots clefs d'exemption on renvoi TAG exemption   
    if (TAG == exempted || TAG == exemptProd) return ExemptTAG;

    List<string> commands = new List<string>();
    List<string> sub_commands = new List<string>();

    // On récupère les commandes   
    commands = this.GetTAGCommands(TAG);

    // On parcours chaque commande   
    for (int i = 0; i < commands.Count; i++)
    {
        // On récupère chaque sous-commande   
        sub_commands = this.GetSubCommands(commands[i]);

        // On parcours chaque sous-commande   
        for (int n = 0; n < sub_commands.Count; n++)
        {
            // Si il s'agit de la première sous-commande , il s'agit de la commande principale   
            if (n == 0)
            {
                // Vérification que la commande principale est valide et n'est présente qu'une seule fois dans la balise   
                if (MainTypes.IndexOf(sub_commands[n]) == -1 && sub_commands[n] != exempted) return InvalidTAG;
                if (TAG.IndexOf(sub_commands[n]) != TAG.LastIndexOf(sub_commands[n])) return InvalidTAG;
            }
            else
            {
                // Vérification que la sous-commande est valide est n'est présente qu'une seule fois dans la commande   
                if (combinaisons[sub_commands[0]].IndexOf(sub_commands[n]) == -1) return InvalidTAG;
                if (sub_commands.IndexOf(sub_commands[n]) != sub_commands.LastIndexOf(sub_commands[n])) return InvalidTAG;
            }
        }
    }
    // Si il n'y aucune erreur on valide le TAG   
    return ValidTAG;
}

//########################################################   
//  Méthode qui renvoi le contenu de la balise d'un block   
//########################################################   
string GetContainerTAG(string ContainerName)
{
    // Récupération du contenu de la balise   
    string TAG = null;
    TAG = ContainerName.Substring((ContainerName.LastIndexOf(BeginTAG.ToString())) + 1,
                                (ContainerName.Length - ContainerName.LastIndexOf(BeginTAG.ToString()) - 2));

    // Transformation en minuscule et renvoi du TAG   
    TAG = TAG.ToLower();

    return TAG;
}

//####################################################   
// Méthode qui vérifie si un block possède la balise   
//####################################################   
bool ContainerIsTagged(string ContainerName)
{
    // Si le block possède la balise on renvoi vrai   
    if ((ContainerName.Contains(BeginTAG.ToString()) && ContainerName.EndsWith(EndTAG.ToString()))) return true;
    // Sinon on renvoi faux   
    return false;
}

//######################################################################   
//  Méthode qui renvoi la liste des commandes présentes dans la balise   
//######################################################################   
List<string> GetTAGCommands(string TAG)
{
    string[] str_separator = new string[] { Separator };
    string[] commands;

    List<string> TAGCommands = new List<string>();

    // Split de la balise pour récupérer les différentes commandes   
    commands = TAG.Split(str_separator, StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i < commands.Length; i++)
    {
        // On ajoute chaque élément dans la liste des commandes   
        TAGCommands.Add(commands[i]);
    }

    return TAGCommands;
}

//####################################################################   
//  Méthode qui décompose une commande en sous-commandes   
//####################################################################   
List<string> GetSubCommands(string command)
{
    string[] str_sub_separator = new string[] { set_subtype, negate_subtype, sub_separator };
    string[] sub_commands;

    List<string> sub = new List<string>();

    // Split de la commande en sous-commandes   
    sub_commands = command.Split(str_sub_separator, StringSplitOptions.RemoveEmptyEntries);

    for (int i = 0; i < sub_commands.Length; i++)
    {
        // Ajout de chaque sous-commande à la liste des sous-commandes   
        sub.Add(sub_commands[i]);
    }

    return sub;
}

//####################################################################################   
//  Méthode qui met à jour le nom d'un block avec le TAG de validation ou de refut   
//####################################################################################   
void UpdateBlockName(Sandbox.ModAPI.Ingame.IMyTerminalBlock block, int action)
{
    string name;

    if (block.CustomName != null){
        // On récupère le nom du block sans le TAG de validation/refut   
        if (block.CustomName.Substring(0, 3) == TAGInvalid || block.CustomName.Substring(0, 3) == TAGValid) name = block.CustomName.Substring(3, block.CustomName.Length - 3);
        else name = block.CustomName;

        // Mise à jour du nom en fonction de l'action demandé   
        switch (action)
        {
            // Si validation, on renomme le conteneur en ajoutant le TAG de validation [V]   
            case ValidTAG:
                block.CustomName = TAGValid + name;
                break;

            // Si refut , on renomme le conteneur en ajoutant le TAG de refut [X]   
            case InvalidTAG:
                block.CustomName = TAGInvalid + name;
                break;

            // Sinon par défaut, on retire du nom du block le TAG de validation/refut   
            default:
                block.CustomName = name;
                break;
        }
    }
}

//###################################################################   
//  Méthode de prédicat qui ne sélectionne que les blocks source    
//     (blocks ayant un inventaire et pas d'exemption totale)   
//###################################################################   
bool GetSourceBlock(Sandbox.ModAPI.Ingame.IMyTerminalBlock block)
{
    string CustomName;

    // Si l'utilisation des structures connectées n'est pas autorisé
    // on rejette le block si il n'appartient pas à la même structure que le block programmable
    if (!MultiGrid && !block.IsSameConstructAs(Me)) return false;

    // Si le block est un réacteur on renvoi FAUX
    if (block is IMyReactor) return false;           

    // Si le block est une arme on renvoi FAUX
    if (block is IMyUserControllableGun) return false;           

    // Si le block n'a pas d'inventaire on renvoi FAUX   
    if (block.InventoryCount == 0) return false;

    // Si le block a une balise ET une exemption TOTALE, on renvoi FAUX   
    CustomName = block.CustomName;
    if (this.ContainerIsTagged(CustomName) == true && this.GetContainerTAG(CustomName).Contains(exempted)) return false;

    // Sinon on renvoi VRAI   
    return true;
}

//##########################################   
//  Méthode de prédicat qui ne sélectionne    
//    que les blocks avec un inventaire   
//##########################################   
bool GetBlockWithInventory(Sandbox.ModAPI.Ingame.IMyTerminalBlock block)
{
    if (block.InventoryCount > 0)
    {
        // Si l'utilisation des structures connectées n'est pas autorisé
        // on rejette le block si il n'appartient pas à la même structure que le block programmable
        if (!MultiGrid && !block.IsSameConstructAs(Me)) return false;
        else return true;
    }
    else return false;
}

//##################################   
// Méthode de trie des inventaires   
//##################################   
void SortInventories()
{
    string CustomName;
    Sandbox.ModAPI.Ingame.IMyTerminalBlock block;
    IMyInventory inventory;
    float ItemVolume;
    float ContainerVolume;
    VRage.MyFixedPoint AmountToTransfer;
    VRage.MyFixedPoint Amount;

    List<MyInventoryItem> items = new List<MyInventoryItem>();
    List<IMyCargoContainer> TargetContainers = new List<IMyCargoContainer>();

    // Parcours de tous les blocks source   
    for (int b = 0; b < SourceBlocks.Count; b++)
    {
        block = SourceBlocks[b];
        CustomName = block.CustomName;

        // Parcours des inventaires du block   
        for (int i = 0; i < block.InventoryCount; i++)
        {
            // Récupération de l'inventaire et de la liste des objets   
            inventory = block.GetInventory(i);
            items.Clear();
            inventory.GetItems(items);

            // Si il n'y a aucun objet ou si il s'agit de l'inventaire de ressource sur un block avec Exemption de l'inventaire des ressources   
            // On passe directement à l'inventaire suivant 
            if (items.Count == 0) continue;
            if (i == 0 && block.InventoryCount > 1 && this.ContainerIsTagged(CustomName) == true && this.GetContainerTAG(CustomName).Contains(exemptProd)) continue;

            // Parcours de chacun des objets   
            for (int t = items.Count - 1; t > -1; t--)
            {
                // Vérification si l'objet est connu, sinon on passe directement au suivant sans le déplacer   
                if (!this.IsKnownItem(items[t])) continue;

                // Si l'objet est déjà dans un conteneur convenable on passe directement au suivant   
                if (this.IsItemSorted(block, items[t]) == true) continue;

                // On vide l'ancienne liste des conteneurs cible   
                TargetContainers.Clear();

                // On récupère la liste des conteneurs cible potentiel pour cet objet   
                TargetContainers = this.GetTargetContainers(items[t]);

                // Si il n'y a aucun conteneur cible désigné, on passe directement à l'objet suivant   
                if (TargetContainers.Count == 0) continue;

                // Détermination de la quantité de cette objet et du volume d'1 seul   
                Amount = (VRage.MyFixedPoint)items[t].Amount;
                ItemVolume = this.GetItemVolume(items[t]);

                // Parcours des conteneurs cible   
                for (int c = 0; c < TargetContainers.Count; c++)
                {
                    // Si le conteneur cible n'est pas connecté au conteneur de l'objet on passe directement au conteneur suivant   
                    if (!inventory.IsConnectedTo(TargetContainers[c].GetInventory(0))) continue;

                    // Détermination du volume disponible dans le conteneur et de la quantité pouvant être transféré   
                    ContainerVolume = this.GetContainerVolume(TargetContainers[c]);
                    AmountToTransfer = (VRage.MyFixedPoint)(ContainerVolume / ItemVolume);

                    // Si la quantité pouvant être transféré est supérieur à la quantité disponible   
                    // On utilisera la quantité disponible   
                    if (AmountToTransfer > Amount) AmountToTransfer = Amount;

                    // Si il ne reste plus d'objet à transférer on passe directement à l'objet suivant   
                    if (Amount == 0) break;

                    // Transfère de l'objet vers l'inventaire cible   
                    inventory.TransferItemTo(TargetContainers[c].GetInventory(0), t, stackIfPossible: true, amount: AmountToTransfer);

                    // Mise à jour de la quantité restante à transférer   
                    Amount = Amount - AmountToTransfer;
                }
            }
        }
    }
}

//############################################   
//  Méthode qui vérifie si l'objet est connu   
//############################################   
bool IsKnownItem(MyInventoryItem item)
{
    // Si le type ou le sous-type de l'objet est introuvable, on renvoi FAUX   
    if (ExactTypes.IndexOf(item.Type.TypeId + item.Type.SubtypeId) == -1)
    {
        Me.CustomData = Me.CustomData + "\nType : " + item.Type.TypeId + "\nSous-type : " + item.Type.SubtypeId;
        Unknown = true;
        return false;
    }
    // Sinon on renvoi VRAI   
    return true;
}

//##########################################   
//  Méthode qui test si un objet est déjà   
//      dans un conteneur convenable   
//##########################################   
bool IsItemSorted(Sandbox.ModAPI.Ingame.IMyTerminalBlock block, MyInventoryItem item)
{
    // Si le block n'a pas de balise, on renvoi FAUX   
    if (!this.ContainerIsTagged(block.CustomName)) return false;
    // Si le conteneur est exempté, on renvoi VRAI   
    else if (this.GetContainerTAG(block.CustomName).Contains(exempted)) return true;
    // Sinon on test si le conteneur est convenable   
    else return IsSuitableTarget(block, item);
}

//########################################################################   
// Méthode qui test si un conteneur convient pour un objet en particulier   
//########################################################################   
bool IsSuitableTarget(Sandbox.ModAPI.Ingame.IMyTerminalBlock block, MyInventoryItem item)
{
    string MainType = this.GetMainType(item);
    string SubType = this.GetSubType(item);
    string TAG = this.GetContainerTAG(block.CustomName);

    // Si la balise ne contient pas le type principal de l'objet, on renvoi FAUX   
    if (!TAG.Contains(MainType)) return false;

    // Récupération de la commande concernant le type principale et ses sous-commandes   
    List<string> commands = this.GetTAGCommands(TAG);
    string command = this.GetCommand(commands, MainType);
    List<string> sub_commands = this.GetSubCommands(command);

    // Si la commande ne contient pas de sous-commande, on renvoi vrai (aucun filtre)
    if (command.Length == MainType.Length) return true;

    // Sinon, si il y a une WhiteList et une sous-commande avec le sous-type de l'objet
    // OU si il y a une BlackList et aucune sous-commande correpondant au sous-type de l'objet, on renvoi VRAI
    if ((command.Contains(set_subtype) && (sub_commands.IndexOf(SubType) != -1))
            || (command.Contains(negate_subtype) && sub_commands.IndexOf(SubType) == -1)) return true;

    // sinon on renvoi FAUX   
    return false;
}

//###########################################################   
//  Méthode qui récupère la commande complète correspondant   
//          à la commande principale demandée
//###########################################################   
string GetCommand(List<string> commands, string MainType)
{
    // Parcours de la liste des commande jusqu'à trouver une commande qui correspond   
    // à la commande demandé   
    for (int i = 0; i < commands.Count; i++)
    {
        if (commands[i].Contains(MainType)) return commands[i];
    }
    // Si aucune ne correspond, on renvoi une commande vide   
    return "";
}

//#######################################   
// Méthode qui renvoi le type d'un objet    
//     (la commande correspondante)   
//#######################################   
string GetMainType(MyInventoryItem item)
{
    // Récupération du type de l'objet   
    string terme = item.Type.TypeId;
    // Récupération de l'index correspondant   
    int index = ExactMainTypes.IndexOf(terme);
    // On renvoi le type qui se trouve à l'index déterminé   
    return MainTypes[index];
}

//##############################################   
//  Méthode qui renvoi le sous-type d'un objet   
//      (la sous-commande correspondante)   
//##############################################   
string GetSubType(MyInventoryItem item)
{
    // Récupération du sous-type de l'objet   
    string terme = item.Type.TypeId + item.Type.SubtypeId;
    // Récupération de l'index correspondant   
    int index = ExactTypes.IndexOf(terme);
    // On renvoi le sous-type qui se trouve à l'index déterminé   
    return SubTypes[index];
}

//#####################################################   
//  Méthode qui renvoi la liste des conteneurs cibles   
//              pour l'objet fourni   
//#####################################################   
List<IMyCargoContainer> GetTargetContainers(MyInventoryItem item)
{
    List<IMyCargoContainer> containers = new List<IMyCargoContainer>();

    // Parcours des conteneurs possédant une balise valide   
    for (int i = 0; i < TaggedContainers.Count; i++)
    {
        // Si le conteneur est une cible convenable, on l'ajoute à la liste des conteneurs cible   
        if (this.IsSuitableTarget(TaggedContainers[i], item)) containers.Add(TaggedContainers[i]);
    }
    // On renvoi la liste des conteneurs cible   
    return containers;
}

//###########################################   
//  Méthode qui renvoi le volume d'un objet   
//###########################################   
float GetItemVolume(MyInventoryItem item)
{
    float volume = 0f;

    volume = item.Type.GetItemInfo().Volume * 1000;

    // On renvoi le volume   
    return volume;
}

//#######################################################   
//  Méthode qui renvoi le volume restant d'un conteneur   
//#######################################################   
float GetContainerVolume(IMyCargoContainer container)
{
    float volume = 0f;
    // Calcul du volume restant   
    volume = (float)container.GetInventory(0).MaxVolume - (float)container.GetInventory(0).CurrentVolume;

    // On multiplie le volume par 1000 (il y a une erreur d'unité dans les méthodes de récupération de volume)   
    volume = volume * 1000;
    return volume;
}

//#################################################   
//  Méthode qui empile les inventaires d'un block   
//#################################################   
private void Stack_inventories()
{
    List<Sandbox.ModAPI.Ingame.IMyTerminalBlock> contenants = new List<Sandbox.ModAPI.Ingame.IMyTerminalBlock>();
    List<string> ItemsList = new List<string>();
    IMyInventory inventaire;
    int index_dest;

    GridTerminalSystem.GetBlocksOfType<Sandbox.ModAPI.Ingame.IMyTerminalBlock>(contenants, GetBlockWithInventory);

    for (int c = 0; c < contenants.Count; c++)
    {
        // Boucle sur les inventaires de l'objets     
        //(ex : une raffinerie possède 1 inventaire pour les ressources et 1 pour sa production)    
        for (int i = 0; i < contenants[c].InventoryCount; i++)
        {
            inventaire = contenants[c].GetInventory(i);

            // On récupère la liste des objets sous forme de list string                           
            ItemsList.Clear();
            ItemsList = this.GetItemsList(inventaire);

            // Boucle sur les items de l'inventaire :                     
            for (int n = ItemsList.Count - 1; n > -1; n--)
            {
                // Si l'item ne peut être empilé on passe directement au suivant   
                if (ItemsNotStackable.IndexOf(ItemsList[n]) != -1) continue;

                // Récupération de l'index du premier objet de même type   
                index_dest = ItemsList.IndexOf(ItemsList[n]);

                // Si l'index de l'objet en cours n'est pas l'index du premier objet de ce type   
                if (n != index_dest && inventaire.IsItemAt(index_dest))
                {
                    // On déplace le doublon sur l'objet original   
                    inventaire.TransferItemTo(inventaire, n, index_dest);
                }
            }

        }
    }
}

//##################################################################   
//  Méthode qui récupère les objets d'un inventaire en List string    
//          Pour faciliter la recherche des doublons   
//##################################################################   
List<string> GetItemsList(IMyInventory inventaire)
{
    List<string> ItemsList = new List<string>();
    List<MyInventoryItem> items = new List<MyInventoryItem>();
    inventaire.GetItems(items);

    for (int i = 0; i < items.Count; i++)
    {
        ItemsList.Add(items[i].Type.SubtypeId);
    }
    return ItemsList;
}