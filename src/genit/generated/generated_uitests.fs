module generated_uitests

open generated_forms
open generated_validation
open canopy

let run () =
  start firefox

  context "Product"

  once (fun _ -> url "http://localhost:8083/product/create"; click ".btn") 

  "Name is required" &&& fun _ ->
    displayed "Name is required"
    
  "Description is required" &&& fun _ ->
    displayed "Description is required"
    
  "Price is required" &&& fun _ ->
    displayed "Price is required"
    
  "Price must be a valid double" &&& fun _ ->
    displayed "Price is not a valid number (decimal)"
    
  "Category is required" &&& fun _ ->
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
    
    

  canopy.runner.run()

  quit()
