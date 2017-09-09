module page_register

open canopy
open canopyExtensions

let random = System.Random()
let private letters = [ 'a' .. 'z' ]
let genChars length = [| 1 .. length |] |> Array.map (fun _ -> letters.[random.Next(25)]) |> System.String

type Validation = Valid | Invalid

let baseuri = "http://localhost:8083/"

let uri = baseuri + "register"

//selectors
let _first_name = placeholder "First Name"
let _last_name = placeholder "Last Name"
let _email = placeholder "Email"
let _password = placeholder "Password"
let _confirm = placeholder "Confirm Password"
let _submit = value "Submit"

//validation messages
let _first_name64 =  text "First Name can not be more than 64 characters"
let _last_name64 =  text "Last Name can not be more than 64 characters"
let _nameTaken =  text "Name is already taken"
let _emailNotValid =  text "Email is not a valid email"
let _password6to10 = text "Password must be between 6 and 100 characters"
let _passwordsMatch = text "Confirm Password must be the same as Password"

//helpers
let generateUniqueUser () =
  let letters = genChars 10
  let email = sprintf "test_%s@null.dev" letters
  let firstName = letters
  let lastName = genChars 12
  firstName, lastName, email

//this is kinda hacky but it cleans up code well enough
let generateUniqueUser' (firstName : byref<string>) (lastName : byref<string>) (email : byref<string>) =
  let firstName', lastName', email' = generateUniqueUser ()
  firstName <- firstName'
  lastName <- lastName'
  email <- email'

let name length validation =
  goto uri

  match validation with
  | Valid -> //force failure so we make sure that error goes away when valid
    click _submit
    _first_name << genChars length
    _last_name << genChars length
    click _submit
    notDisplayed _first_name64
    notDisplayed _last_name64
  | Invalid ->
    _first_name << genChars length
    _last_name << genChars length
    click _submit
    displayed _first_name64
    displayed _last_name64

let password length validation =
  goto uri

  match validation with
  | Valid -> //force failure so we make sure that he error goes away when valid
    _password << genChars 1
    click _submit
    displayed _password6to10
    let password = genChars length
    _password << password
    _confirm << password
    click _submit
    notDisplayed _password6to10
  | Invalid ->
    _password << genChars length
    click _submit
    displayed _password6to10

let tryRegister firstName lastName email =
  goto uri

  _first_name << firstName
  _last_name << lastName
  _email << email
  _password << "test1234"
  _confirm << "test1234"

  click _submit

let register firstName lastName email =
  tryRegister firstName lastName email
  on baseuri
