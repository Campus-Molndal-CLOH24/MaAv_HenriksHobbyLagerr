# HenriksHobbyLager

## Interfaces

### IProductFacade
Är ett gränssnitt som definierar vilka metoder som ska finnas i en implementerande klass (i detta fallet Products).

### IRepository
Är ett generellt gränssnitt som definierar basoperationer för CRUD (Create, Read, Update, Delete) mot databasen.

## Models

### Products
Denna klass fungerar som en modell för produkter i applikationen, och definierar vilka egenskaper varje produkt ska ha.

Vad man kan göra framöver är att implementera en ProductService som separerar databasoperationer från affärslogik. ProductService skulle fungera som en mall för vad varje produkt skall innehålla. 
Har man en IProductService så kan man även använda samma bas för andra tabeller, tex om man vill skapa en för tapeter.

#### Exempel IProductService; 

```
namespace HenriksHobbyLager.Interfaces {
    public interface IProductService {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void UpdateProduct(Product product);
        void DeleteProduct(int id);
        IEnumerable<Product> SearchProducts(Func<Product, bool> predicate);
    }
}
```
#### Exempel implementering av ProductService; 

```namespace HenriksHobbyLager.Services {
    public class ProductService : IProductService {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) {
            _repository = repository;
        }

        public IEnumerable<Product> GetAllProducts() => _repository.GetAll();

        public Product GetProductById(int id) => _repository.FindById(id);

        public void AddProduct(Product product) => _repository.Add(product);

        public void UpdateProduct(Product product) => _repository.Update(product);

        public void DeleteProduct(int id) => _repository.Delete(id);

        public IEnumerable<Product> SearchProducts(Func<Product, bool> predicate) => _repository.Search(predicate);
    }
}
```

Skulle tapeter innehålla ett unikt attribut som tex färg, så kan man lägga till det specifikt i tabellen för tapeter, utan att det påverkar andra.

## Repositories

### MongoDbRepository
Innehåller inställningar för att koppla upp sig till en MongoDb-databas.

### SqliteRepository
Innehåller inställningar för att koppla upp sig till en SQLite-databas.

#### Jämförelse mellan SQLite och MongoDB Repositories

Funktion              | SQLite                         | MongoDB  
----------------------|---------------------------------|--------------------------------------  
Databaskoppling       | Använder AppDbContext           | Använder MongoDbContext  
Hämta alla produkter  | Products.ToList()               | Products.Find(_ => true).ToList()  
Hämta produkt via ID  | Products.Find(id)               | Products.Find(p => p.Id == id).FirstOrDefault()  
Lägg till produkt     | Products.Add(entity), SaveChanges() | Products.InsertOne(entity)  
Uppdatera produkt     | Products.Update(entity), SaveChanges() | Products.ReplaceOne(p => p.Id == entity.Id, entity)  
Ta bort produkt       | Products.Remove(product), SaveChanges() | Products.DeleteOne(p => p.Id == id)  

## ZProgram

### Program 

Denna klass fungerar som applikationens startpunkt. Den instansierar och anropar ProgramManager, som hanterar huvudlogiken för programmet. Designen separerar programmets startflöde från dess kärnlogik, vilket gör koden renare och enklare att underhålla.

Skapa och anropa ProgramManager.
Starta huvudloopen för applikationen.
Följande principer i SOLID:
SRP och OCP. Gör det enkelt att isolera och testa huvudlogiken i en separat klass. Håller Main-metoden kort och överskådlig.

### ProgramManager

ProgramManager är ansvarig för programmets huvudflöde. Den sköter interaktionen med användaren genom att visa en meny, läsa användarinmatning och utföra åtgärder baserade på användarens val.

Primära uppgifter:
Klassen följer principerna om separation av ansvar och modularitet, vilket gör den lätt att underhålla och utöka.

Struktur:

```
HenriksHobbyLager/
├── Database/
│   ├── AppDbContext.cs
│   └── MongoDbContext.cs
├── Interfaces/
│   ├── IProductFacade.cs
│   └── IRepository.cs
├── Models/
│   └── Product.cs
├── Repositories/
│   ├── MongoDbRepository.cs
│   └── SqliteRepository.cs
└── ZProgram/
    ├── Program.cs
    └── ProgramManager.cs
```


Sekvensdiagram MÅSTE LÄGGAS TILL
