module generated_unittests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Newtonsoft.Json

let run () =
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

  let status (response : Response) expected =
    if response.StatusCode <> expected then
      failwith (sprintf "Expected StatusCode: %A, Got: %A" expected response.StatusCode)

  let errors (response : Response) (errors' : string list) =
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

  let ( == ) response (right : 'a) =
    let body = match response.EntityBody with | Some body -> body | _ -> ""
    let left = JsonConvert.DeserializeObject<Result<'a>>(body)
    if left.Data <> right then failwith (sprintf "Expected: %A, Got: %A" right left.Data)

  let ( != ) response (right : 'a) =
    let body = match response.EntityBody with | Some body -> body | _ -> ""
    let left = JsonConvert.DeserializeObject<Result<'a>>(body)
    if left.Data = right then failwith (sprintf "Expected NOT: %A, Got: %A" right left.Data)

  ///////////////////////

  context "Register"

  "registration validation works" &&& fun _ ->
    let asJson = JsonConvert.SerializeObject(badRegistration)

    let response =
      "http://localhost:8083/api/register"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    errors response ["Email is not a valid email"; "Email is required"; "First Name is required"; "Last Name is required"; "Password is required"; "Password must be between 6 and 100 characters"]
    status response 400

  "good registration works" &&& fun _ ->
    let asJson = JsonConvert.SerializeObject(validRegistration)

    let response =
      "http://localhost:8083/api/register"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    errors response []
    status response 200
    response != 0L

  ///////////////////////////

  context "Product"

  "product validation works" &&& fun _ ->
    let asJson = JsonConvert.SerializeObject(badProduct)

    let response =
      "http://localhost:8083/api/product/create"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    errors response ["Category is required"; "Description is required"; "Name is required"; "Price is required"]
    status response 400

  "good product works" &&& fun _ ->
    let asJson = JsonConvert.SerializeObject(validProduct)

    let response =
      "http://localhost:8083/api/product/create"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    errors response []
    status response 200
    response != 0L

  canopy.runner.run()
