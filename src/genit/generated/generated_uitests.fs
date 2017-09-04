module generated_uitests

open generated_forms
open generated_validation
open canopy

let run () =
  start chrome

  context "Register"

  once (fun _ -> url "http://localhost:8083/register"; click "Submit")

  "New registration validation" &&& fun _ ->
    displayed "First Name is required"
    displayed "Last Name is required"
    displayed "Email is not a valid email"
    displayed "Email is required"
    displayed "Password must be between 6 and 100 characters"
    displayed "Password is required"
    displayed "Confirm Password must be between 6 and 100 characters"
    displayed "Confirm Password is required"

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
