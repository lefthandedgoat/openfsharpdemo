module generated_unittests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Nessos.FsPickler
open Nessos.FsPickler.Json
open Newtonsoft.Json

let run () =
  context "Register"

  let serializer = FsPickler.CreateJsonSerializer(indent = true)
  let badRegistration =
    {
      Register.UserID = 0L
      FirstName = ""
      LastName = ""
      Email = ""
      Password = ""
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

  "registration validation works" &&& fun _ ->
    let asJson = serializer.PickleToString(badRegistration)

    let response =
      "http://localhost:8083/register"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    status response 400
    errors response ["Email is not a valid email"; "Email is required"; "First Name is required"; "Last Name is required"; "Password is required"; "Password must be between 6 and 100 characters"]

  canopy.runner.run()
