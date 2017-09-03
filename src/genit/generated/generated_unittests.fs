module generated_unittests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Nessos.FsPickler
open Nessos.FsPickler.Json

let run () =
  context "Register"

  "can register" &&& fun _ ->
    let registration =
      {
        Register.UserID = 0L
        FirstName = ""
        LastName = ""
        Email = ""
        Password = ""
      }

    let serializer = FsPickler.CreateJsonSerializer(indent = true)
    let asJson = serializer.PickleToString(registration)

    let result =
      "http://localhost:8083/register"
      |> createRequest Post
      |> withHeader (ContentType "application/json")
      |> withBody asJson
      |> getResponse

    printfn "%A" result

  canopy.runner.run()
