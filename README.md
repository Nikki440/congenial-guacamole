1. Inleiding
Deze documentatie beschrijft de architectuur, functionaliteiten en implementatie van de Virtuele Dierentuin, ontwikkeld als onderdeel van de Eindopdracht van C#. De applicatie is een ASP.NET Core MVC-webapplicatie met een bijbehorende API voor het beheren van dieren, categorieën en verblijven.
2. Pagina Functionaliteiten 
Animals
•	Overzicht van alle dieren met filters op categorie en verblijf
•	Zoeken en filteren op naam of soort
•	Filteren ongedaan maken
•	weergave van alle dieren
•	toevoegen, bewerken en verwijderen van dieren
•	autoassign van dieren aan een enclosure
•	Alle dieren verwijderen van enclosure
•	Sunset en Sunrise 
Enclosures 
•	weergave van alle Enclosures
•	zoeken en filteren
•	toevoegen, bewerken en verwijderen van enclosures
•	Animals in enclosure bekijken
Category 
•	weergave van alle Category’s
•	zoeken en filteren
•	toevoegen, bewerken en verwijderen van Category’s

Wireframe
 
![image](https://github.com/user-attachments/assets/6046438a-c9cf-4cf7-ac21-70ac908fd30b)


ERD
![image](https://github.com/user-attachments/assets/5d5f1977-8985-463c-83e7-aff66453480f)

 
Animals
Attributes:
•	Id (INT): Primary Key, auto-incremented.
•	Name (NVARCHAR(MAX)): Name of the animal.
•	Species (NVARCHAR(MAX)): Species of the animal.
•	CategoryId (INT, nullable): Foreign Key referencing Categories(Id).
•	Size (INT): Size of the animal.
•	DietaryClass (INT): Dietary classification of the animal.
•	ActivityPattern (INT): Activity pattern of the animal.
•	prey (BIT): Indicates whether the animal is a prey.
•	EnclosureId (INT, nullable): Foreign Key referencing Enclosures(Id).
•	SpaceRequirement (INT): Space required for the animal.
•	SecurityRequirement (INT): Security level required for the animal.
•	ZooId (INT, nullable): Foreign Key referencing Zoos(Id).
•	FeedingTime (DATETIME2(7), nullable): Feeding time for the animal.

Relationships:
Many-to-One with Categories: Each animal belongs to one category.
Many-to-One with Enclosures: Each animal is housed in one enclosure.
Many-to-One with Zoos: Each animal is assigned to one zoo.

Categories
Attributes:
•	Id (INT): Primary Key, auto-incremented.
•	Name (NVARCHAR(MAX)): Name of the category.
Relationships:
One-to-Many with Animals: Each category can have many animals.

Enclosures
Attributes:
•	Id (INT): Primary Key, auto-incremented.
•	Name (NVARCHAR(MAX)): Name of the enclosure.
•	Climate (INT): Climate condition of the enclosure.
•	HabitatType (INT): Type of habitat in the enclosure.
•	SecurityLevel (INT): Security level of the enclosure.
•	Size (FLOAT(53)): Size of the enclosure.
•	ZooId (INT, nullable): Foreign Key referencing Zoos(Id).

Relationships:
One-to-Many with Animals: Each enclosure can have multiple animals.
Many-to-One with Zoos: Each enclosure is located in one zoo.

Zoo
Attributes:
•	Id (INT): Primary Key, auto-incremented.
•	Name (NVARCHAR(MAX)): Name of the zoo.
Relationships:
One-to-Many with Enclosures: Each zoo can have many enclosures.
One-to-Many with Animals: Each zoo can have many animals (via the enclosure).

4. API Documentatie
De API biedt endpoints voor het beheren van dieren, categorieën en verblijven. Hieronder enkele belangrijke endpoints:
Animals API
•	GET /api/animals
Retourneert een lijst met alle dieren.
Parameter	Beschrijving
searchString	Zoekterm om te filteren op naam, soort, categorie of verblijf.
categoryId	Filtert de dieren op een specifieke categorie.
timeOfDay	Filter op activiteitspatroon (sunrise, sunset).

•	POST /api/animals
Voegt een nieuw dier toe.
•	PUT /api/animals/{id}
Wijzigt een bestaand dier.
Parameter	Beschrijving
id	ID van het dier dat gewijzigd moet worden.

•	DELETE /api/animals/{id}
Verwijdert een dier uit de database.
Parameter	Beschrijving
id	ID van het dier dat verwijderd moet worden.

Categorieën API:
•	GET /api/categories
Retourneert een lijst met alle categorieën.
Parameter	Beschrijving
categorySearch	Filtert op naam
enclosureId	Filtert dieren in enclosure


•	GET /api/categories/{id}
Retourneert specifieke informatie over een categorie op basis van ID.
•	POST /api/categories
Voegt een nieuwe categorie toe.
•	PUT /api/categories/{id}
Wijzigt de gegevens van een bestaande categorie.
Parameter	Beschrijving
Id	ID die gewijzigd moet worden

•	DELETE /api/categories/{id}
Verwijdert een categorie uit de database.
Parameter	Beschrijving
Id	ID die verwijderd moet worden
•	
Enclosure API
•	POST /api/enclosures/autoassign
Willekeurig toewijzen van dieren aan verblijven.
•	GET /Enclosure/Index
Haal alle enclosures op
Parameter	Beschrijvin
enclosureSearch	Zoeken op eigenschappen 

GET /Enclosure/Create
Geeft een formulier weer voor het aanmaken van een verblijf.
POST /Enclosure/Create
Voegt een nieuw verblijf toe aan de database.
Parameter	Beschrijving
Name	Naam van verblijf
Climate	Klimaat van verblijf
HabitatType	Habitatcategorie van verblijf
SecurityLevel	Beveiligingsniveau van verblijf
Size	Grootte van verblijf
GET /Enclosure/Edit/{id}
Geeft een formulier weer om een verblijf te bewerken.
POST /Enclosure/Edit/{id}
Bewerkt een verblijf en slaat de wijzigingen op in de database.
POST /Enclosure/Delete/{id}
Verwijder een verblijf
GET /Enclosure/AnimalsInEnclosure/{enclosureId}
Haal alle dieren op
Parameter	Beschrijving
enclosureId	ID van het verblijf waarvan de dieren worden opgehaald

5. Reflectie
Wat ging goed?
Onze samenwerking verliep zonder problemen. We hadden geen moeite met een taakverdeling en we hadden allebei de motivatie om hieraan te werken. We hielpen elkaar wanneer iemand vastliep en zo hebben wij deze dierentuin kunnen maken zonder problemen 
Uitdagingen en leerpunten
Het was uitdagend voor ons om te beginnen aan zo’n grote opdracht. We hadden vaak moeite met het snappen van de opdracht. Elke keer wanneer we weer keken naar de opdracht vonden we nieuwe functies die we eerder over het hoofd hebben gezien of de implementatie anders van hebben gedaan.
Verbeterpunten
 Ons enigste verbeterpunt is dat we eerder moeten beginnen. We hebben de hele opdracht in een best korte tijd gemaakt en het zou fijn zijn als we de volgende keer iets meer de tijd kunnen nemen.


------------------------------------------------------------------------------


To crate database, open Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console) and run: 
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```
To drop database, open Package Manager Console and run:
```
dotnet ef database drop --force
```
If this fails. make sure you're in the right folder with:
```
cd WebApplication1
```
and ensure "WebApplication1.csproj" is listed when running "ls"
