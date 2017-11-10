using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Common.Constants;
using Common.Enums;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DataAccessLayer.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DatabaseContext context)
        {
            CheckAndCreateRole(context, CommonRoles.Regular);
            CheckAndCreateRole(context, CommonRoles.CarService);
            CheckAndCreateRole(context, CommonRoles.Administrator);
            CheckAndCreateUser(context, "user@user.com", "password", CommonRoles.Regular);
            CheckAndCreateUserProfile(context, "user@user.com", "Test", "+7(978)-111-11-11");
            CheckAndCreateUser(context, "mechanic@mechanic.com", "password", CommonRoles.CarService);
            CheckAndCreateCarService(context, "mechanic@mechanic.com");
            CheckAndCreateUser(context, "admin@admin.com", "password", CommonRoles.Administrator);

            #region Car marks & models

            CheckAndCreateCarMarkWithModels(context, "Abarth", new[] { "Fiat 500", "Fiat 595" });
            CheckAndCreateCarMarkWithModels(context, "Acura", new[] { "CL", "EL", "ILX", "Integra", "Legend", "MDX", "NSX", "RDX", "RL", "RLX", "RSX", "SLX", "TL", "TLX", "TSX", "Vigor", "ZDX" });
            CheckAndCreateCarMarkWithModels(context, "Adler", new[] { "Stromform", "Trumpf" });
            CheckAndCreateCarMarkWithModels(context, "Aero", new[] { "30" });
            CheckAndCreateCarMarkWithModels(context, "Aixam", new[] { "400", "500", "City", "Scouty" });
            CheckAndCreateCarMarkWithModels(context, "Alfa Romeo", new[] { "1333", "145", "146", "147", "149", "155", "156", "159", "164", "166", "169", "2600", "33", "4C", "6 (119)", "75", "90", "Alfasud", "Alfetta", "AR", "Arna", "Brera", "Crosswagon", "Das", "Giulia", "Giulietta", "Gold Cloverleaf", "GT", "GTA", "GTV", "Imola", "Junior", "Mito", "Montreal", "Spider", "Sprint", "Stelvio", "SZ" });
            CheckAndCreateCarMarkWithModels(context, "Alpine", new[] { "A110", "A310", "A610" });
            CheckAndCreateCarMarkWithModels(context, "Altamarea", new[] { "2E" });
            CheckAndCreateCarMarkWithModels(context, "Aro", new[] { "10", "104", "24", "240", "243", "244", "245", "246", "461", "Spartana" });
            CheckAndCreateCarMarkWithModels(context, "Artega", new[] { "GT" });
            CheckAndCreateCarMarkWithModels(context, "Asia", new[] { "Cosmos", "Rocsta", "Topic", "Towner" });
            CheckAndCreateCarMarkWithModels(context, "Aston Martin", new[] { "Bulldog", "Cygnet", "DB11", "DB7", "DB9", "DBS", "Lagonda", "Rapide", "Tick", "Tickford Capri", "V12", "V8", "V8 Vantage", "Vanquish", "Vantage", "Virage", "Volante", "Zagato" });
            CheckAndCreateCarMarkWithModels(context, "Audi", new[] { "100", "200", "50", "80", "90", "A1", "A2", "A3", "A3 Sportback E-tron", "A4", "A4 Allroad", "A5", "A6", "A6 Allroad", "A7", "A8", "Coupe", "DKW", "Q2", "Q3", "Q5", "Q7", "Q7 E-tron", "R8", "RS 4 Avant", "RS Q3", "RS3", "RS4", "RS5", "RS6", "RS7", "S2", "S3", "S4", "S5", "S6", "S7 Sportback", "S8", "SQ", "SQ5", "SQ7", "TT", "TT (все)", "TT RS", "TTS", "V8" });
            CheckAndCreateCarMarkWithModels(context, "Austin", new[] { "Allegro", "Ambassador", "FX", "Maestro", "Maxi", "Maxi 2", "Metro", "Mini", "Mini Classic", "Mini MK", "Montego", "Montego Kombi", "Princess", "Princess 2", "Rover" });
            CheckAndCreateCarMarkWithModels(context, "Austin-Healey", new[] { "3000" });
            CheckAndCreateCarMarkWithModels(context, "Autobianchi", new[] { "A 112" });
            CheckAndCreateCarMarkWithModels(context, "Avia", new[] { "31" });
            CheckAndCreateCarMarkWithModels(context, "Baoya", new[] { "New Energy" });
            CheckAndCreateCarMarkWithModels(context, "Barkas (Баркас)", new[] { "1001", "1990", "B1000", "VEB" });
            CheckAndCreateCarMarkWithModels(context, "Baw", new[] { "BJ1021ME", "Hiace Minibus", "Land King", "Luling", "Reach" });
            CheckAndCreateCarMarkWithModels(context, "Beijing", new[] { "BJ 2020", "BJ 2021", "BW4Y", "Land King" });
            CheckAndCreateCarMarkWithModels(context, "Bentley", new[] { "Arnage", "Azure", "Bentayga", "Brooklands", "Continental", "Continental GT", "Continental GT V8", "Continental GT V8 S", "Continental Supersports", "Corniche", "Eight", "Flying Spur", "Flying Spur V8", "Mark VI", "Mulsanne", "S 1", "S 2", "Series II", "Speed 8", "T 1", "T 2", "Turbo R", "Turbo RT" });
            CheckAndCreateCarMarkWithModels(context, "Bertone", new[] { "Freeclimber" });
            CheckAndCreateCarMarkWithModels(context, "Bio Auto", new[] { "evA-2", "evA-4", "evA-5" });
            CheckAndCreateCarMarkWithModels(context, "Blonell", new[] { "TF 2000 MK1" });
            CheckAndCreateCarMarkWithModels(context, "BMW", new[] { "1 Series", "1 Series М", "116", "118", "120", "123", "125", "128", "130", "135", "1602", "2 Series", "2000", "2002", "3 Series", "3 Series GT", "315", "316", "318", "320", "321", "323", "324", "325", "326", "328", "330", "335", "340", "4 Series", "4 Series Gran Coupe", "420", "428", "430", "435", "5 Series", "5 Series GT", "518", "520", "523", "524", "525", "528", "530", "535", "540", "545", "550", "6 Series", "6 Series Gran Coupe", "6 Series GT", "628", "630", "633", "635", "640", "645", "650", "7 Series", "725", "728", "730", "732", "735", "740", "745", "750", "760", "8 Series", "Active Hybrid 7", "Alpina", "Dixi", "E", "F10", "I3", "I8", "Isetta", "M", "M1", "M2", "M3", "M4", "M5", "M6", "Neue Klasse", "R 1150GS", "RR", "X series", "X1", "X2", "X3", "X4", "X5", "X5 M", "X6", "X6 M", "X7", "X8", "Z1", "Z3", "Z4", "Z8" });
            CheckAndCreateCarMarkWithModels(context, "Borgward", new[] { "Hansa" });
            CheckAndCreateCarMarkWithModels(context, "Brilliance", new[] { "BS2", "BS4", "BS6", "FRV", "FSV", "Jinbei", "M 1", "M 2", "M3" });
            CheckAndCreateCarMarkWithModels(context, "Bristol", new[] { "412", "603", "Beaufighter", "Blenheim", "Brigand", "Britannia", "Fighter", "Speedster" });
            CheckAndCreateCarMarkWithModels(context, "Bugatti", new[] { "Chiron", "EB 110", "EB 112", "Galibier", "Veyron" });
            CheckAndCreateCarMarkWithModels(context, "Buick", new[] { "Cascada", "Century", "Eight", "Electra", "Enclave", "Enclave USA", "Encore", "Envision", "GL 8", "LaCrosse", "LaCrosse USA", "LE Sabre", "Limitet", "LuCerne", "Park Avenue", "Rainer", "Reatta", "Regal", "Regal GS", "Rendezvous", "Riviera", "Roadmaster", "Skyhawk", "Skylark", "Special", "Super", "Verano", "Wildcat" });
            CheckAndCreateCarMarkWithModels(context, "BYD", new[] { "E6", "F0", "F3", "F3R", "F6", "F8", "Flyer", "G3", "G6", "M6", "New F3", "S3", "S6" });
            CheckAndCreateCarMarkWithModels(context, "Cadillac", new[] { "Allante", "ATS", "BLS", "Brougham", "Catera", "Cimarron", "Convertible", "CT6", "CTS", "CTS-V Coupe", "DE Ville", "DTS", "Eldorado", "ELR", "Escalade", "Eureka", "Evoq", "Fleetwood", "LSE", "Seville", "SRX", "STS", "Vizon", "XLR", "XT5", "XTS" });
            CheckAndCreateCarMarkWithModels(context, "Caterham", new[] { "21", "7", "Classic" });
            CheckAndCreateCarMarkWithModels(context, "Chana", new[] { "Benni", "SC" });
            CheckAndCreateCarMarkWithModels(context, "Changan", new[] { "Benni", "Ideal", "Jiexun", "SC" });
            CheckAndCreateCarMarkWithModels(context, "Changhe", new[] { "CH1012L", "Freedom", "Ideal", "Ideal II" });
            CheckAndCreateCarMarkWithModels(context, "Chery", new[] { "A13", "A15", "Amulet", "Arrizo 3", "Arrizo 7", "B11", "Beat", "CrossEastar", "E 5", "Eastar", "Elara", "Flagcloud", "Fora", "Jaggi", "Karry", "Kimo", "M", "M11", "Oriental Son", "QQ", "Riich", "SQR", "Tiggo", "Tiggo 3", "Tiggo 5" });
            CheckAndCreateCarMarkWithModels(context, "Chevrolet", new[] { "Alero", "Astra", "Astro груз.", "Astro пасс.", "Avalanche", "Aveo", "Beauville", "Bel Air", "Beretta", "Blazer", "Bolt EV", "C/K", "Camaro", "Camaro Convertible", "Caprice", "Captiva", "Cavalier", "Celebrity", "Celta", "Chance", "Chery", "Chevelle", "Chevy", "Citation", "Classic", "Cobalt", "Colorado", "Convertible", "Corsa", "Corsica", "Corvette", "Cruze", "DeLuxe", "El Camino", "Epica", "Equinox", "Evanda", "Explorer", "Express груз.", "Express пасс.", "Geo Metro", "Geo Storm", "HHR", "Impala", "Kalos", "Lacetti", "Lanos", "Lumina", "Malibu", "Master", "Master De luxe", "Matiz", "Metro", "Monte Carlo", "Monza", "Niva", "Nubira", "Omega", "Opala", "Orlando", "Prizm", "R3500", "Rezzo", "S-10", "Silverado", "Sonic", "Sonoma", "Spark", "Spectrum", "SS", "SSR", "Suburban", "Tacuma", "Tahoe", "Tracker", "TrailBlazer", "Trans Sport", "Traverse", "Trax", "Uplander", "Van G-20", "Vandura", "Ventura", "Volt", "Тахо" });
            CheckAndCreateCarMarkWithModels(context, "Chrysler", new[] { "160", "180", "200", "300", "300 C", "300 M", "300 S", "Aspen", "Avenger", "Cirrus", "Concorde", "Crossfire", "Daytona Shelby", "Dynasty", "ES", "Grand Voyager", "HHR", "Imperial", "Intrepid", "Jeep Cherokee", "Laser", "LE Baron", "LHS", "Neon", "New Yorker", "Pacifica", "Phantom", "Prowler", "PT Cruiser", "Reliant", "Royal", "Saratoga", "Sebring", "Simca", "Stratus", "Sunbeam", "Tolbot", "Town & Country", "Viper", "Vision", "Voyager" });
            CheckAndCreateCarMarkWithModels(context, "Citroen", new[] { "2CV", "Acadiane", "AMI", "Athena", "AX", "Axel", "Berlingo груз.", "Berlingo пасс.", "BX", "C-Crosser", "C-Elysee", "C-Zero", "C1", "C15", "C2", "C3", "C3 Picasso", "C4", "C4 Aircross", "C4 Cactus", "C4 Picasso", "C5", "C6", "C8", "CQ", "CX", "DS3", "DS4", "DS5", "Dyane", "Evasion", "Grand C4 Picasso", "GS", "GSA", "ID", "Jumper груз.", "Jumper пасс.", "Jumpy груз.", "Jumpy пасс.", "LNA", "Nemo груз.", "Nemo пасс.", "Oltcit", "Reflex", "Rosalie", "Saxo", "Space Tourer", "Synergie", "Traction Avant", "Visa", "Xantia", "XM", "Xsara", "Xsara Picasso", "ZX" });
            CheckAndCreateCarMarkWithModels(context, "Dacia", new[] { "1300", "1304", "1310", "1325", "1410", "Clima", "Denem", "Dokker", "Duster", "Express", "Lodgy", "Logan", "Nova", "Rapsodie", "Sandero", "Sandero StepWay", "Solenza", "SuperNova" });
            CheckAndCreateCarMarkWithModels(context, "Dadi", new[] { "Aurora", "BDD", "Bliss", "City Leading", "Groz", "Huabey", "Shuttle", "Smoothing", "Soyat", "Suv" });
            CheckAndCreateCarMarkWithModels(context, "Daewoo", new[] { "Arcadia", "Brougham", "BV", "Charman", "Cielo", "Damas", "Espero", "Evanda", "Gentra", "Kalos", "Korando", "Lacetti", "Lanos", "LE Mans", "Leganza", "Lublin груз.", "Magnus", "Matiz", "Musso", "Nexia", "Nubira", "Nubira Sx", "Polonez", "Prince", "Racer", "Royale", "Sens", "Super Salon", "Tacuma", "Tico", "Tosca" });
            CheckAndCreateCarMarkWithModels(context, "Daf", new[] { "200", "46", "600" });
            CheckAndCreateCarMarkWithModels(context, "DAF / VDL", new[] { "DB250", "ldv-400 convoy", "TE47" });
            CheckAndCreateCarMarkWithModels(context, "Dagger", new[] { "GT" });
            CheckAndCreateCarMarkWithModels(context, "Daihatsu", new[] { "Altis", "Applause", "Atrai/extol", "Charade", "Charmant", "Copen", "Cuore", "Delta", "Domino", "Feroza", "Fourtrak", "Gran Move", "Hijet", "IRV", "Leeza", "Materia", "MAX", "Mira", "Move", "Rocky", "Sirion", "Sportrak", "Taft", "Terios", "Tianjin", "Trevis", "YRV" });
            CheckAndCreateCarMarkWithModels(context, "Daimler", new[] { "Coupe", "Daimler", "Double Six", "Landaulette", "Limousine", "Series III", "Sovereign", "XJ Series", "XJ12", "XJ6" });
            CheckAndCreateCarMarkWithModels(context, "Datsun", new[] { "100", "on-DO" });
            CheckAndCreateCarMarkWithModels(context, "De Lorean", new[] { "DMC" });
            CheckAndCreateCarMarkWithModels(context, "Derways", new[] { "Aurora", "Cowboy" });
            CheckAndCreateCarMarkWithModels(context, "Detroit Electric", new[] { "SP:01" });
            CheckAndCreateCarMarkWithModels(context, "DKW", new[] { "F5", "F7", "F8" });
            CheckAndCreateCarMarkWithModels(context, "Dodge", new[] { "Aries", "Arrow", "Aspen", "Avenger", "Caliber", "Caravan", "Challenger", "Charger", "Charger Daytona", "Colt", "D2", "D6", "Dakota", "Dart", "Daytona", "Diplomat", "Durango", "Dynasty", "Grand Caravan", "Intrepid", "Journey", "Magnum", "Monaco", "Neon", "Nitro", "Omni", "Polara", "Power Wagon", "RAM", "Ram Van", "Ramcharger", "Shadow", "Spirit", "Sprinter пасс.", "Stealth", "Stratus", "SXT", "Viper", "WC", "М 886" });
            CheckAndCreateCarMarkWithModels(context, "Dongfeng", new[] { "EQ1021", "EQ6380", "H30" });
            CheckAndCreateCarMarkWithModels(context, "Dr. Motor", new[] { "DR5" });
            CheckAndCreateCarMarkWithModels(context, "DS", new[] { "3", "4" });
            CheckAndCreateCarMarkWithModels(context, "Eagle", new[] { "Premier", "Summit", "Talon", "Vision" });
            CheckAndCreateCarMarkWithModels(context, "Ernst Auwarter", new[] { "Eurostar SHD" });
            CheckAndCreateCarMarkWithModels(context, "Estrima", new[] { "Biro" });
            CheckAndCreateCarMarkWithModels(context, "FAW", new[] { "6350", "6371 груз.", "6371 пасс.", "Besturn", "CA 6371 Cargo", "HQ3", "Oley", "V2", "V5", "Vita (C1)" });
            CheckAndCreateCarMarkWithModels(context, "Ferrari", new[] { "208/308", "250 GTO", "328", "348", "360", "400", "412", "456", "456M", "458", "458 Italia", "488 GTB", "488 Spider", "512", "550", "575M", "599", "599 GTO", "612 Scaglietti", "Barchetta", "California", "Dino", "Enzo", "F12", "F355", "F40", "F430", "F50", "F512", "FF", "Fiorano", "LaFerrari", "Maranello California", "Maranello California USA", "Modena Spider", "Mondial", "Scuderia Spider 16M Convertible", "Testarossa" });
            CheckAndCreateCarMarkWithModels(context, "Fiat", new[] { "1100B", "124", "125", "126", "127", "128", "130", "131", "132", "133", "138", "1500", "2300", "238", "242", "500", "500 C", "500 L", "500 X", "508", "600", "850", "900", "Abarth", "Albea", "Argenta", "Barchetta", "Brava", "Bravo", "Campagnola", "Cinquecento", "Cordoba", "Coupe", "Croma", "Doblo Panorama", "Doblo груз.", "Doblo пасс.", "Ducato", "Ducato груз.", "Ducato пасс.", "Duna", "Elba", "Fiorino груз.", "Fiorino пасс.", "Freemont", "FSO Polonez", "Fullback", "Grande Punto", "Ibiza", "Idea", "Leon", "Linea", "Lusso Familiare", "Marea", "Mirafiori", "Multipla", "Palio", "Panda", "Punto", "Punto Evo", "Qubo груз.", "Qubo пасс.", "Regata", "Ritmo", "Scudo", "Scudo груз.", "Scudo пасс.", "Sedici", "Seicento", "Siena", "Simca", "Stilo", "Strada", "Talento груз.", "Talento пасс.", "Tempra", "Tipo", "Topolino", "Torino", "Ulysse", "Uno", "X1/9", "Yugo" });
            CheckAndCreateCarMarkWithModels(context, "Fiat-Abarth", new[] { "1000 Berlina", "500", "595", "695", "700 spider", "750", "850 TC" });
            CheckAndCreateCarMarkWithModels(context, "Fisker", new[] { "Karma" });
            CheckAndCreateCarMarkWithModels(context, "Ford", new[] { "3430", "Aerostar", "Antara", "Aspire", "Auborn", "B-Max", "Bronco", "C-Max", "C-Max Energi", "Cabster", "Canyon", "Capri", "Cobra", "Consul", "Contour", "Cortina", "Cougar", "Courier", "Crown Victoria", "Diamant", "E-series", "Eafil", "Econoline", "Econovan", "EcoSport", "Edge", "Eifel", "Escape", "Escort", "Escort Express", "Escort van", "Excursion", "Expedition", "Explorer", "F-150", "F-250", "F-350", "F-450", "F-550", "F-650", "F-Series", "Fairlane", "Fairmont", "Falcon", "Festiva", "Fiesta", "Fireline", "Five Hundred", "Flex", "Focus", "Focus Electric", "Freestar", "Freestyle", "Fusion", "Galaxie", "Galaxy", "Gran Torino", "Granada", "Grand C-MAX", "GT", "KA", "Kuga", "Laser", "LTD", "Maverick", "Mercury", "Model A", "Model T", "Mondeo", "Mustang", "Mustang GT", "Mustang Shelby", "Orion", "Otosan", "Probe", "Puma", "Ranch Wagon", "Ranger", "Raptor", "S-Max", "Scorpio", "Sierra", "Sport KA", "Street KA", "Streetka", "Taunus", "Taurus", "Telstar", "Tempo", "Think", "Thunderbird", "Torino", "Tourneo Connect груз.", "Tourneo Connect пасс.", "Tourneo Courier", "Tourneo Custom", "Transit", "Transit Chassis", "Transit Connect груз.", "Transit Connect", "Transit Connect пасс.", "Transit Courier", "Transit Custom", "Transit Van", "Transit груз.", "Transit пасс.", "V8", "Willis", "Windstar", "Т" });
            CheckAndCreateCarMarkWithModels(context, "Fornasari", new[] { "RR" });
            CheckAndCreateCarMarkWithModels(context, "FSO", new[] { "125P", "126P", "127P", "1300", "132P", "Caro", "Polonez", "Syrena", "Warszawa" });
            CheckAndCreateCarMarkWithModels(context, "FUQI", new[] { "FQ" });
            CheckAndCreateCarMarkWithModels(context, "Gac", new[] { "Gonow" });
            CheckAndCreateCarMarkWithModels(context, "Geely", new[] { "BO", "CK", "CK-2", "CK1", "Emgrand 7 (EC7)", "Emgrand 8", "Emgrand X7", "Emgrand X9", "FC", "FS", "GC2", "GC5", "GC6", "GC7", "GХ2", "HA", "HS", "JL", "LC", "Maple", "MK", "MK Cross", "MK-2", "MR", "Panda", "Safe", "SC", "SL", "SMA", "UL", "Vision" });
            CheckAndCreateCarMarkWithModels(context, "Geo", new[] { "Metro", "Prizm", "Storm", "Tracker" });
            CheckAndCreateCarMarkWithModels(context, "GMC", new[] { "100", "Acadia", "Acadia USA", "C", "Canyon", "Delorean", "Envoy", "Jimmy", "Safari", "Savana", "Sierra", "Sonoma", "Suburban", "T6500", "Terrain", "Vandura груз.", "Vandura пасс.", "Yukon" });
            CheckAndCreateCarMarkWithModels(context, "Gonow", new[] { "DianGo", "GX6", "Jetstar", "Troy Suv", "Victor Suv" });
            CheckAndCreateCarMarkWithModels(context, "Great Wall", new[] { "C30", "CC", "Cowry", "Deer", "Florid", "H6", "Haval", "Haval H3", "Haval H5", "Haval H6", "Haval M2", "Haval M4", "Hover", "Hover F&L", "Pegasus", "Safe", "Sing", "SoCool", "SUV", "Tianma", "Voleex", "Voleex C10", "Wingle", "М4" });
            CheckAndCreateCarMarkWithModels(context, "Groz", new[] { "BAW", "BAW 1044", "BAW 1065", "Bliss", "Dacota", "Fox", "Polarsun Business Van", "Polarsun Cargo Van", "Rocky", "Shuttle", "Target", "Vertus" });
            CheckAndCreateCarMarkWithModels(context, "Hafei", new[] { "Lobo", "Minyi", "Princip", "Ruiyi", "Saibao", "Saima", "Sigma", "Zhongyi" });
            CheckAndCreateCarMarkWithModels(context, "Haima", new[] { "3" });
            CheckAndCreateCarMarkWithModels(context, "Hanomag", new[] { "Rekord", "Sturm" });
            CheckAndCreateCarMarkWithModels(context, "Hansa", new[] { "1100", "1700", "Test Hansa" });
            CheckAndCreateCarMarkWithModels(context, "Hindustan", new[] { "Ambassador" });
            CheckAndCreateCarMarkWithModels(context, "Honda", new[] { "Accord", "Accord Tourer", "Acty", "Aerodeck", "Avancier", "Ballade", "Beat", "Capa", "City", "Civic", "Concerto", "CR-V", "CR-Z", "Crosstour", "CRX", "Domani", "Element", "Elysion", "Eve", "F-mx", "FIT", "Fit Aria", "FR-V", "HR-V", "Insight", "Inspire", "Integra", "Jazz", "Lagreat", "Legend", "Life", "Logo", "Mobilio", "NSX", "Odyssey", "Orthia", "Partner", "Passport", "Pilot", "Prelude", "Quintet", "Rafaga", "Ridgeline", "S2000", "Saber", "Shuttle", "Sm-X", "Stepwgn", "Stream", "That S", "Torneo", "Vamos", "Vigor", "VLX" });
            CheckAndCreateCarMarkWithModels(context, "Huabei", new[] { "HC", "HG", "Poni" });
            CheckAndCreateCarMarkWithModels(context, "Huanghai", new[] { "Antelope", "Aurora", "DD", "Landscape", "Major", "Plutus", "XG" });
            CheckAndCreateCarMarkWithModels(context, "Humber", new[] { "Hawk", "Sceptre" });
            CheckAndCreateCarMarkWithModels(context, "Hummer", new[] { "H1", "H2", "H3", "H3X", "H4", "Hummer" });
            CheckAndCreateCarMarkWithModels(context, "Humvee", new[] { "C-Series", "Marshal" });
            CheckAndCreateCarMarkWithModels(context, "Hyundai", new[] { "Accent", "Amica", "Atos", "Avante", "Azera", "Centennial", "County", "Coupe", "Creta", "Dynasty", "Elantra", "Equus", "Excel", "Galloper", "Genesis", "Genesis Coupe", "Getz", "GLS", "Grand Santa Fe", "Grand Starex", "Grandeur", "GX", "H 100 груз.", "H 100 пасс.", "H 150 пасс.", "H 200 груз.", "H 200 пасс.", "H 300 груз.", "H 300 пасс.", "H1 груз.", "H1 пасс.", "HLF", "i10", "i20", "i30", "i40", "Ioniq", "IX20", "IX35", "ix55 (Veracruz)", "Kona", "Lantra", "Marcia", "Matrix", "Pony", "Prest", "S-Coupe", "Santa FE", "Santamo", "Solaris", "Sonata", "Starex", "Stellar", "Terracan", "Tiburon", "Trajet", "Tucson", "Veloster", "XG" });
            CheckAndCreateCarMarkWithModels(context, "Infiniti", new[] { "EX", "EX 25", "EX 30", "EX 35", "EX 37", "FX", "FX 30", "FX 35", "FX 37", "FX 45", "FX 50", "G", "G25", "G35", "G37", "I", "J", "JX", "M", "M25", "M30", "M35", "M37", "M45", "Q", "Q30", "Q45", "Q50", "Q60", "Q70", "QX", "QX30", "QX4", "QX50", "QX56", "QX60", "QX70", "QX80" });
            CheckAndCreateCarMarkWithModels(context, "Innocenti", new[] { "Elba" });
            CheckAndCreateCarMarkWithModels(context, "Iran Khodro", new[] { "Runna", "Samand", "Soren" });
            CheckAndCreateCarMarkWithModels(context, "Isuzu", new[] { "Amigo", "Ascender", "Aska", "Axiom", "Bighorn", "Campo", "D-Max", "Fargo", "Faster", "Florian", "FRR", "Gemini", "Hombre", "Impulse", "MD", "Midi груз.", "Midi пасс.", "Piazza", "Pick Up", "Rodeo", "Stylus", "TFR", "Trooper", "VehiCross" });
            CheckAndCreateCarMarkWithModels(context, "ItalCar", new[] { "Attiva" });
            CheckAndCreateCarMarkWithModels(context, "Iveco", new[] { "Daily 4x4", "Daily пасс.", "Massif", "Menarini", "Unic" });
            CheckAndCreateCarMarkWithModels(context, "JAC", new[] { "J2", "J3", "J5", "J6", "Refine", "Rein", "S2", "S3", "S5" });
            CheckAndCreateCarMarkWithModels(context, "Jaguar", new[] { "4000", "Daimler", "DS", "E-Type", "F-Pace", "F-Type", "Mark", "S-Type", "SL", "Sovereign", "Vanden", "X-Type", "XE", "XF", "XFR", "XJ", "XJ6", "XJ8", "XJL", "XJR", "XJR-S", "XJS", "XK", "XKR" });
            CheckAndCreateCarMarkWithModels(context, "JCB", new[] { "Workmax" });
            CheckAndCreateCarMarkWithModels(context, "Jeep", new[] { "Cherokee", "CJ", "Comanche", "Commander", "Compass", "Grand Cherokee", "Liberty", "Patriot", "Renegade", "Willys", "Wrangler" });
            CheckAndCreateCarMarkWithModels(context, "Jiangnan", new[] { "Alto", "Chuanqi", "City Spirit", "JNJ7081", "JNJ7111", "JNJ7150" });
            CheckAndCreateCarMarkWithModels(context, "Jinbei", new[] { "Haise" });
            CheckAndCreateCarMarkWithModels(context, "Jinbei Minibusus", new[] { "SY6482Q2" });
            CheckAndCreateCarMarkWithModels(context, "JMC", new[] { "Baodian", "BD", "YunBa" });
            CheckAndCreateCarMarkWithModels(context, "Jonway", new[] { "Ufo" });
            CheckAndCreateCarMarkWithModels(context, "Karosa", new[] { "HD" });
            CheckAndCreateCarMarkWithModels(context, "Kia", new[] { "Avella", "Besta", "Borrego", "Cadenza", "Capital", "Carens", "Carnival", "Carstar", "Ceed", "Ceed Sportswagon", "Ceed SW", "Cerato", "Cerato Koup", "Ceres", "Clarus", "Concord", "Credos", "Enterprise", "Forte", "Grand Sportage", "Joice", "Jumbo Titan", "Kosmos", "Koup", "Magentis", "Mentor", "Mentor II", "Mohave", "Niro", "Opirus", "Optima", "Picanto", "Potentia", "Pregio груз.", "Pregio пасс.", "Pride", "Pro Ceed", "Quoris", "Retona", "Rio", "Rio Hatchback 3D", "Rio Hatchback 5D", "Roadster", "Sedona", "Sephia", "Sephia II", "Shuma", "Sorento", "Soul", "Spectra", "Sportage", "Stinger", "Stonic", "Towner", "Venga", "Visto" });
            CheckAndCreateCarMarkWithModels(context, "King Long", new[] { "Kingte" });
            CheckAndCreateCarMarkWithModels(context, "KingWoo", new[] { "KW 500", "KW 625", "KW 625W", "KYG5S", "KYGDG08A", "KYGDG11A", "XD-BB" });
            CheckAndCreateCarMarkWithModels(context, "Kirkham", new[] { "427 KMS" });
            CheckAndCreateCarMarkWithModels(context, "Koenigsegg", new[] { "Agera", "CCX", "CCXR Trevita" });
            CheckAndCreateCarMarkWithModels(context, "Konecranes", new[] { "Steyr 55" });
            CheckAndCreateCarMarkWithModels(context, "Lamborghini", new[] { "400 GT", "Aventador", "Countach", "Diablo", "Espada", "Gallardo", "Gallardo LP 550-2", "Huracan", "Jalpa", "Jarama", "Lm-001", "Lm-002", "Murcielago", "Reventon", "Urraco", "Urus" });
            CheckAndCreateCarMarkWithModels(context, "Lancia", new[] { "A 112", "Beta", "Dedra", "Delta", "Fulvia", "Gamma", "Kappa", "Lybra", "Monte Carlo", "Musa", "Phedra", "Prisma", "Thema", "Thesis", "Trevi", "Y", "Y10", "Ypsilon", "Zeta" });
            CheckAndCreateCarMarkWithModels(context, "Land Rover", new[] { "Defender", "Discovery", "Discovery Sport", "Freelander", "Range Rover", "Range Rover Evoque", "Range Rover Sport", "Range Rover Velar" });
            CheckAndCreateCarMarkWithModels(context, "Landwind", new[] { "CV9", "SUV", "X6", "X9" });
            CheckAndCreateCarMarkWithModels(context, "LDV", new[] { "Maxus", "Pilot" });
            CheckAndCreateCarMarkWithModels(context, "Lexus", new[] { "CT", "CT 200H", "ES", "ES 200", "ES 250", "ES 300", "ES 330", "ES 350", "GS", "GS 200", "GS 250", "GS 300", "GS 350", "GS 400", "GS 430", "GS 450", "GS 460", "GS F", "GX", "HS", "IS", "IS 200", "IS 220", "IS 250", "IS 300", "IS 350", "IS-C", "IS-F", "LC", "LF", "LS", "LS 400", "LS 430", "LS 460", "LS 600", "LX", "LX 450", "LX 470", "LX 570", "NX", "NX 200", "NX 300", "RC", "RH", "RX", "RX 200", "RX 270", "RX 300", "RX 330", "RX 350", "RX 400", "RX 450", "SC", "SC 300", "SC 400", "SC 430" });
            CheckAndCreateCarMarkWithModels(context, "Lifan", new[] { "320", "520", "530", "620", "Breez", "Solano", "X50", "X60" });
            CheckAndCreateCarMarkWithModels(context, "Lincoln", new[] { "Aviator", "Blackwood", "Cartier", "Continental", "Excalibur", "LS", "Mark", "Mercury", "MKC", "MKS", "MKT", "MKX", "MKZ", "Navigator", "Town Car", "Zephyr" });
            CheckAndCreateCarMarkWithModels(context, "Lotus", new[] { "Eclat", "Elan", "Elise", "Elite", "Esprit", "Europa", "Evora", "Excel", "Exige", "Seven", "Super Seven" });
            CheckAndCreateCarMarkWithModels(context, "LTI", new[] { "TX" });
            CheckAndCreateCarMarkWithModels(context, "Luxgen", new[] { "5", "7" });
            CheckAndCreateCarMarkWithModels(context, "Marshell", new[] { "DG-C2", "DN" });
            CheckAndCreateCarMarkWithModels(context, "Maruti", new[] { "1000", "800", "Alto", "Baleno", "Esteem", "Gypsy", "Omni", "Suzuki", "Versa", "Wagon R", "Zen" });
            CheckAndCreateCarMarkWithModels(context, "Maserati", new[] { "222", "228", "3200", "420/430", "Barchetta Stradale", "Biturbo", "Bora", "Chubasco", "Coupe", "Ghibli", "GranCabrio", "GranSport", "GranTurismo", "Indy", "Karif", "Khamsin", "Kyalami", "Levante", "Mc12", "Merak", "Mexico", "Quattroporte", "Royale", "Shamal", "Spyder" });
            CheckAndCreateCarMarkWithModels(context, "Maybach", new[] { "52", "57", "62", "DS8 Zeppelin", "Exelero", "landaulet", "S500", "S600" });
            CheckAndCreateCarMarkWithModels(context, "Mazda", new[] { "121", "2", "3", "3 MPS", "323", "5", "6", "6 MPS", "626", "929", "Atenza", "Az-offroad", "Az-wagon", "B-series", "Bongo", "BT-50", "Business", "Capella", "Carol", "Clef", "Cosmo", "Cronos", "CX-3", "CX-5", "CX-7", "CX-9", "Demio", "E-series груз.", "E-series пасс.", "Eunos", "Eunos 500", "Eunos Cargo", "Eunos Cosmo", "Eunos Presso", "Familia", "Lantis", "Luce", "Millenia", "MPV", "MS-6", "MS-8", "MS-9", "MX-3", "MX-5", "MX-6", "Persona", "Premacy", "Proceed", "Protege", "RX-7", "RX-8", "Sentia", "Speed", "Tribute", "Xedos", "Xedos 6", "Xedos 9" });
            CheckAndCreateCarMarkWithModels(context, "McLaren", new[] { "570 GT", "650S", "675LT", "F1", "MP4", "P1" });
            CheckAndCreateCarMarkWithModels(context, "MEGA", new[] { "Multitruck" });
            CheckAndCreateCarMarkWithModels(context, "Mercedes-Benz", new[] { "10/20 HP Posen", "1628", "170", "190", "200", "206 пасс.", "208 пасс.", "210", "210 пасс.", "220", "230", "240", "250", "260", "280", "290", "300", "308 груз.", "310 пасс.", "320", "400", "420", "450", "500", "560", "600", "A 140", "A 150", "A 160", "A 170", "A 180", "A 190", "A 200", "A 210", "A 220", "A 250", "A-Class", "A45 AMG", "AMG", "B 150", "B 160", "B 170", "B 180", "B 200", "B 220", "B-Class", "B-Class Electric Drive", "C-Class", "Citan", "CL 160", "CL 180", "CL 200", "CL 220", "CL 230", "CL 320", "CL 420", "CL 500", "CL 55 AMG", "CL 550", "CL 600", "CL 63 AMG", "CL 65 AMG", "CL-Class", "CLA 180", "CLA 200", "CLA 220", "CLA 250", "CLA 45 AMG", "CLA-Class", "CLC 160", "CLC 180", "CLC 200", "CLC 220", "CLC 230", "CLC 250", "CLC 350", "CLC-Class", "CLK 200", "CLK 220", "CLK 230", "CLK 240", "CLK 270", "CLK 280", "CLK 320", "CLK 350", "CLK 430", "CLK 500", "CLK 55 AMG", "CLK 63 AMG", "CLK-Class", "CLS 250", "CLS 280", "CLS 300", "CLS 320", "CLS 350", "CLS 400", "CLS 500", "CLS 55 AMG", "CLS 550", "CLS 63 AMG", "CLS-Class", "E 500", "E-Class", "E-Class All-Terrain", "Electric Drive", "G 230", "G 240", "G 250", "G 270", "G 280", "G 290", "G 300", "G 320", "G 350", "G 400", "G 500", "G 55 AMG", "G 63 AMG", "G 65 AMG", "G-Class", "GL 320", "GL 350", "GL 400", "GL 420", "GL 450", "GL 500", "GL 55 AMG", "GL 550", "GL 63 AMG", "GL-Class", "GLA-Class", "GLC-Class", "GLE-Class", "GLK 200", "GLK 220", "GLK 250", "GLK 280", "GLK 300", "GLK 320", "GLK 350", "GLK-Class", "GLS 350", "GLS 400", "GLS 450", "GLS 500", "GLS 63", "GLS-Class", "M-Class", "Maybach", "MB груз.", "MB пасс.", "ML 230", "ML 250", "ML 270", "ML 280", "ML 300", "ML 320", "ML 350", "ML 400", "ML 420", "ML 430", "ML 500", "ML 55 AMG", "ML 550", "ML 63 AMG", "ML-Class", "N 1000", "N 1300", "R 280", "R 300", "R 320", "R 350", "R 500", "R 63 AMG", "R-Class", "S 140", "S 220", "S 250", "S 260", "S 280", "S 300", "S 320", "S 350", "S 400", "S 420", "S 430", "S 450", "S 500", "S 55", "S 550", "S 560", "S 600", "S 63 AMG", "S 65 AMG", "S 67", "S-Class", "S-Guard", "SE", "SL 280", "SL 300", "SL 320", "SL 350", "SL 380", "SL 400", "SL 420", "SL 450", "SL 500 (550)", "SL 55 AMG", "SL 560", "SL 60 AMG", "SL 600", "SL 63 AMG", "SL 65 AMG", "SL 70 AMG", "SL 73 AMG", "SL-Class", "SLC-Class", "SLK 200", "SLK 230", "SLK 250", "SLK 280", "SLK 300", "SLK 32 AMG", "SLK 320", "SLK 350", "SLK 55 AMG", "SLK-Class", "SLR-Class", "SLS-Class", "Smart", "Sprinter 208 груз.", "Sprinter 208 пасс.", "Sprinter 209 груз.", "Sprinter 209 пасс.", "Sprinter 210 груз.", "Sprinter 210 пасс.", "Sprinter 211 груз.", "Sprinter 211 пасс.", "Sprinter 212 груз.", "Sprinter 212 пасс.", "Sprinter 213 груз.", "Sprinter 213 пасс.", "Sprinter 216 груз.", "Sprinter 216 пасс.", "Sprinter 219 груз.", "Sprinter 219 пасс.", "Sprinter 308 груз.", "Sprinter 308 пасс.", "Sprinter 309 пасс.", "Sprinter 310 груз.", "Sprinter 310 пасс.", "Sprinter 311 груз.", "Sprinter 311 пасс.", "Sprinter 312 груз.", "Sprinter 312 пасс.", "Sprinter 313 груз.", "Sprinter 313 пасс.", "Sprinter 315 груз.", "Sprinter 315 пасс.", "Sprinter 316 груз.", "Sprinter 316 пасс.", "Sprinter 318 груз.", "Sprinter 318 пасс.", "Sprinter 319 груз.", "Sprinter 319 пасс.", "Sprinter 324 груз.", "Sprinter 324 пасс.", "Sprinter 410 груз.", "Sprinter 410 пасс.", "Sprinter 412 груз.", "Sprinter 412 пасс.", "Sprinter 413 пасс.", "Sprinter 415 пасс.", "Sprinter 513 пасс.", "Sprinter 515 груз.", "Sprinter 515 пасс.", "Sprinter 516 груз.", "Sprinter 516 пасс.", "Sprinter 518 груз.", "Sprinter 518 пасс.", "Sprinter груз.", "Sprinter пасс.", "V 200", "V 220", "V 230", "V 250", "V 280", "V-Class", "Vaneo", "Viano", "Viano груз.", "Viano пасс.", "Vito", "Vito груз.", "Vito пасс.", "X-Class" });
            CheckAndCreateCarMarkWithModels(context, "Mercury", new[] { "50ELPTO", "90ELPTO", "Black Max", "Cougar", "Grand Marquis", "Marauder", "Mariner", "Montego", "Monterey", "Mountaineer", "Mystique", "Sable", "Topaz", "Tracer", "Villager", "Zephyr" });
            CheckAndCreateCarMarkWithModels(context, "Merkur", new[] { "XR4Ti" });
            CheckAndCreateCarMarkWithModels(context, "MG", new[] { "3", "350", "5", "550", "6", "6 5D", "750", "F", "Maestro", "Montego", "TF", "ZR", "ZS", "ZT" });
            CheckAndCreateCarMarkWithModels(context, "Miles", new[] { "OR70", "ZX40" });
            CheckAndCreateCarMarkWithModels(context, "MINI", new[] { "Cabrio", "Clubman", "Cooper", "Cooper D", "Cooper S", "Countryman", "Coupe", "Hatch", "Mini", "One", "Paceman", "Roadster", "Rover" });
            CheckAndCreateCarMarkWithModels(context, "Mitsubishi", new[] { "3000 GT", "Airtrek", "Aspire", "ASX", "Attrage", "Axia ES", "Carisma", "Celeste", "Challenger", "Chariot", "Colt", "Cordia", "Debonair", "Delica", "Diamante", "Dingo", "Dion", "Eclipse", "Eclipse USA", "EK Wagon", "Emeraude", "Endeavor", "Eterna", "FTO", "Galant", "Galloper", "Grandis", "GTO", "i-MiEV", "Jeep", "L 200", "L 300 груз.", "L 300 пасс.", "L 400 груз.", "L 400 пасс.", "Lancer", "Lancer Evolution", "Lancer X", "Lancer X Ralliart", "Lancer X Sportback", "Lanser Sportback", "Legnum", "Libero", "Magna", "Minica", "Minicab", "Mirage", "Montero", "Nativa", "Outlander", "Outlander PHEV", "Outlander XL", "Pajero", "Pajero Pinin", "Pajero Sport", "Pajero Wagon", "Pistachio", "Prestij", "Proton", "Proudia", "Raider", "Ralli Art", "RVR", "Santamo", "Sapporo", "Shogun", "Shogun Pinin", "Shogun Sport", "Sigma", "Space Gear", "Space Runner", "Space Star", "Space Wagon", "Starion", "Toppo", "Town Box", "Tredia", "Virage" });
            CheckAndCreateCarMarkWithModels(context, "Mitsuoka", new[] { "Dore", "Orochi" });
            CheckAndCreateCarMarkWithModels(context, "Morgan", new[] { "Aero 8", "Aero Supersports", "Four Four", "Plus 4", "Plus 8", "Runabout" });
            CheckAndCreateCarMarkWithModels(context, "Morris", new[] { "Ital", "Marina", "Minor" });
            CheckAndCreateCarMarkWithModels(context, "MPM Motors", new[] { "PS 160" });
            CheckAndCreateCarMarkWithModels(context, "MYBRO", new[] { "Umi-1", "А01", "В01", "К7" });
            CheckAndCreateCarMarkWithModels(context, "Nissan", new[] { "100NS", "100NX", "120Y Sunny", "140J Violet", "140Y Sunny", "160B Bluebird", "160J Violet", "180B Bluebird", "200", "200 SX", "240K Skyline", "280C", "280ZX", "300", "300ZX", "350Z", "370Z", "70", "Almera", "Almera Classic", "Almera Tino", "Altima", "Armada", "Arna", "Auster", "Avenir", "Bassara", "Bluebird", "Caravan", "Cedric", "Cefiro", "Cherry", "Cima", "Cube", "Datsun", "Datsun MI-DO", "Datsun on-DO", "e-NV200", "Elgrand", "Frontier", "Fuga", "Gazelle", "Gloria", "GT-R", "Homy", "Interstar", "Juke", "Juke Nismo", "King Cab", "Kubistar", "L35", "Langley", "Largo", "Laurel", "Leaf", "Leopard", "Liberta Villa", "Liberty", "Maxima", "Maxima QX", "Micra", "Murano", "Navara", "Note", "NP300", "NV", "Paladin", "Pathfinder", "Patrol", "Patrol GR", "Pickup", "Pixo", "Prairie", "Presage", "Presea", "Primastar груз.", "Primastar пасс.", "Primera", "Pulsar", "Qashqai", "Qashqai+2", "Quest", "QX", "R'nessa", "Rogue", "Safari", "Sentra", "Serena груз.", "Serena пасс.", "Silvia", "Sima", "Skyline", "Stagea", "Stanza", "Sunny", "Teana", "Terrano", "Terrano II", "TIIDA", "Tino", "Titan", "Trade", "Urvan", "Vanette груз.", "Vanette пасс.", "Vehiculos", "Versa", "Wingroad", "X-Terra", "X-Trail" });
            CheckAndCreateCarMarkWithModels(context, "Norster", new[] { "600R" });
            CheckAndCreateCarMarkWithModels(context, "Nysa (Ныса)", new[] { "521", "522", "552" });
            CheckAndCreateCarMarkWithModels(context, "Oldsmobile", new[] { "98", "Achieva", "Alero", "Aurora", "Bravada", "Cutlass", "Cutlass Ciera", "Delta", "Eighty-Eight", "Holiday", "Intrigue", "Ninety Eidht", "Omega", "Regency", "Royal", "Silhouette", "Super 88", "Tornado", "Urbee" });
            CheckAndCreateCarMarkWithModels(context, "Oltcit", new[] { "Club" });
            CheckAndCreateCarMarkWithModels(context, "Opel", new[] { "Adam", "Admiral", "Agila", "Ampera", "Antara", "Arena груз.", "Arena пасс.", "Ascona", "Astra", "Astra F", "Astra G", "Astra H", "Astra H OPC", "Astra J", "Astra K", "Calibra", "Campo", "Capitan", "Cascada", "Chevette", "Combo груз.", "Combo пасс.", "Commodore", "Corsa", "Corsa OPC", "Crossland X", "Diamant", "Diplomat", "Frontera", "Grandland X", "GT", "Insignia", "Kadett", "Kapitan", "Komador", "Manta", "Meriva", "Mokka", "Monterey", "Monza", "Movano груз.", "Movano пасс.", "Olimpia", "Omega", "Orion", "P4", "Ranger", "Rekord", "Saturn", "Senator", "Signum", "Sintra", "Speedster", "Super 6", "Tigra", "Trabant", "Vectra", "Vectra A", "Vectra B", "Vectra C", "Vivaro груз.", "Vivaro пасс.", "Zafira" });
            CheckAndCreateCarMarkWithModels(context, "Packard", new[] { "Hawk", "One Twenty", "Super Eight" });
            CheckAndCreateCarMarkWithModels(context, "Pagani", new[] { "Huayra", "Zonda" });
            CheckAndCreateCarMarkWithModels(context, "Peg-Perego", new[] { "Gaucho", "Ranger" });
            CheckAndCreateCarMarkWithModels(context, "Peugeot", new[] { "1007", "104", "106", "107", "107 Hatchback (3d)", "107 Hatchback (5d)", "108", "117", "2008", "203", "204", "205", "206", "206 Hatchback (3d)", "206 Hatchback (5d)", "206 Sedan", "206 SW", "206 СС", "207", "207 CC", "207 Hatchback (3d)", "207 Hatchback (5d)", "208", "208 GTI", "208 Hatchback (5d)", "3008", "301", "304", "305", "306", "306 Sedan", "307", "307 CC", "308", "308 CC", "308 Hatchback (3d)", "308 Hatchback (5d)", "308 Sportium", "308 SW", "309", "4007", "4008", "403", "404", "405", "406", "407", "407 Coupe", "407 Sedan", "407 SW", "408", "5008", "504", "505", "508", "508 RXH", "604", "605", "607", "806", "807", "BB1", "Bipper груз.", "Bipper пасс.", "Boxer", "Boxer груз.", "Boxer пасс.", "Expert груз.", "Expert пасс.", "G 5", "iOn", "J5 груз.", "J5 пасс.", "Karsan", "P4", "Pars", "Partner груз.", "Partner пасс.", "Ranch", "RCZ", "Scenic", "Traveller" });
            CheckAndCreateCarMarkWithModels(context, "Pininfarina", new[] { "Cambiano" });
            CheckAndCreateCarMarkWithModels(context, "Pinzgauer", new[] { "710" });
            CheckAndCreateCarMarkWithModels(context, "Plymouth", new[] { "Acclaim", "Barracuda", "Breeze", "Caravelle", "Colt", "Coltvista", "Conquest", "Fury", "Gran Fury", "Grand Voyager", "Horizon", "Laser", "Neon", "P7", "Prowler", "Reliant", "Sapporo", "Satellite", "Scamp", "Sundance", "Turismo", "valare", "Voyager" });
            CheckAndCreateCarMarkWithModels(context, "Pontiac", new[] { "6000", "Aztec", "Beaumont", "Bonneville", "Catalina", "Fiero", "Firebird", "G5", "G6", "G8", "Grand AM", "Grand Prix", "GTO", "Laurentian", "Lemans", "Montana", "Parisienne", "Phoenix", "Solstice", "Strato Chief", "Sunbird", "Sunburst", "Sunfire", "Tempest", "Trans Sport", "Vibe" });
            CheckAndCreateCarMarkWithModels(context, "Porsche", new[] { "356", "550", "911", "918 Spyder", "924", "928", "944", "959", "964", "968", "996", "997", "Boxster", "Cayenne", "Cayman", "Macan", "Panamera" });
            CheckAndCreateCarMarkWithModels(context, "Praga Baby", new[] { "Baby", "Tudor" });
            CheckAndCreateCarMarkWithModels(context, "Proton", new[] { "415", "Compact", "Coupe", "Impian", "Iswara", "Juara", "Perdana", "Persona", "Proton", "Saga", "Saloon", "Satria", "Waja", "Wira" });
            CheckAndCreateCarMarkWithModels(context, "Quicksilver", new[] { "540", "Adventure" });
            CheckAndCreateCarMarkWithModels(context, "Ram", new[] { "1500", "2500", "3500", "Chassis Cab", "Promaster", "Promaster City" });
            CheckAndCreateCarMarkWithModels(context, "Ravon", new[] { "Gentra", "Matiz", "Nexia", "R2", "R4" });
            CheckAndCreateCarMarkWithModels(context, "Renault", new[] { "11", "12", "14", "15", "16", "17", "18", "19", "20", "21", "25", "30", "4", "5", "6", "8", "9", "A610", "Alliance", "Avantime", "Captur", "Chamade", "Clio", "Dokker VAN", "Dokker пасс.", "Duster", "Espace", "Estafette", "Express", "Florida", "Fluence", "Fluence Z.E", "Fuego", "Grand Espace", "Grand Modus", "Grand Scenic", "GTA", "Kadjar", "Kangoo груз.", "Kangoo пасс.", "Koleos", "Laguna", "Latitude", "Lodgy", "Logan", "Master груз.", "Master пасс.", "Megane", "Megane RS", "Modus", "Nevada", "Rapid", "Rodeo", "Safrane", "Samsung SM5", "Sandero", "Sandero StepWay", "Scenic", "Scenic Conquest", "Sport Spider", "Supernova", "Symbol", "Talisman", "Thalia", "Trafic груз.", "Trafic пасс.", "Twingo", "Twizy", "Vel Satis", "Wind", "Zoe" });
            CheckAndCreateCarMarkWithModels(context, "Renault Samsung Motors", new[] { "QM5", "SM3", "SM5", "SM7" });
            CheckAndCreateCarMarkWithModels(context, "Rezvani", new[] { "Beast" });
            CheckAndCreateCarMarkWithModels(context, "Rimac", new[] { "Concept One" });
            CheckAndCreateCarMarkWithModels(context, "Rolls-Royce", new[] { "Carmargue", "Corniche", "Dawn", "Drophead", "Flying Spur", "Ghost", "Limousine", "Park Ward", "Phantom", "Phantom V", "Phantom VI", "Phantom VII", "Silver Cloud", "Silver Dawn", "Silver Seraph", "Silver Shadow", "Silver Spirit", "Silver Spur", "Silver Wraith", "Wraith" });
            CheckAndCreateCarMarkWithModels(context, "Rover", new[] { "100", "114", "200", "2000", "213", "214", "216", "218", "220", "2300", "2400", "25", "2600", "3500", "400", "414", "416", "418", "420", "45", "600", "618", "620", "75", "75 Tourer", "800", "820", "825", "827", "Cabriolet", "Coupe", "Freelander", "Land Rover", "Maestro", "Metro", "MGF", "Mini MK", "Montego", "Range Rover", "SD1", "Streetwise", "Targa", "Tourer", "Vitesse" });
            CheckAndCreateCarMarkWithModels(context, "Saab", new[] { "9-2", "9-3", "9-3 X", "9-5", "9-7X", "90", "900", "9000", "96", "99", "Aero", "Griffin" });
            CheckAndCreateCarMarkWithModels(context, "Saipa", new[] { "Tiba" });
            CheckAndCreateCarMarkWithModels(context, "Saleen", new[] { "S281", "S331", "S7" });
            CheckAndCreateCarMarkWithModels(context, "Samand", new[] { "EL", "LX", "Runna", "SE", "Soren", "SPG", "TAXI" });
            CheckAndCreateCarMarkWithModels(context, "Samson", new[] { "F" });
            CheckAndCreateCarMarkWithModels(context, "Samsung", new[] { "SM3", "SM5", "SM7", "SQ5" });
            CheckAndCreateCarMarkWithModels(context, "Saturn", new[] { "Astra", "Aura", "Ion", "LS", "LW", "Outlook", "Palace Resort", "Red line", "Relay", "SC", "Sky", "SL", "SW", "Twin cam", "Vue" });
            CheckAndCreateCarMarkWithModels(context, "Sceo", new[] { "C3", "Shuanghuan" });
            CheckAndCreateCarMarkWithModels(context, "Scion", new[] { "FR-S", "tC", "xA", "xB", "xD" });
            CheckAndCreateCarMarkWithModels(context, "Seat", new[] { "124", "127", "131", "132L", "133", "Alhambra", "Altea", "Altea XL", "Arona", "Arosa", "Ateca", "Cordoba", "Cupra", "Exeo", "Freetrack", "Fura", "Ibiza", "Inca", "Leon", "Leon X-Perience", "Malaga", "Marbella", "Mii", "Ronda", "Terra", "Toledo" });
            CheckAndCreateCarMarkWithModels(context, "Secma", new[] { "Extrem 500", "F16", "Fun Family" });
            CheckAndCreateCarMarkWithModels(context, "Selena", new[] { "EC" });
            CheckAndCreateCarMarkWithModels(context, "Shelby", new[] { "Cobra", "Cobra Mk III" });
            CheckAndCreateCarMarkWithModels(context, "Shuanghuan", new[] { "SCEO" });
            CheckAndCreateCarMarkWithModels(context, "Sidetracker", new[] { "418" });
            CheckAndCreateCarMarkWithModels(context, "Skoda", new[] { "100", "1000 MB", "105", "110", "120", "1201", "1202", "130", "440", "Ambiente", "Citigo", "Estelle", "Fabia", "Fabia Combi", "Favorit", "Felicia", "Forman", "Karoq", "Kodiaq", "Liaz", "Octavia", "Octavia A5", "Octavia A5 Combi", "Octavia A7", "Octavia A7 Combi", "Octavia Combi", "Octavia Combi NEW", "Octavia Elegance", "Octavia NEW", "Octavia RS", "Octavia Scout", "Octavia Tour", "Octavia Tour Combi", "Pickup", "Popular", "Praktik", "Rapid", "Roomster", "S 100", "S 110", "Scout", "Spaceback", "Superb", "SuperB New", "SuperВ Combi", "Taz", "Yeti" });
            CheckAndCreateCarMarkWithModels(context, "SMA", new[] { "Maple C31", "Maple C32", "Maple C51", "Maple C52", "Maple C81", "R" });
            CheckAndCreateCarMarkWithModels(context, "Smart", new[] { "Cabrio", "City", "Crossblade", "Forfour", "Fortwo", "Fortwo ED", "Kitas", "MCC", "Pulse", "Roadster", "Roadster Coupe" });
            CheckAndCreateCarMarkWithModels(context, "SouEast", new[] { "Delica", "Lioncel", "V3" });
            CheckAndCreateCarMarkWithModels(context, "Soyat", new[] { "Unique Van", "Yuejin" });
            CheckAndCreateCarMarkWithModels(context, "Spyker", new[] { "C8 Laviolette", "C8 Spyder", "D8" });
            CheckAndCreateCarMarkWithModels(context, "SsangYong", new[] { "Actyon", "Actyon Sports", "Chairman", "Family", "Istana", "Kallista", "Korando", "Kyron", "KYRON DELUXE", "Musso", "Rexton", "Rexton II", "Rexton W", "Rodius", "SCEO", "Tivoli", "XLV" });
            CheckAndCreateCarMarkWithModels(context, "Star", new[] { "12.155", "1466", "8.125", "944" });
            CheckAndCreateCarMarkWithModels(context, "Studebaker", new[] { "Diktator", "Lark", "Starlight" });
            CheckAndCreateCarMarkWithModels(context, "Subaru", new[] { "1600", "1800", "Alcyone", "Baja", "Bistro", "BRZ", "Crosstrek", "Domingo", "Forester", "Impreza", "Impreza WRX STI", "Impreza Hatchback", "Impreza Sedan", "Impreza WRX Sedan", "Impreza XV", "Justy", "Legacy", "Legacy NEW", "Legacy Outback", "Legacy Wagon", "Leone", "Levorg", "Libero", "Mini Jumbo", "Outback", "Pleo", "Rex", "Sambar", "SVX", "Traviq", "Trezia", "Tribeca", "Vivio", "WRX", "WRX STI Hatchback", "WRX STI Sedan", "XT", "XV" });
            CheckAndCreateCarMarkWithModels(context, "Sunbeam", new[] { "Alpine", "Stiletto" });
            CheckAndCreateCarMarkWithModels(context, "Suzuki", new[] { "Aerio", "Alto", "Baleno", "Cappuccino", "Carry", "Celerio", "Cervo", "Cultus", "Dingo", "Esteem", "Every Landy", "Forenza", "Fronte", "Geo Tracker", "Grand Vitara", "Ignis", "Ignis II", "Jimny", "KEI", "Kizashi", "Liana", "LJ 80", "MR Wagon", "Reno", "SA310 Swift", "Samurai", "SC100", "Sidekick", "Splash", "Super Carry Bus", "Swift", "SX4", "UE", "Vitara", "Wagon R", "X90", "XL7" });
            CheckAndCreateCarMarkWithModels(context, "Talbot", new[] { "1510", "Alpine", "Avenger", "Horizon", "Matra", "Samba", "Simca", "Solara", "Sunbeam", "Tagora" });
            CheckAndCreateCarMarkWithModels(context, "Tarpan Honker", new[] { "237", "PW" });
            CheckAndCreateCarMarkWithModels(context, "TATA", new[] { "Gurkha", "Indica", "Indigo", "Nano", "Safari", "Telcoline", "Xenon" });
            CheckAndCreateCarMarkWithModels(context, "Tatra", new[] { "107", "603", "613", "87" });
            CheckAndCreateCarMarkWithModels(context, "Tazzari", new[] { "ZERO" });
            CheckAndCreateCarMarkWithModels(context, "Tesla", new[] { "Model 3", "Model S", "Model X", "Roadster" });
            CheckAndCreateCarMarkWithModels(context, "Think Global", new[] { "City" });
            CheckAndCreateCarMarkWithModels(context, "Thunder Tiger", new[] { "Gonow" });
            CheckAndCreateCarMarkWithModels(context, "Tianma", new[] { "Century", "Dragon", "Fengchi", "Fengling" });
            CheckAndCreateCarMarkWithModels(context, "Tiger", new[] { "R6" });
            CheckAndCreateCarMarkWithModels(context, "Tofas", new[] { "Dogan", "Sahin" });
            CheckAndCreateCarMarkWithModels(context, "Toyota", new[] { "1000 (Publica)", "4Runner", "7FBMF16", "8FBMT16", "Allex", "Allion", "Alphard", "Altezza", "Aristo", "Aurion", "Auris", "Avalon", "Avanza", "Avensis", "Avensis Verso", "Aygo", "Brevis", "C-HR", "Caldina", "Cami", "Camry", "Carib", "Carina", "Carina E", "Cavalier", "Celica", "Celsior", "Century", "Chaser", "Corolla", "Corolla Levin", "Corolla Verso", "Corona", "Corsa", "Cressida", "Cresta", "Crown", "Curren", "Cynos", "Duet", "Echo", "Estima", "F", "FJ Cruiser", "Fortuner", "Funcargo", "Gaia", "GT 86", "Harrier", "Hiace груз.", "Hiace пасс.", "Highlander", "Hilux", "Hino", "Inova", "Ipsum", "IQ", "Isis", "Land Cruiser (все)", "Land Cruiser 100", "Land Cruiser 105", "Land Cruiser 200", "Land Cruiser 40", "Land Cruiser 60", "Land Cruiser 70", "Land Cruiser 71", "Land Cruiser 73", "Land Cruiser 76", "Land Cruiser 78", "Land Cruiser 79", "Land Cruiser 80", "Land Cruiser 90", "Land Cruiser Prado", "Lite Ace", "Mark", "Mark II", "Master", "Matrix", "Mirai", "MR2", "Nadia", "Opa", "Paseo", "Picnic", "Previa", "Prius", "Prius C", "Proace", "Progres", "Raum", "Rav 4", "Regular Cab", "Scion", "Sequoia", "Sera", "Sienna", "Soarer", "Solara", "Space Cruiser", "Sprinter", "Sprinter Trueno", "Starlet", "Supra", "Tacoma", "Tercel", "Town Ace", "Tundra", "Urban Cruiser", "Venza", "Verossa", "Verso", "Verso-S", "Vista", "Voxy", "Will Vs", "Windom", "XA", "Yaris", "Yaris Verso", "Zelas" });
            CheckAndCreateCarMarkWithModels(context, "Trabant", new[] { "1.1", "601", "P 601" });
            CheckAndCreateCarMarkWithModels(context, "Triumph", new[] { "1500", "Acclaim", "Daytona", "Dolomite", "Spitfire", "Stag", "Thruxton", "Toledo", "TR7" });
            CheckAndCreateCarMarkWithModels(context, "TVR", new[] { "2500M", "280i", "3000", "3000M", "350i", "390", "400", "420", "450", "Cerbera", "Chimaera", "Griffith", "S", "S2", "S3", "S4c", "Speed Eight", "T350c", "T400R", "T440R", "Taimar", "Tamora", "Tasmin", "Tuscan", "Tuscan R", "V8" });
            CheckAndCreateCarMarkWithModels(context, "Ultima", new[] { "GTR" });
            CheckAndCreateCarMarkWithModels(context, "Van Hool", new[] { "3B1057", "A300", "A303", "A360", "AG300T", "Oplegger" });
            CheckAndCreateCarMarkWithModels(context, "Vauxhall", new[] { "25 D type", "Agila", "Astra", "Astra Belmont", "Belmont", "Calibra", "Carlton", "Cavalier", "Chevette", "Corsa", "Frontera", "Lotus Carlton", "Meriva", "Monterey", "Movano", "Nova", "Omega", "Royale", "Senator", "Signum", "Sintra", "Tigra", "Vectra", "Ventora", "Viceroy", "Viva", "Vivaro", "VX 2300", "VX220", "Zafira" });
            CheckAndCreateCarMarkWithModels(context, "Venturi", new[] { "Atlantique" });
            CheckAndCreateCarMarkWithModels(context, "Vepr", new[] { "Commander" });
            CheckAndCreateCarMarkWithModels(context, "Volkswagen", new[] { "Amarok", "Arteon", "Beetle", "Bora", "Caddy", "Caddy груз.", "Caddy пасс.", "Caravelle", "Corrado", "Crafter груз.", "Crafter пасс.", "Cross", "Cross Golf", "Cross Polo", "Cross Touran", "Derby", "e-Golf", "Eos", "Fontana", "Fox", "Garbus", "Golf", "Golf GTI", "Golf I", "Golf II", "Golf III", "Golf IV", "Golf Plus", "Golf R", "Golf Sportsvan", "Golf SportWagen", "Golf V", "Golf Variant", "Golf VI", "Golf VII", "Jetta", "K70", "Kafer", "LT", "LT груз.", "LT пасс.", "Lupo", "Multivan", "New Beetle", "Passat", "Passat Alltrack", "Passat B1", "Passat B2", "Passat B3", "Passat B4", "Passat B5", "Passat B6", "Passat B7", "Passat B8", "Passat CC", "Phaeton", "Pointer", "Polo", "Rabbit", "Routan", "Santana", "Scirocco", "Sharan", "Syncro", "T1 (Transporter)", "T2 (Transporter)", "T3 (Transporter)", "T4", "T4 (Transporter) пасс.", "T4 (Transporter) груз", "T5", "T5 (Transporter) пасс.", "T5 (Transporter) груз", "T6", "T6 (Transporter) груз", "T6 (Transporter) пасс.", "Taro", "Tiguan", "Touareg", "Touran", "Up", "Vento", "Westfalia" });
            CheckAndCreateCarMarkWithModels(context, "Volvo", new[] { "140", "142", "144", "145", "164", "1800", "240", "242", "244", "245", "260", "264", "265", "340", "343", "344", "345", "360", "440", "460", "480", "610", "66", "670", "740", "744", "760", "780", "850", "940", "960", "C30", "C70", "FL 220", "FS", "FS 10", "GX", "S40", "S60", "S70", "S80", "S90", "V40", "V50", "V60", "V70", "V90", "VHD", "XC40", "XC60", "XC70", "XC90" });
            CheckAndCreateCarMarkWithModels(context, "Wanderer", new[] { "W21", "W23", "W24", "W245", "W30", "W40" });
            CheckAndCreateCarMarkWithModels(context, "Wanfeng", new[] { "SHK" });
            CheckAndCreateCarMarkWithModels(context, "Wartburg", new[] { "1.3", "1300", "311", "312", "313", "353", "Tourist" });
            CheckAndCreateCarMarkWithModels(context, "Wiesmann", new[] { "MF3", "MF4", "MF5" });
            CheckAndCreateCarMarkWithModels(context, "Willys", new[] { "Aero", "M38", "MA", "MB" });
            CheckAndCreateCarMarkWithModels(context, "Wuling", new[] { "LZW", "Sunshine", "Xingwang" });
            CheckAndCreateCarMarkWithModels(context, "Xiaolong", new[] { "XLW" });
            CheckAndCreateCarMarkWithModels(context, "Xin kai", new[] { "6490", "HXK", "Pickup X3", "SR-V X3", "SUV X3", "X3 2021" });
            CheckAndCreateCarMarkWithModels(context, "Yugo", new[] { "311", "45", "511", "513", "55", "65", "Florida", "Sana", "Tempo", "ZLC", "ZLM", "ZLX", "ZLXE" });
            CheckAndCreateCarMarkWithModels(context, "Zastava", new[] { "1100", "128", "750", "Yugo", "Yugo Florida" });
            CheckAndCreateCarMarkWithModels(context, "Zimmer", new[] { "Golden Spirit" });
            CheckAndCreateCarMarkWithModels(context, "Zotye", new[] { "Nomad", "T600", "Z100", "Z300" });
            CheckAndCreateCarMarkWithModels(context, "Zuk", new[] { "A-06" });
            CheckAndCreateCarMarkWithModels(context, "ZX", new[] { "Admiral", "GL", "Grand Tiger", "GS", "LandMark" });
            CheckAndCreateCarMarkWithModels(context, "Богдан", new[] { "2110", "2111", "2310", "23101", "2312" });
            CheckAndCreateCarMarkWithModels(context, "Бронто", new[] { "Рысь" });
            CheckAndCreateCarMarkWithModels(context, "ВАЗ", new[] { "1111 Ока", "1117", "1118", "1119", "2101", "2102", "2103", "2104", "2105", "2106", "2107", "2108", "21081", "2109", "2109 (Балтика)", "21093", "21099", "2110", "21106", "2111", "2112", "21123", "2113", "2114", "2115", "2116", "2119", "2120 Надежда", "2121", "2123", "2129", "2131", "2170", "2171", "2172", "2173", "2190 Гранта", "2191 Гранта", "2192", "2194", "2302", "2329", "Lada XRAY", "Vesta", "Жигули", "Калина", "Калина Кросс", "Лада 110", "Ларгус", "Нива", "Приора", "Самара", "Шевроле-Нива" });
            CheckAndCreateCarMarkWithModels(context, "ВИС", new[] { "1706", "2345", "2346", "23461", "23464", "2347" });
            CheckAndCreateCarMarkWithModels(context, "ГАЗ", new[] { "12", "13", "14", "1506", "20", "21", "22", "2217 Соболь", "23", "2308 (Атаман)", "23213", "23312", "24", "2401", "2402", "2403", "2404", "2410", "2411", "2412", "2413", "2417", "2476", "2705 Газель", "27057", "2747", "2752 Соболь", "31010", "3102", "31022", "310221", "31029", "3104", "3105", "3106", "3109", "3110", "31104", "31105", "3111", "3115", "3202 Газель", "3212", "3221 Газель", "32212", "32213", "3234", "32705", "3274", "3302 Газель", "33021 Газель", "33023 Газель", "3310 Валдай", "33104", "3321", "5204", "5205", "64", "67", "69", "M21", "Maxus", "Siber", "ВС-18", "М 1", "М 20", "М 21", "М 22", "М 72", "РУТА", "САЗ 3507", "Соболь", "Тигр" });
            CheckAndCreateCarMarkWithModels(context, "ГолАЗ", new[] { "3207" });
            CheckAndCreateCarMarkWithModels(context, "ЕРАЗ", new[] { "762 пасс.", "ЕРАЗ" });
            CheckAndCreateCarMarkWithModels(context, "Жук", new[] { "A11", "А06", "А07", "Жук" });
            CheckAndCreateCarMarkWithModels(context, "ЗАЗ", new[] { "1102 Таврия", "1103 Славута", "1104", "1105 Дана", "11055", "110557", "1106", "1107", "1109", "1122 Таврия", "1140", "965", "966", "968", "969", "A075", "Chance", "Forza", "Lanos", "Sens", "Vida", "Таврия-Нова" });
            CheckAndCreateCarMarkWithModels(context, "ЗИЛ", new[] { "4104" });
            CheckAndCreateCarMarkWithModels(context, "ЗИМ", new[] { "12" });
            CheckAndCreateCarMarkWithModels(context, "ЗИС", new[] { "101", "110", "125", "5" });
            CheckAndCreateCarMarkWithModels(context, "ИЖ", new[] { "2117", "2125", "2126", "2715", "2716", "2717", "412" });
            CheckAndCreateCarMarkWithModels(context, "ЛуАЗ", new[] { "1301", "1302", "1901", "402", "696", "699Р", "966", "967", "968", "969 Волынь", "969М" });
            CheckAndCreateCarMarkWithModels(context, "Москвич / АЗЛК", new[] { "21215 Иж Комби", "2137", "2138", "2140", "2141", "2142", "2335", "2716", "2901", "400", "401", "402", "403", "407", "408", "410", "411", "412", "420", "423", "424", "426", "427", "430", "433", "434", "Дуэт" });
            CheckAndCreateCarMarkWithModels(context, "РАФ", new[] { "2203" });
            CheckAndCreateCarMarkWithModels(context, "Самодельный", new[] { "Самодельный авто" });
            CheckAndCreateCarMarkWithModels(context, "СеАЗ", new[] { "1111", "СЗД" });
            CheckAndCreateCarMarkWithModels(context, "СМЗ", new[] { "С-3А", "С-3Д", "С1Л" });
            CheckAndCreateCarMarkWithModels(context, "СПЭВ / SPEV", new[] { "Буян" });
            CheckAndCreateCarMarkWithModels(context, "ТагАЗ", new[] { "Aquila", "Vortex Tingo" });
            CheckAndCreateCarMarkWithModels(context, "ТАМ", new[] { "190", "222", "80" });
            CheckAndCreateCarMarkWithModels(context, "ТогАЗ", new[] { "Road Partner", "Tiger", "Vega" });
            CheckAndCreateCarMarkWithModels(context, "Тренер", new[] { "Admiral bq" });
            CheckAndCreateCarMarkWithModels(context, "УАЗ", new[] { "1400", "2107", "2206", "2206 груз.", "2206 пасс.", "2315", "3151", "31512", "3151201", "31513", "31514", "31519", "315195", "3152", "3153", "3159", "3160/3162", "3162", "3163", "3301", "3303", "331512", "33741", "36221", "3741", "3902", "3909", "39094", "39095", "39099", "3962", "39621", "39625", "450", "451", "451Д", "452 Д", "452 пасс.", "452П", "469", "469Б", "Cargo", "Hunter", "Pickup", "военный", "ГАЗ 69", "ЛЭК 45277", "Патриот", "скорая помощь-пассажир." });
            CheckAndCreateCarMarkWithModels(context, "Циклон", new[] { "DS4" });
            CheckAndCreateCarMarkWithModels(context, "Эстония", new[] { "19", "21" });

            #endregion

            #region WorkTags

            CheckAndCreateWorkTag(context, "Топливные (бензиновые) работы");
            CheckAndCreateWorkTag(context, "Топливные (дизельные) работы");
            CheckAndCreateWorkTag(context, "Кузовные работы");
            CheckAndCreateWorkTag(context, "Малярные работы");
            CheckAndCreateWorkTag(context, "Ремонт подвески");
            CheckAndCreateWorkTag(context, "Ремонт двигателя");
            CheckAndCreateWorkTag(context, "Стекольные работы");
            CheckAndCreateWorkTag(context, "Ремонт АКПП");
            CheckAndCreateWorkTag(context, "Ремонт МКПП");
            CheckAndCreateWorkTag(context, "Шиномонтажные работы");
            CheckAndCreateWorkTag(context, "Ремонт тормозной системы");
            CheckAndCreateWorkTag(context, "Сварочные работы");
            CheckAndCreateWorkTag(context, "Работа с электроникой");
            CheckAndCreateWorkTag(context, "Ремонт дисков");
            CheckAndCreateWorkTag(context, "Ремонт климатических установок"); 
            CheckAndCreateWorkTag(context, "Ремонт глушителей"); 
            CheckAndCreateWorkTag(context, "Салонные работы"); 
            CheckAndCreateWorkTag(context, "Диагностические работы"); 
            CheckAndCreateWorkTag(context, "Тюннинг");

            #endregion

            #region Cities

            CheckAndCreateCity(context, "Алупка");
            CheckAndCreateCity(context, "Алушта");
            CheckAndCreateCity(context, "Армянск");
            CheckAndCreateCity(context, "Белогорск");
            CheckAndCreateCity(context, "Джанкой");
            CheckAndCreateCity(context, "Евпатория");
            CheckAndCreateCity(context, "Керчь");
            CheckAndCreateCity(context, "Красноперекопск");
            CheckAndCreateCity(context, "Саки");
            CheckAndCreateCity(context, "Севастополь");
            CheckAndCreateCity(context, "Симферополь");
            CheckAndCreateCity(context, "Старый Крым");
            CheckAndCreateCity(context, "Судак");
            CheckAndCreateCity(context, "Феодосия");
            CheckAndCreateCity(context, "Щелкино");
            CheckAndCreateCity(context, "Ялта");

            #endregion

        }

        #region Private methods

        /// <summary>
        /// Создает роль по умолчанию
        /// </summary>
        private void CheckAndCreateRole(DatabaseContext context, string roleName)
        {
            if (context.Roles.Any(r => r.Name == roleName))
            {
                return;
            }
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);
            var role = new IdentityRole
            {
                Name = roleName
            };
            manager.Create(role);
        }

        /// <summary>
        /// Создает пользователя по умолчанию
        /// </summary>
        private void CheckAndCreateUser(DatabaseContext context, string userName, string password, string roleName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user == null)
            {
                var store = new AppUserStore(context);
                var manager = new UserManager<ApplicationUser>(store);
                user = new ApplicationUser
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = userName,
                    Email = userName,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    AccessFailedCount = 0
                };
                manager.Create(user, password);
                manager.AddToRole(user.Id, roleName);
            }
        }

        /// <summary>
        /// Создает профайл пользователя по умолчанию
        /// </summary>
        private void CheckAndCreateUserProfile(DatabaseContext context, string userName, string contactName, string phoneNumber)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null && user.UserProfile == null)
            {
                var profile = new UserProfile
                {
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Name = contactName,
                    Phone = phoneNumber,
                    State = UserState.Active,
                };
                user.UserProfile = profile;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Создать автосервис по умолчанию
        /// </summary>
        private void CheckAndCreateCarService(DatabaseContext context, string userName)
        {
            var user = context.Users.FirstOrDefault(u => u.UserName == userName);
            if (user != null && user.CarService == null)
            {
                var carService = new CarService
                {
                    Name = "Test",
                    Address = "Test",
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    Email = "test@test.com",
                    ManagerName = "Test",
                    About = "Test test test",
                    TimetableWorks = "Test",
                    Site = "http://test.com",
                    State = CarServiceState.Active
                };
                user.CarService = carService;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Создать марку автомобиля с моделями
        /// </summary>
        private void CheckAndCreateCarMarkWithModels(DatabaseContext context, string name, string[] models)
        {
            var mark = context.CarMarks.FirstOrDefault(u => u.Name == name);
            if (mark == null)
            {
                mark = new CarMark
                {
                    Name = name,
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsDeleted = false
                };
                var markModels = new List<CarModel>();
                foreach (var model in models) {
                    markModels.Add(new CarModel
                    {
                        Name = model,
                        Created = DateTime.UtcNow,
                        Updated = DateTime.UtcNow,
                        IsDeleted = false
                    });
                }
                mark.Models = markModels;
                context.CarMarks.Add(mark);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Создать вид работы
        /// </summary>
        private void CheckAndCreateWorkTag(DatabaseContext context, string name)
        {
            var work = context.WorkTags.FirstOrDefault(u => u.Name == name);
            if (work == null)
            {
                work = new WorkTag
                {
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsDeleted = false,
                    Name = name
                };
                context.WorkTags.Add(work);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Создать город
        /// </summary>
        private void CheckAndCreateCity(DatabaseContext context, string name)
        {
            var city = context.Cities.FirstOrDefault(x => x.Name == name);
            if (city == null)
            {
                city = new City
                {
                    Created = DateTime.UtcNow,
                    Updated = DateTime.UtcNow,
                    IsDeleted = false,
                    Name = name
                };
                context.Cities.Add(city);
                context.SaveChanges();
            }
        }

        #endregion Private methods
    }
}
