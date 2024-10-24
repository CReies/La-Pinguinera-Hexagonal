
# La-Pinguinera-Hexagonal

This is a quoting system for a library, is builded with Hexagonal Architecture, Domain driven design and Reactive Programming for the Use Cases and Controllers. It has an API to make use of it. Is developed on .NET with MongoDB to persist the information.

To persist the information it uses Event Sourcing, so basically it doesn't save information about the entities but about the events that modifies the aggregate.

The library has Books and Novels, each one has their own properties.

## Installation

You will need a Mongo database to use this project, once you have your database go to `/src/Infrastructure/appsetings.json`, there you must have this object to connect to your database

```json
{
  "Database": {
    "ConnectionString": "",
    "DatabaseName": "",
    "CollectionName": ""
  }
}
```
## Functions

The quoting system has different functionalities, to quote more than one book, it calculates a discount for seniority as a customer, and a **Wholesale Discount** if the quote has more than 10 books or a **Retail Increase** if the quote has less or equal to 10 books

For **Retail**, the increase is of 2% for each book.

For a **WholeSale** the price of the 10 first books isn't affected (doesn't apply Retail), and from the 10th the discount increases by 0,15%.

There are 4 main functionalities

- **Individual Quote**: It receives the data of the book, creates it in the inventory, and depending of wich type of book is (book or novel) applies a base increase to the price, + 1/3 of his base price for books and x2 for novels. The increased price will be the price for the customers.

- **List Quote**: It receives a list of books, calculates the total price of the list of books and applies the price modificators described above.

- **Budget Quote**: It recives any quantity of books and calculates how many books can you quote, the budget must have at least one book and one novel.

- **Group Quote**: It recives a list of lists of books and calculates the price modificators for each list of books
## Schemas

**BookDTO**

```json
{
    "id": "string",
    "title": "string",
    "author": "string",
    "type": "BOOK | NOVEL"
}
```

**BookIdWithQuantityReqDTO**

```json
{
    "bookId": "string",
    "quantity": "int"
}
```

**CalculateIndividualResDTO**

```json
{
    "id": "string",
    "title": "string",
    "author": "string",
    "basePrice": "decimal",
    "finalPrice": "decimal",
    "type": "BOOK | NOVEL"
    "discount": "decimal",
    "increase": "decimal",
}
```

**CalculateListResDTO**

```json
{
    "books": "CalculateIndividualResDTO",
    "totalPrice": "decimal"
    "totalBasePrice": "decimal",
    "totalDiscount": "decimal",
    "totalIncrease": "decimal"
}
```
## API Reference

#### Create aggregate (Required before start using the API)

```http
  POST /api/v1/secret
```
**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | The aggregate id that will be used for the API |

---

#### Get last aggregate id

```http
  POST /api/v1/secret
```
**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | The aggregate id that will be used for the API |

---

#### Get all the books

```http
  POST /api/v1/books
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | **Required** |


**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `books` | `List<BookDTO>` | A list a list of all the books on the aggregate |

---

#### Calculate Individual

```http
  POST /api/v1/quote/individual
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | **Required** |
| `title` | `string` | **Required** The title of the book |
| `author` | `string` | **Required** The name of the author of the book |
| `price` | `decimal` | **Required** The base price of the book, it cannot be less or equal to 0 |
| `type` | `"BOOK" \| "Novel"` | **Required** The book type |


**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `id` | `string` | The id of the book created |
| `title` | `string` | The title of the book |
| `author` | `string` | The name of the author of the book |
| `basePrice` | `decimal` | The base price of the book entered by user |
| `finalPrice` | `decimal` | The price of the book modified by the base increase, depending of the book type |
| `type` | `"BOOK" \| "Novel"` | The book type |
| `discount` | `decimal` | The discounted value of `finalPrice` respect to `basePrice` |
| `increase` | `decimal` | The increased value of `finalPrice` respect to `basePrice` |

---

#### Calculate List

```http
  POST /api/v1/quote/list
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | **Required** |
| `books` | `List<BookIdWithQuantityReqDTO>` | **Required** A list of book ids with a quantity |
| `customerRegisterDate` | `DateOnly` | **Required** The customer register date, used to calculate the seniority discount |

**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `books` | `List<CalculateIndividualResDTO>` | An object with the same data as **Calculate Individual** response |
| `totalPrice` | `decimal` | The sum of all the finalPrices |
| `totalBasePrice` | `decimal` | The sum of all the basePrices |
| `totalDiscount` | `decimal` | The sum of all the discounts |
| `totalIncrease` | `decimal` | The sum of all the increases |

---

#### Calculate Budget

```http
  POST /api/v1/quote/budget
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | **Required** |
| `bookIds` | `List<string>` | **Required** A list of book ids, it must have at least one book id and one novel id |
| `budget` | `decimal` | **Required** The budget to calculate how many books - novels can you quote, it cannot be less or equal to 0 |
| `customerRegisterDate` | `DateOnly` | **Required** The customer register date, used to calculate the seniority discount |

**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `books` | `List<CalculateIndividualResDTO>` | An object with the same data as **Calculate Individual** response |
| `totalPrice` | `decimal` | The sum of all the finalPrices |
| `totalBasePrice` | `decimal` | The sum of all the basePrices |
| `totalDiscount` | `decimal` | The sum of all the discounts |
| `totalIncrease` | `decimal` | The sum of all the increases |
| `restOfBudget` | `decimal` | The remaining value after quoting all the books  |
| `totalBooks` | `int` | The quantity of books that can be quoted with the budget  |
| `totalNovels` | `int` | The quantity of novels that can be quoted with the budget  |

---

#### Calculate Group

```http
  POST /api/v1/quote/group
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `aggregateId` | `string` | **Required** |
| `group` | `List<List<BookIdWithQuantityReqDTO>>` | **Required** A list of lists of book ids with a quantity  |
| `customerRegisterDate` | `DateOnly` | **Required** The customer register date, used to calculate the seniority discount |

**Returns**

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `groups` | `List<CalculateListResDTO>` | An object with the same data as **Calculate List** response |
| `totalPrice` | `decimal` | The sum of all the finalPrices |
| `totalBasePrice` | `decimal` | The sum of all the basePrices |
| `totalDiscount` | `decimal` | The sum of all the discounts |
| `totalIncrease` | `decimal` | The sum of all the increases |