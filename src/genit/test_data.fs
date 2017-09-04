module test_data

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Newtonsoft.Json

////////baddies
let badRegistration =
  {
    Register.UserID = 0L
    FirstName = ""
    LastName = ""
    Email = ""
    Password = ""
  }

let badProduct =
  {
    Product.ProductID = 0L
    Name = ""
    Description = ""
    Price = 0.0
    Category = ""
  }

let badCart =
  {
    Cart.CartID = 0L
    UserFK = 0L
  }

///////valid
let validRegistration =
  {
    Register.UserID = 0L
    FirstName = "Chris"
    LastName = "Holt"
    Email = "fake@fakeemail.com"
    Password = "123456"
  }

let validProduct =
  {
    Product.ProductID = 0L
    Name = "Best Soda"
    Description = "its great"
    Price = 1.0
    Category = "food"
  }

let validCart =
  {
    Cart.CartID = 0L
    UserFK = 1L
  }
