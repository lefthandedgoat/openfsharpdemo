module helper_tests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Newtonsoft.Json

/////
//assertions
/////

let status' expected (response : Response) =
  if response.StatusCode <> expected then
    failwith (sprintf "Expected StatusCode: %A, Got: %A" expected response.StatusCode)
  response

let status expected (response : Response) = status' expected response |> ignore

let errors (errors' : string list) (response : Response) =
  let body = match response.EntityBody with | Some body -> body | _ -> ""
  let results = JsonConvert.DeserializeObject<Result<_>>(body)
  let actual = results.Errors |> Set.ofList
  let expected = errors' |> Set.ofList
  let extra = actual - expected
  let missing = expected - actual
  if extra <> Set.empty && missing <> Set.empty then
    failwith (sprintf "Extra errors: %A, Missing Errors: %A" extra missing)
  else if extra <> Set.empty then
    failwith (sprintf "Extra errors: %A" extra)
  else if missing <> Set.empty then
    failwith (sprintf "Missing Errors: %A" missing)
  response

let extract<'a> response =
  let body = match response.EntityBody with | Some body -> body | _ -> ""
  let results = JsonConvert.DeserializeObject<Result<'a>>(body)
  results.Data

let ( == ) left right = if left <> right then failwith (sprintf "Expected: %A, Got: %A" right left)

let eq (other : 'a) response =
  let left = extract response
  if left <> other then failwith (sprintf "Expected: %A, Got: %A" other left)
  response

let notEq (other : 'a) response =
  let left = extract response
  if left = other then failwith (sprintf "Expected NOT: %A, Got: %A" other left)
  response

let validateJson example response =
  let body = match response.EntityBody with | Some body -> body | _ -> ""
  let actual = body
  jsonValidator.validate example actual
  response

/////
//verbs
/////

let post data uri =
  sprintf "http://localhost:8083%s" uri
  |> createRequest Post
  |> withHeader (ContentType "application/json")
  |> withBody (JsonConvert.SerializeObject(data))
  |> getResponse

let get uri =
  sprintf "http://localhost:8083%s" uri
  |> createRequest Get
  |> withHeader (ContentType "application/json")
  |> getResponse

let put data uri =
  sprintf "http://localhost:8083%s" uri
  |> createRequest Put
  |> withHeader (ContentType "application/json")
  |> withBody (JsonConvert.SerializeObject(data))
  |> getResponse

let delete uri =
  sprintf "http://localhost:8083%s" uri
  |> createRequest Delete
  |> getResponse

/////
//actions
/////

let addProduct product =
  "/api/product/create"
  |> post product
  |> errors []
  |> notEq 0L
  |> status' 200
  |> extract<int64>

let editProduct product =
  "/api/product/edit"
  |> put product
  |> status 204

let getProduct (id : int64) =
  sprintf "/api/product/%i" id
  |> get
  |> errors []
  |> status' 200
  |> extract<Product>

let deleteProduct (id : int64) =
  sprintf "/api/product/delete/%i" id
  |> delete
  |> status 204

let registerUser register =
  "/api/register"
  |> post register
  |> errors []
  |> notEq 0L
  |> status' 200
  |> extract<int64>

let addCart cart =
  "/api/cart/create"
  |> post cart
  |> errors []
  |> notEq 0L
  |> status' 200
  |> extract<int64>

let getCart (id : int64) =
  sprintf "/api/cart/%i" id
  |> get
  |> errors []
  |> status' 200
  |> extract<Cart>
