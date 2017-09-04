module generated_unittests

open generated_forms
open generated_validation
open canopy
open HttpClient
open generated_types
open Newtonsoft.Json
open helper_tests
open test_data

let run () =

  context "Register"

  "registration validation works" &&& fun _ ->
    "/api/register"
    |> post badRegistration
    |> errors ["Email is not a valid email"; "Email is required"; "First Name is required"; "Last Name is required"; "Password is required"; "Password must be between 6 and 100 characters"]
    |> status 400

  "good registration works" &&& fun _ ->
    "/api/register"
    |> post validRegistration
    |> errors []
    |> notEq 0L
    |> status 200

  context "Product"

  "product validation works" &&& fun _ ->
    "/api/product/create"
    |> post badProduct
    |> errors ["Category is required"; "Description is required"; "Name is required"; "Price is required"]
    |> status 400

  "good product works" &&& fun _ ->
    "/api/product/create"
    |> post validProduct
    |> errors []
    |> notEq 0L
    |> status 200
