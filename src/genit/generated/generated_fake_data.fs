module generated_fake_data

open generated_types
open generated_data_access
open helper_general

let fake_product () =
  {
    ProductID = -1L 
    Name = (randomItem firstNames) + " " + (randomItem lastNames) 
    Description = randomItems 6 words 
    Price = random.Next(10) |> double 
    Category = randomItems 6 words 
  }

let fake_many_product number =
  [| 1..number |]
  |> Array.map (fun _ -> fake_product ()) //no parallel cause of RNG
  |> Array.Parallel.map insert_product
  |> ignore
 
 
let fake_cart () =
  {
    CartID = -1L 
    RegisterFK = random.Next(100) |> int64 
  }

let fake_many_cart number =
  [| 1..number |]
  |> Array.map (fun _ -> fake_cart ()) //no parallel cause of RNG
  |> Array.Parallel.map insert_cart
  |> ignore
 
 
let fake_cartItem () =
  {
    CartItemID = -1L 
    CartFK = random.Next(100) |> int64 
    ProductFK = random.Next(100) |> int64 
  }

let fake_many_cartItem number =
  [| 1..number |]
  |> Array.map (fun _ -> fake_cartItem ()) //no parallel cause of RNG
  |> Array.Parallel.map insert_cartItem
  |> ignore
 
 
let fake_checkout () =
  {
    CheckoutID = -1L 
    CartFK = random.Next(100) |> int64 
  }

let fake_many_checkout number =
  [| 1..number |]
  |> Array.map (fun _ -> fake_checkout ()) //no parallel cause of RNG
  |> Array.Parallel.map insert_checkout
  |> ignore
 
 
  