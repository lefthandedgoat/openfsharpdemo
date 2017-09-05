module generated_data_access

open System.Data
open generated_types
open helper_general
open helper_ado
open helper_npgado
open Npgsql
open dsl
open BCrypt.Net

let toChartData (reader : IDataReader) : ChartData =
  let temp =
    [ while reader.Read() do
      yield getString "description" reader, getInt32 "count" reader
    ]
  {
    Descriptions = temp |> List.map (fun data -> fst data)
    Data =  temp |> List.map (fun data -> snd data)
  }

[<Literal>]
let connectionString = "Server=127.0.0.1;User Id=best_online_store; Password=NOTSecure1234;Database=best_online_store;"


let insert_register (register : Register) =
  let sql = "
INSERT INTO best_online_store.users
  (
    user_id,
    first_name,
    last_name,
    email,
    password
  ) VALUES (
    DEFAULT,
    :first_name,
    :last_name,
    :email,
    :password
  ) RETURNING user_id;
"

  let bCryptScheme = getBCryptScheme currentBCryptScheme
  let salt = BCrypt.GenerateSalt(bCryptScheme.WorkFactor)
  let password = BCrypt.HashPassword(register.Password, salt)

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "first_name" register.FirstName
  |> param "last_name" register.LastName
  |> param "email" register.Email
  |> param "password" password
  |> executeScalar
  |> string |> int64

let toLogin (reader : IDataReader) : Login list =
  [ while reader.Read() do
    yield {
      UserID = getInt64 "user_id" reader
      Email = getString "email" reader
      Password = getString "password" reader
    }
  ]


let authenticate (login : Login) =
  let sql = "
SELECT * FROM best_online_store.users
WHERE email = :email
"
  use connection = connection connectionString
  use command = command connection sql
  let user =
    command
    |> param "email" login.Email
    |> read toLogin
    |> firstOrNone
  match user with
    | None -> None
    | Some(user) ->
      let verified = BCrypt.Verify(login.Password, user.Password)
      if verified
      then Some(user)
      else None

let toProduct (reader : IDataReader) : Product list =
  [ while reader.Read() do
    yield {
      ProductID = getInt64 "product_id" reader
      Name = getString "name" reader
      Description = getString "description" reader
      Price = getDouble "price" reader
      Category = getString "category" reader
    }
  ]


let insert_product (product : Product) =
  let sql = "
INSERT INTO best_online_store.products
  (
    product_id,
    name,
    description,
    price,
    category
  ) VALUES (
    DEFAULT,
    :name,
    :description,
    :price,
    :category
  ) RETURNING product_id;
"

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "name" product.Name
  |> param "description" product.Description
  |> param "price" product.Price
  |> param "category" product.Category
  |> executeScalar
  |> string |> int64


let update_product (product : Product) =
  let sql = "
UPDATE best_online_store.products
SET
  product_id = :product_id,
  name = :name,
  description = :description,
  price = :price,
  category = :category
WHERE product_id = :product_id;
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "product_id" product.ProductID
  |> param "name" product.Name
  |> param "description" product.Description
  |> param "price" product.Price
  |> param "category" product.Category
  |> executeNonQuery

let delete_product id =
  let sql = "
DELETE FROM best_online_store.products
WHERE product_id = :product_id;
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "product_id" id
  |> executeNonQuery

let tryById_product id =
  let sql = "
SELECT * FROM best_online_store.products
WHERE product_id = :product_id
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "product_id" id
  |> read toProduct
  |> firstOrNone

let getMany_product () =
  let sql = "
SELECT * FROM best_online_store.products
LIMIT 500
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> read toProduct

let search_product term =
  let sql = "
SELECT * FROM best_online_store.products
WHERE lower(name) LIKE lower(:term)
   OR lower(description) LIKE lower(:term)
   OR lower(category) LIKE lower(:term)
LIMIT 100
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "term" term
  |> read toProduct

let getManyWhere_product field how value =
  let field = to_postgres_dbColumn field
  let search = searchHowToClause how value
  let sql =
    sprintf "SELECT * FROM best_online_store.products
WHERE lower(%s) LIKE lower(:search)
LIMIT 500" field

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "search" search
  |> read toProduct

let toCart (reader : IDataReader) : Cart list =
  [ while reader.Read() do
    yield {
      CartID = getInt64 "cart_id" reader
      UserFK = getInt64 "register_fk" reader
      Items = []
    }
  ]


