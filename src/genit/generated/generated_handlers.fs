module generated_handlers

open dsl
open System.Web
open Suave
open Suave.Authentication
open Suave.State.CookieStateStore
open Suave.Filters
open Suave.Successful
open Suave.Redirection
open Suave.Operators
open generated_views
open generated_forms
open generated_types
open generated_validation
open generated_data_access
open generated_fake_data
open generated_bundles
open helper_html
open helper_handler
open forms
open Newtonsoft.Json
open Suave.RequestErrors

let hasHeader header (req : HttpRequest) = req.headers |> List.exists (fun header' -> header = header')
let fromJson<'a> json = JsonConvert.DeserializeObject(json, typeof<'a>) :?> 'a
let toJson obj = JsonConvert.SerializeObject(obj)
let mapErrors validation = validation |> List.map (fun (_,error) -> error)

let home = GET >=> OK view_jumbo_home

let register =
  choose
    [
      GET >=> OK view_register
      POST >=> bindToForm registerForm (fun registerForm ->
        let validation = validation_registerForm registerForm
        if validation = [] then
          let converted = convert_registerForm registerForm
          let id = insert_register converted
          setAuthCookieAndRedirect id "/"
        else
          OK (view_errored_register validation registerForm))
    ]

let login =
  choose
    [
      GET >=> (OK <| view_login false "")
      POST >=> request (fun req ->
        bindToForm loginForm (fun loginForm ->
        let validation = validation_loginForm loginForm
        if validation = [] then
          let converted = convert_loginForm loginForm
          let loginAttempt = authenticate converted
          match loginAttempt with
            | Some(_) ->
              let returnPath = getQueryStringValue req "returnPath"
              let returnPath = if returnPath = "" then "/" else returnPath
              setAuthCookieAndRedirect id returnPath
            | None -> OK <| view_login true loginForm.Email
        else
          OK (view_errored_login validation loginForm)))
    ]

let create_product =
  choose
    [
      GET >=> warbler (fun _ -> createGET bundle_product)
      POST >=> bindToForm productForm (fun form -> createPOST form bundle_product)
    ]

let generate_product count =
  choose
    [
      GET >=> warbler (fun _ -> generateGET count bundle_product)
      POST >=> bindToForm productForm (fun form -> generatePOST form bundle_product)
    ]

let view_product id =
  GET >=> warbler (fun _ -> viewGET id bundle_product)

let edit_product id =
  choose
    [
      GET >=> warbler (fun _ -> editGET id bundle_product)
      POST >=> bindToForm productForm (fun productForm -> editPOST id productForm bundle_product)
    ]

let list_product =
  GET >=> warbler (fun _ -> getMany_product () |> view_list_product |> OK)

let search_product =
  choose
    [
      GET >=> request (fun req -> searchGET req bundle_product)
      POST >=> bindToForm searchForm (fun searchForm -> searchPOST searchForm bundle_product)
    ]

let create_cart =
  choose
    [
      GET >=> warbler (fun _ -> createGET bundle_cart)
      POST >=> bindToForm cartForm (fun form -> createPOST form bundle_cart)
    ]

let generate_cart count =
  choose
    [
      GET >=> warbler (fun _ -> generateGET count bundle_cart)
      POST >=> bindToForm cartForm (fun form -> generatePOST form bundle_cart)
    ]

let view_cart id =
  GET >=> warbler (fun _ -> viewGET id bundle_cart)

let edit_cart id =
  choose
    [
      GET >=> warbler (fun _ -> editGET id bundle_cart)
      POST >=> bindToForm cartForm (fun cartForm -> editPOST id cartForm bundle_cart)
    ]

let list_cart =
  GET >=> warbler (fun _ -> getMany_cart () |> view_list_cart |> OK)

let create_cartItem =
  choose
    [
      GET >=> warbler (fun _ -> createGET bundle_cartItem)
      POST >=> bindToForm cartItemForm (fun form -> createPOST form bundle_cartItem)
    ]

let generate_cartItem count =
  choose
    [
      GET >=> warbler (fun _ -> generateGET count bundle_cartItem)
      POST >=> bindToForm cartItemForm (fun form -> generatePOST form bundle_cartItem)
    ]

let view_cartItem id =
  GET >=> warbler (fun _ -> viewGET id bundle_cartItem)

let edit_cartItem id =
  choose
    [
      GET >=> warbler (fun _ -> editGET id bundle_cartItem)
      POST >=> bindToForm cartItemForm (fun cartItemForm -> editPOST id cartItemForm bundle_cartItem)
    ]

