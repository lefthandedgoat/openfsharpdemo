module generated_uitests

open generated_forms
open generated_validation
open canopy
open canopyExtensions
open page_register

let run () =

  let baseuri = "http://localhost:8083/"
  start chrome

  context "Register"

  before (fun _ -> goto page_register.uri)

  "New registration validation" &&& fun _ ->
    click "Submit"
    displayed "First Name is required"
    displayed "Last Name is required"
    displayed "Email is not a valid email"
    displayed "Email is required"
    displayed "Password must be between 6 and 100 characters"
    displayed "Password is required"
    displayed "Confirm Password must be between 6 and 100 characters"
    displayed "Confirm Password is required"

  "Name invalid" &&& fun _ ->
    name 65 Invalid
    name 66 Invalid

  "Name valid" &&& fun _ ->
    name 1 Valid
    name 2 Valid
    name 63 Valid
    name 64 Valid

  "Email invalid" &&& fun _ ->
    _email << "junk"
    click _submit

    displayed _emailNotValid

  "Email valid" &&& fun _ ->
    click _submit
    displayed _emailNotValid

    _email << "junk@null.dev"
    click _submit
    notDisplayed _emailNotValid

  "Password invalid" &&& fun _ ->
    password 1 Invalid
    password 2 Invalid
    password 3 Invalid
    password 4 Invalid
    password 5 Invalid
    password 101 Invalid

  "Password valid" &&& fun _ ->
    password 6 Valid
    password 7 Valid
    password 99 Valid
    password 100 Valid

  "Password mismatch" &&& fun _ ->
    _password << "123456"
    _repeat << "654321"
    click _submit
    displayed _passwordsMatch

  "Can register new unique user" &&& fun _ ->
    let firstName, lastName, email = generateUniqueUser ()

    _first_name << firstName
    _last_name << lastName
    _email << email
    _password << "test1234"
    _repeat << "test1234"
    click _submit

    on baseuri







  context "Product"

  once (fun _ -> url "http://localhost:8083/product/create"; click ".btn")

  "New product validation" &&& fun _ ->
    displayed "Name is required"
    displayed "Description is required"
    displayed "Price is required"
    displayed "Price is not a valid number (decimal)"
    displayed "Category is required"

  context "Cart"

  once (fun _ -> url "http://localhost:8083/cart/create"; click ".btn")

  "Register FK must be a valid integer" &&& fun _ ->
    displayed "Register FK is not a valid number (int)"


  context "CartItem"

  once (fun _ -> url "http://localhost:8083/cartItem/create"; click ".btn")

  "Cart FK must be a valid integer" &&& fun _ ->
    displayed "Cart FK is not a valid number (int)"

  "Product FK must be a valid integer" &&& fun _ ->
    displayed "Product FK is not a valid number (int)"


  context "Checkout"

  once (fun _ -> url "http://localhost:8083/checkout/create"; click ".btn")

  "Cart FK must be a valid integer" &&& fun _ ->
    displayed "Cart FK is not a valid number (int)"
