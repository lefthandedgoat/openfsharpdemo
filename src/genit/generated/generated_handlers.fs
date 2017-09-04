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
open Nessos.FsPickler
open Nessos.FsPickler.Json
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
      POST >=> request (fun req ->
        match hasHeader ("content-type", "application/json") req with
        | true ->
            let register = fromJson<Register> (System.Text.Encoding.UTF8.GetString(req.rawForm))
            let validation = validation_registerJson register
            if validation = [] then
              OK "GOOD"
            else
              let result = { Data = 0; Errors = mapErrors validation } |> toJson
              BAD_REQUEST result
        | false ->
          bindToForm registerForm (fun registerForm ->
            let validation = validation_registerForm registerForm
            if validation = [] then
              let converted = convert_registerForm registerForm
              let id = insert_register converted
              setAuthCookieAndRedirect id "/"
            else
              OK (view_errored_register validation registerForm)))
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

let api_product id =
  GET >=> request (fun req ->
    let data = tryById_product id
    match data with
    | None -> OK error_404
    | Some(data) ->
      match (getQueryStringValue req "format").ToLower() with
      | "xml" ->
         let serializer = FsPickler.CreateXmlSerializer(indent = true)
         Writers.setMimeType "application/xml"
         >=> OK (serializer.PickleToString(data))
      | "json" | _ ->
         let serializer = FsPickler.CreateJsonSerializer(indent = true)
         Writers.setMimeType "application/json"
         >=> OK (serializer.PickleToString(data)))

let api_cart id =
  GET >=> request (fun req ->
    let data = tryById_cart id
    match data with
    | None -> OK error_404
    | Some(data) ->
      match (getQueryStringValue req "format").ToLower() with
      | "xml" ->
         let serializer = FsPickler.CreateXmlSerializer(indent = true)
         Writers.setMimeType "application/xml"
         >=> OK (serializer.PickleToString(data))
      | "json" | _ ->
         let serializer = FsPickler.CreateJsonSerializer(indent = true)
         Writers.setMimeType "application/json"
         >=> OK (serializer.PickleToString(data)))

let api_checkout id =
  GET >=> request (fun req ->
    let data = tryById_checkout id
    match data with
    | None -> OK error_404
    | Some(data) ->
      match (getQueryStringValue req "format").ToLower() with
      | "xml" ->
         let serializer = FsPickler.CreateXmlSerializer(indent = true)
         Writers.setMimeType "application/xml"
         >=> OK (serializer.PickleToString(data))
      | "json" | _ ->
         let serializer = FsPickler.CreateJsonSerializer(indent = true)
         Writers.setMimeType "application/json"
         >=> OK (serializer.PickleToString(data)))
