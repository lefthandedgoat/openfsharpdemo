module generated_unittests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Newtonsoft.Json
open helper_tests
open test_data
open generated_fake_data

let run () =

  context "Register"

  "registration validation works" &&& fun _ ->
    "/api/register"
    |> post badRegistration
    |> errors ["Email is not a valid email"; "Email is required"; "First Name is required"; "Last Name is required"; "Password is required"; "Password must be between 6 and 100 characters"]
    |> status 400

  "good registration works" &&& fun _ ->
    "/api/register"
    |> post validRegistration
    |> errors []
    |> notEq 0L
    |> status 200

  context "Product"

  "product validation works" &&& fun _ ->
    "/api/product/create"
    |> post badProduct
    |> errors ["Category is required"; "Description is required"; "Name is required"; "Price is required"]
    |> status 400

  "good product works" &&& fun _ -> // *
    "/api/product/create"
    |> post validProduct
    |> errors []
    |> notEq 0L
    |> status 200

  "getting product works" &&& fun _ -> // *
    let fakeProduct = fake_product ()
    let id = addProduct fakeProduct

    let product =
      sprintf "/api/product/%i" id
      |> get
      |> errors []
      |> status' 200
      |> extract<Product>

    product == { fakeProduct with ProductID = id }

  "editing product validation works" &&& fun _ ->
    let id = addProduct (fake_product ())
    let edited = { badProduct with ProductID = id }

    "/api/product/edit"
    |> put edited
    |> errors ["Category is required"; "Description is required"; "Name is required"; "Price is required"]
    |> status 400

  "editing invalid product gives 404" &&& fun _ ->
    let edited = { badProduct with ProductID = -1L }

    "/api/product/edit"
    |> put edited
    |> status 404

  "editing valid product works" &&& fun _ -> // *
    let fakeProduct = fake_product ()
    let id = addProduct fakeProduct
    let product = getProduct id
    let edited = { product with Price = product.Price * 2.0 }

    "/api/product/edit"
    |> put edited
    |> status 204

    getProduct id == edited

  "deleting invalid product gives 404" &&& fun _ ->
    "/api/product/delete/0"
    |> delete
    |> status 404

  "deleting product works" &&& fun _ -> // *
    let fakeProduct = fake_product ()
    let id = addProduct fakeProduct

    sprintf "/api/product/delete/%i" id
    |> delete
    |> status 204

    sprintf "/api/product/%i" id
    |> get
    |> status 404

  "Product: POST/PUT/GET/DELETE" &&& fun _ ->
    let fakeProduct = fake_product ()

    //POST
    let id = addProduct fakeProduct

    //GET and validate
    let product = getProduct id
    product == { fakeProduct with ProductID = id }

    //PUT
    editProduct { product with Price = product.Price * 2.0 }

    //GET and validate
    getProduct id == { product with Price = product.Price * 2.0 }

    //DELETE and validate
    deleteProduct id

    sprintf "/api/product/%i" id
    |> get
    |> status 404

  context "Cart"

  //delete
  "cart validation works" &&& fun _ ->
    "/api/cart/create"
    |> post badCart
    |> errors ["User FK is required"]
    |> status 400

  //delete
  "good cart works" &&& fun _ -> // *
    let id = registerUser (fake_register())
    let cart = { validCart with UserFK = id }

    "/api/cart/create"
    |> post cart
    |> errors []
    |> notEq 0L
    |> status 200

  "can add products to cart" &&& fun _ -> // *
    let productId1 = addProduct (fake_product())
    let userId = registerUser (fake_register())
    let cartId = addCart { validCart with UserFK = userId }
    let item1 = { CartItemID = 0L; CartFK = cartId; ProductFK = productId1 }

    "/api/cart/add"
    |> post item1
    |> errors []
    |> notEq 0L
    |> status 200