let list_cartItem =
  GET >=> warbler (fun _ -> getMany_cartItem () |> view_list_cartItem |> OK)

let create_checkout =
  choose
    [
      GET >=> warbler (fun _ -> createGET bundle_checkout)
      POST >=> bindToForm checkoutForm (fun form -> createPOST form bundle_checkout)
    ]

let generate_checkout count =
  choose
    [
      GET >=> warbler (fun _ -> generateGET count bundle_checkout)
      POST >=> bindToForm checkoutForm (fun form -> generatePOST form bundle_checkout)
    ]

let view_checkout id =
  GET >=> warbler (fun _ -> viewGET id bundle_checkout)

let edit_checkout id =
  choose
    [
      GET >=> warbler (fun _ -> editGET id bundle_checkout)
      POST >=> bindToForm checkoutForm (fun checkoutForm -> editPOST id checkoutForm bundle_checkout)
    ]

let list_checkout =
  GET >=> warbler (fun _ -> getMany_checkout () |> view_list_checkout |> OK)

let api_register =
  POST >=> request (fun req ->
    let register = fromJson<Register> (System.Text.Encoding.UTF8.GetString(req.rawForm))
    let validation = validation_registerJson register
    if validation = [] then
      let id = insert_register register
      OK ({ Data = id; Errors = [] } |> toJson)
    else
      let result = { Data = 0; Errors = mapErrors validation } |> toJson
      BAD_REQUEST result)

let api_product id =
  GET >=>
    let data = tryById_product id
    match data with
    | None -> NOT_FOUND error_404
    | Some(data) ->
       Writers.setMimeType "application/json"
       >=> OK (toJson { Data = data; Errors = [] })

let api_search_product =
  GET >=> request (fun req ->
      match req.queryParam "term" with
      | Choice1Of2 term -> OK (toJson { Data = generated_data_access.search_product term; Errors = [] })
      | Choice2Of2 _ -> BAD_REQUEST (toJson { Data = []; Errors = ["No search term provided"] }))

let api_create_product =
  POST >=> request (fun req ->
    let product = fromJson<Product> (System.Text.Encoding.UTF8.GetString(req.rawForm))
    let validation = validation_productJson product
    if validation = [] then
      let id = insert_product product
      OK (toJson { Data = id; Errors = [] })
    else
      let result = { Data = 0; Errors = mapErrors validation } |> toJson
      BAD_REQUEST result)

let api_edit_product =
  PUT >=> request (fun req ->
    let product = fromJson<Product> (System.Text.Encoding.UTF8.GetString(req.rawForm))
    let data = tryById_product product.ProductID
    match data with
    | None -> NOT_FOUND error_404
    | Some(_) ->
      let validation = validation_productJson product
      if validation = [] then
        update_product product
        NO_CONTENT
      else
        let result = { Data = 0; Errors = mapErrors validation } |> toJson
        BAD_REQUEST result)

let api_delete_product id =
  DELETE >=>
    let data = tryById_product id
    match data with
    | None -> NOT_FOUND error_404
    | Some(_) ->
        delete_product id
        NO_CONTENT

let api_cart id =
  GET >=>
    let data = tryById_cart id
    match data with
    | None -> NOT_FOUND error_404
    | Some(data) ->
       let data = { data with Items = getMany_cartItem_byCartId data.CartID }
       Writers.setMimeType "application/json"
       >=> OK (toJson { Data = data; Errors = [] })

let api_create_cart =
  POST >=> request (fun req ->
    let cart = fromJson<Cart> (System.Text.Encoding.UTF8.GetString(req.rawForm))
    let validation = validation_cartJson cart
    if validation = [] then
      let id = insert_cart cart
      OK (toJson { Data = id; Errors = [] })
    else
      let result = { Data = 0; Errors = mapErrors validation } |> toJson
      BAD_REQUEST result)

let api_add_to_cart =
  POST >=> request (fun req ->
    let cartItem = fromJson<CartItem> (System.Text.Encoding.UTF8.GetString(req.rawForm))
    let validation = validation_cartItemJson cartItem
    if validation = [] then
      let id = insert_cartItem cartItem
      OK (toJson { Data = id; Errors = [] })
    else
      let result = { Data = 0; Errors = mapErrors validation } |> toJson
      BAD_REQUEST result)

let api_checkout id =
  GET >=>
    let data = tryById_checkout id
    match data with
    | None -> NOT_FOUND error_404
    | Some(data) ->
       Writers.setMimeType "application/json"
       >=> OK (toJson { Data = data; Errors = [] })