let insert_cart (cart : Cart) =
  let sql = "
INSERT INTO best_online_store.carts
  (
    cart_id,
    register_fk
  ) VALUES (
    DEFAULT,
    :register_fk
  ) RETURNING cart_id;
"

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "register_fk" cart.UserFK
  |> executeScalar
  |> string |> int64


let update_cart (cart : Cart) =
  let sql = "
UPDATE best_online_store.carts
SET
  cart_id = :cart_id,
  register_fk = :register_fk
WHERE cart_id = :cart_id;
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_id" cart.CartID
  |> param "register_fk" cart.UserFK
  |> executeNonQuery


let tryById_cart id =
  let sql = "
SELECT * FROM best_online_store.carts
WHERE cart_id = :cart_id
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_id" id
  |> read toCart
  |> firstOrNone

let delete_cart id =
  let sql = "
DLEETE FROM best_online_store.carts
WHERE cart_id = :cart_id
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_id" id
  |> executeNonQuery

let getMany_cart () =
  let sql = "
SELECT * FROM best_online_store.carts
LIMIT 500
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> read toCart

let toCartItem (reader : IDataReader) : CartItem list =
  [ while reader.Read() do
    yield {
      CartItemID = getInt64 "cartitem_id" reader
      CartFK = getInt64 "cart_fk" reader
      ProductFK = getInt64 "product_fk" reader
    }
  ]


let insert_cartItem (cartItem : CartItem) =
  let sql = "
INSERT INTO best_online_store.cartitems
  (
    cartitem_id,
    cart_fk,
    product_fk
  ) VALUES (
    DEFAULT,
    :cart_fk,
    :product_fk
  ) RETURNING cartitem_id;
"

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_fk" cartItem.CartFK
  |> param "product_fk" cartItem.ProductFK
  |> executeScalar
  |> string |> int64


let update_cartItem (cartItem : CartItem) =
  let sql = "
UPDATE best_online_store.cartitems
SET
  cartitem_id = :cartitem_id,
  cart_fk = :cart_fk,
  product_fk = :product_fk
WHERE cartitem_id = :cartitem_id;
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cartitem_id" cartItem.CartItemID
  |> param "cart_fk" cartItem.CartFK
  |> param "product_fk" cartItem.ProductFK
  |> executeNonQuery


let tryById_cartItem id =
  let sql = "
SELECT * FROM best_online_store.cartitems
WHERE cartitem_id = :cartitem_id
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cartitem_id" id
  |> read toCartItem
  |> firstOrNone

let delete_cartItems cartId =
  let sql = "
DELETE FROM best_online_store.cartitems
WHERE cart_fk = :cart_fk
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_fk" cartId
  |> executeNonQuery

let getMany_cartItem_byCartId id =
  let sql = "
SELECT * FROM best_online_store.cartitems
WHERE cart_fk = :cart_fk
LIMIT 500
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_fk" id
  |> read toCartItem

let getMany_cartItem () =
  let sql = "
SELECT * FROM best_online_store.cartitems
LIMIT 500
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> read toCartItem

let toCheckout (reader : IDataReader) : Checkout list =
  [ while reader.Read() do
    yield {
      CheckoutID = getInt64 "checkout_id" reader
      CartFK = getInt64 "cart_fk" reader
    }
  ]


let insert_checkout (checkout : Checkout) =
  let sql = "
INSERT INTO best_online_store.checkouts
  (
    checkout_id,
    cart_fk
  ) VALUES (
    DEFAULT,
    :cart_fk
  ) RETURNING checkout_id;
"

  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "cart_fk" checkout.CartFK
  |> executeScalar
  |> string |> int64


let update_checkout (checkout : Checkout) =
  let sql = "
UPDATE best_online_store.checkouts
SET
  checkout_id = :checkout_id,
  cart_fk = :cart_fk
WHERE checkout_id = :checkout_id;
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "checkout_id" checkout.CheckoutID
  |> param "cart_fk" checkout.CartFK
  |> executeNonQuery


let tryById_checkout id =
  let sql = "
SELECT * FROM best_online_store.checkouts
WHERE checkout_id = :checkout_id
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> param "checkout_id" id
  |> read toCheckout
  |> firstOrNone

let getMany_checkout () =
  let sql = "
SELECT * FROM best_online_store.checkouts
LIMIT 500
"
  use connection = connection connectionString
  use command = command connection sql
  command
  |> read toCheckout
