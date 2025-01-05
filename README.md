# HenriksHobbyLager

## Interfaces

### IProductRepository;

Interfacet IProductRepository fastställer standardoperationerna för att hantera produkter i datalagret. Det följer SOLID-principerna genom att vara välstrukturerat och fokuserat på en specifik uppgift. Detta möjliggör enkel utbytbarhet av implementationer och framtida utökningar av applikationen.

### IProduct;

Interfacet IProduct fungerar som en bas för att definiera egenskaper för produkter i applikationen. Det hjälper till att skapa en enhetlig struktur för klasser som hanterar produkter, vilket gör det enkelt att utöka systemet vid behov. Det följer SOLID-principerna och möjliggör lös koppling och återanvändbarhet.

### IProductServices;

Interfacet IProductService definierar de huvudsakliga metoderna för att hantera produkter i applikationen. Det fokuserar på affärslogik och tillhandahåller funktionalitet för att lägga till, ta bort, uppdatera och visa produkter.

Metoderna är utformade för att stödja enkel interaktion med produktdata och kan implementeras på olika sätt beroende på applikationens behov. Interfacet följer SOLID-principerna genom att möjliggöra flexibel koppling och utbytbarhet av implementationer.

## Program

### ProgramManager; 

HenriksHobbyLagerProgramManager är ansvarig för programmets huvudflöde. Den sköter interaktionen med användaren genom att visa en meny, läsa användarinmatning och utföra åtgärder baserade på användarens val.

Primära uppgifter:

- Visa menyalternativ via en MenuHandler.
- Validera användarens val och kalla på rätt funktion.
- Stänga av programmet när användaren väljer att avsluta.

Klassen följer principerna om separation av ansvar och modularitet, vilket gör den lätt att underhålla och utöka.

### Program; 

Denna klass fungerar som applikationens startpunkt. Den instansierar och anropar HenriksHobbyLagerProgramManager, som hanterar huvudlogiken för programmet. Designen separerar programmets startflöde från dess kärnlogik, vilket gör koden renare och enklare att underhålla.

- Skapa och anropa HenriksHobbyLagerProgramManager.
- Starta huvudloopen för applikationen.

Följande principer i SOLID: 

SRP och OCP.
Gör det enkelt att isolera och testa huvudlogiken i en separat klass.
Håller Mainmetoden kort och överskådlig.



## Struktur

| Katalog         | Fil(er)                                   |
|------------------|-------------------------------------------|
| Data            | HHL.sqlite                               |
| Database        | AppDbContext.cs                          |
| Database        | SqliteDatabaseInitializer.cs             |
| Interfaces      | IProduct.cs, IProductRepository.cs, IProductService.cs |
| Models          | Product.cs                               |
| ProgramManager  | LagerProgramManager.cs                   |
| Repositories    | ProductRepositories.cs                   |
| Services        | ConsoleHelper.cs, MenuHandler.cs, ProductServices.cs |


## Sekvensdiagram

<img width="473" alt="image" src="https://github.com/user-attachments/assets/ce27ba06-50f8-4699-9989-e0de75281650" />











